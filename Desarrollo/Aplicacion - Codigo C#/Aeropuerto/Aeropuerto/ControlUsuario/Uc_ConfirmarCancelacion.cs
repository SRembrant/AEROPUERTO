using Aeropuerto.logica;
using System;
using System.Windows.Forms;

namespace Aeropuerto
{
    public partial class Uc_ConfirmarCancelacion : UserControl
    {
        private PaginaPrincipal principal;
        private UsuarioRegistrado usuario;
        private int idPasaje;
        private Pasaje gestorPasaje;
        Uc_Informacion_Vuelo anterior;

        public Uc_ConfirmarCancelacion(PaginaPrincipal principal, UsuarioRegistrado usuario, int idPasaje, Pasaje gestorPasaje, Uc_Informacion_Vuelo anterior)
        {
            InitializeComponent();
            this.principal = principal;
            this.usuario = usuario;
            this.idPasaje = idPasaje;
            this.gestorPasaje = gestorPasaje;
            this.anterior = anterior;
            this.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            anterior.Visible = true;
            principal.PanelMisVuelos.Refresh();
        }

        private void btnConfirmarCancelacion_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamar al procedimiento de cancelación
                string resultado = gestorPasaje.CancelarPasaje(idPasaje);

                MessageBox.Show(resultado, "Cancelación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Regresar a MisVuelos
                /*
                Uc_MisVuelos ucMisVuelos = new Uc_MisVuelos(principal, usuario);
                principal.PanelMisVuelos.Controls.Clear();
                principal.PanelMisVuelos.Controls.Add(ucMisVuelos);
                ucMisVuelos.Dock = DockStyle.Fill;*/

                principal.PanelContenedorMisVuelos.Visible = false;

                principal.PanelMisVuelos.Visible = true;
                principal.PanelMisVuelos.BringToFront();

                principal.ActualizarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar el pasaje: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
