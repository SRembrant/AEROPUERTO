using Aeropuerto.accesoDatos; //importo la capa de acceso a datos 
using Aeropuerto.utilidades;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types; //agregada 
using System;
using System.Collections.Generic;
using System.Data; //importo la libreria para manejo de datos
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeropuerto.logica
{
    public class Vuelo
    {
        public int IdVuelo { get; set; }
        public string CiuOrigenVuelo { get; set; }
        public string PaisOrigenVuelo { get; set; }
        public string CiuDestinoVuelo { get; set; }
        public string PaisDestinoVuelo { get; set; }
        public decimal PrecioBaseVuelo { get; set; }
        public string EstadoVuelo { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public int IdZEmbarque { get; set; }
        public int IdPuerta { get; set; }
        public int IdAvion { get; set; }

        Datos datos = new Datos();

        public DataTable ObtenerCiudadesOrigen()
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_resultado", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
            };

            return datos.EjecutarProcedureCursor(
                "MONITOREAR_VUELOS.OBTENER_CIUDADES_PAISES_ORIGEN",
                parametros
            );
        }

        public DataTable ObtenerCiudadesDestino()
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_resultado", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.Output
                    }
            };

            return datos.EjecutarProcedureCursor(
                "MONITOREAR_VUELOS.OBTENER_CIUDADES_PAISES_DESTINO",
                parametros
            );
        }

        public DataTable ConsultarVuelosIda(string ciudadOrigen, string paisOrigen,
                                   string ciudadDestino, string paisDestino,
                                   DateTime fecha)
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_ciuOrigen", ciudadOrigen),
                new OracleParameter("p_paisOrigen", paisOrigen),
                new OracleParameter("p_ciuDestino", ciudadDestino),
                new OracleParameter("p_paisDestino", paisDestino),
                new OracleParameter("p_fecha", fecha),
                new OracleParameter("p_resultado", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return datos.EjecutarProcedureCursor(
                "MONITOREAR_VUELOS.CONSULTAR_VUELOS_POR_ORIGEN_DESTINO_FECHA",
                parametros
            );
        }


        public (DataTable VuelosIda, DataTable VuelosVuelta) ConsultarVuelosIdaVuelta(
                    string ciudadOrigen, string paisOrigen,
                    string ciudadDestino, string paisDestino,
                    DateTime fechaIda, DateTime fechaRegreso)
        {
            DataTable vuelosIda = new DataTable();
            DataTable vuelosVuelta = new DataTable();

            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_ciuOrigen", ciudadOrigen),
                new OracleParameter("p_paisOrigen", paisOrigen),
                new OracleParameter("p_ciuDestino", ciudadDestino),
                new OracleParameter("p_paisDestino", paisDestino),
                new OracleParameter("p_fechaIda", fechaIda),
                new OracleParameter("p_fechaRegreso", fechaRegreso),

                new OracleParameter("p_resultadoIda", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                },
                new OracleParameter("p_resultadoVuelta", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            // Ejecuta el procedure y obtiene ambos cursores en un DataSet
            DataSet ds = datos.EjecutarProcedureDataSet(
                "MONITOREAR_VUELOS.VUELOS_IDA_Y_VUELTA",
                parametros
            );

            if (ds != null && ds.Tables.Count >= 2)
            {
                vuelosIda = ds.Tables[0];
                vuelosVuelta = ds.Tables[1];
            }

            return (vuelosIda, vuelosVuelta);
        }

        public DataTable ObtenerVuelosDisponibles()
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_resultado", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return datos.EjecutarProcedureCursor(
                "MONITOREAR_VUELOS.OBTENER_VUELOS_DISPONIBLES",
                parametros
            );
        }


    }
}
