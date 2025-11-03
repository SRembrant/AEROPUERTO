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

        private void btnSiguiente_Click(object sender, EventArgs e)
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
