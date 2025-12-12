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
    public partial class Uc_ModificarDatos : UserControl
    {
        UsuarioRegistrado objUsuarioRegistrado;
        PaginaPrincipal principal;
        public Uc_ModificarDatos(PaginaPrincipal principal, UsuarioRegistrado objUsuarioRegistrado)
        {
            InitializeComponent();
            this.principal = principal;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            txtTuNombre_Modificar.Text = objUsuarioRegistrado.NombreUsuario;
            txtDireccionCorreo_Modificar.Text = objUsuarioRegistrado.CorreoUsuario;
            txtNombreUsuario_Modificar.Text = objUsuarioRegistrado.UsuarioAcceso;
            txtApellidoUsuario_Modificar.Text = objUsuarioRegistrado.ApellidoUsuario;
            txtNumIdentificacion_Modificar.Text = objUsuarioRegistrado.DocIdUsuario.ToString();
            cbxTipoIdentificacion.SelectedItem = objUsuarioRegistrado.TipoIdUsuario;
            cbxNuevoGenero.SelectedItem = objUsuarioRegistrado.GeneroUsuario;
            this.Visible = true;

            // Asociar eventos
            txtTuNombre_Modificar.Leave += ValidarNombre;
            txtApellidoUsuario_Modificar.Leave += ValidarApellido;
            txtDireccionCorreo_Modificar.Leave += ValidarCorreo;
            txtNombreUsuario_Modificar.Leave += ValidarNombreUsuario;
            txtNumIdentificacion_Modificar.Leave += ValidarIdentificacion;
        }


        //Validaciones
        private void ValidarNombre(object sender, EventArgs e)
        {
            string nombre = LimpiarExtremos(txtTuNombre_Modificar.Text);

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblErrorCampoObligatorioNombre.Text = "El nombre no puede estar vacío";
                lblErrorCampoObligatorioNombre.Visible = true;
            }
            else if (!nombre.All(c => char.IsLetter(c)))
            {
                lblErrorCampoObligatorioNombre.Text = "El nombre solo puede contener letras";
                lblErrorCampoObligatorioNombre.Visible = true;
            }
            else if (!TextoValido(nombre))
            {
                lblErrorCampoObligatorioNombre.Text = "Nombre inválido. No debe tener espacios al inicio/fin y solo letras.";
                lblErrorCampoObligatorioNombre.Visible = true;
            }
            else
            {
                txtTuNombre_Modificar.Text = nombre;
                lblErrorCampoObligatorioNombre.Visible = false;
            }
        }

        private void ValidarApellido(object sender, EventArgs e)
        {
            string apellido = LimpiarExtremos(txtApellidoUsuario_Modificar.Text);

            if (string.IsNullOrWhiteSpace(apellido))
            {
                lblErrorApellidoValoresNumericos.Text = "El apellido no puede estar vacío";
                lblErrorApellidoValoresNumericos.Visible = true;
            }
            else if (!apellido.All(c => char.IsLetter(c)))
            {
                lblErrorApellidoValoresNumericos.Text = "El apellido solo puede contener letras";
                lblErrorApellidoValoresNumericos.Visible = true;
            }
            else if (!TextoValido(apellido))
            {
                lblErrorApellidoValoresNumericos.Text = "Apellido inválido. No debe tener espacios al inicio/fin y solo letras.";
                lblErrorApellidoValoresNumericos.Visible = true;
            }
            else
            {
                txtApellidoUsuario_Modificar.Text = apellido;
                lblErrorApellidoValoresNumericos.Visible = false;
            }
        }

        private void ValidarCorreo(object sender, EventArgs e)
        {
            string correo = LimpiarExtremos(txtDireccionCorreo_Modificar.Text);

            if (string.IsNullOrWhiteSpace(txtDireccionCorreo_Modificar.Text))
            {
                lblErrorCorreoInvalido.Text = "El correo no puede estar vacío";
                lblErrorCorreoInvalido.Visible = true;
            }
            else if (!EsCorreoValido(correo))
            {
                lblErrorCorreoInvalido.Text = "Formato de correo inválido";
                lblErrorCorreoInvalido.Visible = true;
            }
            else if (correo.Contains(" "))
            {
                lblErrorCorreoInvalido.Text = "El correo no puede contener espacios.";
                lblErrorCorreoInvalido.Visible = true;
            }
            else
            {
                txtDireccionCorreo_Modificar.Text = correo;
                lblErrorCorreoInvalido.Visible = false;
            }
        }

        private void ValidarNombreUsuario(object sender, EventArgs e)
        {
            string nombreUsuario = LimpiarExtremos(txtNombreUsuario_Modificar.Text);

            if (string.IsNullOrWhiteSpace(nombreUsuario))
            {
                lblErrorCampoObligNombreUsuario.Text = "El nombre de usuario no puede estar vacío";
                lblErrorCampoObligNombreUsuario.Visible = true;
            }
            else if (ContieneEspaciosInternos(nombreUsuario))
            {
                lblErrorCampoObligNombreUsuario.Text = "No se permiten espacios en el nombre de usuario.";
                lblErrorCampoObligNombreUsuario.Visible = true;
            }
            else
            {
                string patron = @"^[A-Za-z0-9\*\#_\-/]+$";


                if (!System.Text.RegularExpressions.Regex.IsMatch(nombreUsuario, patron))
                {
                    lblErrorCampoObligNombreUsuario.Text =
                        "El nombre de usuario solo permite estos caracteres especiales: *  #  _  -  /";
                    lblErrorCampoObligNombreUsuario.Visible = true;
                    return;
                }

                txtNombreUsuario_Modificar.Text = nombreUsuario;
                lblErrorCampoObligNombreUsuario.Visible = false;
            }
        }

        private void ValidarIdentificacion(object sender, EventArgs e)
        {
            string id = txtNumIdentificacion_Modificar.Text;

            if (!int.TryParse(txtNumIdentificacion_Modificar.Text, out int numID) || numID <= 0)
            {
                lblErrorIdentificacionValoresNoNumericos.Text = "Debe ingresar un número válido";
                lblErrorIdentificacionValoresNoNumericos.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(txtNumIdentificacion_Modificar.Text))
            {
                lblErrorIdentificacionValoresNoNumericos.Text = "El ID no puede estar vacío";
                lblErrorIdentificacionValoresNoNumericos.Visible = true;
            }
            else if (ContieneEspaciosInternos(id))
            {
                lblErrorIdentificacionValoresNoNumericos.Text = "No se permiten espacios en el número.";
                lblErrorIdentificacionValoresNoNumericos.Visible = true;
            }
            else
            {
                txtNumIdentificacion_Modificar.Text = id;
                lblErrorIdentificacionValoresNoNumericos.Visible = false;
            }
        }

        //Apoyo a validaciones
        private bool EsCorreoValido(string correo)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }

        private string LimpiarExtremos(string input)
        {
            return input?.Trim();
        }

        private bool ContieneEspaciosInternos(string input)
        {
            return input.Contains(" ");
        }

        private bool TextoValido(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Quitar espacios al inicio y al final
            string limpio = input.Trim();

            // No permitir que tenga múltiples espacios consecutivos
            if (limpio.Contains("  "))
                return false;

            return limpio.All(c => char.IsLetter(c) || c == ' ');
        }


        private void btnGuardarYContinuar_Click(object sender, EventArgs e)
        {
            // Llamar validaciones manualmente para asegurar que todas se ejecuten
            ValidarNombre(null, null);
            ValidarApellido(null, null);
            ValidarCorreo(null, null);


            // Si algún label de error está visible, no continuar
            if (lblErrorCampoObligatorioNombre.Visible ||
                lblErrorApellidoValoresNumericos.Visible ||
                lblErrorCorreoInvalido.Visible ||
                lblErrorIdentificacionValoresNoNumericos.Visible)
            {
                MessageBox.Show("Por favor, corrige los errores antes de continuar.",
                                "Campos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            var objModificar2 = new Uc_ModificarDatos_Parte2(this, this.principal, this.objUsuarioRegistrado, originalID, nuevoNombre, nuevoApellido, nuevoCorreo,
                                    nuevoUsuario, nuevoGenero, nuevaID, nuevoTipoID);

            // Limpia el panel y agrega el nuevo control
            this.Visible = false;
            principal.PanelContenedorPerfil.Controls.Add(objModificar2);
            objModificar2.Dock = DockStyle.Fill;

            principal.PanelMiPerfil.Refresh();
        }

        private void pbxRegresar_Modificar1_Click(object sender, EventArgs e)
        {
            principal.PanelContenedorPerfil.Visible = false;

            principal.PanelMiPerfil.Visible = true;
            principal.PanelMiPerfil.BringToFront();

            principal.ActualizarPantalla();
        }

        
    }
}
