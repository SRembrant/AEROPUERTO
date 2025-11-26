using Aeropuerto.ControlUsuario;
using Aeropuerto.logica;
using Aeropuerto.utilidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aeropuerto
{
    public partial class Uc_Informacion_Pasajero : UserControl
    {
        PaginaPrincipal principal;
        Uc_AcVuelosDisponibles anterior;
        Vuelo objVuelo;
        UsuarioRegistrado objUsuarioRegistrado;
        DataTable vuelosIda;
        DataTable vuelosRegreso;
        Pasaje gestorPasaje = new Pasaje();

        public int NumeroPasajeros { get; private set; }

        public Uc_Informacion_Pasajero(PaginaPrincipal principal, Uc_AcVuelosDisponibles anterior, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelosIda, DataTable vuelosRegreso, int numeroPasajeros)
        {
            InitializeComponent();
            this.principal = principal;
            this.anterior = anterior;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.vuelosIda = vuelosIda;
            this.vuelosRegreso = vuelosRegreso;
            this.NumeroPasajeros = numeroPasajeros;
            lblPrecio_InformPasajero.Text = "";
            this.Visible = true;
            GenerarPasajeros(numeroPasajeros);
        }

        public Uc_Informacion_Pasajero(int numero)
        {
            InitializeComponent();
            NumeroPasajeros = numero;
            this.Visible = true;
        }

        public void GenerarPasajeros(int cantidad)
        {
            flowLayoutPanelPasajeros.Controls.Clear();

            for (int i = 1; i <= cantidad; i++)
            {
                var uc = new Uc_DatosPasajero(i);
                uc.Margin = new Padding(10);
                flowLayoutPanelPasajeros.Controls.Add(uc);
            }
            CalcularTotal();
        }


        private void btnRealizarCompra_Click(object sender, EventArgs e)
        {
            if (!chk_TerminosPoliticas.Checked)
            {
                MessageBox.Show("Debe confirmar que ha leído y aceptado los términos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idUsuario = objUsuarioRegistrado.PKIdUsuario ?? 0;
                string medioPago = cbxMetodoPago.SelectedItem?.ToString();

                if (string.IsNullOrEmpty(medioPago))
                {
                    MessageBox.Show("Seleccione un método de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool insercionExitosa = true;
                bool reservaExitosa = true;

                foreach (Uc_DatosPasajero uc in flowLayoutPanelPasajeros.Controls)
                {
                    if (!uc.ValidarTodo())
                    {
                        MessageBox.Show("Corrige los errores antes de continuar.",
                                        "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string nombreCategoria = uc.Categoria;
                    int idCategoria = gestorPasaje.ObtenerIdCategoria(nombreCategoria);

                    int idPasajero = int.Parse(uc.Identificacion);
                    string tipoId = uc.TipoIdentificacion;
                    string nombre = uc.Nombre;
                    string apellido = uc.Apellido;
                    string correo = uc.Correo;

                    // Registrar pasajero
                    string mensaje = gestorPasaje.InsertarPasajero(idPasajero, tipoId, nombre, apellido, correo);
                    if (mensaje == null) return;
                    if (!mensaje.Contains("correctamente"))
                    {
                        insercionExitosa = false;
                        MessageBox.Show($"Error al registrar pasajero {nombre} {apellido}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    // Reservar vuelo de ida
                    foreach (DataRow vuelo in vuelosIda.Rows)
                    {
                        int idVueloIda = Convert.ToInt32(vuelo["IDVUELO"]);
                        string resultadoIda = gestorPasaje.ReservarPasaje(idUsuario, idPasajero, idVueloIda, idCategoria);
                        if (resultadoIda == null) return;
                        if (!resultadoIda.Contains("Reserva completada"))
                        {
                            reservaExitosa = false;
                            MessageBox.Show($"Error al reservar vuelo de ida para {nombre}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    // Reservar vuelo de regreso (si existe)
                    if (vuelosRegreso != null && vuelosRegreso.Rows.Count > 0 && reservaExitosa)
                    {
                        foreach (DataRow vuelo in vuelosRegreso.Rows)
                        {
                            int idVueloRegreso = Convert.ToInt32(vuelo["IDVUELO"]);
                            string resultadoRegreso = gestorPasaje.ReservarPasaje(idUsuario, idPasajero, idVueloRegreso, idCategoria);
                            if (resultadoRegreso==null) return;
                            if (!resultadoRegreso.Contains("Reserva completada"))
                            {
                                reservaExitosa = false;
                                MessageBox.Show($"Error al reservar vuelo de regreso para {nombre}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                    }

                    if (!insercionExitosa || !reservaExitosa)
                        break;
                }

                // Solo generar factura si TODO fue exitoso
                if (insercionExitosa && reservaExitosa)
                {
                    int idVueloFactura = Convert.ToInt32(vuelosIda.Rows[0]["IDVUELO"]);
                    Uc_GenerarFactura ucGenerarFactura = new Uc_GenerarFactura(idVueloFactura, idUsuario, medioPago, gestorPasaje, objUsuarioRegistrado, principal);
                    this.Visible = false;
                    principal.PanelContenedorBuscarVuelos.Controls.Add(ucGenerarFactura);
                    ucGenerarFactura.Dock = DockStyle.Fill;

                    principal.PanelBuscarVuelos.Refresh();
                }
                else
                {
                    MessageBox.Show("No se pudo completar la compra. Revise los errores mostrados.", "Compra incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la compra, revise que los compos estén completos y que tengan el formato indicado ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CalcularTotal()
        {
            decimal total = 0;

            foreach (Uc_DatosPasajero uc in flowLayoutPanelPasajeros.Controls)
            {
                string categoria = uc.Categoria;
                decimal sobrecosto = gestorPasaje.ObtenerSobrecostoCategoria(categoria);

                // sumar todos los vuelos de ida
                if (vuelosIda != null)
                {
                    foreach (DataRow vuelo in vuelosIda.Rows)
                    {
                        decimal precioBase = Convert.ToDecimal(vuelo["PRECIOBASEVUELO"]);
                        decimal precioFinal = precioBase + (precioBase * (sobrecosto / 100));
                        total += precioFinal;
                    }
                }

                // sumar todos los vuelos de regreso
                if (vuelosRegreso != null)
                {
                    foreach (DataRow vuelo in vuelosRegreso.Rows)
                    {
                        decimal precioBase = Convert.ToDecimal(vuelo["PRECIOBASEVUELO"]);
                        decimal precioFinal = precioBase + (precioBase * (sobrecosto / 100));
                        total += precioFinal;
                    }
                }
            }

            // Mostrar total formateado
            lblPrecio_InformPasajero.Text = $"Precio total: {total:N0} COP";
        }

        private void MostrarDisponibilidadAsientos()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("=======================================");
                sb.AppendLine("     ✈ DISPONIBILIDAD DE ASIENTOS ✈");
                sb.AppendLine("=======================================");

                // DISPONIBILIDAD PARA VUELO DE IDA
                if (vuelosIda != null && vuelosIda.Rows.Count > 0)
                {
                    int idVueloIda = Convert.ToInt32(vuelosIda.Rows[0]["IDVUELO"]);
                    DataTable dtIda = gestorPasaje.ObtenerAsientosDisponiblesPorVuelo(idVueloIda);

                    sb.AppendLine($"🔵 VUELO DE IDA (ID: {idVueloIda})");
                    sb.AppendLine("---------------------------------------");

                    if (dtIda.Rows.Count == 0)
                    {
                        sb.AppendLine("No hay asientos disponibles.");
                    }
                    else
                    {
                        foreach (DataRow row in dtIda.Rows)
                        {
                            sb.AppendLine($"• {row["nombreCategoria"]}: {row["asientosDisponibles"]} disponible(s)");
                        }
                    }

                    sb.AppendLine("---------------------------------------");
                }

                // 🔹 DISPONIBILIDAD PARA VUELO DE REGRESO
                if (vuelosRegreso != null && vuelosRegreso.Rows.Count > 0)
                {
                    int idVueloRegreso = Convert.ToInt32(vuelosRegreso.Rows[0]["IDVUELO"]);
                    DataTable dtRegreso = gestorPasaje.ObtenerAsientosDisponiblesPorVuelo(idVueloRegreso);

                    sb.AppendLine($"🟣 VUELO DE REGRESO (ID: {idVueloRegreso})");
                    sb.AppendLine("---------------------------------------");

                    if (dtRegreso.Rows.Count == 0)
                    {
                        sb.AppendLine("No hay asientos disponibles.");
                    }
                    else
                    {
                        foreach (DataRow row in dtRegreso.Rows)
                        {
                            sb.AppendLine($"• {row["nombreCategoria"]}: {row["asientosDisponibles"]} disponible(s)");
                        }
                    }

                    sb.AppendLine("---------------------------------------");
                }

                sb.AppendLine("Gracias por preferirnos. ¡Buen viaje!");
                sb.AppendLine("=======================================");

                MessageBox.Show(sb.ToString(),
                                "Disponibilidad de Asientos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar disponibilidad: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void VerDisponibilidad_Click(object sender, EventArgs e)
        {
            MostrarDisponibilidadAsientos();
        }

        private void pbxlblVolvelBuscarVuelos_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            anterior.Visible = true;
            principal.PanelBuscarVuelos.Refresh();
        }


    }

}
