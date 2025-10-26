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
    public partial class AccionIda_Vuelta : Form
    {
        Vuelo objVuelo;
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;
        public AccionIda_Vuelta(PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado)
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
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (cbxOrigen.SelectedIndex == -1 || cbxDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar origen y destino primero.");
                return;
            }

            if (dtmFechaViaje_Regreso.Value < dtmFechaViaje_Ida.Value)
            {
                MessageBox.Show("La fecha de regreso no puede ser menor que la de ida.");
                return;
            }

            string ciudadOrigen = cbxOrigen.Text;
            string paisOrigen = cbxOrigen.SelectedValue?.ToString();

            string ciudadDestino = cbxDestino.Text;
            string paisDestino = cbxDestino.SelectedValue?.ToString();

            DateTime fechaIda = dtmFechaViaje_Ida.Value;
            DateTime fechaRegreso = dtmFechaViaje_Regreso.Value;

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

            AcVuelosDisponibles vista = new AcVuelosDisponibles(
                principal, objVuelo, objUsuarioRegistrado,
                resultados.VuelosIda, resultados.VuelosVuelta
            );
            vista.Show();
            this.Hide();
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
            this.Hide();
            principal.Show();
        }

        
    }
}
