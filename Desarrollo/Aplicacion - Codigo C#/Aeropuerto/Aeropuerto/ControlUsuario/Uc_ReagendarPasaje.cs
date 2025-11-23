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
    public partial class Uc_ReagendarPasaje : UserControl
    {

        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;
        int idPasaje;
        Pasaje gestorPasaje;
        Vuelo objVuelo = new Vuelo();
        string ciudadOrigen, ciudadDestino, paisOrigen, paisDestino;
        

        public Uc_ReagendarPasaje(PaginaPrincipal principal, UsuarioRegistrado objUsuarioRegistrado, int idPasaje, Pasaje gestorPasaje)
        {
            InitializeComponent();
            this.principal = principal;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.idPasaje = idPasaje;
            this.gestorPasaje = gestorPasaje;
            dtmFechaViaje_ReagendarVuelo.MaxDate = new DateTime(DateTime.Today.Year + 1, 12, 31);
            dtmFechaViaje_ReagendarVuelo.MinDate = DateTime.Today;
            CargarInformacionVuelo();
        }

        private void CargarInformacionVuelo()
        {
            try
            {
                DataTable dt = gestorPasaje.ObtenerInfoVueloReagendar(idPasaje);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró la información del vuelo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow row = dt.Rows[0];

                ciudadOrigen = row["CIUORIGEN"].ToString();
                ciudadDestino = row["CIUDESTINO"].ToString();
                paisOrigen = row["PORIGEN"].ToString();
                paisDestino = row["PDESTINO"].ToString();

                lb_Origen_ReagPasaje.Text = ciudadOrigen;
                lb_Destino_ReagendarPasaje.Text = ciudadDestino;
                lb_Estado_ReagendarPasaje.Text = row["ESTADOVUELO"].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la información del vuelo: " + ex.Message);
            }
        }

        private void btnReagendarPasaje_Click(object sender, EventArgs e)
        {
            DateTime fechaIda = dtmFechaViaje_ReagendarVuelo.Value;

            DataTable vuelosExistentes = objVuelo.ConsultarVuelosIda(ciudadOrigen, paisOrigen, ciudadDestino, paisDestino, fechaIda);

            if (vuelosExistentes.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron vuelos disponibles para la selección.");
                return;
            }
            else
            {
                DataTable vuelos = gestorPasaje.ObtenerVuelosDisponiblesParaReagendo(idPasaje);

                if (vuelos.Rows.Count == 0)
                {
                    MessageBox.Show("Usted no tiene vuelos disponibles para reagendar.");
                    return;
                }
                else
                {
                    var ucVerVuelos = new Uc_VuelosDisponibles_Reagendo(principal, objVuelo, objUsuarioRegistrado, vuelos, gestorPasaje, idPasaje);

                    // Limpia el panel y agrega el nuevo control
                    this.Visible = false;
                    principal.PanelContenedorMisVuelos.Controls.Add(ucVerVuelos);
                    ucVerVuelos.Dock = DockStyle.Fill;

                    principal.PanelMisVuelos.Refresh();
                }

                    
            }
            
        }
    }
}
