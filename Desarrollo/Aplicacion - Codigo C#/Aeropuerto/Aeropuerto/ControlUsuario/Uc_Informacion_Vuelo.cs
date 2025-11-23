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
    public partial class Uc_Informacion_Vuelo : UserControl
    {
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;
        int idPasaje;
        Pasaje gestorPasaje;
        public Uc_Informacion_Vuelo(PaginaPrincipal principal, UsuarioRegistrado objUsuarioRegistrado, int idPasaje, Pasaje gestorPasaje)
        {
            InitializeComponent();
            this.principal = principal;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.idPasaje = idPasaje;
            this.gestorPasaje = gestorPasaje;
            CargarInformacionVuelo();
            this.Visible = true;
        }

        private void CargarInformacionVuelo()
        {
            try
            {
                DataTable dt = gestorPasaje.ObtenerInfoVueloPasaje(idPasaje);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró la información del vuelo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow row = dt.Rows[0];

                lbl_NumeroVuelo.Text = row["NUMEROVUELO"].ToString();
                lblOrigen_InfoVuelos.Text = row["ORIGEN"].ToString();
                lblDestino_InfoVuelos.Text = row["DESTINO"].ToString();
                lblFechaIda_infoVuelos.Text = Convert.ToDateTime(row["FECHAVUELO"]).ToString("dd/MM/yyyy");
                lblDuracion_infoVuelos.Text = row["DURACION"].ToString() + " h";
                lblEstado_infoVuelos.Text = row["ESTADOVUELO"].ToString();
                lblNumeroPasajeros_infoVuelos.Text = row["NUMPASAJE"].ToString();
                lblNumeroPuerta_InfoVuelos.Text = row["PUERTAEMBARQUE"].ToString();
                lblNumeroZona_infoVuelos.Text = row["ZONAEMBARQUE"].ToString();
                lblCategoria_infoVuelos.Text = row["CATEGORIA"].ToString();
                lblAerolinea_infoVuelos.Text = row["NOMBREAEROLINEA"].ToString();
                lblHoraSalida.Text = row["HORASALIDA"].ToString() + " h";
                lblHora_LLegada.Text = row["HORALLEGADA"].ToString() + " h";
                lblOrigen_Avr_InfoVuelos.Text = row["ORIGEN"].ToString().Substring(0, 3).ToUpper();
                lblDestino_Avr_InfoVuelos.Text = row["DESTINO"].ToString().Substring(0, 3).ToUpper();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la información del vuelo: " + ex.Message);
            }
        }

        private void btnCancelarVuelo_Click(object sender, EventArgs e)
        {
            var ucCancelar = new Uc_ConfirmarCancelacion(principal, objUsuarioRegistrado, idPasaje, gestorPasaje, this);

            // Limpia el panel y agrega el nuevo control
            this.Visible = false;
            principal.PanelContenedorMisVuelos.Controls.Add(ucCancelar);
            ucCancelar.Dock = DockStyle.Fill;

            principal.PanelMisVuelos.Refresh();
        }

        private void lblVolvelMisVuelos_Click(object sender, EventArgs e)
        {
            principal.PanelContenedorMisVuelos.Visible = false;

            principal.PanelMisVuelos.Visible = true;
            principal.PanelMisVuelos.BringToFront();

            principal.ActualizarPantalla();
        }

        private void btnReagendarVuelo_Click(object sender, EventArgs e)
        {
            var ucReagendarSobrecosto = new Uc_SobrecostoReagendarVuelo(principal, objUsuarioRegistrado, idPasaje, gestorPasaje, this);

            // Limpia el panel y agrega el nuevo control
            this.Visible = false;
            principal.PanelContenedorMisVuelos.Controls.Add(ucReagendarSobrecosto);
            ucReagendarSobrecosto.Dock = DockStyle.Fill;

            principal.PanelMisVuelos.Refresh();
        }
    }
}
