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
    public partial class Registrar_Usuario_Parte2 : Form
    {
        UsuarioRegistrado objUsuarioRegistrado;
        Inicio_Sesion inicio;
        Registrar_usuario registrar1;
        DateTime fechaNacimiento;
        int? numID;
        long? numTelefono;
        string nombre, apellido, correo, nombreUsuario, tipoID, genero, pasword, direccion, detalles, nacionalidad = null;

        public Registrar_Usuario_Parte2(Registrar_usuario registrar1, Inicio_Sesion inicio, UsuarioRegistrado objUsuarioRegistrado, int? numID, string nombre, string apellido, string correo,
                                string nombreUsuario, string tipoID, string genero)
        {
            InitializeComponent();
            dtmFechaNacimiento.MinDate = DateTime.Today.AddYears(-85);
            dtmFechaNacimiento.MaxDate = DateTime.Today;
            this.inicio = inicio;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.registrar1 = registrar1;
            this.numID = numID;
            this.nombre = nombre;
            this.apellido = apellido;
            this.correo = correo;
            this.nombreUsuario = nombreUsuario;
            this.tipoID = tipoID;
            this.genero = genero;
        }

        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            // Validación general de campos vacíos
            if (txtNumeroTelefonico.Text == "" && txtContrasenia.Text == "" && txtDireccion.Text == "")
            {
                MessageBox.Show("Debe llenar los campos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                
                // Validar número telefónico
                if (string.IsNullOrWhiteSpace(txtNumeroTelefonico.Text))
                {
                    lblErrorCampoObligatorioNumTelefono.Show();
                    MessageBox.Show("El número telefónico no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorCampoObligatorioNumTelefono.Hide();
                    try
                    {
                        numTelefono = long.Parse(txtNumeroTelefonico.Text);
                        if (numTelefono <= 0)
                        {
                            lblErrorCampoObligatorioNumTelefono.Show();
                            MessageBox.Show("El número telefónico debe ser un número positivo y diferente de cero.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch
                    {
                        lblErrorCampoObligatorioNumTelefono.Show();
                        MessageBox.Show("El número telefónico debe contener solo números.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Validar contraseña
                if (string.IsNullOrWhiteSpace(txtContrasenia.Text))
                {
                    lblErrorCampoObligatorioContrasenia.Show();
                    MessageBox.Show("La contraseña no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorCampoObligatorioContrasenia.Hide();
                    pasword = txtContrasenia.Text.Trim();
                }

                // Validar dirección
                if (string.IsNullOrWhiteSpace(txtDireccion.Text))
                {
                    lblErrorCampoObligatorioDireccion.Show();
                    MessageBox.Show("La dirección no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorCampoObligatorioDireccion.Hide();
                    direccion = txtDireccion.Text.Trim();
                }

                // Validar nacionalidad
                if (cbxNacionalidad.SelectedItem != null)
                {
                    nacionalidad = cbxNacionalidad.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una nacionalidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar fecha nacimiento
                fechaNacimiento = dtmFechaNacimiento.Value.Date;
                if (fechaNacimiento > DateTime.Now)
                {
                    lblErrorCampoObligatorioFechaNac.Show();
                    MessageBox.Show("La fecha de nacimiento no puede ser mayor a la fecha actual.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    lblErrorCampoObligatorioFechaNac.Hide();
                }

                // Validar detalles opcionalmente
                if (string.IsNullOrWhiteSpace(txtDetalles.Text))
                {
                    detalles = "Sin observaciones";
                }
                else
                {
                    detalles = txtDetalles.Text.Trim();
                }

                // Validar términos y condiciones
                if (!chbxAceptoTerminosCondiciones.Checked)
                {
                    MessageBox.Show("Debe aceptar los términos y condiciones.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string resultado = objUsuarioRegistrado.RegistrarUsuario(numID, tipoID, nombre, apellido, correo, genero, fechaNacimiento, nacionalidad, nombreUsuario, pasword, direccion, numTelefono, detalles);
                    MessageBox.Show(resultado, "Resultado del registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                    
            }
        }

        private void lblVolverAInicioSesion_2_Click(object sender, EventArgs e)
        {
            this.Hide();
            inicio.Show();
        }

        private void lblVolverARegistrar_1_Click(object sender, EventArgs e)
        {
            this.Hide();
            registrar1.Show();
        }

        private void txtNumeroTelefonico_Click(object sender, EventArgs e)
        {
            txtNumeroTelefonico.Text = "";
        }
        private void txtContrasenia_Click(object sender, EventArgs e)
        {
            txtContrasenia.Text = "";
        }
        private void txtDetalles_Click(object sender, EventArgs e)
        {
            txtDetalles.Text = "";
        }
        private void txtDireccion_Click(object sender, EventArgs e)
        {
            txtDireccion.Text = "";
        }
    }
}
