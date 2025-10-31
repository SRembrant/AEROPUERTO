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
    public partial class Inicio_Sesion : Form
    {
        UsuarioRegistrado objUsuarioRegistrado = new UsuarioRegistrado();
        public Inicio_Sesion()
        {
            InitializeComponent();
            lblOlvidarContrasenia.Hide();
        }


        private void btnInicio_sesion_Click(object sender, EventArgs e)
        {
            string usuarioNombre = txtNombreUsuario.Text;
            string contrasenia = txtContrasenia.Text;

            if (string.IsNullOrWhiteSpace(usuarioNombre) || string.IsNullOrWhiteSpace(contrasenia))
            {
                MessageBox.Show("Debe ingresar usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string resultado = objUsuarioRegistrado.ValidarCredenciales(usuarioNombre, contrasenia);

            if (resultado == null) return;
            
            if (resultado.Contains("permitido"))
            {
                int id = objUsuarioRegistrado.ObtenerIdPorNombreUsuario(usuarioNombre);
                
                /*Console.WriteLine(usuarioNombre);
                Console.WriteLine(id);
                Boolean flag = objUsuarioRegistrado.CargarDatosPorID(id);
                Console.WriteLine(flag);*/

                if (id > 0 && objUsuarioRegistrado.CargarDatosPorID(id))
                {
                    PaginaPrincipal ventana = new PaginaPrincipal(this, objUsuarioRegistrado);
                    ventana.Show();
                    this.Hide();
                    txtNombreUsuario.Text = "";
                    txtContrasenia.Text = "";
                }
                else
                {
                    MessageBox.Show("No se pudo cargar la información del usuario.");
                }
            }
            else
            {
                MessageBox.Show(resultado, "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblCrearCuenta_Click(object sender, EventArgs e)
        {
            Registrar_usuario registro = new Registrar_usuario(this, objUsuarioRegistrado);
            registro.Show();
            this.Hide();
        }

        private void txtNombreUsuario_Click(object sender, EventArgs e)
        {
            txtNombreUsuario.Text = "";
        }

        private void txtContrasenia_Click(object sender, EventArgs e)
        {
            txtContrasenia.Text = "";
        }
    }
}
