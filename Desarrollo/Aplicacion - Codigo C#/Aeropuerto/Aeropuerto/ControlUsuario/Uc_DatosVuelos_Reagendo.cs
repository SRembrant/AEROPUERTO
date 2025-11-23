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
    public partial class Uc_DatosVuelos_Reagendo : UserControl
    {
        public event Action<int> OnSeleccionarVuelo;
        private int idVuelo;
        public Uc_DatosVuelos_Reagendo(DataRow vuelo, int numero)
        {
            InitializeComponent();
            CargarDatos(vuelo);
            lblTituloVuelo.Text = $"Vuelo {numero}";
        }

        private void CargarDatos(DataRow vuelo)
        {
            idVuelo = Convert.ToInt32(vuelo["IDVUELO"]);
            lbOrigen_VDisponibles.Text = vuelo["CIUORIGENVUELO"].ToString();
            lbDestino_VDisponibles.Text = vuelo["CIUDESTINOVUELO"].ToString();
            lbFecha_VDisponibles.Text = vuelo["HORASALIDAVUELO"].ToString();
            lbPrecio_VuelosDisponibles.Text = Convert.ToDecimal(vuelo["PRECIOBASEVUELO"]).ToString("N0") + " COP";
            lbOrigen_Avr_VDisponibles.Text = vuelo["CIUORIGENVUELO"].ToString().Substring(0, 3).ToUpper();
            lbDestino_Avr_VDisponibles.Text = vuelo["CIUDESTINOVUELO"].ToString().Substring(0, 3).ToUpper();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OnSeleccionarVuelo?.Invoke(idVuelo);
        }

    }
}
