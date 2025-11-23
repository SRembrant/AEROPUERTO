using Aeropuerto.ControlUsuario;
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
    public partial class Uc_ConfirmarReagendamiento : UserControl
    {
        private PaginaPrincipal principal;
        private UsuarioRegistrado objUsuario;
        private int idPasaje, idVuelo, idCategoria, idUsuario;
        private string medioPago;
        private Pasaje gestorPasaje;
        Uc_VuelosDisponibles_Reagendo anterior;

        public Uc_ConfirmarReagendamiento(PaginaPrincipal principal, Uc_VuelosDisponibles_Reagendo anterior, Pasaje gestorPasaje, UsuarioRegistrado objUsuario, int idVuelo, int idCategoria, int idPasaje, int idUsuario, string medioPago)
        {
            InitializeComponent();
            this.principal = principal;
            this.anterior = anterior;
            this.gestorPasaje = gestorPasaje;
            this.objUsuario = objUsuario;
            this.idVuelo = idVuelo;
            this.idCategoria = idCategoria;
            this.idPasaje = idPasaje;
            this.idUsuario = idUsuario;
            this.medioPago = medioPago;
            this.Visible = true;
        }

        private void btnCancelarReagendamiento_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            anterior.Visible = true;
            principal.PanelMisVuelos.Refresh();
        }

        private void btnConfirmarReagendamiento_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamar al procedimiento de reagendamiento
                string resultado = gestorPasaje.ReagendarPasaje(idVuelo, idCategoria, idPasaje, medioPago);

                MessageBox.Show(resultado, "Reagendación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var verFactura = new Uc_GenerarFactura_Reagendo(idVuelo, idUsuario, medioPago, gestorPasaje, objUsuario, principal);

                // Limpia el panel y agrega el nuevo control
                this.Visible = false;
                principal.PanelContenedorMisVuelos.Controls.Add(verFactura);
                verFactura.Dock = DockStyle.Fill;

                principal.PanelMisVuelos.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reagendar el pasaje: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
