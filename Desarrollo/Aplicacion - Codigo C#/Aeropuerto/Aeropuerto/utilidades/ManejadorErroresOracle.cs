using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace Aeropuerto.utilidades
{
    internal class ManejadorErroresOracle
    {
        public static string ObtenerMensaje(OracleException ex)
        {
            string mensaje = Traducir(ex);

            // Limpiar trazas de ORA
            if (mensaje.Contains("ORA-"))
            {
                int index = mensaje.IndexOf("ORA-");
                if (index >= 0)
                    mensaje = mensaje.Substring(0, index).Trim();
            }

            return mensaje.Replace("\r", "").Replace("\n", " ").Trim();
        }



        public static string Traducir(OracleException ex)
        {
            int errorNumber = Math.Abs(ex.Number);

            switch (errorNumber)
            {
                // 🔹 Errores comunes
                case 1: return "Ya existe un registro con esos datos.";
                case 2290: return "Se violó una restricción CHECK. Revisa los valores ingresados.";
                case 1400: return "Un campo obligatorio está vacío.";
                case 1017: return "Usuario o contraseña incorrectos al conectar con la base de datos.";
                case 12560: return "Error de conexión con Oracle. Verifica el servicio.";
                case 2291: return "Violación de clave foránea (referencia inexistente).";
                //case 50048: return "Muchos campos con valores erroneos, inténtelo de nuevo";

                // 🔹 Gestión Usuario
                case 20001: return "El usuario con ese ID, correo o nombre ya existe.";
                case 20002: return "Error de tipo de dato o valor nulo en campo obligatorio.";
                case 20003: return "Violación de restricción CHECK (valores inválidos).";
                case 20010: return "No existe ningún usuario con ese nombre.";
                case 20011: return "Existen múltiples usuarios con ese nombre.";
                case 20012: return "Error al buscar usuario";
                case 20030: return "El usuario no existe.";
                case 20031: return "El documento ya está en uso.";
                case 20032: return "No se pudo actualizar el usuario.";
                case 20034: return "Error en tipo o tamaño de valor.";
                case 20005: return "El correo ingresado ya existe en el sistema.";
                case 20006: return "El nombre de usuario ingresado ya está registrado.";
                case 20007: return "El documento de identificacion ya existe en el sistema.";
                case 20040: return "Usuario no encontrado.";
                case 20041: return "Más de un usuario tiene el mismo nombre de usuario.";
                case 20042: return "Error inesperado al validar credenciales.";

                case 20000: return "El documento ya está en uso.";
                case 20100: return "No se pudo registrar el usuario.";
                case 20200: return "'El correo ingresado ya existe en el sistema.";
                case 20300: return "El nombre de usuario ingresado ya está registrado.";
                case 20400: return "El documento de identificacion ya existe en el sistema.";
                


                // 🔹 Triggers validaciones
                case 20050: return "El nombre de usuario ya existe.";
                case 20060: return "La contraseña no puede tener espacios al inicio o al final.";
                case 20061: return "La contraseña debe tener al menos 8 caracteres.";
                case 20062: return "La contraseña no puede exceder 64 caracteres.";
                case 20070: return "El correo no debe tener espacios al inicio o al final.";
                case 20071: return "El correo no tiene un formato válido.";
                case 20072: return "El correo no debe tener espacios al inicio o al final.";
                case 20073: return "El correo no tiene un formato válido.";
                case 20080: return "El usuario debe ser mayor de 18 años.";
                case 20081: return "La edad ingresada excede el máximo permitido (100 años).";
                case 20500: return "El texto ingresado contiene una palabra SQL reservada o peligrosa.";
                case 20600: return "El número de teléfono no puede ser nulo.";
                case 20601: return "El número de teléfono debe contener exactamente 10 dígitos numéricos.";
                case 20092: return "La dirección contiene caracteres no permitidos. Solo se permiten letras, números, espacios, - # y ''.";
                case 20093: return "La observación contiene caracteres no permitidos. Solo se permiten letras, números, espacios, - # y ''.";

                // 🔹 Gestión Pasajes
                case 20014: return "No hay asientos disponibles en la categoría seleccionada.";
                case 20015: return "Se encontró más de un asiento cuando solo se esperaba uno.";
                case 20016: return "Error de conversión al asignar número de asiento.";
                case 20017: return "Error inesperado al obtener asiento.";
                case 20018: return "Parámetros nulos en el cálculo del precio.";
                case 20019: return "Valores negativos no válidos.";
                case 20020: return "Error al calcular el precio total.";
                case 20021: return "Error en tipo o tamaño de valor.";
                case 20102: return "Error de tipo de dato o valor nulo en campo obligatorio.";
                case 20105: return "El correo ingresado ya existe en el sistema.";
                case 20103: return "Violación de restricción CHECK (valores inválidos).";
                case 20013: return "Error desconocido al guardar pasajero";
                case 20023: return "Otro usuario está reservando asientos de esta categoría. Intente más tarde.";
                case 20025: return "Error inesperado al reservar el pasaje.";
                case 20027: return "No se encontraron registros de compra o vuelo.";
                case 20035: return "Error en tipo o tamaño de valor.";
                case 20028: return "Registro duplicado al generar la factura.";
                case 20029: return "Violación de integridad referencial en compra o factura.";
                case 20033: return "Error inesperado al asociar compras a factura.";
                case 20036: return "No se encontró la factura generada.";
                case 20037: return "Error inesperado al recuperar la factura.";
                case 20008: return "Pasaje no encontrado.";
                case 20101: return "El pasaje ya se encuentra inactivo.";
                case 20009: return "Error inesperado al cancelar el pasaje.";
                case 20052: return "No existe la categoría con ese nombre.";
                case 20053: return "Existen múltiples categorías con ese nombre.";
                case 20054: return "Error inesperado al obtener la categoría.";
                case 20055: return "No existe la categoría con ese nombre";
                case 20056: return "Error inesperado al obtener el sobrecosto.";
                
                // 🔹 Por defecto
                default:
                    return "Ocurrió un error inesperado. Intente nuevamente o contacte al administrador.";
            }
        }
    }
}
