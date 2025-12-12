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
//using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Aeropuerto
{
    public partial class Registrar_usuario : Form
    {
        UsuarioRegistrado objUsuarioRegistrado;
        Inicio_Sesion inicio;
        public Registrar_usuario(Inicio_Sesion inicio, UsuarioRegistrado objUsuarioRegistrado)
        {
            InitializeComponent();
            this.inicio = inicio;
            this.objUsuarioRegistrado = objUsuarioRegistrado;

            // Asociar eventos
            txtPrimerNombre.Leave += ValidarNombre;
            txtApellido.Leave += ValidarApellido;
            txtDireccionCorreo_ru.Leave += ValidarCorreo;
            txtNombreUsuario.Leave += ValidarNombreUsuario;
            txtNumIdentificacion.Leave += ValidarIdentificacion;
            cbxTipoIdentificacion.SelectedIndexChanged += ValidarTipoIdentificacion;

        }

        
        private void lblVolverAInicioSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            inicio.Show();
        }

        private void txtPrimerNombre_Click(object sender, EventArgs e)
        {
            txtPrimerNombre.Text = "";
        }

        private void txtApellido_Click(object sender, EventArgs e)
        {
            txtApellido.Text = "";
        }

        private void txtDireccionCorreo_ru_Click(object sender, EventArgs e)
        {
            txtDireccionCorreo_ru.Text = "";
        }

        private void txtNombreUsuario_Click(object sender, EventArgs e)
        {
            txtNombreUsuario.Text = "";
        }

        private void txtNumIdentificacion_Click(object sender, EventArgs e)
        {
            txtNumIdentificacion.Text = "";
        }
        

        private void ValidarNombre(object sender, EventArgs e)
        {
            string nombre = LimpiarExtremos(txtPrimerNombre.Text);

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
                txtPrimerNombre.Text = nombre;
                lblErrorCampoObligatorioNombre.Visible = false;
            }
        }

        private void ValidarApellido(object sender, EventArgs e)
        {
            string apellido = LimpiarExtremos(txtApellido.Text);

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
                txtApellido.Text = apellido;
                lblErrorApellidoValoresNumericos.Visible = false;
            }
        }


        private void ValidarCorreo(object sender, EventArgs e)
        {
            string correo = LimpiarExtremos(txtDireccionCorreo_ru.Text);

            if (string.IsNullOrWhiteSpace(txtDireccionCorreo_ru.Text))
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
                txtDireccionCorreo_ru.Text = correo;
                lblErrorCorreoInvalido.Visible = false;
            }
        }

        
        private void ValidarNombreUsuario(object sender, EventArgs e)
        {
            string nombreUsuario = LimpiarExtremos(txtNombreUsuario.Text);

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

                txtNombreUsuario.Text = nombreUsuario;
                lblErrorCampoObligNombreUsuario.Visible = false;
            }
        }

        private void ValidarIdentificacion(object sender, EventArgs e)
        {
            string id = txtNumIdentificacion.Text;

            if (!int.TryParse(txtNumIdentificacion.Text, out int numID) || numID <= 0)
            {
                lblErrorIdentificacionValoresNoNumericos.Text = "Debe ingresar un número válido";
                lblErrorIdentificacionValoresNoNumericos.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(txtNumIdentificacion.Text))
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
                txtNumIdentificacion.Text = id;
                lblErrorIdentificacionValoresNoNumericos.Visible = false;
            }
        }

        private void ValidarTipoIdentificacion(object sender, EventArgs e)
        {
            lblErrorCampObligIdentificacion.Visible = (cbxTipoIdentificacion.SelectedItem == null);
        }

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

        //Apoyo a validaciones
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
            ValidarNombreUsuario(null, null);
            ValidarIdentificacion(null, null);
            ValidarTipoIdentificacion(null, null);
            

            // Si algún label de error está visible, no continuar
            if (lblErrorCampoObligatorioNombre.Visible ||
                lblErrorApellidoValoresNumericos.Visible ||
                lblErrorCorreoInvalido.Visible ||
                lblErrorIdentificacionValoresNoNumericos.Visible ||
                lblErrorCampObligIdentificacion.Visible)
            {
                MessageBox.Show("Por favor, corrige los errores antes de continuar.",
                                "Campos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Todo válido → Continuar
            int numID = int.Parse(txtNumIdentificacion.Text);
            string nombre = txtPrimerNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string correo = txtDireccionCorreo_ru.Text.Trim();
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string tipoID = cbxTipoIdentificacion.SelectedItem.ToString();
            string genero = cbxGenero.SelectedItem.ToString();

            Registrar_Usuario_Parte2 parte2 = new Registrar_Usuario_Parte2(
                this, inicio, objUsuarioRegistrado, numID, nombre, apellido, correo, nombreUsuario, tipoID, genero);
            parte2.Show();
            this.Hide();
        }

    }
}
