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
    public partial class AcVuelosDisponibles : Form
    {
        Vuelo objVuelo;
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;

        DataTable vuelosIda;
        DataTable vuelosRegreso;

        //Constructor solo ida
        public AcVuelosDisponibles(PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelosIda)
        {
            InitializeComponent();
            this.principal = principal;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.vuelosIda = vuelosIda;
            this.vuelosRegreso = null;
            panelRegreso.Hide();
            MostrarVuelos(vuelosIda, null);
        }

        //Constructor ida y vuelta
        public AcVuelosDisponibles(PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelosIda, DataTable vuelosRegreso)
        {
            InitializeComponent();
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

                lbOrigen_VDisponibles.Text = vuelo["CIUORIGENVUELO"].ToString();
                lbDestino_VDisponibles.Text = vuelo["CIUDESTINOVUELO"].ToString();

                lbOrigen_Avr_VDisponibles.Text = vuelo["CIUORIGENVUELO"].ToString().Substring(0, 3).ToUpper();
                lbDestino_Avr_VDisponibles.Text = vuelo["CIUDESTINOVUELO"].ToString().Substring(0, 3).ToUpper();

                decimal precioIda = Convert.ToDecimal(vuelo["PRECIOBASEVUELO"]);
                lbPrecio_VDisponibles.Text = precioIda.ToString("N0") + " COP";

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

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {
            this.Hide();
            principal.Show();
        }
    }
}
