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
        }

        public string Nombre => txtNombre_DatosPasajeros.Text;
        public string Apellido => txtApellido_DatosPasajero.Text;
        public string Correo => txtCorreo_DatosPasajero.Text;
        public string TipoIdentificacion => cbxTipoID_DatosPasajero.SelectedItem?.ToString() ?? "";
        public string Identificacion => txtIdentificacion_DatosPasajero.Text;
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
    }
}
