using Aeropuerto.ControlUsuario;
using Aeropuerto.logica;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Aeropuerto
{
    public partial class Uc_MisVuelos : UserControl
    {
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;
        Pasaje gestorPasaje = new Pasaje();

        public Uc_MisVuelos(PaginaPrincipal principal, UsuarioRegistrado objUsuarioRegistrado)
        {
            InitializeComponent();
            this.principal = principal;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            CargarVuelosUsuario();
        }

        private void CargarVuelosUsuario()
        {
            try
            {
                int idUsuario = objUsuarioRegistrado.PKIdUsuario ?? 0;
                DataTable vuelos = gestorPasaje.ObtenerVuelosUsuario(idUsuario);

                flp_MisVuelos.Controls.Clear();

                if (vuelos.Rows.Count == 0)
                {
                    Label lbl = new Label
                    {
                        Text = "No hay vuelos comprados",
                        Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                        ForeColor = Color.RoyalBlue,
                        AutoSize = true
                    };
                    flp_MisVuelos.Controls.Add(lbl);
                    return;
                }

                int contador = 1;
                foreach (DataRow row in vuelos.Rows)
                {
                    var ucVuelo = new Uc_DatosMisVuelos(row);
                    ucVuelo.Margin = new Padding(10);
                    ucVuelo.OnVerVuelo += VerVueloSeleccionado;
                    ucVuelo.Tag = contador++; // opcional: numerar
                    flp_MisVuelos.Controls.Add(ucVuelo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vuelos: " + ex.Message);
            }
        }

        private void VerVueloSeleccionado(int idPasaje)
        {
            // Mostrar contenedor, ocultar panel base, y asegurar Z-order
            principal.PanelContenedorMisVuelos.SuspendLayout();

            principal.PanelContenedorMisVuelos.Visible = true;
            principal.PanelContenedorMisVuelos.BringToFront();
            principal.PanelMisVuelos.Visible = false;

            // Cargar el UC dentro del contenedor
            var ucVerVuelo = new Uc_Informacion_Vuelo(principal, objUsuarioRegistrado, idPasaje, gestorPasaje);
            ucVerVuelo.Dock = DockStyle.Fill;

            principal.PanelContenedorMisVuelos.Controls.Add(ucVerVuelo);
            principal.PanelContenedorMisVuelos.ResumeLayout();
            principal.PanelMisVuelos.Refresh();
            principal.PanelContenedorMisVuelos.Refresh();
        }

       
    }
}



