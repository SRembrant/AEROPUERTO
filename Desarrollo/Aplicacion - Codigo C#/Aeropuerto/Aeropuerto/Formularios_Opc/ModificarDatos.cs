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
    public partial class ModificarDatos : Form
    {
        UsuarioRegistrado objUsuarioRegistrado;
        PaginaPrincipal principal;
        
        public ModificarDatos(PaginaPrincipal principal, UsuarioRegistrado objUsuarioRegistrado)
        {
            InitializeComponent();
            this.principal = principal;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            txtTuNombre_Modificar.Text = objUsuarioRegistrado.NombreUsuario;
            txtDireccionCorreo_Modificar.Text = objUsuarioRegistrado.CorreoUsuario;
            txtNombreUsuario_Modificar.Text = objUsuarioRegistrado.UsuarioAcceso;
            txtApellidoUsuario_Modificar.Text =objUsuarioRegistrado.ApellidoUsuario;
            txtNumIdentificacion_Modificar.Text = objUsuarioRegistrado.DocIdUsuario.ToString();
            cbxTipoIdentificacion.SelectedItem = objUsuarioRegistrado.TipoIdUsuario;
            cbxNuevoGenero.SelectedItem = objUsuarioRegistrado.GeneroUsuario;
        }

        private void btnGuardarYContinuar_Click(object sender, EventArgs e)
        {
            string nuevoNombre, nuevoApellido, nuevoCorreo, nuevoUsuario, nuevoGenero, nuevoTipoID;
            int? nuevaID, originalID;

            originalID = this.objUsuarioRegistrado.DocIdUsuario;

            nuevoNombre = txtTuNombre_Modificar.Text;
            nuevoApellido = txtApellidoUsuario_Modificar.Text;
            nuevoCorreo = txtDireccionCorreo_Modificar.Text;
            nuevoUsuario = txtNombreUsuario_Modificar.Text;
            nuevaID = int.Parse(txtNumIdentificacion_Modificar.Text);
            nuevoTipoID = cbxTipoIdentificacion.SelectedItem.ToString();
            nuevoGenero = cbxNuevoGenero.SelectedItem.ToString();

            var objModificar2 = new ModificarDatos_Parte2(this, this.principal, this.objUsuarioRegistrado, originalID, nuevoNombre, nuevoApellido, nuevoCorreo, 
                                    nuevoUsuario, nuevoGenero, nuevaID, nuevoTipoID);
            objModificar2.Show();
            this.Hide();


        }

        private void pbxRegresar_Modificar1_Click(object sender, EventArgs e)
        {
            this.Hide();
            principal.Show();
        }
    }
}
