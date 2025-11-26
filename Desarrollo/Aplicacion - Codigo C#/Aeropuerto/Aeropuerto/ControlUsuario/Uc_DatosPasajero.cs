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

namespace Aeropuerto.ControlUsuario
{
    public partial class Uc_DatosPasajero : UserControl
    {
        Pasaje gestorPasaje = new Pasaje();

        public Uc_DatosPasajero(int numero)
        {
            InitializeComponent();
            lblTituloPasajero.Text = $"Pasajero {numero}";
            CargarCategorias();

            cbxCategoriaAsiento.SelectedIndexChanged += (s, e) =>
            {
                var contenedor = this.Parent?.Parent as Uc_Informacion_Pasajero;
                contenedor?.CalcularTotal();
            };

            this.Visible = true;

            // Asociar validaciones automáticas
            txtNombre_DatosPasajeros.Leave += ValidarNombre;
            txtApellido_DatosPasajero.Leave += ValidarApellido;
            txtCorreo_DatosPasajero.Leave += ValidarCorreo;
            txtIdentificacion_DatosPasajero.Leave += ValidarIdentificacion;
        }

        public string Nombre => txtNombre_DatosPasajeros.Text.Trim();
        public string Apellido => txtApellido_DatosPasajero.Text.Trim();
        public string Correo => txtCorreo_DatosPasajero.Text.Trim();
        public string TipoIdentificacion => cbxTipoID_DatosPasajero.SelectedItem?.ToString() ?? "";
        public string Identificacion => txtIdentificacion_DatosPasajero.Text.Trim();
        public string Categoria => cbxCategoriaAsiento.Text;

        private void CargarCategorias()
        {
            try
            {
                DataTable tabla = gestorPasaje.ObtenerCategoriasAsiento();
                cbxCategoriaAsiento.DataSource = tabla;
                cbxCategoriaAsiento.DisplayMember = "NOMBRE";
                cbxCategoriaAsiento.ValueMember = "ID";
                cbxCategoriaAsiento.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }
        }


        private void ValidarNombre(object sender, EventArgs e)
        {
            string nombre = LimpiarExtremos(txtNombre_DatosPasajeros.Text);

            if (string.IsNullOrWhiteSpace(nombre))
            {
                lblErrorNombre.Text = "El nombre no puede estar vacío";
                lblErrorNombre.Visible = true;
            }
            else if (!nombre.All(c => char.IsLetter(c)))
            {
                lblErrorNombre.Text = "El nombre solo puede contener letras";
                lblErrorNombre.Visible = true;
            }
            else if (!TextoValido(nombre))
            {
                lblErrorNombre.Text = "Nombre inválido. No debe tener espacios al inicio/fin y solo letras.";
                lblErrorNombre.Visible = true;
            }
            else
            {
                txtNombre_DatosPasajeros.Text = nombre;
                lblErrorNombre.Visible = false;
            }
        }

        private void ValidarApellido(object sender, EventArgs e)
        {
            string apellido = LimpiarExtremos(txtApellido_DatosPasajero.Text);

            if (string.IsNullOrWhiteSpace(apellido))
            {
                lblErrorApellido.Text = "El apellido no puede estar vacío";
                lblErrorApellido.Visible = true;
            }
            else if (!apellido.All(c => char.IsLetter(c)))
            {
                lblErrorApellido.Text = "El apellido solo puede contener letras";
                lblErrorApellido.Visible = true;
            }
            else if (!TextoValido(apellido))
            {
                lblErrorApellido.Text = "Apellido inválido. No debe tener espacios al inicio/fin y solo letras.";
                lblErrorApellido.Visible = true;
            }
            else
            {
                txtApellido_DatosPasajero.Text = apellido;
                lblErrorApellido.Visible = false;
            }
        }


        private void ValidarCorreo(object sender, EventArgs e)
        {
            string correo = LimpiarExtremos(txtCorreo_DatosPasajero.Text);

            if (string.IsNullOrWhiteSpace(correo))
            {
                lblErrorCorreo.Text = "El correo no puede estar vacío";
                lblErrorCorreo.Visible = true;
            }
            else if (!EsCorreoValido(txtCorreo_DatosPasajero.Text))
            {
                lblErrorCorreo.Text = "Formato de correo inválido";
                lblErrorCorreo.Visible = true;
            }
            else if (correo.Contains(" "))
            {
                lblErrorCorreo.Text = "El correo no puede contener espacios.";
                lblErrorCorreo.Visible = true;
            }
            else
            {
                txtCorreo_DatosPasajero.Text = correo;
                lblErrorCorreo.Visible = false;
            }
        }

        private void ValidarIdentificacion(object sender, EventArgs e)
        {
            string id = txtIdentificacion_DatosPasajero.Text;

            if (!int.TryParse(txtIdentificacion_DatosPasajero.Text, out int numID) || numID <= 0)
            {
                lblErrorID.Text = "Debe ingresar un número válido";
                lblErrorID.Visible = true;
            }
            else if (string.IsNullOrWhiteSpace(txtIdentificacion_DatosPasajero.Text))
            {
                lblErrorID.Text = "El ID no puede estar vacío";
                lblErrorID.Visible = true;
            }
            else if (ContieneEspaciosInternos(id))
            {
                lblErrorID.Text = "No se permiten espacios en el número.";
                lblErrorID.Visible = true;
            }
            else
            {
                txtIdentificacion_DatosPasajero.Text = id;
                lblErrorID.Visible = false;
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

        public bool ValidarTodo()
        {
            ValidarNombre(null, null);
            ValidarApellido(null, null);
            ValidarCorreo(null, null);
            ValidarIdentificacion(null, null);

            if (lblErrorNombre.Visible || lblErrorApellido.Visible || lblErrorCorreo.Visible || lblErrorID.Visible)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
