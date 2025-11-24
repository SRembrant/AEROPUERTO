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
            txtApellido_PgPerfil.Text = objUsuarioRegistrado.ApellidoUsuario;
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

            MostrarVuelos();


            //Mi Perfil
            pnlMiPerfil.Parent = tbpMiperfil;
            pnlContenedorPerfil.Parent = tbpMiperfil;

            pnlMiPerfil.Dock = DockStyle.Fill;
            pnlContenedorPerfil.Dock = DockStyle.Fill;

            pnlMiPerfil.Visible = true;
            pnlContenedorPerfil.Visible = false;

            this.Refresh();

            //Buscar Vuelos
            pnlBuscarVuelos.Parent = tbpBuscarVuelos;
            pnlContenedorBuscarVuelos.Parent = tbpBuscarVuelos;

            pnlBuscarVuelos.Dock = DockStyle.Fill;
            pnlContenedorBuscarVuelos.Dock = DockStyle.Fill;

            pnlBuscarVuelos.Visible = true;
            pnlContenedorBuscarVuelos.Visible = false;

            //Mis Vuelos
            pnlMisVuelos.Parent = tbpMisVuelos;
            pnlContenedorMisVuelos.Parent = tbpMisVuelos;

            pnlMisVuelos.Dock = DockStyle.Fill;
            pnlContenedorMisVuelos.Dock = DockStyle.Fill;

            pnlMisVuelos.Visible = true;
            pnlContenedorMisVuelos.Visible = false;

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
            txtApellido_PgPerfil.Text = objUsuarioRegistrado.ApellidoUsuario;
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
                this.pnlContenedorMisVuelos.Controls.Clear();
                this.pnlMisVuelos.Controls.Add(ucMisVuelos);
                ucMisVuelos.Dock = DockStyle.Fill;

            }

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (cbxOrigen.SelectedItem == null && cbxDestino.SelectedItem == null && string.IsNullOrEmpty(txtCantidadPasajeros.Text) || cbxOrigen.SelectedItem == null && cbxDestino.SelectedItem == null)
            {
                MessageBox.Show("Debe llenar los campos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string ciudadOrigen = cbxOrigen.Text;
                string paisOrigen = cbxOrigen.SelectedValue.ToString();

                string ciudadDestino = cbxDestino.Text;
                string paisDestino = cbxDestino.SelectedValue.ToString();

                DateTime fechaIda = dtmFechaViaje_Ida.Value;

                if (string.IsNullOrEmpty(txtCantidadPasajeros.Text))
                {
                    MessageBox.Show("Debe seleccionar la cantidad de pasajeros", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    int cantidadPasajeros = int.Parse(txtCantidadPasajeros.Text);

                    DataTable vuelos = objVuelo.ConsultarVuelosIda(ciudadOrigen, paisOrigen, ciudadDestino, paisDestino, fechaIda);

                    if (vuelos.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron vuelos disponibles para la selección.");
                        return;
                    }

                    tabControl_PaginaPrincipal.SelectedTab = tbpBuscarVuelos;

                    // Mostrar contenedor, ocultar panel base, y asegurar Z-order
                    pnlContenedorBuscarVuelos.SuspendLayout();

                    pnlContenedorBuscarVuelos.Visible = true;
                    pnlContenedorBuscarVuelos.BringToFront();
                    pnlBuscarVuelos.Visible = false;

                    // Cargar el UC dentro del contenedor
                    var ucVerVuelos = new Uc_AcVuelosDisponibles(cantidadPasajeros, this, objVuelo, objUsuarioRegistrado, vuelos);
                    ucVerVuelos.Dock = DockStyle.Fill;

                    pnlContenedorBuscarVuelos.Controls.Add(ucVerVuelos);
                    pnlContenedorBuscarVuelos.ResumeLayout();
                }

            }

        }

        private void btnIdaYVuelta_Click(object sender, EventArgs e)
        {
            // Mostrar contenedor, ocultar panel base, y asegurar Z-order
            pnlContenedorBuscarVuelos.SuspendLayout();

            pnlContenedorBuscarVuelos.Visible = true;
            pnlContenedorBuscarVuelos.BringToFront();
            pnlBuscarVuelos.Visible = false;

            // Cargar el UC dentro del contenedor
            var ucIdaVuelta = new Uc_AccionIda_Vuelta(this, objVuelo, objUsuarioRegistrado);
            ucIdaVuelta.Dock = DockStyle.Fill;

            pnlContenedorBuscarVuelos.Controls.Add(ucIdaVuelta);
            pnlContenedorBuscarVuelos.ResumeLayout();

        }

        //Monitorear Vuelos
        
        // Se obtiene la tabla con todos los vuelos
        private void MostrarVuelos()
        {
            DataTable vuelosDisponibles = objVuelo.ObtenerVuelosDisponibles();

            flowlp_vuelos_menuprincipal.Controls.Clear();
            int contador1 = 1;
            foreach (DataRow vuelo in vuelosDisponibles.Rows)
            {
                var ucIda = new Uc_DatosIda(vuelo, contador1);
                ucIda.Margin = new Padding(10);
                flowlp_vuelos_menuprincipal.Controls.Add(ucIda);
                contador1++;
            }

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

        public Guna.UI2.WinForms.Guna2Panel PanelContenedorBuscarVuelos
        {
            get { return pnlContenedorBuscarVuelos; }
        }

        public Guna.UI2.WinForms.Guna2Panel PanelContenedorMisVuelos
        {
            get { return pnlContenedorMisVuelos; }
        }

    }
}
