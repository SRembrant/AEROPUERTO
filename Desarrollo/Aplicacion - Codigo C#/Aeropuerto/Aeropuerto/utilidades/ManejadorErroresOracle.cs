using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeropuerto.utilidades
{
    internal class ManejadorErroresOracle
    {
        public static string Traducir(OracleException ex)
        {
            // Errores personalizados con RAISE_APPLICATION_ERROR
            if (ex.Number >= 20000 && ex.Number < 21000)
            {
                return ex.Message.Replace($"ORA-{ex.Number}:", "").Trim();
            }

            // Errores comunes de Oracle
            switch (ex.Number)
            {
                case 1:
                    return "Ya existe un registro con esos datos (violación de clave única).";
                case 2290:
                    return "Se violó una restricción CHECK. Revisa los valores ingresados.";
                case 1400:
                    return "Un campo obligatorio está vacío (violación de NOT NULL).";
                case 1017:
                    return "Usuario o contraseña incorrectos al conectar con la base de datos.";
                case 12560:
                    return "Error de conexión con Oracle. Verifica el servicio.";

                // Errores de paquete GESTION_USUARIO
                case -20001:
                    return "El usuario con ese ID, correo o usuario ya existe.";
                case -20002:
                    return "Error de tipo de dato o valor nulo en campo obligatorio.";
                case -20003:
                    return "Violación de restricción CHECK (valores inválidos).";
                case -20004:
                    return "Error desconocido al insertar usuario.";

                case -20010:
                    return "No existe ningún usuario con ese nombre de usuario.";
                case -20011:
                    return "Existen múltiples usuarios con ese nombre de usuario.";
                case -20012:
                    return "Error al buscar usuario.";

                case -20021:
                    return "No existe el usuario a eliminar.";
                case -20022:
                    return "Error al eliminar la cuenta.";
                case -20024:
                    return "El usuario ya se encuentra inactivo.";

                case -20030:
                    return "El usuario no existe.";
                case -20031:
                    return "El nuevo documento ya está en uso.";
                case -20032:
                    return "No se pudo actualizar el usuario.";
                case -20033:
                    return "Valor duplicado en un campo único.";
                case -20034:
                    return "Error en tipo o tamaño del valor ingresado.";
                case -20035:
                    return "Error al modificar el usuario.";

                case -20040:
                    return "No se encontró al usuario ingresado.";
                case -20041:
                    return "Más de un usuario tiene el mismo nombre de usuario.";
                case -20042:
                    return "Error inesperado al validar credenciales.";

                // Triggers
                case -20050:
                    return "El nombre de usuario no puede tener espacios al inicio o al final.";
                case -20051:
                    return "El nombre de usuario ya está en uso.";

                case -20060:
                    return "La contraseña no puede tener espacios al inicio o al final.";
                case -20061:
                    return "La contraseña debe tener al menos 8 caracteres.";
                case -20062:
                    return "La contraseña no puede exceder 64 caracteres.";

                case -20070:
                    return "El correo no debe tener espacios al inicio o al final.";
                /*case -20071:
                    return "El correo electrónico no tiene un formato válido.";*/

                case -20080:
                    return "El usuario debe ser mayor de 18 años.";

                default:
                    return $"Error inesperado de Oracle ({ex.Number}): {ex.Message}";
            }

        }
    }
}
