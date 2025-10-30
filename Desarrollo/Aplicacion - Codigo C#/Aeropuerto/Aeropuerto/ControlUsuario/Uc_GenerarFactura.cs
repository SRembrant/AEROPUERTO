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
    public partial class Uc_GenerarFactura : UserControl
    {
        PaginaPrincipal principal;
        Vuelo objVuelo;
        UsuarioRegistrado objUsuarioRegistrado;
        DataTable vuelosIda;
        DataTable vuelosRegreso;
        Pasaje gestorPasaje;
        int idVuelo;
        int idUsuario;
        string medioPago;
        public Uc_GenerarFactura(int idVuelo, int idUsuario, string medioPago, Pasaje gestorPasaje)
        {
            InitializeComponent();
            this.idVuelo = idVuelo;
            this.idUsuario = idUsuario;
            this.medioPago = medioPago;
            this.gestorPasaje = gestorPasaje;
        }

        private void lbGenerarFactura_Click(object sender, EventArgs e)
        {
            DataTable factura = gestorPasaje.RecuperarFactura(idVuelo, idUsuario, medioPago);

            if (factura.Rows.Count > 0)
            {
                var row = factura.Rows[0];
                string resultado = $"Factura #{row["IDFACTURA"]}\nMonto: {row["MONTOFACTURA"]}\nPago: {row["MEDIOPAGOFACTURA"]}";
                MessageBox.Show(resultado, "Cancelación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
