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
    public partial class Uc_VuelosDisponibles_Reagendo : UserControl
    {
        Vuelo objVuelo;
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;
        Pasaje gestorPasaje;
        int idPasaje;

        DataTable vuelos;
        public Uc_VuelosDisponibles_Reagendo(PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelos, Pasaje gestorPasaje, int idPasaje)
        {
            InitializeComponent();
            this.principal = principal;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.vuelos = vuelos;
            this.gestorPasaje = gestorPasaje;
            this.idPasaje = idPasaje;
            CargarCategorias();
            MostrarVuelos(vuelos);
            this.Visible = true;
        }

        private void MostrarVuelos(DataTable vuelos)
        {
            flowVuelosDisponibles.Controls.Clear();
            
            int contador1 = 1;
            foreach (DataRow vuelo in vuelos.Rows)
            {
                var ucIda = new Uc_DatosVuelos_Reagendo(vuelo, contador1);
                ucIda.OnSeleccionarVuelo += SeleccionarVuelo;
                ucIda.Margin = new Padding(10);
                flowVuelosDisponibles.Controls.Add(ucIda);
                contador1++;
            }
        }

        private void SeleccionarVuelo(int idVuelo)
        {
            string medioPago = cbxMetodoPago.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(medioPago))
            {
                MessageBox.Show("Seleccione un método de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idUsuario = objUsuarioRegistrado.PKIdUsuario ?? 0;
            //string nombreCategoria = cbxCategoriaAsiento.Text;
            string nombreCategoria = cbxCategoriaAsiento.GetItemText(cbxCategoriaAsiento.SelectedItem);
            int idCategoria = gestorPasaje.ObtenerIdCategoria(nombreCategoria);

            int idVueloFactura = Convert.ToInt32(vuelos.Rows[0]["IDVUELO"]);

            var ucConfirmarReagendo = new Uc_ConfirmarReagendamiento(principal, this, gestorPasaje, objUsuarioRegistrado, idVueloFactura, idCategoria, idPasaje, idUsuario, medioPago);
            MostrarConfirmacion(ucConfirmarReagendo);
        }

        private void MostrarConfirmacion(UserControl uc)
        {
            this.Visible = false;
            principal.PanelContenedorMisVuelos.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            principal.PanelMisVuelos.Refresh();
        }

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

        private void pbx_VolverMisVuelos_Click(object sender, EventArgs e)
        {
            principal.PanelContenedorMisVuelos.Visible = false;

            this.Visible = false;
            principal.PanelMisVuelos.Visible = true;
            principal.PanelMisVuelos.BringToFront();

            principal.ActualizarPantalla();
        }
    }
}
