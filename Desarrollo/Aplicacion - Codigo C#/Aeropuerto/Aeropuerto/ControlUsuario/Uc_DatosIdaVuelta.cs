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
    public partial class Uc_DatosIdaVuelta : UserControl
    {
        public event Action<int, int> OnSeleccionarParVuelos;
        private int idVueloIda;
        private int idVueloRegreso;

        public Uc_DatosIdaVuelta(DataRow vueloIda, DataRow vueloRegreso, int numero)
        {
            InitializeComponent();
            CargarDatos(vueloIda, vueloRegreso);
            lblTituloVuelo.Text = $"Vuelo {numero}";
        }

        private void CargarDatos(DataRow ida, DataRow regreso)
        {
            idVueloIda = Convert.ToInt32(ida["IDVUELO"]);
            idVueloRegreso = Convert.ToInt32(regreso["IDVUELO"]);

            lbOrigen_VDisponibles.Text = ida["CIUORIGENVUELO"].ToString();
            lbDestino_VDisponibles.Text = ida["CIUDESTINOVUELO"].ToString();
            lbFecha_VDisponibles.Text = ida["HORASALIDAVUELO"].ToString();
            lbPrecio_VuelosDisponibles.Text = Convert.ToDecimal(ida["PRECIOBASEVUELO"]).ToString("N0") + " COP";
            lbOrigen_Avr_VDisponibles.Text = ida["CIUORIGENVUELO"].ToString().Substring(0, 3).ToUpper();
            lbDestino_Avr_VDisponibles.Text = ida["CIUDESTINOVUELO"].ToString().Substring(0, 3).ToUpper();

            lbOrigen_VDisponibles_Regreso.Text = regreso["CIUORIGENVUELO"].ToString();
            lbDestino_VDisponibles_Regreso.Text = regreso["CIUDESTINOVUELO"].ToString();
            lbFecha_VDisponibles_Regreso.Text = regreso["HORASALIDAVUELO"].ToString();
            lbPrecio_VDisponibles_Regreso.Text = Convert.ToDecimal(regreso["PRECIOBASEVUELO"]).ToString("N0") + " COP";
            lbOrigen_Avr_VDisponibles_Regreso.Text = regreso["CIUORIGENVUELO"].ToString().Substring(0, 3).ToUpper();
            lbDestino_Avr_VDisponibles_Regreso.Text = regreso["CIUDESTINOVUELO"].ToString().Substring(0, 3).ToUpper();

        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            OnSeleccionarParVuelos?.Invoke(idVueloIda, idVueloRegreso);
        }
    }

}
