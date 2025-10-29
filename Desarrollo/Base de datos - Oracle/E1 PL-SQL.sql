--EPICA 1. FUNCIONES Y PROCEDIMIENTOS, ADEMAS DE ALGUNOS TRIGGERS

------------------------------------------------------------
-- PAQUETE: GESTION_USUARIO
-- Descripción: Agrupa todos los procedimientos y funciones
-- relacionadas con la gestión de usuarios registrados.
------------------------------------------------------------

-- ===============================================
-- Especificación del paquete (INTERFAZ PÚBLICA)
-- ===============================================
CREATE OR REPLACE PACKAGE GESTION_USUARIO AS

    -- Procedimiento para insertar un nuevo usuario
    PROCEDURE INSERTAR_USUARIO_NUEVO(
        p_docIdUsuario         IN USUARIOREGISTRADO.DOCIDUSUARIO%TYPE,
        p_tipoIdUsuario        IN USUARIOREGISTRADO.TIPOIDUSUARIO%TYPE,
        p_nombreUsuario        IN USUARIOREGISTRADO.NOMBREUSUARIO%TYPE,
        p_apellidoUsuario      IN USUARIOREGISTRADO.APELLIDOUSUARIO%TYPE,
        p_correoUsuario        IN USUARIOREGISTRADO.CORREOUSUARIO%TYPE,
        p_generoUsuario        IN USUARIOREGISTRADO.GENEROUSUARIO%TYPE,
        p_fechaNacUsuario      IN USUARIOREGISTRADO.FECHANACUSUARIO%TYPE,
        p_nacionalidadUsuario  IN USUARIOREGISTRADO.NACIONALIDADUSUARIO%TYPE,
        p_usuarioAcceso        IN USUARIOREGISTRADO.USUARIOACCESO%TYPE,
        p_contraseniaUsuario   IN USUARIOREGISTRADO.CONTRASENIAUSUARIO%TYPE,
        p_direccionUsuario     IN USUARIOREGISTRADO.DIRECCIONUSUARIO%TYPE,
        p_observacionUsuario   IN USUARIOREGISTRADO.OBSERVACIONUSUARIO%TYPE,
        p_telefonoUsuario      IN USUARIOREGISTRADO.TELEFONOUSUARIO%TYPE,
        p_bandera OUT NUMBER
    );

    -- Función: obtener ID de usuario por correo
    FUNCTION OBTENER_ID_PASAJERO_POR_NOMBRE_USUARIO(
        p_nombreUsuario IN USUARIOREGISTRADO.USUARIOACCESO%TYPE
    ) RETURN USUARIOREGISTRADO.IDUSUARIO%TYPE;

    -- Procedimiento: eliminar cuenta (estado = 'Inactivo')
    PROCEDURE ELIMINAR_CUENTA(
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE
    );

    -- Procedimiento: modificar información del usuario
    PROCEDURE MODIFICAR_USUARIO(
        p_IdUsuario            IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_antiguoDocIdUsuario  IN USUARIOREGISTRADO.DOCIDUSUARIO%TYPE,
        p_nuevoDocIdUsuario    IN USUARIOREGISTRADO.DOCIDUSUARIO%TYPE,
        p_tipoIdUsuario        IN USUARIOREGISTRADO.TIPOIDUSUARIO%TYPE,
        p_nombreUsuario        IN USUARIOREGISTRADO.NOMBREUSUARIO%TYPE,
        p_apellidoUsuario      IN USUARIOREGISTRADO.APELLIDOUSUARIO%TYPE,
        p_correoUsuario        IN USUARIOREGISTRADO.CORREOUSUARIO%TYPE,
        p_generoUsuario        IN USUARIOREGISTRADO.GENEROUSUARIO%TYPE,
        p_fechaNacUsuario      IN USUARIOREGISTRADO.FECHANACUSUARIO%TYPE,
        p_nacionalidadUsuario  IN USUARIOREGISTRADO.NACIONALIDADUSUARIO%TYPE,
        p_usuarioAcceso        IN USUARIOREGISTRADO.USUARIOACCESO%TYPE,
        p_contraseniaUsuario   IN USUARIOREGISTRADO.CONTRASENIAUSUARIO%TYPE,
        p_direccionUsuario     IN USUARIOREGISTRADO.DIRECCIONUSUARIO%TYPE,
        p_observacionUsuario   IN USUARIOREGISTRADO.OBSERVACIONUSUARIO%TYPE,
        p_telefonoUsuario      IN USUARIOREGISTRADO.TELEFONOUSUARIO%TYPE
    );

    -- Función: validar credenciales de acceso
    FUNCTION VALIDAR_CREDENCIALES(
        p_usuarioAcceso IN USUARIOREGISTRADO.USUARIOACCESO%TYPE,
        p_contrasenia   IN USUARIOREGISTRADO.CONTRASENIAUSUARIO%TYPE
    ) RETURN NUMBER;
    
    -- Procedimiento: cargar info usuario
    
    PROCEDURE CONSULTAR_USUARIO_POR_ID (
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_resultado OUT SYS_REFCURSOR
    );
    
    -- validar si es viable o no hacer un cambio de contraseña
    --FUNCTION VALIDAR_CAMBIO_CONTRASENIA(
        --p_usuarioAcceso USUARIOREGISTRADO.USUARIOACCESO,
      --  p_docIdUsuario USUARIOREGISTRADO.DOCIDUSUARIO
    --)
   -- RETURN NUMBER;

END GESTION_USUARIO;

------------------------------------------------------------

-- ===============================================
-- Cuerpo del paquete (IMPLEMENTACIÓN)
-- ===============================================
CREATE OR REPLACE PACKAGE BODY GESTION_USUARIO AS

    ---------------------------------------------------------
    PROCEDURE INSERTAR_USUARIO_NUEVO(
        p_docIdUsuario         IN USUARIOREGISTRADO.DOCIDUSUARIO%TYPE,
        p_tipoIdUsuario        IN USUARIOREGISTRADO.TIPOIDUSUARIO%TYPE,
        p_nombreUsuario        IN USUARIOREGISTRADO.NOMBREUSUARIO%TYPE,
        p_apellidoUsuario      IN USUARIOREGISTRADO.APELLIDOUSUARIO%TYPE,
        p_correoUsuario        IN USUARIOREGISTRADO.CORREOUSUARIO%TYPE,
        p_generoUsuario        IN USUARIOREGISTRADO.GENEROUSUARIO%TYPE,
        p_fechaNacUsuario      IN USUARIOREGISTRADO.FECHANACUSUARIO%TYPE,
        p_nacionalidadUsuario  IN USUARIOREGISTRADO.NACIONALIDADUSUARIO%TYPE,
        p_usuarioAcceso        IN USUARIOREGISTRADO.USUARIOACCESO%TYPE,
        p_contraseniaUsuario   IN USUARIOREGISTRADO.CONTRASENIAUSUARIO%TYPE,
        p_direccionUsuario     IN USUARIOREGISTRADO.DIRECCIONUSUARIO%TYPE,
        p_observacionUsuario   IN USUARIOREGISTRADO.OBSERVACIONUSUARIO%TYPE,
        p_telefonoUsuario      IN USUARIOREGISTRADO.TELEFONOUSUARIO%TYPE,
        p_bandera OUT NUMBER
    )
    IS
        e_unique_violation EXCEPTION;
        PRAGMA EXCEPTION_INIT(e_unique_violation, -00001);
        v_error_message VARCHAR2(500);
        
    BEGIN
        INSERT INTO USUARIOREGISTRADO(
            DOCIDUSUARIO, TIPOIDUSUARIO, NOMBREUSUARIO, APELLIDOUSUARIO, CORREOUSUARIO,
            GENEROUSUARIO, FECHANACUSUARIO, NACIONALIDADUSUARIO, ESTADOUSUARIO,
            USUARIOACCESO, CONTRASENIAUSUARIO, DIRECCIONUSUARIO, OBSERVACIONUSUARIO, TELEFONOUSUARIO
        )
        VALUES (
            p_docIdUsuario, p_tipoIdUsuario, p_nombreUsuario, p_apellidoUsuario,
            p_correoUsuario, p_generoUsuario, p_fechaNacUsuario, p_nacionalidadUsuario,
            'Activo', p_usuarioAcceso, p_contraseniaUsuario, p_direccionUsuario, p_observacionUsuario, p_telefonoUsuario
        );

        p_bandera := 1;

    EXCEPTION
       -- WHEN DUP_VAL_ON_INDEX THEN
          --  RAISE_APPLICATION_ERROR(-20001, 'El usuario con ese ID, correo o usuario ya existe.');
        WHEN VALUE_ERROR THEN
            RAISE_APPLICATION_ERROR(-20002, 'Error de tipo de dato o valor nulo en campo obligatorio.');
            
        WHEN e_unique_violation THEN
            v_error_message := SQLERRM;
            IF INSTR(v_error_message, 'uq_usuarioRegistrado_correo') > 0 THEN
                -- Violación del DNI
                RAISE_APPLICATION_ERROR(-20005, 'El correo ingresado ya existe en el sistema.');    
            ELSIF INSTR(v_error_message, 'uq_usuarioRegistrado_usuarioAcceso') > 0 THEN
                -- Violación del Email
                RAISE_APPLICATION_ERROR(-20006, 'El nombre de usuario ingresado ya está registrado.');   
             ELSIF INSTR(v_error_message, 'uq_usuarioRegistrado_docIdUsuario') > 0 THEN
                -- Violación del Email
                RAISE_APPLICATION_ERROR(-20007, 'El documento de identificacion ya existe en el sistema.');       
            END IF;
            
        WHEN OTHERS THEN
            IF SQLCODE = -2290 THEN
                RAISE_APPLICATION_ERROR(-20003, 'Violación de restricción CHECK (valores inválidos).');
            ELSE
                RAISE_APPLICATION_ERROR(-20004, 'Error desconocido al insertar usuario: ' || SQLERRM);
            END IF;
    END INSERTAR_USUARIO_NUEVO;

    ---------------------------------------------------------
    FUNCTION OBTENER_ID_PASAJERO_POR_NOMBRE_USUARIO(
        p_nombreUsuario IN USUARIOREGISTRADO.USUARIOACCESO%TYPE
    )
    RETURN USUARIOREGISTRADO.IDUSUARIO%TYPE
    IS
        v_idUsuario USUARIOREGISTRADO.DOCIDUSUARIO%TYPE;
    BEGIN
        SELECT IDUSUARIO
        INTO v_idUsuario
        FROM USUARIOREGISTRADO
        WHERE USUARIOACCESO = p_nombreUsuario;

        RETURN v_idUsuario;
        
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20010, 'No existe ningún usuario con el nombre de usuario buscado');
        WHEN TOO_MANY_ROWS THEN
            RAISE_APPLICATION_ERROR(-20011, 'Existen múltiples usuarios con el mismo nombre de usuario.');
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20012, 'Error al buscar usuario');
    END OBTENER_ID_PASAJERO_POR_NOMBRE_USUARIO;

    ---------------------------------------------------------
    PROCEDURE ELIMINAR_CUENTA(
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE
    )
    IS
        v_estadoUsuario USUARIOREGISTRADO.ESTADOUSUARIO%TYPE;
    BEGIN
        SELECT estadoUsuario
        INTO v_estadoUsuario
        FROM USUARIOREGISTRADO
        WHERE idUsuario = p_idUsuario;

        IF v_estadoUsuario = 'Inactivo' THEN
            RAISE_APPLICATION_ERROR(-20024, 'El usuario ya se encuentra inactivo.');
        END IF;

        UPDATE USUARIOREGISTRADO
        SET estadoUsuario = 'Inactivo'
        WHERE idUsuario = p_idUsuario;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20021, 'No existe el usuario a eliminar.');
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20022, 'Error al eliminar la cuenta ');
    END ELIMINAR_CUENTA;

    ---------------------------------------------------------
    PROCEDURE MODIFICAR_USUARIO(
        p_IdUsuario            IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_antiguoDocIdUsuario  IN USUARIOREGISTRADO.DOCIDUSUARIO%TYPE,
        p_nuevoDocIdUsuario    IN USUARIOREGISTRADO.DOCIDUSUARIO%TYPE,
        p_tipoIdUsuario        IN USUARIOREGISTRADO.TIPOIDUSUARIO%TYPE,
        p_nombreUsuario        IN USUARIOREGISTRADO.NOMBREUSUARIO%TYPE,
        p_apellidoUsuario      IN USUARIOREGISTRADO.APELLIDOUSUARIO%TYPE,
        p_correoUsuario        IN USUARIOREGISTRADO.CORREOUSUARIO%TYPE,
        p_generoUsuario        IN USUARIOREGISTRADO.GENEROUSUARIO%TYPE,
        p_fechaNacUsuario      IN USUARIOREGISTRADO.FECHANACUSUARIO%TYPE,
        p_nacionalidadUsuario  IN USUARIOREGISTRADO.NACIONALIDADUSUARIO%TYPE,
        p_usuarioAcceso        IN USUARIOREGISTRADO.USUARIOACCESO%TYPE,
        p_contraseniaUsuario   IN USUARIOREGISTRADO.CONTRASENIAUSUARIO%TYPE,
        p_direccionUsuario     IN USUARIOREGISTRADO.DIRECCIONUSUARIO%TYPE,
        p_observacionUsuario   IN USUARIOREGISTRADO.OBSERVACIONUSUARIO%TYPE,
        p_telefonoUsuario      IN USUARIOREGISTRADO.TELEFONOUSUARIO%TYPE
    )
    IS
        v_count INTEGER;
        
        e_unique_violation EXCEPTION;
        PRAGMA EXCEPTION_INIT(e_unique_violation, -00001);
        v_error_message VARCHAR2(500);
        
    BEGIN
        SELECT COUNT(*) INTO v_count
        FROM USUARIOREGISTRADO
        WHERE IDUSUARIO = p_IdUsuario;

        IF v_count = 0 THEN
            RAISE_APPLICATION_ERROR(-20030, 'El usuario no existe.');
        END IF;

        IF p_nuevoDocIdUsuario IS NOT NULL AND p_nuevoDocIdUsuario != p_antiguoDocIdUsuario THEN
            SELECT COUNT(*) INTO v_count
            FROM USUARIOREGISTRADO
            WHERE DOCIDUSUARIO = p_nuevoDocIdUsuario;

            IF v_count > 0 THEN
                RAISE_APPLICATION_ERROR(-20031, 'El nuevo documento ya está en uso.');
            END IF;
        END IF;

        UPDATE USUARIOREGISTRADO
        SET
            DOCIDUSUARIO       = p_nuevoDocIdUsuario,
            TIPOIDUSUARIO      = p_tipoIdUsuario,
            NOMBREUSUARIO      = p_nombreUsuario,
            APELLIDOUSUARIO    = p_apellidoUsuario,
            CORREOUSUARIO      = p_correoUsuario,
            GENEROUSUARIO      = p_generoUsuario,
            FECHANACUSUARIO    = p_fechaNacUsuario,
            NACIONALIDADUSUARIO = p_nacionalidadUsuario,
            USUARIOACCESO      = p_usuarioAcceso,
            CONTRASENIAUSUARIO = p_contraseniaUsuario,
            DIRECCIONUSUARIO = p_direccionUsuario,
            OBSERVACIONUSUARIO = p_observacionUsuario,
            TELEFONOUSUARIO    = p_telefonoUsuario
        WHERE IDUSUARIO = p_IdUsuario;

        IF SQL%ROWCOUNT = 0 THEN
            RAISE_APPLICATION_ERROR(-20032, 'No se pudo actualizar el usuario.');
        END IF;
    EXCEPTION
      --  WHEN DUP_VAL_ON_INDEX THEN
          --  RAISE_APPLICATION_ERROR(-20033, 'Valor duplicado en campo único.');
        WHEN VALUE_ERROR THEN
            RAISE_APPLICATION_ERROR(-20034, 'Error en tipo o tamaño de valor.');
            
        WHEN e_unique_violation THEN
            v_error_message := SQLERRM;
            IF INSTR(v_error_message, 'uq_usuarioRegistrado_correo') > 0 THEN
                -- Violación del email
                RAISE_APPLICATION_ERROR(-20005, 'El correo ingresado ya existe en el sistema.');    
            ELSIF INSTR(v_error_message, 'uq_usuarioRegistrado_usuarioAcceso') > 0 THEN
                -- Violación del usuario
                RAISE_APPLICATION_ERROR(-20006, 'El nombre de usuario ingresado ya está registrado.');   
             ELSIF INSTR(v_error_message, 'uq_usuarioRegistrado_docIdUsuario') > 0 THEN
                -- Violación del id
                RAISE_APPLICATION_ERROR(-20007, 'El documento de identificacion ya existe en el sistema.');       
            END IF;
              
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20035, 'Error al modificar usuario: ' || SQLERRM);
    END MODIFICAR_USUARIO;

    ---------------------------------------------------------
    FUNCTION VALIDAR_CREDENCIALES(
        p_usuarioAcceso IN USUARIOREGISTRADO.USUARIOACCESO%TYPE,
        p_contrasenia   IN USUARIOREGISTRADO.CONTRASENIAUSUARIO%TYPE
    )
    RETURN NUMBER
    IS
        v_contraseniaBD USUARIOREGISTRADO.CONTRASENIAUSUARIO%TYPE;
    BEGIN
        SELECT CONTRASENIAUSUARIO
        INTO v_contraseniaBD
        FROM USUARIOREGISTRADO
        WHERE USUARIOACCESO = p_usuarioAcceso;


        IF v_contraseniaBD = p_contrasenia THEN
            RETURN 1; -- TRUE
        ELSE
            RETURN 0; -- FALSE
        END IF;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20010, 'No existe ningún usuario con el nombre de usuario buscado');
        WHEN TOO_MANY_ROWS THEN
            RAISE_APPLICATION_ERROR(-20011, 'Existen múltiples usuarios con el mismo nombre de usuario.');
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20042,'Error inesperado al validar credenciales');
    END VALIDAR_CREDENCIALES;
    
    ----------------------------------------------------------------------------
     PROCEDURE CONSULTAR_USUARIO_POR_ID (
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_resultado OUT SYS_REFCURSOR
    )
    IS
    BEGIN
        OPEN p_resultado FOR
            SELECT 
                IDUSUARIO,
                DOCIDUSUARIO,
                TIPOIDUSUARIO,
                NOMBREUSUARIO,
                APELLIDOUSUARIO,
                CORREOUSUARIO,
                GENEROUSUARIO,
                FECHANACUSUARIO,
                NACIONALIDADUSUARIO,
                ESTADOUSUARIO,
                USUARIOACCESO,
                CONTRASENIAUSUARIO,
                DIRECCIONUSUARIO,
                OBSERVACIONUSUARIO,
                TELEFONOUSUARIO             
            FROM USUARIOREGISTRADO
            WHERE IDUSUARIO = p_idUsuario;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            -- Si no se encuentra el usuario, devolvemos cursor vacío
            OPEN p_resultado FOR
                SELECT 
                    NULL AS IDUSUARIO,
                    NULL AS DOCIDUSUARIO,
                    NULL AS TIPOIDUSUARIO,
                    NULL AS NOMBREUSUARIO,
                    NULL AS APELLIDOUSUARIO,
                    NULL AS CORREOUSUARIO,
                    NULL AS GENEROUSUARIO,
                    NULL AS FECHANACUSUARIO,
                    NULL AS NACIONALIDADUSUARIO,
                    NULL AS ESTADOUSUARIO,
                    NULL AS USUARIOACCESO,
                    NULL AS CONTRASENIAUSUARIO,
                    NULL AS DIRECCIONUSUARIO,
                    NULL AS OBSERVACIONUSUARIO,
                    NULL AS TELEFONOUSUARIO         
                FROM DUAL
                WHERE 1=0;
        WHEN OTHERS THEN
            -- Si ocurre un error inesperado, también devolvemos cursor vacío
            OPEN p_resultado FOR
                SELECT 
                    NULL AS IDUSUARIO,
                    NULL AS DOCIDUSUARIO,
                    NULL AS TIPOIDUSUARIO,
                    NULL AS NOMBREUSUARIO,
                    NULL AS APELLIDOUSUARIO,
                    NULL AS CORREOUSUARIO,
                    NULL AS GENEROUSUARIO,
                    NULL AS FECHANACUSUARIO,
                    NULL AS NACIONALIDADUSUARIO,
                    NULL AS ESTADOUSUARIO,
                    NULL AS USUARIOACCESO,
                    NULL AS CONTRASENIAUSUARIO,
                    NULL AS DIRECCIONUSUARIO,
                    NULL AS OBSERVACIONUSUARIO,
                    NULL AS TELEFONOUSUARIO        
                FROM DUAL
                WHERE 1=0;
    END CONSULTAR_USUARIO_POR_ID;
    
--------------------------------------------

   -- FUNCTION VALIDAR_CAMBIO_CONTRASENIA(
   --     p_usuarioAcceso USUARIOREGISTRADO.USUARIOACCESO,
   --     p_docIdUsuario USUARIOREGISTRADO.DOCIDUSUARIO
   -- )
   -- RETURN NUMBER
   -- IS
   --     v_usuarioAcceso NUMBER;
   -- BEGIN
   --     SELECT USUARIOACCESO INTO v_usuarioAcceso
   --     FROM USUARIOREGISTRADO
   --     WHERE DOCIDUSUARIO = p_docIdUsuario;
        
   --     IF v_usuarioAcceso = p_usuarioAcceso THEN
   --         RETURN 1; -- es valido, se permite hacer un update de la contraseña
   --     ELSE
   --         RETURN 0; --no es valido, no se permite hacer update de la contraseña
   --     END IF;
        
 --   EXCEPTION
  --      WHEN NO_DATA_FOUND THEN
--RAISE_APPLICATION_ERROR(-20090, 'No se encontro un usuario con el documento ingresado');
  --      WHEN OTHERS THEN
    --        RAISE_APPLICATION_ERROR(-20091, 'Error inesperado al intentar cambiar la contraseña');
   -- END CAMBIO_CONTRASENIA;

END GESTION_USUARIO;

--- TRIGGERS EPICA 1
/*Trigger. Antes de insertar un nuevo usuario o actualizar el nombre de usuario, se debe garantizar que nadie más (activo) 
        en la base de datos tiene el mismo nombre de usuario para evitar confusiones.
*/        
CREATE OR REPLACE TRIGGER TR_VALIDAR_USERNAME_UNICO
BEFORE INSERT ON UsuarioRegistrado
FOR EACH ROW
DECLARE
    v_count INTEGER;
BEGIN
    -- Validar que no tenga espacios adelante ni atrás
    IF TRIM(:NEW.usuarioAcceso) != :NEW.usuarioAcceso THEN
        RAISE_APPLICATION_ERROR(-20050,'El nombre de usuario no puede tener espacios al inicio o al final.');
    END IF;

    -- Validar que el username sea único (sensible a mayúsculas/minúsculas)
    SELECT COUNT(*)
    INTO v_count
    FROM UsuarioRegistrado
    WHERE usuarioAcceso = :NEW.usuarioAcceso
      AND idUsuario != NVL(:OLD.idUsuario, -1);  

    IF v_count > 0 THEN
        RAISE_APPLICATION_ERROR(-20051,'El nombre de usuario ya está en uso.');
    END IF;
END;

--trigger de ciberseguridad
CREATE OR REPLACE TRIGGER TR_VALIDAR_CONTRA_USUARIO
BEFORE INSERT OR UPDATE ON UsuarioRegistrado
FOR EACH ROW
BEGIN
    -- Validar que no tenga espacios al inicio o al final
    IF TRIM(:NEW.contraseniaUsuario) != :NEW.contraseniaUsuario THEN
        RAISE_APPLICATION_ERROR(-20060,'La contraseña no puede tener espacios al inicio o al final.');
    END IF;

    -- 2 Validar longitud mínima y máxima (entre 8 y 64 caracteres)
    IF LENGTH(:NEW.contraseniaUsuario) < 8 THEN
        RAISE_APPLICATION_ERROR(-20061,'La contraseña debe tener al menos 8 caracteres.');
    ELSIF LENGTH(:NEW.contraseniaUsuario) > 20 THEN
        RAISE_APPLICATION_ERROR(-20062,'La contraseña no puede tener más de 20 caracteres.');
    END IF;
END;


-- trigger de edad >18
CREATE OR REPLACE TRIGGER TRG_VALIDAR_EDAD_USUARIO
BEFORE INSERT OR UPDATE OF FECHANACUSUARIO
ON USUARIOREGISTRADO
FOR EACH ROW
DECLARE
    v_edad NUMBER;
BEGIN
    -- Calculamos la edad en años
    v_edad := TRUNC(MONTHS_BETWEEN(SYSDATE, :NEW.FECHANACUSUARIO) / 12);

    -- Validación de edad mínima
    IF v_edad < 18 THEN
        RAISE_APPLICATION_ERROR(
            -20080,
            'El usuario debe tener al menos 18 años para registrarse.'
        );
    END IF;
END TRG_VALIDAR_EDAD_USUARIO;

-- 
