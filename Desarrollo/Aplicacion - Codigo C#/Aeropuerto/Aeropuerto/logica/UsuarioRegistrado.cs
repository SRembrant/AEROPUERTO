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
using System.Windows.Forms;


namespace Aeropuerto.logica
{
    public class UsuarioRegistrado
    {
        //atributos con setters y gettes
        public int? PKIdUsuario { get; set; }
        public int? DocIdUsuario { get; set; }
        public string TipoIdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string GeneroUsuario { get; set; }
        public DateTime FechaNacUsuario { get; set; }
        public string NacionalidadUsuario { get; set; }
        public string UsuarioAcceso { get; set; }
        public string ContraseniaUsuario { get; set; }
        public string DireccionUsuario { get; set; }
        public string DetalleUsuario { get; set; }
        public long? TelefonoUsuario { get; set; }

        //Creo un objeto de la clase Datos*
        Datos datos = new Datos();

        public string RegistrarUsuario(int? docId, string tipoId, string nombre, string apellido, string correo, string genero, DateTime fechaNac,
                                       string nacionalidad, string nombreUsuario, string contrasenia, string direccion, long? telefono, string detalle)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_docIdUsuario", docId),
                    new OracleParameter("p_tipoIdUsuario", tipoId),
                    new OracleParameter("p_nombreUsuario", nombre),
                    new OracleParameter("p_apellidoUsuario", apellido),
                    new OracleParameter("p_correoUsuario", correo),
                    new OracleParameter("p_generoUsuario", genero),
                    new OracleParameter("p_fechaNacUsuario", fechaNac),
                    new OracleParameter("p_nacionalidadUsuario", nacionalidad),
                    new OracleParameter("p_usuarioAcceso", nombreUsuario),
                    new OracleParameter("p_contraseniaUsuario", contrasenia),
                    new OracleParameter("p_direccionUsuario", direccion),
                    new OracleParameter("p_observacionUsuario", detalle),
                    new OracleParameter("p_telefonoUsuario", OracleDbType.Int64) { Value = telefono },
                    new OracleParameter("p_bandera", OracleDbType.Int32) { Direction = System.Data.ParameterDirection.Output }
                };

                datos.EjecutarProcedimiento("GESTION_USUARIO.INSERTAR_USUARIO_NUEVO", parametros);

                var valorSalida = parametros[parametros.Length - 1].Value;
                int bandera = 0;

                if (valorSalida is Oracle.ManagedDataAccess.Types.OracleDecimal oracleDecimal)
                {
                    bandera = oracleDecimal.ToInt32();
                }

                if (bandera == 1)
                    return "Usuario registrado correctamente.";
                else
                    return "Error desconocido al registrar.";

            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (Exception ex)
            {
                return "Error general en la aplicación: " + ex.Message;
            }
        }


        public string ValidarCredenciales(string usuarioAcceso, string contrasenia)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_usuarioAcceso", usuarioAcceso),
                    new OracleParameter("p_contrasenia", contrasenia)
                };

                var resultado = datos.EjecutarFuncion("GESTION_USUARIO.VALIDAR_CREDENCIALES", parametros, OracleDbType.Int32);
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

                if (valor == 1)
                    return "Acceso permitido. Bienvenido al sistema.";
                else
                    return "Usuario o contraseña incorrectos.";


            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (Exception ex)
            {
                return "Error general al validar credenciales: " + ex.Message;
            }
        }

        public string ModificarUsuario(int? PKusuario, int? antiguoDocId, int? nuevoDocId, string tipoId, string nombre, string apellido,
                               string correo, string genero, DateTime fechaNac, string nacionalidad,
                               string usuarioAcceso, string contrasenia, string direccion,
                               string observacion, long? telefono)
        {
            try
            {
                OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_IdUsuario", PKIdUsuario),
                    new OracleParameter("p_antiguoDocIdUsuario", antiguoDocId),
                    new OracleParameter("p_nuevoDocIdUsuario", nuevoDocId),
                    new OracleParameter("p_tipoIdUsuario", tipoId),
                    new OracleParameter("p_nombreUsuario", nombre),
                    new OracleParameter("p_apellidoUsuario", apellido),
                    new OracleParameter("p_correoUsuario", correo),
                    new OracleParameter("p_generoUsuario", genero),
                    new OracleParameter("p_fechaNacUsuario", fechaNac),
                    new OracleParameter("p_nacionalidadUsuario", nacionalidad),
                    new OracleParameter("p_usuarioAcceso", usuarioAcceso),
                    new OracleParameter("p_contraseniaUsuario", contrasenia),
                    new OracleParameter("p_direccionUsuario", direccion),
                    new OracleParameter("p_observacionUsuario", observacion),
                    new OracleParameter("p_telefonoUsuario", OracleDbType.Int64) { Value = telefono }
                };

                // Llamar al procedimiento del paquete
                datos.EjecutarProcedimiento("GESTION_USUARIO.MODIFICAR_USUARIO", parametros);
                int idActual = ObtenerIdPorNombreUsuario(usuarioAcceso);
                CargarDatosPorID(idActual);
                return "Los datos del usuario fueron modificados correctamente.";
            }
            catch (OracleException ex)
            {
                string mensaje = ManejadorErroresOracle.ObtenerMensaje(ex);
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (Exception ex)
            {
                return "Error general al modificar usuario: " + ex.Message;
            }
        }

        public int ObtenerIdPorNombreUsuario(string UsuarioAcceso)
        {
            OracleParameter[] parametros = new OracleParameter[]
                {
                    new OracleParameter("p_nombreUsuario", UsuarioAcceso)
                };

            var resultado = datos.EjecutarFuncion("GESTION_USUARIO.OBTENER_ID_PASAJERO_POR_NOMBRE_USUARIO", parametros, OracleDbType.Int32);
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

        public bool CargarDatosPorID(int idUsuario)
        {
            OracleParameter[] parametros = new OracleParameter[]
            {
                new OracleParameter("p_idUsuario", idUsuario),
                new OracleParameter("p_resultado", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            DataTable dt = datos.EjecutarProcedureCursor(
                                "GESTION_USUARIO.CONSULTAR_USUARIO_POR_ID", parametros);

            if (dt.Rows.Count == 0)
                return false;

            DataRow row = dt.Rows[0];

            this.PKIdUsuario = Convert.ToInt32(row["IDUSUARIO"]);
            this.DocIdUsuario = Convert.ToInt32(row["DOCIDUSUARIO"]);
            this.TipoIdUsuario = row["TIPOIDUSUARIO"].ToString();
            this.NombreUsuario = row["NOMBREUSUARIO"].ToString();
            this.ApellidoUsuario = row["APELLIDOUSUARIO"].ToString();
            this.CorreoUsuario = row["CORREOUSUARIO"].ToString();
            this.GeneroUsuario = row["GENEROUSUARIO"].ToString();
            this.FechaNacUsuario = Convert.ToDateTime(row["FECHANACUSUARIO"]);
            this.NacionalidadUsuario = row["NACIONALIDADUSUARIO"].ToString();
            this.UsuarioAcceso = row["USUARIOACCESO"].ToString();
            this.ContraseniaUsuario = row["CONTRASENIAUSUARIO"].ToString();
            this.DireccionUsuario = row["DIRECCIONUSUARIO"].ToString();
            this.DetalleUsuario = row["OBSERVACIONUSUARIO"].ToString();
            this.TelefonoUsuario = Convert.ToInt64(row["TELEFONOUSUARIO"]);

            return true;
        }



    }
}
