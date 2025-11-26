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
    public partial class Uc_AcVuelosDisponibles : UserControl
    {
        Vuelo objVuelo;
        PaginaPrincipal principal;
        UsuarioRegistrado objUsuarioRegistrado;

        DataTable vuelosIda;
        DataTable vuelosRegreso;

        int cantidadPasajeros;

        public Uc_AcVuelosDisponibles(int cantidadPasajeros, PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelosIda)
        {
            InitializeComponent();
            this.cantidadPasajeros = cantidadPasajeros;
            this.principal = principal;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.vuelosIda = vuelosIda;
            this.vuelosRegreso = null;
            MostrarVuelos(vuelosIda, null);
            this.Visible = true;
        }

        //Constructor ida y vuelta
        public Uc_AcVuelosDisponibles(int cantidadPasajeros, PaginaPrincipal principal, Vuelo objVuelo, UsuarioRegistrado objUsuarioRegistrado, DataTable vuelosIda, DataTable vuelosRegreso)
        {
            InitializeComponent();
            this.cantidadPasajeros = cantidadPasajeros;
            this.principal = principal;
            this.objVuelo = objVuelo;
            this.objUsuarioRegistrado = objUsuarioRegistrado;
            this.vuelosIda = vuelosIda;
            this.vuelosRegreso = vuelosRegreso;
            MostrarVuelos(vuelosIda, vuelosRegreso);
            this.Visible = true;
        }

        private void MostrarVuelos(DataTable vuelosIda, DataTable vuelosVuelta)
        {
            

            flowVuelosDisponibles.Controls.Clear();
            
            if (vuelosVuelta == null || vuelosVuelta.Rows.Count == 0)
            {
                // Solo ida
                int contador1= 1;
                foreach (DataRow vuelo in vuelosIda.Rows)
                {
                    var ucIda = new Uc_DatosIda(vuelo, contador1);
                    ucIda.OnSeleccionarVuelo += SeleccionarVuelo;
                    ucIda.Margin = new Padding(10);
                    flowVuelosDisponibles.Controls.Add(ucIda);
                    contador1++;
                }
            }
            else
            {
                // Ida y vuelta
                int contador2 = 1;
                foreach (DataRow vueloIda in vuelosIda.Rows)
                {
                    foreach (DataRow vueloRegreso in vuelosVuelta.Rows)
                    {
                        var ucIdaVuelta = new Uc_DatosIdaVuelta(vueloIda, vueloRegreso, contador2);
                        ucIdaVuelta.OnSeleccionarParVuelos += SeleccionarParVuelos;
                        ucIdaVuelta.Margin = new Padding(10);
                        flowVuelosDisponibles.Controls.Add(ucIdaVuelta);
                        contador2++;
                    }
                }
            }
        }

        private void SeleccionarVuelo(int idVuelo)
        {
            var ucPasajeros = new Uc_Informacion_Pasajero(principal, this, objVuelo, objUsuarioRegistrado,
                vuelosIda.Select($"IDVUELO = {idVuelo}").CopyToDataTable(),
                null, cantidadPasajeros);

            MostrarPasajeros(ucPasajeros);
        }

        private void SeleccionarParVuelos(int idIda, int idVuelta)
        {
            var ucPasajeros = new Uc_Informacion_Pasajero(principal, this, objVuelo, objUsuarioRegistrado,
                vuelosIda.Select($"IDVUELO = {idIda}").CopyToDataTable(),
                vuelosRegreso.Select($"IDVUELO = {idVuelta}").CopyToDataTable(),
                cantidadPasajeros);

            MostrarPasajeros(ucPasajeros);
        }


        private void MostrarPasajeros(UserControl uc)
        {
            this.Visible = false;
            principal.PanelContenedorBuscarVuelos.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            principal.PanelBuscarVuelos.Refresh();
        }



        private void px_RegresarInicio_VerVuelosDisponibles_Click(object sender, EventArgs e)
        {
            principal.PanelContenedorBuscarVuelos.Visible = false;

            this.Visible = false;
            principal.PanelBuscarVuelos.Visible = true;
            principal.PanelBuscarVuelos.BringToFront();

            principal.ActualizarPantalla();
        }
    }
}
