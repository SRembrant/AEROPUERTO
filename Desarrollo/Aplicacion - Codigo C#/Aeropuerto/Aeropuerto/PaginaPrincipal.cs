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
    public partial class PaginaPrincipal : Form
    {
        UsuarioRegistrado objUsuarioRegistrado;
        Inicio_Sesion login;
        public PaginaPrincipal(Inicio_Sesion login, UsuarioRegistrado objUsuarioRegistrado)
        {
            InitializeComponent();
            this.login = login;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            txtTuNombre_PgPerfil.Text = objUsuarioRegistrado.NombreUsuario;
            txtDireccionCorreo_Pgperfil.Text = objUsuarioRegistrado.CorreoUsuario;
            txtNombreUsuario_Pgperfil.Text = objUsuarioRegistrado.UsuarioAcceso;
            txtGenero_Pgperfil.Text=objUsuarioRegistrado.GeneroUsuario;
            txtNumIdentificacion_Pgperfil.Text = objUsuarioRegistrado.DocIdUsuario.ToString();
            dtmFechaNacimiento_Pgperfil.Value = objUsuarioRegistrado.FechaNacUsuario;
            txtNumeroTelefonico_Pgperfil.Text = objUsuarioRegistrado.TelefonoUsuario.ToString();
            txtContrasenia_Pgperfil.Text = objUsuarioRegistrado.ContraseniaUsuario;
            txtDireccion_Pgperfil.Text = objUsuarioRegistrado.DireccionUsuario;
            txtDetalles_Pgperfil.Text = objUsuarioRegistrado.DetalleUsuario;
            txtNacionalidad_Pgperfil.Text = objUsuarioRegistrado.NacionalidadUsuario;
        }

        private void btnModificarDatos_Pgperfil_Click(object sender, EventArgs e)
        {
            ModificarDatos opcionModificar = new ModificarDatos(this, this.objUsuarioRegistrado);
            opcionModificar.Show();
            this.Hide();
        }

        public void ActualizarPantalla()
        {
            txtTuNombre_PgPerfil.Text = objUsuarioRegistrado.NombreUsuario;
            txtDireccionCorreo_Pgperfil.Text = objUsuarioRegistrado.CorreoUsuario;
            txtNombreUsuario_Pgperfil.Text = objUsuarioRegistrado.UsuarioAcceso;
            txtGenero_Pgperfil.Text = objUsuarioRegistrado.GeneroUsuario;
            txtNumIdentificacion_Pgperfil.Text = objUsuarioRegistrado.DocIdUsuario.ToString();
            dtmFechaNacimiento_Pgperfil.Value = objUsuarioRegistrado.FechaNacUsuario;
            txtNumeroTelefonico_Pgperfil.Text = objUsuarioRegistrado.TelefonoUsuario.ToString();
            txtContrasenia_Pgperfil.Text = objUsuarioRegistrado.ContraseniaUsuario;
            txtDireccion_Pgperfil.Text = objUsuarioRegistrado.DireccionUsuario;
            txtDetalles_Pgperfil.Text = objUsuarioRegistrado.DetalleUsuario;
            txtNacionalidad_Pgperfil.Text = objUsuarioRegistrado.NacionalidadUsuario;
        }

        //EPICA 3
        Vuelo objVuelo = new Vuelo();

        private void CargarOrigenes()
        {
            DataTable tabla = objVuelo.ObtenerCiudadesOrigen();

            cbxOrigen.DataSource = null;
            cbxOrigen.Items.Clear();

            cbxOrigen.DataSource = tabla;
            cbxOrigen.DisplayMember = "CIUDAD";
            cbxOrigen.ValueMember = "PAIS";
            cbxOrigen.SelectedIndex = -1;

        }

        private void CargarDestinos()
        {
            DataTable tabla = objVuelo.ObtenerCiudadesDestino();

            cbxDestino.DataSource = null;
            cbxDestino.Items.Clear();

            cbxDestino.DataSource = tabla;
            cbxDestino.DisplayMember = "CIUDAD";
            cbxDestino.ValueMember = "PAIS";
            cbxDestino.SelectedIndex = -1;
        }

        private void tabControl_PaginaPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_PaginaPrincipal.SelectedTab == tbpBuscarVuelos)
            {
                CargarOrigenes();
                CargarDestinos();
                dtmFechaViaje_Ida.MaxDate = new DateTime(DateTime.Today.Year + 1, 12, 31);
                dtmFechaViaje_Ida.MinDate = DateTime.Today;
                
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            string ciudadOrigen = cbxOrigen.Text;
            string paisOrigen = cbxOrigen.SelectedValue.ToString();

            string ciudadDestino = cbxDestino.Text;
            string paisDestino = cbxDestino.SelectedValue.ToString();

            DateTime fechaIda = dtmFechaViaje_Ida.Value;

            DataTable vuelos = objVuelo.ConsultarVuelosIda(ciudadOrigen, paisOrigen, ciudadDestino, paisDestino, fechaIda);

            if (vuelos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron vuelos disponibles para la selección.");
                return;
            }

            // Pasas los datos al siguiente form
            AcVuelosDisponibles vista = new AcVuelosDisponibles(this, objVuelo, objUsuarioRegistrado, vuelos);
            vista.Show();
            this.Hide();
        }

        private void btnIdaYVuelta_Click(object sender, EventArgs e)
        {
            AccionIda_Vuelta idaVuelta = new AccionIda_Vuelta(this, objVuelo, objUsuarioRegistrado);
            idaVuelta.Show();
            this.Hide();
        }

        private void btnCerrarSesion_Modificar1_Click(object sender, EventArgs e)
        {
            this.Hide();
            login.Show();
            
        }
    }
}
