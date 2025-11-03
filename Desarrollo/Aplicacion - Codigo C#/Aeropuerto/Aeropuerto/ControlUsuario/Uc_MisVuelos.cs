/*using Aeropuerto.logica;
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

        public int NumeroVuelo { get; private set; }

        // Constructor principal
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

                // Cantidad total
                int cantidad = gestorPasaje.ObtenerCantidadPasajesUsuario(idUsuario);
                //lblCantidadVuelos.Text = $"Tienes {cantidad} vuelo(s) reservado(s)";

                // Vuelos reales
                DataTable vuelos = gestorPasaje.ObtenerVuelosUsuario(idUsuario);
                flp_MisVuelos.Controls.Clear();

                if (vuelos.Rows.Count == 0)
                {
                    Label lbl = new Label
                    {
                        Text = "No hay vuelos comprados",
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        ForeColor = Color.RoyalBlue,
                        AutoSize = true
                    };
                    flp_MisVuelos.Controls.Add(lbl);
                    return;
                }

                foreach (DataRow row in vuelos.Rows)
                {
                    var panel = CrearPanelVuelo(row);
                    flp_MisVuelos.Controls.Add(panel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los vuelos: " + ex.Message);
            }
        }

        private Panel CrearPanelVuelo(DataRow row)
        {
            // Panel principal
            Panel pnlVuelo = new Panel
            {
                Width = 620,
                Height = 120,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            // Código del vuelo
            Label lblCodigo = new Label
            {
                Text = $"Vuelo #{row["IDVUELO"]}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            pnlVuelo.Controls.Add(lblCodigo);

            // Aerolínea
            Label lblAerolinea = new Label
            {
                Text = row["NOMBREAEROLINEA"].ToString(),
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.DimGray,
                Location = new Point(10, 35),
                AutoSize = true
            };
            pnlVuelo.Controls.Add(lblAerolinea);

            // Origen y destino
            Label lblRuta = new Label
            {
                Text = $"{row["ORIGEN"]} ➜ {row["DESTINO"]}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(10, 60),
                AutoSize = true
            };
            pnlVuelo.Controls.Add(lblRuta);

            // Fecha
            Label lblFecha = new Label
            {
                Text = $"Fecha: {Convert.ToDateTime(row["FECHA"]).ToString("dd MMM yyyy")}",
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 85),
                AutoSize = true
            };
            pnlVuelo.Controls.Add(lblFecha);

            // Monto factura (si existe)
            if (row["MONTOFACTURA"] != DBNull.Value)
            {
                Label lblMonto = new Label
                {
                    Text = $"Factura: ${Convert.ToDecimal(row["MONTOFACTURA"]).ToString("N0")}",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.DarkGreen,
                    Location = new Point(250, 85),
                    AutoSize = true
                };
                pnlVuelo.Controls.Add(lblMonto);
            }

            // Botón cancelar
            Button btnVer = new Button
            {
                Text = "Ver",
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(500, 40),
                Size = new Size(90, 30)
            };
            btnVer.FlatAppearance.BorderSize = 0;

            // Acción al presionar cancelar
            btnVer.Click += (s, e) =>
            {
                int idPasaje = Convert.ToInt32(row["IDPASAJE"]); // asegúrate de incluirlo en tu SELECT

                Uc_Informacion_Vuelo ucVerVuelo = new Uc_Informacion_Vuelo(principal, objUsuarioRegistrado, idPasaje, gestorPasaje);
                principal.PanelMisVuelos.Controls.Clear();
                principal.PanelMisVuelos.Controls.Add(ucVerVuelo);
                ucVerVuelo.Dock = DockStyle.Fill;
            };
            pnlVuelo.Controls.Add(btnVer);

            return pnlVuelo;
        }


        
    }

}*/

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
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
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
            var ucVerVuelo = new Uc_Informacion_Vuelo(principal, objUsuarioRegistrado, idPasaje, gestorPasaje);
            principal.PanelMisVuelos.Controls.Clear();
            principal.PanelMisVuelos.Controls.Add(ucVerVuelo);
            ucVerVuelo.Dock = DockStyle.Fill;
        }
    }
}



