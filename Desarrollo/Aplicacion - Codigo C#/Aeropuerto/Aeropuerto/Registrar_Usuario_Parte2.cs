using Aeropuerto.logica;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
        string nombre, apellido, correo, nombreUsuario, tipoID, genero, password, direccion, detalles, nacionalidad;

        public Registrar_Usuario_Parte2(Registrar_usuario registrar1, Inicio_Sesion inicio, UsuarioRegistrado objUsuarioRegistrado,
                                        int? numID, string nombre, string apellido, string correo, string nombreUsuario,
                                        string tipoID, string genero)
        {
            InitializeComponent();

            dtmFechaNacimiento.MinDate = DateTime.Today.AddYears(-90);
            dtmFechaNacimiento.MaxDate = DateTime.Today.AddYears(-18);

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

            // Asociar validaciones automáticas
            txtNumeroTelefonico.Leave += ValidarTelefono;
            txtContrasenia.Leave += ValidarContrasenia;
            txtDireccion.Leave += ValidarDireccion;
            dtmFechaNacimiento.ValueChanged += ValidarFechaNacimiento;
        }

        // --- VALIDACIONES INDIVIDUALES ---
        private void ValidarTelefono(object sender, EventArgs e)
        {
            string telefono = txtNumeroTelefonico.Text;

            if (string.IsNullOrWhiteSpace(telefono))
            {
                lblErrorCampoObligatorioNumTelefono.Text = "El número telefónico no puede estar vacío";
                lblErrorCampoObligatorioNumTelefono.Visible = true;
            }
            else if (!telefono.All(char.IsDigit))
            {
                lblErrorCampoObligatorioNumTelefono.Text = "Solo se permiten números";
                lblErrorCampoObligatorioNumTelefono.Visible = true;
            }
            else if (!NumeroSinEspacios(telefono) || telefono.Length != 10)
            {
                lblErrorCampoObligatorioNumTelefono.Text = "El teléfono debe tener 10 dígitos y sin espacios.";
                lblErrorCampoObligatorioNumTelefono.Visible = true;
            }
            else if (ContieneEspaciosInternos(telefono))
            {
                lblErrorCampoObligatorioNumTelefono.Text = "No se permiten espacios en el teléfono.";
                lblErrorCampoObligatorioNumTelefono.Visible = true;
            }
            else
            {
                txtNumeroTelefonico.Text = telefono;
                lblErrorCampoObligatorioNumTelefono.Visible = false;
                numTelefono = long.Parse(telefono);
            }
        }


        private void ValidarContrasenia(object sender, EventArgs e)
        {
            string pass = txtContrasenia.Text.Trim();

            if (string.IsNullOrWhiteSpace(pass))
            {
                lblErrorCampoObligatorioContrasenia.Text = "La contraseña no puede estar vacía";
                lblErrorCampoObligatorioContrasenia.Visible = true;
            }
            else if (pass.Length < 8)
            {
                lblErrorCampoObligatorioContrasenia.Text = "Debe tener al menos 8 caracteres";
                lblErrorCampoObligatorioContrasenia.Visible = true;
            }
            else if (pass.Length > 64)
            {
                lblErrorCampoObligatorioContrasenia.Text = "No puede superar los 64 caracteres";
                lblErrorCampoObligatorioContrasenia.Visible = true;
            }
            else
            {
                txtContrasenia.Text = pass;
                lblErrorCampoObligatorioContrasenia.Visible = false;
                password = pass;
            }
        }
        

        private void ValidarDireccion(object sender, EventArgs e)
        {
            string dir = LimpiarExtremos(txtDireccion.Text);

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lblErrorCampoObligatorioDireccion.Text = "La dirección no puede estar vacía";
                lblErrorCampoObligatorioDireccion.Visible = true;
            }
            else
            {
                txtDireccion.Text = dir;
                lblErrorCampoObligatorioDireccion.Visible = false;
                direccion = txtDireccion.Text.Trim();
            }
        }

        private void ValidarFechaNacimiento(object sender, EventArgs e)
        {
            fechaNacimiento = dtmFechaNacimiento.Value.Date;

            if (fechaNacimiento > DateTime.Today)
            {
                lblErrorCampoObligatorioFechaNac.Text = "La fecha no puede ser futura";
                lblErrorCampoObligatorioFechaNac.Visible = true;
            }
            else
            {
                dtmFechaNacimiento.Value = fechaNacimiento;
                lblErrorCampoObligatorioFechaNac.Visible = false;
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

        private bool NumeroSinEspacios(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            if (input.Contains(" "))
                return false;

            return input.All(char.IsDigit);
        }

        // --- VALIDACIÓN FINAL ---
        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            // Ejecutar validaciones manualmente
            ValidarTelefono(null, null);
            ValidarContrasenia(null, null);
            ValidarDireccion(null, null);
            ValidarFechaNacimiento(null, null);
            nacionalidad = cbxNacionalidad.SelectedItem.ToString();

            // Verificar si hay errores visibles
            if (lblErrorCampoObligatorioNumTelefono.Visible ||
                lblErrorCampoObligatorioContrasenia.Visible ||
                lblErrorCampoObligatorioDireccion.Visible ||
                lblErrorCampoObligatorioFechaNac.Visible )
            {
                MessageBox.Show("Por favor corrige los errores antes de continuar.",
                                "Campos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar términos y condiciones
            if (!chbxAceptoTerminosCondiciones.Checked)
            {
                MessageBox.Show("Debe aceptar los términos y condiciones.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar campo opcional
            detalles = string.IsNullOrWhiteSpace(txtDetalles.Text) ? "Sin observaciones" : txtDetalles.Text.Trim();

            Console.WriteLine($"docId={numID}, tipoId={tipoID}, nombre={nombre}, apellido={apellido}, correo={correo}, genero={genero}, fechaNac={fechaNacimiento}, nacionalidad={nacionalidad}, usuario={nombreUsuario}, contrasenia={password}, direccion={direccion}, telefono={numTelefono}");


            // Enviar datos a la lógica
            string resultado = objUsuarioRegistrado.RegistrarUsuario(numID, tipoID, nombre, apellido, correo, genero,
                                                                     fechaNacimiento, nacionalidad, nombreUsuario,
                                                                     password, direccion, numTelefono, detalles);
            MessageBox.Show(resultado, "Resultado del registro", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        // --- Navegación ---
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

        // Limpiar textos al hacer click
        private void txtNumeroTelefonico_Click(object sender, EventArgs e) => txtNumeroTelefonico.Clear();
        private void txtContrasenia_Click(object sender, EventArgs e) => txtContrasenia.Clear();
        private void txtDetalles_Click(object sender, EventArgs e) => txtDetalles.Clear();
        private void txtDireccion_Click(object sender, EventArgs e) => txtDireccion.Clear();
    }
}
