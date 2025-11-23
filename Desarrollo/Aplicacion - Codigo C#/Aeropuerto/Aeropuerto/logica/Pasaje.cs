using Aeropuerto.accesoDatos;
using Aeropuerto.utilidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Aeropuerto.logica
{
    public class Pasaje
    {
        Datos datos = new Datos(); // Tu clase de conexión centralizada a Oracle

        // Registrar pasajero
        public string InsertarPasajero(int idPasajero, string tipoId, string nombre, string apellido, string correo)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_idPasajero", idPasajero),
                    new OracleParameter("p_tipoIdPasajero", tipoId),
                    new OracleParameter("p_nombrePasajero", nombre),
                    new OracleParameter("p_apellidoPasajero", apellido),
                    new OracleParameter("p_correoPasajero", correo),
                    new OracleParameter("p_bandera", OracleDbType.Int32) { Direction = System.Data.ParameterDirection.Output }
                };

                datos.EjecutarProcedimiento("GESTION_PASAJES.INSERTAR_PASAJERO_NUEVO", parametros);

                var valorSalida = parametros[parametros.Length - 1].Value;
                int bandera = 0;

                if (valorSalida is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
                {
                    bandera = oracleDecimal.ToInt32();
                }

                if (bandera == 1)
                    return "Usuario insertado correctamente.";
                else
                    return "Error desconocido al insertar.";

            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            
        }

        // Reservar pasaje
        public string ReservarPasaje(int idUsuario, int idPasajero, int idVuelo, int idCategoria)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_idUsuario", idUsuario),
                    new OracleParameter("p_idPasajero", idPasajero),
                    new OracleParameter("p_idVuelo", idVuelo),
                    new OracleParameter("p_idCategoria", idCategoria),
                    new OracleParameter("p_idPasajeGenerado", OracleDbType.Int32) { Direction = ParameterDirection.Output },
                    new OracleParameter("p_idCompra", OracleDbType.Int32) { Direction = ParameterDirection.Output },
                    new OracleParameter("p_bandera", OracleDbType.Int32) { Direction = System.Data.ParameterDirection.Output }
                };

                datos.EjecutarProcedimiento("GESTION_PASAJES.RESERVAR_PASAJE", parametros);

                var valorSalida = parametros[parametros.Length - 1].Value;
                int bandera = 0;

                if (valorSalida is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
                {
                    bandera = oracleDecimal.ToInt32();
                }

                if (bandera == 1)
                {
                    return $"Reserva completada.\nPasaje #{parametros[4].Value}\nCompra #{parametros[5].Value}";
                }
                else
                    return "Error al reservar el pasaje.";
            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // Generar factura
        public DataTable RecuperarFactura(int idVuelo, int idUsuario, string medioPago)
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_idVuelo", idVuelo),
                new OracleParameter("p_idUsuario", idUsuario),
                new OracleParameter("p_medioPago", medioPago),
                new OracleParameter("p_infoFactura", OracleDbType.RefCursor) { Direction = ParameterDirection.Output }
            };

            return datos.EjecutarProcedureCursor("GESTION_PASAJES.RECUPERAR_DATOS_FACTURA", parametros);
        }

        // Cancelar pasaje
        public string CancelarPasaje(int idPasaje)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_idPasaje", idPasaje)
                };

                datos.EjecutarProcedimiento("GESTION_PASAJES.CANCELAR_PASAJE", parametros);
                return "Pasaje cancelado correctamente.";
            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // Obtener ID de Categoría por nombre
        public int ObtenerIdCategoria(string nombreCategoria)
        {
            OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_nombreCategoria", nombreCategoria)
                };

            var resultado = datos.EjecutarFuncion("GESTION_PASAJES.OBTENER_ID_CATEGORIA", parametros, OracleDbType.Int32);
            int valor = 0;

            // Conversión segura desde OracleDecimal → int
            if (resultado is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
            {
                valor = oracleDecimal.ToInt32();
            }
            else if (resultado != null)
            {
                valor = Convert.ToInt32(resultado);
            }

            if (valor > 0)
                return valor;
            else
                return -1;
        }

        public DataTable ObtenerCategoriasAsiento()
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
        new OracleParameter("p_resultado", OracleDbType.RefCursor)
        {
            Direction = ParameterDirection.Output
        }
            };

            return datos.EjecutarProcedureCursor(
                "GESTION_PASAJES.OBTENER_CATEGORIAS_ASIENTO_NOMBRES",
                parametros
            );
        }

        public DataTable ObtenerVuelosUsuario(int idUsuario)
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
        new OracleParameter("p_idUsuario", idUsuario),
        new OracleParameter("p_resultado", OracleDbType.RefCursor)
        {
            Direction = ParameterDirection.Output
        }
            };

            return datos.EjecutarProcedureCursor("GESTION_PASAJES.OBTENER_VUELOS_USUARIO", parametros);
        }

        public int ObtenerCantidadPasajesUsuario(int idUsuario)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_idUsuario", idUsuario),
                    //new OracleParameter("p_cantidad", OracleDbType.Int32)
                    new OracleParameter("p_cantidad", OracleDbType.Int32) { Direction = System.Data.ParameterDirection.Output }
                };

                datos.EjecutarProcedimiento("GESTION_PASAJES.OBTENER_CANTIDAD_PASAJES_USUARIO", parametros);
                
                var resultado = parametros[parametros.Length - 1].Value;
                int valor = 0;

                if (resultado is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
                {
                    valor = oracleDecimal.ToInt32();
                }
                else if (resultado != null)
                {
                    valor = Convert.ToInt32(resultado);
                }

                if (valor > 0)
                    return valor;
                else
                    return -1;
            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

        }

        public DataTable ObtenerInfoVueloPasaje(int idPasaje)
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_idPasaje", idPasaje),
                new OracleParameter("p_resultado", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return datos.EjecutarProcedureCursor("GESTION_PASAJES.OBTENER_INFO_VUELO_PASAJE", parametros);
        }

        // Obtener sobrecosto de una categoría por nombre
        public decimal ObtenerSobrecostoCategoria(string nombreCategoria)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
            new OracleParameter("p_nombreCategoria", nombreCategoria)
                };

                var resultado = datos.EjecutarFuncion(
                    "GESTION_PASAJES.OBTENER_SOBRECOSTO_CATEGORIA", 
                    parametros,
                    OracleDbType.Decimal
                );

                decimal valor = 0;

                if (resultado is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
                {
                    valor = oracleDecimal.Value;
                }
                else if (resultado != null)
                {
                    valor = Convert.ToDecimal(resultado);
                }

                return valor;
            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        //Reagendar
        public DataTable ObtenerInfoVueloReagendar(int idPasaje)
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_idPasaje", idPasaje),
                new OracleParameter("p_resultado", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return datos.EjecutarProcedureCursor("GESTION_PASAJES.OBTENER_INFO_VUELO_REAGENDAR", parametros);
        }


        public string ReagendarPasaje(int idVueloNuevo, int idCategoriaNueva, int idPasaje, string medioPagoNuevo)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_idVuelo_nuevo", idVueloNuevo),
                    new OracleParameter("p_idCategoria_nueva", idCategoriaNueva),
                    new OracleParameter("p_idPasaje", idPasaje),
                    new OracleParameter("p_medioPago_nuevo", medioPagoNuevo),

                    new OracleParameter("p_idFactura_nueva", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    },

                    new OracleParameter("p_bandera", OracleDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    }
                };

                datos.EjecutarProcedimiento("GESTION_PASAJES.REAGENDAR_PASAJE", parametros);

                var valorSalida = parametros[parametros.Length - 1].Value;
                int bandera = 0;

                if (valorSalida is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
                {
                    bandera = oracleDecimal.ToInt32();
                }

                if (bandera == 1)
                {
                    return $"Reagendamiento exitoso.";
                }
                else
                    return "No se pudo reagendar el pasaje. Revise nuevamente las reglas de reagendaimiento";

                /*int bandera = Convert.ToInt32(((Oracle.ManagedDataAccess.Types.OracleDecimal)parametros[5].Value).ToInt32());

                if (bandera == 1)
                {
                    return $"Reagendamiento exitoso.";
                }
                else
                    return "No se pudo reagendar el pasaje.";*/
            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable ObtenerVuelosDisponiblesParaReagendo(int idPasaje)
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_idPasaje", idPasaje),
                new OracleParameter("p_vuelos_cursor", OracleDbType.RefCursor)
                {
                    Direction = ParameterDirection.Output
                }
            };

            return datos.EjecutarProcedureCursor("GESTION_PASAJES.VUELOS_DISPONIBLES_REAGENDO", parametros);
        }



    }
}
