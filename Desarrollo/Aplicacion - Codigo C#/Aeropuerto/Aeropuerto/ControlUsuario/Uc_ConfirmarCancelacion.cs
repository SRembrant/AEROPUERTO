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

        public Uc_ConfirmarCancelacion(PaginaPrincipal principal, UsuarioRegistrado usuario, int idPasaje, Pasaje gestorPasaje)
        {
            InitializeComponent();
            this.principal = principal;
            this.usuario = usuario;
            this.idPasaje = idPasaje;
            this.gestorPasaje = gestorPasaje;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Regresar sin cancelar
            Uc_Informacion_Vuelo ucInfo = new Uc_Informacion_Vuelo(principal, usuario, idPasaje, gestorPasaje);
            principal.PanelMisVuelos.Controls.Clear();
            principal.PanelMisVuelos.Controls.Add(ucInfo);
            ucInfo.Dock = DockStyle.Fill;
        }

        private void btnConfirmarCancelacion_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamar al procedimiento de cancelación
                string resultado = gestorPasaje.CancelarPasaje(idPasaje);

                MessageBox.Show(resultado, "Cancelación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Regresar a MisVuelos
                Uc_MisVuelos ucMisVuelos = new Uc_MisVuelos(principal, usuario);
                principal.PanelMisVuelos.Controls.Clear();
                principal.PanelMisVuelos.Controls.Add(ucMisVuelos);
                ucMisVuelos.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar el pasaje: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
