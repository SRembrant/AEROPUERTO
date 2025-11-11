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
using System.Web.UI.WebControls;
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
            txtPrimerNombre.Leave += ValidarNombre;
            txtApellido.Leave += ValidarApellido;
            txtDireccionCorreo_ru.Leave += ValidarCorreo;
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
            string nombre = txtPrimerNombre.Text.Trim();

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
            else
            {
                lblErrorCampoObligatorioNombre.Visible = false;
            }
        }

        private void ValidarApellido(object sender, EventArgs e)
        {
            string apellido = txtApellido.Text.Trim();

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
            else
            {
                lblErrorApellidoValoresNumericos.Visible = false;
            }
        }


        private void ValidarCorreo(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDireccionCorreo_ru.Text))
            {
                lblErrorCorreoInvalido.Text = "El correo no puede estar vacío";
                lblErrorCorreoInvalido.Visible = true;
            }
            else if (!EsCorreoValido(txtDireccionCorreo_ru.Text))
            {
                lblErrorCorreoInvalido.Text = "Formato de correo inválido";
                lblErrorCorreoInvalido.Visible = true;
            }
            else
            {
                lblErrorCorreoInvalido.Visible = false;
            }
        }

        private void ValidarIdentificacion(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNumIdentificacion.Text, out int numID) || numID <= 0)
            {
                lblErrorIdentificacionValoresNoNumericos.Text = "Debe ingresar un número válido";
                lblErrorIdentificacionValoresNoNumericos.Visible = true;
            }
            else
            {
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

        private void btnGuardarYContinuar_Click(object sender, EventArgs e)
        {
            // Llamar validaciones manualmente para asegurar que todas se ejecuten
            ValidarNombre(null, null);
            ValidarApellido(null, null);
            ValidarCorreo(null, null);
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


/*private void btnGuardarYContinuar_Click(object sender, EventArgs e)
        {
            int? numID;
            string nombre, apellido, correo, nombreUsuario, tipoID = null, genero = null;



            if (txtNumIdentificacion.Text == "" && txtPrimerNombre.Text == "" && txtApellido.Text == "" && txtDireccionCorreo_ru.Text == "" && txtNombreUsuario.Text == "")
            {
                MessageBox.Show("Debe llenar los campos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //validaciones
                if (txtPrimerNombre.Text == "")
                {
                    lblErrorCampoObligatorioNombre.Show();
                    MessageBox.Show("El primer nombre no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorCampoObligatorioNombre.Hide();
                    nombre = txtPrimerNombre.Text;
                }

                if (txtApellido.Text == "")
                {
                    lblErrorApellidoValoresNumericos.Show();
                    MessageBox.Show("El apellido no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorApellidoValoresNumericos.Hide();
                    apellido = txtApellido.Text;
                }

                if (txtDireccionCorreo_ru.Text == "")
                {
                    lblErrorCorreoInvalido.Show();
                    MessageBox.Show("El correo no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorCorreoInvalido.Hide();
                    correo = txtDireccionCorreo_ru.Text.Trim();
                }

                if (txtNombreUsuario.Text == "")
                {
                    lblErrorCampoObligNombreUsuario.Show();
                    MessageBox.Show("El nombre de usuario no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorCampoObligNombreUsuario.Hide();
                    nombreUsuario = txtNombreUsuario.Text;
                }


                if (cbxTipoIdentificacion.SelectedItem != null)
                {
                    lblErrorCampObligIdentificacion.Hide();
                    tipoID = cbxTipoIdentificacion.SelectedItem.ToString();
                }
                else
                {
                    lblErrorCampObligIdentificacion.Show();
                    MessageBox.Show("Debe seleccionar un tipo de identificacion.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumIdentificacion.Text))
                {
                    lblErrorIdentificacionValoresNoNumericos.Show();
                    MessageBox.Show("El número de ID no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorIdentificacionValoresNoNumericos.Hide();
                    try
                    {
                        numID = int.Parse(txtNumIdentificacion.Text);
                        if (numID <= 0)
                        {
                            lblErrorIdentificacionValoresNoNumericos.Show();
                            MessageBox.Show("El número de ID debe ser un número positivo y diferente de cero.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch
                    {
                        lblErrorIdentificacionValoresNoNumericos.Show();
                        MessageBox.Show("El número de ID debe contener solo números.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (cbxGenero.SelectedItem != null)
                {
                    genero = cbxGenero.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar genero.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //mandamos la consulta a la siguiente ventana
                Registrar_Usuario_Parte2 objRegistrar_Usuario_Parte2 = new Registrar_Usuario_Parte2(this, inicio, objUsuarioRegistrado, numID, nombre, apellido, correo, nombreUsuario, tipoID, genero);
                objRegistrar_Usuario_Parte2.Show();
                this.Hide();
            }

        }*/