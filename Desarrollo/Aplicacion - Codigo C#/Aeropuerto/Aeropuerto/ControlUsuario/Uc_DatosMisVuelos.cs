using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Aeropuerto.ControlUsuario
{
    public partial class Uc_DatosMisVuelos : UserControl
    {
        public event Action<int> OnVerVuelo; 
        private int idPasaje;

        public Uc_DatosMisVuelos(DataRow row)
        {
            InitializeComponent();
            CargarDatos(row);
        }

        private void CargarDatos(DataRow row)
        {
            idPasaje = Convert.ToInt32(row["IDPASAJE"]);

            lblNumeroVuelo_MisVuelos.Text = $"Vuelo #{row["IDVUELO"]}";
            lblAerolinea_MisVuelos.Text = row["NOMBREAEROLINEA"].ToString();
            lbOrigen_MisVuelos.Text = row["ORIGEN"].ToString();
            lbDestino_misVuelos.Text = row["DESTINO"].ToString();
            lbFechaVuelo_misVuelos.Text = $"Fecha: {Convert.ToDateTime(row["FECHA"]).ToString("dd MMM yyyy")}";
            lbOrigen_Avr_MisVuelos.Text = row["ORIGEN"].ToString().Substring(0, 3).ToUpper();
            lbDestino_Avr_MisVuelos.Text = row["DESTINO"].ToString().Substring(0, 3).ToUpper();
            
            btnVerVuelo.Click += (s, e) => OnVerVuelo?.Invoke(idPasaje);
        }
    }
}
