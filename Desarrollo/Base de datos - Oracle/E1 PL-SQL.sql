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
        WHEN DUP_VAL_ON_INDEX THEN
            RAISE_APPLICATION_ERROR(-20001, 'El usuario con ese ID, correo o usuario ya existe.');
        WHEN VALUE_ERROR THEN
            RAISE_APPLICATION_ERROR(-20002, 'Error de tipo de dato o valor nulo en campo obligatorio.');
        WHEN OTHERS THEN
            IF SQLCODE = -2290 THEN
                RAISE_APPLICATION_ERROR(-20003, 'Violación de restricción CHECK (valores inválidos).');
            END IF;
    END INSERTAR_USUARIO_NUEVO;

    ---------------------------------------------------------
    FUNCTION OBTENER_ID_PASAJERO_POR_NOMBRE_USUARIO(
        p_nombreUsuario IN USUARIOREGISTRADO.USUARIOACCESO%TYPE
    )
    RETURN USUARIOREGISTRADO.IDUSUARIO%TYPE
    IS
        v_idUsuario USUARIOREGISTRADO.IDUSUARIO%TYPE;
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
            RAISE_APPLICATION_ERROR(-20026, 'No existe el usuario a eliminar.');
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
        e_unique_violation EXCEPTION;
        PRAGMA EXCEPTION_INIT (e_unique_violation, -00001);
        v_error_message VARCHAR2(500);
        v_count INTEGER;
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
        --    RAISE_APPLICATION_ERROR(-20033, 'Valor duplicado en campo único.');
        WHEN VALUE_ERROR THEN
            RAISE_APPLICATION_ERROR(-20034, 'Error en tipo o tamaño de valor.');
         WHEN e_unique_violation THEN
            v_error_message := SQLERRM;
            IF INSTR(v_error_message, 'UQ_USUARIOREGISTRADO_CORREO') > 0 THEN
                -- Violación del email
                RAISE_APPLICATION_ERROR(-20005, 'El correo ingresado ya existe en el sistema.');    
            ELSIF INSTR(v_error_message, 'UQ_USUARIOREGISTRADO_USUARIOACCESO') > 0 THEN
                -- Violación del usuario
                RAISE_APPLICATION_ERROR(-20006, 'El nombre de usuario ingresado ya está registrado.');   
             ELSIF INSTR(v_error_message, 'UQ_USUARIOREGISTRADO_DOCIDUSUARIO') > 0 THEN
                -- Violación del id
                RAISE_APPLICATION_ERROR(-20007, 'El documento de identificacion ya existe en el sistema.');       
            END IF;
        
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
            RAISE_APPLICATION_ERROR(-20040,'No se encontró al usuario ingresado');
        WHEN TOO_MANY_ROWS THEN
            RAISE_APPLICATION_ERROR(-20041,'Más de un usuario tiene el mismo nombre de usuario');
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

END GESTION_USUARIO;


--- TRIGGERS EPICA 1
--trigger de ciberseguridad
CREATE OR REPLACE TRIGGER TR_VALIDAR_CONTRA_USUARIO
BEFORE INSERT OR UPDATE OF CONTRASENIAUSUARIO ON UsuarioRegistrado
FOR EACH ROW
BEGIN
    -- Validar que no tenga espacios al inicio o al final
    IF TRIM(:NEW.contraseniaUsuario) != :NEW.contraseniaUsuario THEN
        RAISE_APPLICATION_ERROR(-20060,'La contraseña no puede tener espacios al inicio o al final.');
    END IF;

    -- 2 Validar longitud mínima y máxima (entre 8 y 64 caracteres)
    IF LENGTH(:NEW.contraseniaUsuario) < 8 THEN
        RAISE_APPLICATION_ERROR(-20061,'La contraseña debe tener al menos 8 caracteres.');
    ELSIF LENGTH(:NEW.contraseniaUsuario) > 64 THEN
        RAISE_APPLICATION_ERROR(-20062,'La contraseña no puede tener más de 64 caracteres.');
    END IF;
END;

--trigger de correo electronico
CREATE OR REPLACE TRIGGER trg_validar_correo_usuario
BEFORE INSERT OR UPDATE OF CORREOUSUARIO ON UsuarioRegistrado
FOR EACH ROW
DECLARE
    v_newCorreo VARCHAR2(255);
    v_oldCorreo VARCHAR2(255);
BEGIN
    v_newCorreo := TRIM(LOWER(:NEW.correoUsuario));
    v_oldCorreo := TRIM(LOWER(:OLD.correoUsuario));

    -- Si el correo no cambió realmente, no validar
    IF UPDATING AND v_newCorreo = v_oldCorreo THEN
        RETURN;
    END IF;

    -- Validar espacios
    IF TRIM(:NEW.correoUsuario) != :NEW.correoUsuario THEN
        RAISE_APPLICATION_ERROR(-20070, 'El correo no debe tener espacios al inicio o al final.');
    END IF;

    -- Validar formato general
    IF NOT REGEXP_LIKE(:NEW.correoUsuario, '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$') THEN
        RAISE_APPLICATION_ERROR(-20071, 'El correo electrónico no tiene un formato válido.');
    END IF;
END trg_validar_correo_usuario;



CREATE OR REPLACE TRIGGER trg_validar_correo_pasajero
BEFORE INSERT ON PASAJERO
FOR EACH ROW
DECLARE
    v_newCorreo VARCHAR2(255);
    v_oldCorreo VARCHAR2(255);
BEGIN
    v_newCorreo := TRIM(LOWER(:NEW.correoPasajero));
    v_oldCorreo := TRIM(LOWER(:OLD.correoPasajero));

    -- Validar espacios
    IF TRIM(:NEW.correoPasajero) != :NEW.correoPasajero THEN
        RAISE_APPLICATION_ERROR(-20072, 'El correo no debe tener espacios al inicio o al final.');
    END IF;

    -- Validar formato general
    IF NOT REGEXP_LIKE(:NEW.correoPasajero, '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$') THEN
        RAISE_APPLICATION_ERROR(-20073, 'El correo electrónico no tiene un formato válido.');
    END IF;
END trg_validar_correo_pasajero;




-- trigger de edad >18
CREATE OR REPLACE TRIGGER TRG_VALIDAR_EDAD_USUARIO
BEFORE INSERT OR UPDATE OF FECHANACUSUARIO
ON USUARIOREGISTRADO
FOR EACH ROW
DECLARE
    v_edad NUMBER;
BEGIN
    -- Calculamos la edad en años (truncando los decimales)
    v_edad := TRUNC(MONTHS_BETWEEN(SYSDATE, :NEW.FECHANACUSUARIO) / 12);

    -- Validación de edad mínima y máxima
    IF v_edad < 18 THEN
        RAISE_APPLICATION_ERROR(
            -20080,
            'El usuario debe tener al menos 18 años para registrarse.'
        );
    ELSIF v_edad > 100 THEN
        RAISE_APPLICATION_ERROR(
            -20081,
            'La edad ingresada excede el máximo permitido (100 años).'
        );
    END IF;
END TRG_VALIDAR_EDAD_USUARIO;



CREATE OR REPLACE TRIGGER TR_VALIDAR_PALABRAS_SQL
BEFORE INSERT OR UPDATE OF 
    NOMBREUSUARIO, APELLIDOUSUARIO, USUARIOACCESO, CONTRASENIAUSUARIO, 
    DIRECCIONUSUARIO, OBSERVACIONUSUARIO
ON USUARIOREGISTRADO
FOR EACH ROW
DECLARE
    v_texto VARCHAR2(4000);
    v_palabra VARCHAR2(50);
    v_lista_palabras SYS.ODCIVARCHAR2LIST := SYS.ODCIVARCHAR2LIST(
        'DROP', 'DELETE', 'INSERT', 'UPDATE', 'ALTER', 
        'CREATE', 'TRUNCATE', 'TABLE', 'SELECT', 'FROM', 'WHERE', 'EXECUTE'
    );
BEGIN
    -- Concatenamos todos los campos susceptibles a revisión
    v_texto := UPPER(
        NVL(:NEW.NOMBREUSUARIO, '') || ' ' ||
        NVL(:NEW.APELLIDOUSUARIO, '') || ' ' ||
        NVL(:NEW.USUARIOACCESO, '') || ' ' ||
        NVL(:NEW.CONTRASENIAUSUARIO, '') || ' ' ||
        NVL(:NEW.DIRECCIONUSUARIO, '') || ' ' ||
        NVL(:NEW.OBSERVACIONUSUARIO, '')
    );

    -- Recorremos la lista de palabras SQL prohibidas
    FOR i IN 1 .. v_lista_palabras.COUNT LOOP
        v_palabra := v_lista_palabras(i);
        IF INSTR(v_texto, v_palabra) > 0 THEN
            RAISE_APPLICATION_ERROR(
                -20500,
                'El texto ingresado contiene una palabra SQL reservada o peligrosa: ' || v_palabra
            );
        END IF;
    END LOOP;
END;

CREATE OR REPLACE TRIGGER TRG_VALIDAR_TELEFONO_USUARIO
BEFORE INSERT OR UPDATE OF TELEFONOUSUARIO
ON USUARIOREGISTRADO
FOR EACH ROW
BEGIN
    -- Verificar que el campo no sea nulo
    IF :NEW.TELEFONOUSUARIO IS NULL THEN
        RAISE_APPLICATION_ERROR(
            -20600,
            'El número de teléfono no puede ser nulo.'
        );
    END IF;

    -- Verificar que contenga exactamente 10 dígitos numéricos
    IF NOT REGEXP_LIKE(:NEW.TELEFONOUSUARIO, '^[0-9]{10}$') THEN
        RAISE_APPLICATION_ERROR(
            -20601,
            'El número de teléfono debe contener exactamente 10 dígitos numéricos.'
        );
    END IF;
END TRG_VALIDAR_TELEFONO_USUARIO;


CREATE OR REPLACE TRIGGER TRG_VALIDAR_CARACTERES_USUARIO
BEFORE INSERT OR UPDATE OF DIRECCIONUSUARIO, OBSERVACIONUSUARIO
ON USUARIOREGISTRADO
FOR EACH ROW
DECLARE
    v_cadena VARCHAR2(4000);
BEGIN
    -- Validar campo DIRECCIONUSUARIO (si no es nulo)
    IF :NEW.DIRECCIONUSUARIO IS NOT NULL THEN
        v_cadena := :NEW.DIRECCIONUSUARIO;

        -- Verificar si contiene algún carácter no permitido
        IF REGEXP_LIKE(v_cadena, '[!¡?¿+*_%$@]') THEN
            RAISE_APPLICATION_ERROR(
                -20092,
                'La dirección contiene caracteres no permitidos. Solo se permiten letras, números, espacios, - # y ''.'
            );
        END IF;
    END IF;

    -- Validar campo OBSERVACIONUSUARIO (si no es nulo)
    IF :NEW.OBSERVACIONUSUARIO IS NOT NULL THEN
        v_cadena := :NEW.OBSERVACIONUSUARIO;

        -- Verificar si contiene algún carácter no permitido
        IF REGEXP_LIKE(v_cadena, '[!¡?¿+*_%$@]') THEN
            RAISE_APPLICATION_ERROR(
                -20093,
                'La observación contiene caracteres no permitidos. Solo se permiten letras, números, espacios, - # y ''.'
            );
        END IF;
    END IF;
END TRG_VALIDAR_CARACTERES_USUARIO;









--PRUEBAS DE TRIGGERS Y EXCEPCIONES
UPDATE USUARIOREGISTRADO
SET CORREOUSUARIO = 'correotest.com'
WHERE IDUSUARIO = 1;

UPDATE USUARIOREGISTRADO
SET FECHANACUSUARIO = SYSDATE
WHERE IDUSUARIO = 1;

INSERT INTO PASAJERO (IDPASAJERO, TIPOIDPASAJERO,NOMBREPASAJERO, APELLIDOPASAJERO, CORREOPASAJERO)
VALUES (444,'T.I.','Maria','Lopez', 'correoprueba');

UPDATE USUARIOREGISTRADO
SET CONTRASENIAUSUARIO = ' contraseniatest'
WHERE IDUSUARIO = 1;

SET SERVEROUTPUT ON 
BEGIN
INSERT INTO UsuarioRegistrado (
    docIdUsuario,
    tipoIdUsuario,
    nombreUsuario,
    apellidoUsuario,
    correoUsuario,
    generoUsuario,
    fechaNacUsuario,
    nacionalidadUsuario,
    estadoUsuario,
    usuarioAcceso,
    contraseniaUsuario,
    direccionUsuario,
    observacionUsuario,
    telefonoUsuario
) VALUES (
    123456789,                     -- Documento
    'C.C.',                        -- Tipo de identificación
    'Valentina',                   -- Nombre
    'Balcazar',                    -- Apellido
    'correo@test2.com',           -- Correo
    'Femenino',                    -- Género
    TO_DATE('2000-05-10', 'YYYY-MM-DD'), -- Fecha de nacimiento
    'Colombiana',                  -- Nacionalidad
    'Activo',                      -- Estado
    'valenb',                      -- Usuario de acceso
    '12345abc',                    -- Contraseña
    'Calle 10 #15-30',             -- Dirección
    'Sin observaciones',           -- Observación
    3124567890                    -- Teléfono
);


DBMS_OUTPUT.PUT_LINE(SQL%ROWCOUNT); 

END;

DELETE FROM USUARIOREGISTRADO
WHERE DOCIDUSUARIO=123456789;


DELETE FROM USUARIOREGISTRADO
WHERE IDUSUARIO=125;





