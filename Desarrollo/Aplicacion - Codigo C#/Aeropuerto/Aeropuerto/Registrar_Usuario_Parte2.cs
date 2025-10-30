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

            if (txtNumeroTelefonico.Text=="" || txtContrasenia.Text=="" || txtDireccion.Text=="" )
            {
                MessageBox.Show("Debe llenar los campos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                fechaNacimiento = dtmFechaNacimiento.Value.Date;
                numTelefono = long.Parse(txtNumeroTelefonico.Text);
                pasword = txtContrasenia.Text;
                direccion = txtDireccion.Text;
                detalles = txtDetalles.Text;

                //validaciones de campos vacios o incorrectos
                if (cbxNacionalidad.SelectedItem != null)
                {
                    nacionalidad = cbxNacionalidad.SelectedItem.ToString();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una nacionalidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (numTelefono <= 0 || !numID.HasValue)
                {
                    lblErrorCampoObligatorioNumTelefono.Show();
                    MessageBox.Show("El numero de telefono debe ser un número positivo y no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(pasword))
                {
                    lblErrorCampoObligatorioContrasenia.Show();
                    MessageBox.Show("La contraseña no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (string.IsNullOrWhiteSpace(direccion))
                {
                    lblErrorCampoObligatorioDireccion.Show();
                    MessageBox.Show("La dirección no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (dtmFechaNacimiento.Value > DateTime.Now)
                {
                    lblErrorCampoObligatorioFechaNac.Show();
                    MessageBox.Show("La fecha de nacimiento no puede ser mayor a la actual.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (chbxAceptoTerminosCondiciones.Checked)
                {
                    string resultado = objUsuarioRegistrado.RegistrarUsuario(numID, tipoID, nombre, apellido, correo, genero, fechaNacimiento, nacionalidad, nombreUsuario, pasword, direccion, numTelefono, detalles);
                    MessageBox.Show(resultado, "Resultado del registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Debe aceptar los términos y condiciones.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
