using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client; //libreria de conexion a oracle

namespace Aeropuerto.accesoDatos
{
    internal class Datos
    {
        string cadenaConexion = @"Data Source = localhost; User ID = AEROPUERTO1; Password=oracle";

        // Ejecutar un procedimiento almacenado (sin retorno, tipo DML)
        public void EjecutarProcedimiento(string nombreProcedimiento, OracleParameter[] parametros)
        {
            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            {
                OracleCommand cmd = new OracleCommand(nombreProcedimiento, conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;  

                if (parametros != null)
                {
                    foreach (OracleParameter param in parametros)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Ejecutar una función almacenada que devuelve un valor (por ejemplo, validar credenciales)
        public object EjecutarFuncion(string nombreFuncion, OracleParameter[] parametros, OracleDbType tipoRetorno)
        {
            object resultado;

            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            {
                conexion.Open();

                using (OracleCommand comando = new OracleCommand(nombreFuncion, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // parámetro de retorno
                    OracleParameter retorno = new OracleParameter("return_value", tipoRetorno);
                    retorno.Direction = ParameterDirection.ReturnValue;
                    comando.Parameters.Add(retorno);

                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }

                    comando.ExecuteNonQuery();
                    resultado = retorno.Value;
                }
            }

            return resultado;
        }

        public DataTable EjecutarProcedureCursor(string nombreProcedimiento, OracleParameter[] parametros)
        {
            DataTable tabla = new DataTable();

            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            {
                conexion.Open();

                using (OracleCommand comando = new OracleCommand(nombreProcedimiento, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.BindByName = true;

                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }

                    using (OracleDataAdapter adaptador = new OracleDataAdapter(comando))
                    {
                        adaptador.Fill(tabla);
                    }
                }
            }

            return tabla;
        }

        public DataSet EjecutarProcedureDataSet(string nombreProcedure, OracleParameter[] parametros)
        {
            DataSet ds = new DataSet();

            using (OracleConnection conexion = new OracleConnection(cadenaConexion))
            {
                conexion.Open();

                using (OracleCommand comando = new OracleCommand(nombreProcedure, conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.BindByName = true;

                    if (parametros != null)
                    {
                        comando.Parameters.AddRange(parametros);
                    }

                    using (OracleDataAdapter adaptador = new OracleDataAdapter(comando))
                    {
                        adaptador.Fill(ds); // Lee ambos cursores en DataSet.Tables[0] y DataSet.Tables[1]
                    }
                }
            }

            return ds;
        }


    }
}
