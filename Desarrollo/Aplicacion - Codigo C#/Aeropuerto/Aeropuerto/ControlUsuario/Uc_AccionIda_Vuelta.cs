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

namespace Aeropuerto
{
    public partial class Uc_AccionIda_Vuelta : UserControl
    {
        Vuelo objVuelo;
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;
        public Uc_AccionIda_Vuelta(PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado)
        {
            InitializeComponent();
            this.principal = principal;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            CargarOrigenes();
            CargarDestinos();
            dtmFechaViaje_Ida.MaxDate = new DateTime(DateTime.Today.Year + 1, 12, 31);
            dtmFechaViaje_Ida.MinDate = DateTime.Today;
            dtmFechaViaje_Regreso.MaxDate = dtmFechaViaje_Ida.MaxDate;
            dtmFechaViaje_Ida.MinDate = dtmFechaViaje_Ida.Value;
            this.Visible = true;
        }

        /*private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (dtmFechaViaje_Regreso.Value < dtmFechaViaje_Ida.Value)
            {
                MessageBox.Show("La fecha de regreso no puede ser menor que la de ida.");
                return;
            }

            if (cbxOrigen.SelectedIndex == -1 || cbxDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar origen y destino primero.");
                return;
            }
            else
            {
                string ciudadOrigen = cbxOrigen.Text;
                string paisOrigen = cbxOrigen.SelectedValue?.ToString();

                string ciudadDestino = cbxDestino.Text;
                string paisDestino = cbxDestino.SelectedValue?.ToString();

                DateTime fechaIda = dtmFechaViaje_Ida.Value;
                DateTime fechaRegreso = dtmFechaViaje_Regreso.Value;

                if (string.IsNullOrEmpty(txtCantidadPasajeros.Text))
                {
                    MessageBox.Show("Debe seleccionar cantidad de pasajeros", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    int cantidadPasajeros = int.Parse(txtCantidadPasajeros.Text);
                    var resultados = objVuelo.ConsultarVuelosIdaVuelta(
                                    ciudadOrigen, paisOrigen,
                                    ciudadDestino, paisDestino,
                                    fechaIda, fechaRegreso
                                );

                    if (resultados.VuelosIda.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay vuelos de ida.");
                        return;
                    }
                    if (resultados.VuelosVuelta.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay vuelos de regreso.");
                        return;
                    }


                    Uc_AcVuelosDisponibles ucVerVuelos = new Uc_AcVuelosDisponibles(
                        cantidadPasajeros, principal, objVuelo, objUsuarioRegistrado,
                        resultados.VuelosIda, resultados.VuelosVuelta
                    );

                    this.Visible = false;
                    principal.PanelContenedorBuscarVuelos.Controls.Add(ucVerVuelos);
                    ucVerVuelos.Dock = DockStyle.Fill;

                    principal.PanelBuscarVuelos.Refresh();
                }
            }

        }*/

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            // VALIDAR ORIGEN Y DESTINO
            if (cbxOrigen.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una ciudad de origen.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una ciudad de destino.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxOrigen.SelectedIndex == cbxDestino.SelectedIndex)
            {
                MessageBox.Show("El origen y el destino no pueden ser iguales.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDAR FECHAS
            DateTime fechaIda = dtmFechaViaje_Ida.Value;
            DateTime fechaRegreso = dtmFechaViaje_Regreso.Value;

            if (fechaRegreso < fechaIda)
            {
                MessageBox.Show("La fecha de regreso no puede ser menor que la fecha de ida.",
                                "Fechas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDAR CANTIDAD DE PASAJEROS
            if (string.IsNullOrWhiteSpace(txtCantidadPasajeros.Text))
            {
                MessageBox.Show("Debe ingresar la cantidad de pasajeros.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCantidadPasajeros.Text, out int cantidadPasajeros))
            {
                MessageBox.Show("El número de pasajeros debe ser un número válido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cantidadPasajeros < 1 || cantidadPasajeros > 5)
            {
                MessageBox.Show("La cantidad de pasajeros debe estar entre 1 y 5.",
                                "Cantidad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // EXTRAER DATOS SEGUROS DEL COMBO
            string ciudadOrigen = cbxOrigen.Text;
            string paisOrigen = cbxOrigen.SelectedValue?.ToString();

            string ciudadDestino = cbxDestino.Text;
            string paisDestino = cbxDestino.SelectedValue?.ToString();

            if (paisOrigen == null || paisDestino == null)
            {
                MessageBox.Show("Error interno: faltan datos del país seleccionado.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // CONSULTAR VUELOS
            var resultados = objVuelo.ConsultarVuelosIdaVuelta(
                ciudadOrigen, paisOrigen,
                ciudadDestino, paisDestino,
                fechaIda, fechaRegreso
            );

            if (resultados.VuelosIda.Rows.Count == 0)
            {
                MessageBox.Show("No hay vuelos disponibles para la ida.", "Sin vuelos",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (resultados.VuelosVuelta.Rows.Count == 0)
            {
                MessageBox.Show("No hay vuelos disponibles para el regreso.", "Sin vuelos",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // CARGAR CONTROL DE RESULTADOS
            Uc_AcVuelosDisponibles ucVerVuelos = new Uc_AcVuelosDisponibles(
                cantidadPasajeros, principal, objVuelo, objUsuarioRegistrado,
                resultados.VuelosIda, resultados.VuelosVuelta
            );

            this.Visible = false;

            principal.PanelContenedorBuscarVuelos.Controls.Add(ucVerVuelos);
            ucVerVuelos.Dock = DockStyle.Fill;

            principal.PanelBuscarVuelos.Refresh();
        }


        private void CargarOrigenes()
        {
            DataTable tabla = objVuelo.ObtenerCiudadesOrigen();

            cbxOrigen.DataSource = null;
            cbxOrigen.Items.Clear();

            cbxOrigen.DataSource = tabla;
            cbxOrigen.DisplayMember = "CIUDAD";
            cbxOrigen.ValueMember = "PAIS";
            cbxOrigen.SelectedIndex = -1;

        }

        private void CargarDestinos()
        {
            DataTable tabla = objVuelo.ObtenerCiudadesDestino();

            cbxDestino.DataSource = null;
            cbxDestino.Items.Clear();

            cbxDestino.DataSource = tabla;
            cbxDestino.DisplayMember = "CIUDAD";
            cbxDestino.ValueMember = "PAIS";
            cbxDestino.SelectedIndex = -1;
        }

        private void btnSoloIda_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            principal.PanelContenedorBuscarVuelos.Visible = false;

            principal.PanelBuscarVuelos.Visible = true;
            principal.PanelBuscarVuelos.BringToFront();

            principal.ActualizarPantalla();
        }
    }
}
