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
    public partial class Uc_AcVuelosDisponibles : UserControl
    {
        Vuelo objVuelo;
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;

        DataTable vuelosIda;
        DataTable vuelosRegreso;

        int cantidadPasajeros;

        public Uc_AcVuelosDisponibles(int cantidadPasajeros, PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelosIda)
        {
            InitializeComponent();
            this.cantidadPasajeros = cantidadPasajeros;
            this.principal = principal;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.vuelosIda = vuelosIda;
            this.vuelosRegreso = null;
            panelRegreso.Hide();
            MostrarVuelos(vuelosIda, null);
        }

        //Constructor ida y vuelta
        public Uc_AcVuelosDisponibles(int cantidadPasajeros, PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelosIda, DataTable vuelosRegreso)
        {
            InitializeComponent();
            this.cantidadPasajeros= cantidadPasajeros;
            this.principal = principal;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.vuelosIda = vuelosIda;
            this.vuelosRegreso = vuelosRegreso;
            MostrarVuelos(vuelosIda, vuelosRegreso);
        }

        private void MostrarVuelos(DataTable vuelosIda, DataTable vuelosVuelta)
        {
            if ((vuelosIda == null || vuelosIda.Rows.Count == 0) &&
                (vuelosVuelta == null || vuelosVuelta.Rows.Count == 0))
            {
                MessageBox.Show("No hay vuelos disponibles");
                return;
            }

            // Mostrar IDA (si existe)
            if (vuelosIda != null && vuelosIda.Rows.Count > 0)
            {
                DataRow vuelo = vuelosIda.Rows[0];

                objVuelo.IdVuelo = Convert.ToInt32(vuelo["IDVUELO"]);

                lbOrigen_VDisponibles.Text = vuelo["CIUORIGENVUELO"].ToString();
                lbDestino_VDisponibles.Text = vuelo["CIUDESTINOVUELO"].ToString();

                lbOrigen_Avr_VDisponibles.Text = vuelo["CIUORIGENVUELO"].ToString().Substring(0, 3).ToUpper();
                lbDestino_Avr_VDisponibles.Text = vuelo["CIUDESTINOVUELO"].ToString().Substring(0, 3).ToUpper();

                decimal precioIda = Convert.ToDecimal(vuelo["PRECIOBASEVUELO"]);
                lbPrecio_VuelosDisponibles.Text = precioIda.ToString("N0") + " COP";

                lbFecha_VDisponibles.Text = vuelo["FECHAEJECUCION"].ToString();
            }

            // Mostrar REGRESO (si existe)
            if (vuelosVuelta != null && vuelosVuelta.Rows.Count > 0)
            {
                panelRegreso.Show();
                DataRow vuelo = vuelosVuelta.Rows[0];

                lbOrigen_VDisponibles_Regreso.Text = vuelo["CIUORIGENVUELO"].ToString();
                lbOrigen_Avr_VDisponibles_Regreso.Text = vuelo["CIUORIGENVUELO"].ToString().Substring(0, 3).ToUpper();

                lbDestino_VDisponibles_Regreso.Text = vuelo["CIUDESTINOVUELO"].ToString();
                lbDestino_Avr_VDisponibles_Regreso.Text = vuelo["CIUDESTINOVUELO"].ToString().Substring(0, 3).ToUpper();

                decimal precioVuelta = Convert.ToDecimal(vuelo["PRECIOBASEVUELO"]);
                lbPrecio_VDisponibles_Regreso.Text = precioVuelta.ToString("N0") + " COP";

                lbFecha_VDisponibles_Regreso.Text = vuelo["FECHAEJECUCION"].ToString();

            }
        }

        private void pbComprar_Click(object sender, EventArgs e)
        {
            Uc_Informacion_Pasajero ucPasajeros = new Uc_Informacion_Pasajero(principal, objVuelo, objUsuarioRegistrado, vuelosIda, vuelosRegreso, cantidadPasajeros);
            principal.PanelBuscarVuelos.Controls.Clear();
            principal.PanelBuscarVuelos.Controls.Add(ucPasajeros);
            ucPasajeros.Dock = DockStyle.Fill;
            ucPasajeros.GenerarPasajeros(cantidadPasajeros);
        }
    }
}
