CREATE OR REPLACE PACKAGE MONITOREAR_VUELOS AS

    -- 🔹 Función auxiliar: verifica si hay al menos un asiento disponible en un avión
    FUNCTION VALIDAR_ASIENTO_LIBRE(
        p_idAvion AVION.IDAVION%TYPE
    ) RETURN BOOLEAN;

    -- 🔹 Recuperar todas las ciudades y países de origen (para selector en interfaz)
    PROCEDURE OBTENER_CIUDADES_PAISES_ORIGEN(
        p_resultado OUT SYS_REFCURSOR
    );

    -- 🔹 Recuperar todas las ciudades y países de destino
    PROCEDURE OBTENER_CIUDADES_PAISES_DESTINO(
        p_resultado OUT SYS_REFCURSOR
    );

    -- 🔹 Consultar vuelos según origen, destino y fecha (uso auxiliar)
    PROCEDURE CONSULTAR_VUELOS_POR_ORIGEN_DESTINO_FECHA(
        p_ciuOrigen     IN  VUELO.CIUORIGENVUELO%TYPE,
        p_paisOrigen    IN  VUELO.PAISORIGENVUELO%TYPE,
        p_ciuDestino    IN  VUELO.CIUDESTINOVUELO%TYPE,
        p_paisDestino   IN  VUELO.PAISDESTINOVUELO%TYPE,
        p_fecha         IN  DATE,
        p_resultado     OUT SYS_REFCURSOR
    );

    -- 🔹 Consultar vuelos de ida y vuelta (para capa superior)
    PROCEDURE VUELOS_IDA_Y_VUELTA(
        p_ciuOrigen       IN  VUELO.CIUORIGENVUELO%TYPE,
        p_paisOrigen      IN  VUELO.PAISORIGENVUELO%TYPE,
        p_ciuDestino      IN  VUELO.CIUDESTINOVUELO%TYPE,
        p_paisDestino     IN  VUELO.PAISDESTINOVUELO%TYPE,
        p_fechaIda        IN  DATE,
        p_fechaRegreso    IN DATE,
        p_resultadoIda    OUT SYS_REFCURSOR,
        p_resultadoVuelta OUT SYS_REFCURSOR
    );

END MONITOREAR_VUELOS;


CREATE OR REPLACE PACKAGE BODY MONITOREAR_VUELOS AS

    -- ======================================================
    -- Función auxiliar: Verifica si hay al menos un asiento libre
    -- ======================================================
    FUNCTION VALIDAR_ASIENTO_LIBRE(
        p_idAvion AVION.IDAVION%TYPE
    ) RETURN BOOLEAN
    IS
        v_temp NUMBER;
    BEGIN
        SELECT 1
        INTO v_temp
        FROM ASIENTO
        WHERE idAvion = p_idAvion
          AND estadoAsiento = 'Disponible'
          AND ROWNUM = 1;

        RETURN TRUE;  -- Hay al menos un asiento disponible

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RETURN FALSE;  -- No hay asientos disponibles
        WHEN OTHERS THEN
            RETURN FALSE;  -- Error inesperado
    END VALIDAR_ASIENTO_LIBRE;


    -- ======================================================
    -- Procedimiento: Ciudades y países de origen
    -- ======================================================
    PROCEDURE OBTENER_CIUDADES_PAISES_ORIGEN(
        p_resultado OUT SYS_REFCURSOR
    )
    AS
    BEGIN
        OPEN p_resultado FOR
            SELECT DISTINCT
                CIUORIGENVUELO AS CIUDAD,
                PAISORIGENVUELO AS PAIS
            FROM VUELO
            ORDER BY PAISORIGENVUELO, CIUORIGENVUELO;
    END OBTENER_CIUDADES_PAISES_ORIGEN;


    -- ======================================================
    -- Procedimiento: Ciudades y países de destino
    -- ======================================================
    PROCEDURE OBTENER_CIUDADES_PAISES_DESTINO(
        p_resultado OUT SYS_REFCURSOR
    )
    AS
    BEGIN
        OPEN p_resultado FOR
            SELECT DISTINCT
                CIUDESTINOVUELO AS CIUDAD,
                PAISDESTINOVUELO AS PAIS
            FROM VUELO
            ORDER BY PAISDESTINOVUELO, CIUDESTINOVUELO;
    END OBTENER_CIUDADES_PAISES_DESTINO;


    -- ======================================================
    -- Procedimiento auxiliar: Consultar vuelos por origen, destino y fecha
    -- ======================================================
    PROCEDURE CONSULTAR_VUELOS_POR_ORIGEN_DESTINO_FECHA(
        p_ciuOrigen     IN  VUELO.CIUORIGENVUELO%TYPE,
        p_paisOrigen    IN  VUELO.PAISORIGENVUELO%TYPE,
        p_ciuDestino    IN  VUELO.CIUDESTINOVUELO%TYPE,
        p_paisDestino   IN  VUELO.PAISDESTINOVUELO%TYPE,
        p_fecha         IN  DATE,
        p_resultado     OUT SYS_REFCURSOR
    )
    IS
    BEGIN
        OPEN p_resultado FOR
            SELECT 
                IDVUELO,
                CIUORIGENVUELO,
                PAISORIGENVUELO,
                CIUDESTINOVUELO,
                PAISDESTINOVUELO,
                ESTADOVUELO,
                --FECHAEJECUCION,
                HORASALIDAVUELO,
                IDZEMBARQUE,
                IDPUERTA,
                IDAVION,
                PRECIOBASEVUELO
            FROM VUELO
            WHERE UPPER(TRIM(CIUORIGENVUELO))  = UPPER(TRIM(p_ciuOrigen))
              AND UPPER(TRIM(PAISORIGENVUELO)) = UPPER(TRIM(p_paisOrigen))
              AND UPPER(TRIM(CIUDESTINOVUELO)) = UPPER(TRIM(p_ciuDestino))
              AND UPPER(TRIM(PAISDESTINOVUELO)) = UPPER(TRIM(p_paisDestino))
              AND TRUNC(FECHAEJECUCION) = TRUNC(p_fecha)
              AND horaSalidaVuelo > SYSDATE;
    EXCEPTION
        WHEN OTHERS THEN
            -- Devolver cursor vacío para evitar errores en C#
            OPEN p_resultado FOR
                SELECT 
                    NULL AS IDVUELO,
                    NULL AS CIUORIGENVUELO,
                    NULL AS PAISORIGENVUELO,
                    NULL AS CIUDESTINOVUELO,
                    NULL AS PAISDESTINOVUELO,
                    NULL AS ESTADOVUELO,
                    --NULL AS FECHAEJECUCION,
                    NULL AS HORASALIDAVUELO,
                    NULL AS IDZEMBARQUE,
                    NULL AS IDPUERTA,
                    NULL AS IDAVION,
                    NULL AS PRECIOBASEVUELO
                FROM DUAL
                WHERE 1=0;
    END CONSULTAR_VUELOS_POR_ORIGEN_DESTINO_FECHA;


    -- ======================================================
    -- Procedimiento principal: Consultar vuelos de ida y vuelta
    -- ======================================================
    PROCEDURE VUELOS_IDA_Y_VUELTA(
        p_ciuOrigen       IN  VUELO.CIUORIGENVUELO%TYPE,
        p_paisOrigen      IN  VUELO.PAISORIGENVUELO%TYPE,
        p_ciuDestino      IN  VUELO.CIUDESTINOVUELO%TYPE,
        p_paisDestino     IN  VUELO.PAISDESTINOVUELO%TYPE,
        p_fechaIda        IN  DATE,
        p_fechaRegreso     IN DATE,
        p_resultadoIda    OUT SYS_REFCURSOR,
        p_resultadoVuelta OUT SYS_REFCURSOR
    )
    IS
    BEGIN
        CONSULTAR_VUELOS_POR_ORIGEN_DESTINO_FECHA(
            p_ciuOrigen,
            p_paisOrigen,
            p_ciuDestino,
            p_paisDestino,
            p_fechaIda,
            p_resultadoIda
        );
        
        CONSULTAR_VUELOS_POR_ORIGEN_DESTINO_FECHA(
            p_ciuDestino,
            p_paisDestino,
            p_ciuOrigen,
            p_paisOrigen,
            p_fechaRegreso,
            p_resultadoVuelta
        );

    EXCEPTION
        WHEN OTHERS THEN
            -- Si ocurre cualquier error, devolvemos cursores vacíos
            OPEN p_resultadoIda FOR
                SELECT 
                    NULL AS IDVUELO,
                    NULL AS CIUORIGENVUELO,
                    NULL AS PAISORIGENVUELO,
                    NULL AS CIUDESTINOVUELO,
                    NULL AS PAISDESTINOVUELO,
                    NULL AS ESTADOVUELO,
                    --NULL AS FECHAEJECUCION,
                    NULL AS HORASALIDAVUELO,
                    NULL AS IDZEMBARQUE,
                    NULL AS IDPUERTA,
                    NULL AS IDAVION
                FROM DUAL
                WHERE 1=0;

            OPEN p_resultadoVuelta FOR
                SELECT 
                    NULL AS IDVUELO,
                    NULL AS CIUORIGENVUELO,
                    NULL AS PAISORIGENVUELO,
                    NULL AS CIUDESTINOVUELO,
                    NULL AS PAISDESTINOVUELO,
                    NULL AS ESTADOVUELO,
                    --NULL AS FECHAEJECUCION,
                    NULL AS HORASALIDAVUELO,
                    NULL AS IDZEMBARQUE,
                    NULL AS IDPUERTA,
                    NULL AS IDAVION,
                    NULL AS PRECIOBASEVUELO
                FROM DUAL
                WHERE 1=0;
    END VUELOS_IDA_Y_VUELTA;
END MONITOREAR_VUELOS;


