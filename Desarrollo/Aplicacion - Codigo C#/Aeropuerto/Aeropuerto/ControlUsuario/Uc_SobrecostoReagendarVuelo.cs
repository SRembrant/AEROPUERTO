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
    public partial class Uc_SobrecostoReagendarVuelo : UserControl
    {
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;
        int idPasaje;
        Pasaje gestorPasaje;
        Uc_Informacion_Vuelo anterior;

        public Uc_SobrecostoReagendarVuelo(PaginaPrincipal principal, UsuarioRegistrado objUsuarioRegistrado, int idPasaje, Pasaje gestorPasaje, Uc_Informacion_Vuelo anterior)
        {
            InitializeComponent();
            this.principal = principal;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.idPasaje = idPasaje;
            this.gestorPasaje = gestorPasaje;
            this.anterior = anterior;
            this.Visible = true;
        }

        private void btnConfirmar_ReagendarVuelo_Click(object sender, EventArgs e)
        {
            /*Uc_ReagendarPasaje ucReagendar = new Uc_ReagendarPasaje(principal, objUsuarioRegistrado, idPasaje, gestorPasaje);
            principal.PanelMisVuelos.Controls.Clear();
            principal.PanelMisVuelos.Controls.Add(ucReagendar);
            ucReagendar.Dock = DockStyle.Fill;*/

            var ucReagendar = new Uc_ReagendarPasaje(principal, objUsuarioRegistrado, idPasaje, gestorPasaje);

            // Limpia el panel y agrega el nuevo control
            this.Visible = false;
            principal.PanelContenedorMisVuelos.Controls.Add(ucReagendar);
            ucReagendar.Dock = DockStyle.Fill;

            principal.PanelMisVuelos.Refresh();
        }

        private void btnCancelar_ReagendarVuelo_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            anterior.Visible = true;
            principal.PanelMisVuelos.Refresh();
        }
    }
}
