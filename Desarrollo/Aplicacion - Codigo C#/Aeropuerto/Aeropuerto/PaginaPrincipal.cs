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
        public Inicio_Sesion login;
        Pasaje gestorPasaje = new Pasaje();

        public PaginaPrincipal(Inicio_Sesion login, UsuarioRegistrado objUsuarioRegistrado)
        {
            InitializeComponent();
            this.login = login;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            txtTuNombre_PgPerfil.Text = objUsuarioRegistrado.NombreUsuario;
            txtApellido_PgPerfil.Text=objUsuarioRegistrado.ApellidoUsuario;
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


            pnlMiPerfil.Parent = tbpMiperfil;
            pnlContenedorPerfil.Parent = tbpMiperfil;

            pnlMiPerfil.Dock = DockStyle.Fill;
            pnlContenedorPerfil.Dock = DockStyle.Fill;

            pnlMiPerfil.Visible = true;
            pnlContenedorPerfil.Visible = false;

            this.Refresh();
        }

        private void btnModificarDatos_Pgperfil_Click(object sender, EventArgs e)
        {
            tabControl_PaginaPrincipal.SelectedTab = tbpMiperfil;

            // Mostrar contenedor, ocultar panel base, y asegurar Z-order
            pnlContenedorPerfil.SuspendLayout();

            pnlContenedorPerfil.Visible = true;
            pnlContenedorPerfil.BringToFront();    
            pnlMiPerfil.Visible = false;

            // Cargar el UC dentro del contenedor
            var ucModificar = new Uc_ModificarDatos(this, this.objUsuarioRegistrado);
            ucModificar.Dock = DockStyle.Fill;

            pnlContenedorPerfil.Controls.Add(ucModificar);
            pnlContenedorPerfil.ResumeLayout();
        }

        public void ActualizarPantalla()
        {
            txtTuNombre_PgPerfil.Text = objUsuarioRegistrado.NombreUsuario;
            txtApellido_PgPerfil.Text=objUsuarioRegistrado.ApellidoUsuario;
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

            if (tabControl_PaginaPrincipal.SelectedTab == tbpMisVuelos)
            {
                Uc_MisVuelos ucMisVuelos = new Uc_MisVuelos(this, objUsuarioRegistrado);
                this.pnlMisVuelos.Controls.Clear();
                this.pnlMisVuelos.Controls.Add(ucMisVuelos);
                ucMisVuelos.Dock = DockStyle.Fill;
            }

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            string ciudadOrigen = cbxOrigen.Text;
            string paisOrigen = cbxOrigen.SelectedValue.ToString();

            string ciudadDestino = cbxDestino.Text;
            string paisDestino = cbxDestino.SelectedValue.ToString();

            DateTime fechaIda = dtmFechaViaje_Ida.Value;

            int cantidadPasajeros = int.Parse(txtCantidadPasajeros.Text);

            DataTable vuelos = objVuelo.ConsultarVuelosIda(ciudadOrigen, paisOrigen, ciudadDestino, paisDestino, fechaIda);

            if (vuelos.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron vuelos disponibles para la selección.");
                return;
            }

            Uc_AcVuelosDisponibles ucVerVuelos = new Uc_AcVuelosDisponibles(cantidadPasajeros, this, objVuelo, objUsuarioRegistrado, vuelos);
            this.pnlBuscarVuelos.Controls.Clear();
            this.pnlBuscarVuelos.Controls.Add(ucVerVuelos);
            ucVerVuelos.Dock = DockStyle.Fill;

        }
        
        private void btnIdaYVuelta_Click(object sender, EventArgs e)
        {
            Uc_AccionIda_Vuelta ucIdaVuelta = new Uc_AccionIda_Vuelta(this, objVuelo, objUsuarioRegistrado);
            this.pnlBuscarVuelos.Controls.Clear();
            this.pnlBuscarVuelos.Controls.Add(ucIdaVuelta);
            ucIdaVuelta.Dock = DockStyle.Fill;

        }

        private void btnCerrarSesion_Modificar1_Click(object sender, EventArgs e)
        {
            this.Hide();
            login.Show();
            
        }

        

        public Guna.UI2.WinForms.Guna2Panel PanelMiPerfil
        {
            get { return pnlMiPerfil; }
        }

        public Guna.UI2.WinForms.Guna2Panel PanelBuscarVuelos
        {
            get { return pnlBuscarVuelos; }
        }

        public Guna.UI2.WinForms.Guna2Panel PanelMisVuelos
        {
            get { return pnlMisVuelos; }
        }

        public TabControl TabControl_PaginaPrincipal
        {
            get { return tabControl_PaginaPrincipal; }
        }

        public TabPage TbpMiPerfil
        {
            get { return tbpMiperfil; }
        }

        public Guna.UI2.WinForms.Guna2Panel PanelContenedorPerfil
        {
            get { return pnlContenedorPerfil; }
        }

        

    }
}
