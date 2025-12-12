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

            txtNumeroTelefonico_ModifParte2.Leave += ValidarTelefono;
            txtContrasenia_ModifParte2.Leave += ValidarContrasenia;
            txtDireccion_ModifParte2.Leave += ValidarDireccion;
        }


        // --- VALIDACIONES INDIVIDUALES ---
        private void ValidarTelefono(object sender, EventArgs e)
        {
            string telefono = txtNumeroTelefonico_ModifParte2.Text;

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
                txtNumeroTelefonico_ModifParte2.Text = telefono;
                lblErrorCampoObligatorioNumTelefono.Visible = false;
                nuevoTelefono = long.Parse(telefono);
            }
        }


        private void ValidarContrasenia(object sender, EventArgs e)
        {
            string pass = txtContrasenia_ModifParte2.Text.Trim();

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
                string patron = @"^[A-Za-z0-9\*\#_\-/]+$";


                if (!System.Text.RegularExpressions.Regex.IsMatch(pass, patron))
                {
                    lblErrorCampoObligatorioContrasenia.Text =
                        "La contraseña solo permite estos caracteres especiales: *  #  _  -  /";
                    lblErrorCampoObligatorioContrasenia.Visible = true;
                    return;
                }

                txtContrasenia_ModifParte2.Text = pass;
                lblErrorCampoObligatorioContrasenia.Visible = false;
                nuevaContrasenia = pass;
            }
        }


        private void ValidarDireccion(object sender, EventArgs e)
        {
            string dir = LimpiarExtremos(txtDireccion_ModifParte2.Text);

            if (string.IsNullOrWhiteSpace(txtDireccion_ModifParte2.Text))
            {
                lblErrorCampoObligatorioDireccion.Text = "La dirección no puede estar vacía";
                lblErrorCampoObligatorioDireccion.Visible = true;
            }
            else
            {
                txtDireccion_ModifParte2.Text = dir;
                lblErrorCampoObligatorioDireccion.Visible = false;
                nuevaDireccion = txtDireccion_ModifParte2.Text.Trim();
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

        private void btnActualizarDatos_ModifParte2_Click(object sender, EventArgs e)
        {
            // Ejecutar validaciones manualmente
            ValidarTelefono(null, null);
            ValidarContrasenia(null, null);
            ValidarDireccion(null, null);

            // Verificar si hay errores visibles
            if (lblErrorCampoObligatorioNumTelefono.Visible ||
                lblErrorCampoObligatorioContrasenia.Visible ||
                lblErrorCampoObligatorioDireccion.Visible)
            {
                MessageBox.Show("Por favor corrige los errores antes de continuar.",
                                "Campos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            this.Visible = false;
            objModificarAnterior.Visible = true;

            principal.PanelContenedorPerfil.Visible = false;

            principal.PanelMiPerfil.Visible = true;
            principal.PanelMiPerfil.BringToFront();

            principal.ActualizarPantalla();
        }

    }
}
