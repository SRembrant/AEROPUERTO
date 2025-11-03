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
    public partial class Uc_ModificarDatos_Parte2 : UserControl
    {
        UsuarioRegistrado objUsarioRegistrado;
        Uc_ModificarDatos objModificarAnterior;
        PaginaPrincipal principal;
        string nuevoNombre, nuevoApellido, nuevoCorreo, nuevoUsuario, nuevoGenero, nuevaContrasenia, nuevaDireccion, nuevoDetalle, nuevaNacionalidad, nuevotipoID;
        int? nuevaID, originalID, pkUsuario;
        long? nuevoTelefono;
        DateTime nuevaFechaNacimiento;

        public Uc_ModificarDatos_Parte2(Uc_ModificarDatos objModificarAnterior, PaginaPrincipal principal, UsuarioRegistrado objUsarioRegistrado, int? originalID, string nuevoNombre, string nuevoApellido, string nuevoCorreo, string nuevoUsuario, string nuevoGenero, int? nuevaID, string nuevoTipoID)
        {
            InitializeComponent();
            this.pkUsuario = objUsarioRegistrado.PKIdUsuario;
            this.objModificarAnterior = objModificarAnterior;
            this.principal = principal;
            this.objUsarioRegistrado = objUsarioRegistrado;
            this.originalID = originalID;
            this.nuevoNombre = nuevoNombre;
            this.nuevoApellido = nuevoApellido;
            this.nuevoCorreo = nuevoCorreo;
            this.nuevoUsuario = nuevoUsuario;
            this.nuevoGenero = nuevoGenero;
            this.nuevaID = nuevaID;
            this.nuevotipoID = nuevoTipoID;
            dtmFechaNacimiento_ModifParte2.Value = objUsarioRegistrado.FechaNacUsuario;
            txtNumeroTelefonico_ModifParte2.Text = objUsarioRegistrado.TelefonoUsuario.ToString();
            txtContrasenia_ModifParte2.Text = objUsarioRegistrado.ContraseniaUsuario.ToString();
            txtDireccion_ModifParte2.Text = objUsarioRegistrado.DireccionUsuario;
            txtDetalles_ModifParte2.Text = objUsarioRegistrado.DetalleUsuario;
            cbxNacionalidad_ModifParte2.SelectedItem = objUsarioRegistrado.NacionalidadUsuario;
            this.Visible = true;
        }

        private void btnActualizarDatos_ModifParte2_Click(object sender, EventArgs e)
        {
            nuevaFechaNacimiento = dtmFechaNacimiento_ModifParte2.Value.Date;
            nuevoTelefono = long.Parse(txtNumeroTelefonico_ModifParte2.Text);
            nuevaContrasenia = txtContrasenia_ModifParte2.Text;
            nuevaDireccion = txtDireccion_ModifParte2.Text;
            nuevoDetalle = txtDetalles_ModifParte2.Text;
            nuevaNacionalidad = cbxNacionalidad_ModifParte2.SelectedItem.ToString();

            string resultado = objUsarioRegistrado.ModificarUsuario(pkUsuario, originalID, nuevaID, nuevotipoID, nuevoNombre,
                    nuevoApellido, nuevoCorreo, nuevoGenero, nuevaFechaNacimiento, nuevaNacionalidad, nuevoUsuario,
                    nuevaContrasenia, nuevaDireccion, nuevoDetalle, nuevoTelefono);

            if (!string.IsNullOrWhiteSpace(resultado))
            {
                MessageBox.Show(resultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void lblRegresarAnterior_ModifParte2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            objModificarAnterior.Visible = true;
            principal.PanelMiPerfil.Refresh();
        }

        private void lblRegresarInicio_Modificar2_Click(object sender, EventArgs e)
        {
            principal.PanelContenedorPerfil.Visible = false;

            principal.PanelMiPerfil.Visible = true;
            principal.PanelMiPerfil.BringToFront();

            principal.ActualizarPantalla();
        }

    }
}
