using Aeropuerto.ControlUsuario;
using Aeropuerto.logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        private void VerPoliticas_Reagendamiento_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=======================================");
            sb.AppendLine("        ✈ REGLAS DE REAGENDAMIENTO ✈");
            sb.AppendLine("=======================================");
            sb.AppendLine("1. Un pasaje puede ser reagendado un máximo de dos veces.");
            sb.AppendLine("   Un tercer intento NO está permitido.");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("2. El sistema ofrecerá por defecto opciones con la MISMA aerolínea");
            sb.AppendLine("   y el MISMO trayecto del vuelo original.");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("3. El sobrecosto depende de los días de antelación.");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("4. El sobrecosto se calcula sobre el precio base del vuelo original.");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("5. Si el nuevo vuelo tiene el mismo precio base o uno menor,");
            sb.AppendLine("   el pasajero solo paga el sobrecosto.");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("6. Si el nuevo vuelo tiene un precio base mayor, el recargo es:");
            sb.AppendLine("   Precio base nuevo - Precio base viejo + sobrecosto.");
            sb.AppendLine("----------------------------------------");
            sb.AppendLine("TABLA DE PORCENTAJES REGISTRADA:");
            sb.AppendLine(">30 días de antelación ............ 10%");
            sb.AppendLine("15 a 29 días ....................... 20%");
            sb.AppendLine("7 a 14 días ........................ 35%");
            sb.AppendLine("3 a 6 días ......................... 50%");
            sb.AppendLine("1 a 2 días ......................... 70%");
            sb.AppendLine("Mismo día o <24h ................... 90%");
            sb.AppendLine("=======================================");
            sb.AppendLine("Gracias por preferirnos. ¡Buen viaje!");
            sb.AppendLine("=======================================");


            // Mostrar reglas simuladas en ventana
            MessageBox.Show(sb.ToString(), "Reglas Reagendamiento", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
