using Aeropuerto.logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aeropuerto.ControlUsuario
{
    public partial class Uc_GenerarFactura_Reagendo : UserControl
    {
        PaginaPrincipal principal;
        Vuelo objVuelo;
        UsuarioRegistrado objUsuarioRegistrado;
        DataTable vuelosIda;
        DataTable vuelosRegreso;
        Pasaje gestorPasaje;
        int idVuelo;
        int idUsuario;
        string medioPago;

        public Uc_GenerarFactura_Reagendo(int idVuelo, int idUsuario, string medioPago, Pasaje gestorPasaje, UsuarioRegistrado objUsuario, PaginaPrincipal principal)
        {
            InitializeComponent();
            this.idVuelo = idVuelo;
            this.idUsuario = idUsuario;
            this.medioPago = medioPago;
            this.gestorPasaje = gestorPasaje;
            this.objUsuarioRegistrado = objUsuario;
            this.principal = principal;
            this.Visible = true;
        }

        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            // Recupera los datos de la factura según tu procedure actual
            DataTable factura = gestorPasaje.RecuperarFactura(idVuelo, idUsuario, medioPago);

            if (factura.Rows.Count > 0)
            {
                var row = factura.Rows[0];
                decimal monto = Convert.ToDecimal(row["MONTOFACTURA"]);
                DateTime fecha = Convert.ToDateTime(row["FECHAFACTURA"]);
                string medio = row["MEDIOPAGOFACTURA"].ToString();

                // Construir el texto de la factura visualmente formateado
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("=======================================");
                sb.AppendLine("         ✈ AEROPUERTO EL DORADO ✈");
                sb.AppendLine("=======================================");
                sb.AppendLine($"Factura N°: {row["IDFACTURA"]}");
                sb.AppendLine($"Fecha emisión: {fecha:dd/MM/yyyy HH:mm}");
                sb.AppendLine("----------------------------------------");
                sb.AppendLine($"Cliente: {objUsuarioRegistrado.NombreUsuario} {objUsuarioRegistrado.ApellidoUsuario}");
                sb.AppendLine($"Correo: {objUsuarioRegistrado.CorreoUsuario}");
                sb.AppendLine("----------------------------------------");
                sb.AppendLine("✈ Detalle de compra:");
                sb.AppendLine($"Vuelo ID: {idVuelo}");
                sb.AppendLine("----------------------------------------");
                sb.AppendLine($"Subtotal: {monto:N0} COP");
                sb.AppendLine($"IVA (19%): {(monto * 0.19m):N0} COP");
                sb.AppendLine($"TOTAL A PAGAR: {(monto * 1.19m):N0} COP");
                sb.AppendLine($"Método de pago: {medio}");
                sb.AppendLine("----------------------------------------");
                sb.AppendLine("Gracias por preferirnos. ¡Buen viaje!");
                sb.AppendLine("=======================================");

                // Mostrar factura simulada en ventana
                MessageBox.Show(sb.ToString(), "Factura generada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Volver a la pantalla de mis vuelos
                principal.PanelContenedorMisVuelos.Visible = false;
                this.Visible = false;
                principal.PanelMisVuelos.Visible = true;
                principal.PanelMisVuelos.BringToFront();
                principal.ActualizarPantalla();
            }
            else
            {
                MessageBox.Show("No se encontró información de la factura generada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
