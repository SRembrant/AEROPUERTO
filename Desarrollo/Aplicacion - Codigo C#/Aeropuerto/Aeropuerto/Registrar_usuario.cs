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
        }

        private void btnGuardarYContinuar_Click(object sender, EventArgs e)
        {
            int? numID;
            string nombre, apellido, correo, nombreUsuario, tipoID = null, genero = null;

            

            if (txtNumIdentificacion.Text == "" && txtPrimerNombre.Text=="" && txtApellido.Text == "" && txtDireccionCorreo_ru.Text== "" && txtNombreUsuario.Text=="")
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
    }
}
