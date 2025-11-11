-- Epica 2. Aqui esta el paquete de la gestion de pasajes por parte del usuario
-- Para el segundo sprint las HU son: Comprar pasaje, Cancelar Pasaje

--==========================================
--DECLARACION DEL PAQUETE
--==========================================
CREATE OR REPLACE PACKAGE GESTION_PASAJES AS

    -- -------------------------------------------------------------------------
    -- COMPRA DE PASAJES
    -- -------------------------------------------------------------------------

    --funcion para verificar la disponibilidad de sillas en una categoria especifica. ()
    FUNCTION OBTENER_ASIENTO_LIBRE
    (
        p_idAvion Avion.idAvion%TYPE, 
        p_idCategoria CATEGORIAASIENTO.idCategoria%TYPE
    )
    RETURN NUMBER;
    
    --procedimiento para calcular la suma de precios base de acuerdo al numero de pasajeros
    FUNCTION CALCULAR_SUMA_PRECIO_BASE
    (
        p_numPasajeros NUMBER, 
        p_precioBase VUELO.PRECIOBASEVUELO%TYPE
    )
    RETURN NUMBER;
    
    --procedimiento para registrar un pasajero
    PROCEDURE INSERTAR_PASAJERO_NUEVO (
        p_idPasajero         IN PASAJERO.IDPASAJERO%TYPE,
        p_tipoIdPasajero     IN PASAJERO.TIPOIDPASAJERO%TYPE,
        p_nombrePasajero     IN PASAJERO.NOMBREPASAJERO%TYPE,
        p_apellidoPasajero   IN PASAJERO.APELLIDOPASAJERO%TYPE,
        p_correoPasajero     IN PASAJERO.CORREOPASAJERO%TYPE,
        p_bandera OUT NUMBER
    );

    --procedimiento para comprar un pasaje (esta es la transaccion como tal)
    PROCEDURE RESERVAR_PASAJE (
        p_idUsuario   IN UsuarioRegistrado.idUsuario%TYPE,
        p_idPasajero  IN Pasajero.idPasajero%TYPE,
        p_idVuelo     IN Vuelo.idVuelo%TYPE,
        p_idCategoria IN CategoriaAsiento.idCategoria%TYPE,
        p_idPasajeGenerado OUT Pasaje.idPasaje%TYPE,
        p_idCompra OUT Compra.idCompra%TYPE,
        p_bandera OUT NUMBER
    );
    
     --procedimiento para asociar monto y factura
    PROCEDURE ASOCIAR_COMPRAS_A_FACTURA
    (
        p_idVuelo IN VUELO.IDVUELO%TYPE,
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_medioPago IN FACTURA.MEDIOPAGOFACTURA%TYPE,
        p_idFacturaGenerada OUT FACTURA.IDFACTURA%TYPE
    );
    
    --funcion para generar la factura de compra
    PROCEDURE RECUPERAR_DATOS_FACTURA (
        p_idVuelo IN VUELO.IDVUELO%TYPE,
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_medioPago IN FACTURA.MEDIOPAGOFACTURA%TYPE,
        p_infoFactura OUT SYS_REFCURSOR
    );

    -- -------------------------------------------------------------------------
    -- CANCELACION DE PASAJES
    -- -------------------------------------------------------------------------
    
    --funcion para cancelar un pasaje
    PROCEDURE CANCELAR_PASAJE 
    (
        p_idPasaje IN PASAJE.IDPASAJE%TYPE
    );
    
-- ===============================================================
-- Función para obtener el ID de una categoría a partir de su nombre
-- ===============================================================
    FUNCTION OBTENER_ID_CATEGORIA (
        p_nombreCategoria CATEGORIAASIENTO.NOMBRECATEGORIA%TYPE
    )
    RETURN CATEGORIAASIENTO.IDCATEGORIA%TYPE;

    PROCEDURE OBTENER_CATEGORIAS_ASIENTO_NOMBRES
    (
        p_resultado OUT SYS_REFCURSOR
    );
    
    
    PROCEDURE OBTENER_VUELOS_USUARIO (
            p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
            p_resultado OUT SYS_REFCURSOR
        );
        
    PROCEDURE OBTENER_CANTIDAD_PASAJES_USUARIO (
            p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
            p_cantidad OUT NUMBER
        );
        
    PROCEDURE OBTENER_INFO_VUELO_PASAJE (
            p_idPasaje IN PASAJE.IDPASAJE%TYPE,
            p_resultado OUT SYS_REFCURSOR
        );
        
    FUNCTION OBTENER_SOBRECOSTO_CATEGORIA (
        p_nombreCategoria CATEGORIAASIENTO.NOMBRECATEGORIA%TYPE
    )
    RETURN CATEGORIAASIENTO.SOBRECOSTOCATEGORIA%TYPE;    

-- ================================================================
-- REAGENDAMIENTO DE PASAJES
-- ================================================================


END GESTION_PASAJES;




--==========================================
--IMPLEMENTACION DEL PAQUETE
--==========================================
CREATE OR REPLACE PACKAGE BODY GESTION_PASAJES AS
    -- funcion para verificar la disponibilidad de sillas en una categoria especifica. ()
    FUNCTION OBTENER_ASIENTO_LIBRE
    (
        p_idAvion AVION.IDAVION%TYPE, 
        p_idCategoria CATEGORIAASIENTO.IDCATEGORIA%TYPE
    )
    RETURN NUMBER
    IS
        v_numAsiento Asiento.numAsiento%TYPE;
    BEGIN
        SELECT numAsiento
        INTO v_numAsiento
        FROM Asiento
        WHERE idAvion = p_idAvion 
          AND estadoAsiento = 'Disponible'
          AND idCategoria = p_idCategoria
          AND ROWNUM = 1
        FOR UPDATE NOWAIT;
        
        RETURN v_numAsiento;

    EXCEPTION
        -- No hay asientos disponibles
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20014,'No hay asientos disponibles para el avion en la categoria ingresada');
    
        -- Si la consulta devuelve más de una fila (teóricamente imposible por ROWNUM=1)
        WHEN TOO_MANY_ROWS THEN
            RAISE_APPLICATION_ERROR(-20015,'Se encontró más de un asiento cuando solo se esperaba uno');
    
        -- Errores de valor nulo o integridad
        WHEN VALUE_ERROR THEN
            RAISE_APPLICATION_ERROR(-20016,'Error de tipo o conversión al asignar el número de asiento.');
    
        -- Cualquier otro error inesperado
        WHEN OTHERS THEN
            IF SQLCODE = -54 THEN
                RAISE_APPLICATION_ERROR(-20023, 'Otro usuario está reservando asientos de esta categoría. Intente más tarde.');
            ELSE
                RAISE_APPLICATION_ERROR(-20017, 'Error inesperado al obtener asiento: ' || SQLERRM);
            END IF;
           
    END OBTENER_ASIENTO_LIBRE;
    
     --procedimiento para calcular la suma de precios base de acuerdo al numero de pasajeros
    FUNCTION CALCULAR_SUMA_PRECIO_BASE
    (
        p_numPasajeros NUMBER, 
        p_precioBase   VUELO.PRECIOBASEVUELO%TYPE
    )
    RETURN NUMBER
    IS
        v_sumaPrecioBase NUMBER := 0;
    BEGIN
        -- Validar entradas nulas
        IF p_numPasajeros IS NULL OR p_precioBase IS NULL THEN
            RAISE_APPLICATION_ERROR(-20018,'Parametros nulos');
        END IF;
    
        -- Validar valores negativos
        IF p_numPasajeros < 0 OR p_precioBase < 0 THEN
            RAISE_APPLICATION_ERROR(-20019,'Valores negativos no validos');
        END IF;
    
        -- Calcular el total
        v_sumaPrecioBase := p_precioBase * p_numPasajeros;
        RETURN v_sumaPrecioBase;
    
    EXCEPTION
        WHEN VALUE_ERROR THEN
            RAISE_APPLICATION_ERROR(-20021,'Error en tipo o tamaño de valor.');
    
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20020,'Error desconocido al calcular el total de precio base');
    END CALCULAR_SUMA_PRECIO_BASE;

    --procedimiento para registrar un pasajero
    PROCEDURE INSERTAR_PASAJERO_NUEVO (
        p_idPasajero         IN PASAJERO.IDPASAJERO%TYPE,
        p_tipoIdPasajero     IN PASAJERO.TIPOIDPASAJERO%TYPE,
        p_nombrePasajero     IN PASAJERO.NOMBREPASAJERO%TYPE,
        p_apellidoPasajero   IN PASAJERO.APELLIDOPASAJERO%TYPE,
        p_correoPasajero     IN PASAJERO.CORREOPASAJERO%TYPE,
        p_bandera OUT NUMBER
    )
    IS
        e_unique_violation EXCEPTION;
        PRAGMA EXCEPTION_INIT(e_unique_violation, -00001);
    BEGIN
        MERGE INTO PASAJERO tgt
        USING (SELECT p_idPasajero AS idPasajero FROM dual) src
        ON (tgt.idPasajero = src.idPasajero)
        WHEN MATCHED THEN
            UPDATE SET
                TIPOIDPASAJERO = p_tipoIdPasajero,
                NOMBREPASAJERO = p_nombrePasajero,
                APELLIDOPASAJERO = p_apellidoPasajero,
                CORREOPASAJERO = p_correoPasajero
        WHEN NOT MATCHED THEN
            INSERT (
                IDPASAJERO, TIPOIDPASAJERO, NOMBREPASAJERO, APELLIDOPASAJERO,
                CORREOPASAJERO
            )
            VALUES (
                p_idPasajero, p_tipoIdPasajero, p_nombrePasajero, p_apellidoPasajero,
                p_correoPasajero
            );
    
        p_bandera := 1; -- Éxito
    
    EXCEPTION
        WHEN VALUE_ERROR THEN
            RAISE_APPLICATION_ERROR(-20102, 'Error de tipo de dato o valor nulo en campo obligatorio.');
        
        WHEN e_unique_violation THEN
            RAISE_APPLICATION_ERROR(-20105, 'El correo ingresado ya existe en el sistema.');
        
        WHEN OTHERS THEN
            IF SQLCODE = -2290 THEN
                RAISE_APPLICATION_ERROR(-20103, 'Violación de restricción CHECK (valores inválidos).');
            /*ELSE
                RAISE_APPLICATION_ERROR(-20013, 'Error desconocido al guardar pasajero: ' || SQLERRM);*/
            END IF;
    END INSERTAR_PASAJERO_NUEVO;

     --procedimiento para comprar un pasaje (esta es la transaccion como tal)
    PROCEDURE RESERVAR_PASAJE (
        p_idUsuario   IN UsuarioRegistrado.idUsuario%TYPE,
        p_idPasajero  IN Pasajero.idPasajero%TYPE,
        p_idVuelo     IN Vuelo.idVuelo%TYPE,
        p_idCategoria IN CategoriaAsiento.idCategoria%TYPE,
        p_idPasajeGenerado OUT Pasaje.idPasaje%TYPE,
        p_idCompra OUT Compra.idCompra%TYPE,
        p_bandera OUT NUMBER
    )
    IS
        v_numAsiento  Asiento.numAsiento%TYPE;
        v_idAvionAsignado Avion.idAvion%TYPE;
        v_fechaUsoPasaje Pasaje.fechaUsoPasaje%TYPE;
        v_id_pasaje Pasaje.idPasaje%TYPE;
        v_id_compra Compra.idCompra%TYPE;
    BEGIN
        -- 0. nivel de aislamiento serializable
        --SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
        SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

        
        -- 1. obtener el id del avion asignado a un vuelo
        SELECT idAvion 
        INTO v_idAvionAsignado 
        FROM vuelo 
        WHERE idVuelo = p_idVuelo;
        
        -- 2. Obtener asiento disponible
        v_numAsiento := obtener_asiento_libre(v_idAvionAsignado, p_idCategoria);
        
        -- 3. Insertar pasaje
        
            -- 3.1 Obtener fecha de ejecucion
            SELECT fechaEjecucion
            INTO v_fechaUsoPasaje
            FROM vuelo
            WHERE idVuelo = p_idVuelo;
        
            -- 3.2 Generar Pasaje
            INSERT INTO Pasaje (fechaCompraPasaje, fechaUsoPasaje, estadoPasaje, idVuelo, idPasajero, idUsuario, idAvion, numAsiento)
            VALUES (CURRENT_DATE, v_fechaUsoPasaje, 'Activo', p_idVuelo, p_idPasajero, p_idUsuario, v_idAvionAsignado, v_numAsiento)
            RETURNING idPasaje INTO v_id_pasaje;
            
        -- 4. Actualizar asiento como reservado
        UPDATE Asiento
        SET estadoAsiento = 'Reservado'
        WHERE idAvion = v_idAvionAsignado AND numAsiento = v_numAsiento;
        
        -- 5. Mensaje a la capa superior de confirmacion. Caso contrario se levantan excepciones
        p_bandera := 1;
        p_idPasajeGenerado := v_id_pasaje; -- para eliminar en caso de que el ciclo completo falle.
         
        -- 6. Generar registro de la compra
        INSERT INTO COMPRA (idUsuario, idPasaje, estadoCompra) 
        VALUES (p_idUsuario, v_id_pasaje,'Sin Procesar')
        RETURNING idCompra INTO v_id_compra;
        
        p_idCompra := v_id_compra;
        -- 7. Confirmar transacción
        COMMIT;
        
    EXCEPTION
        WHEN OTHERS THEN
            IF SQLCODE = -20014 THEN
                ROLLBACK;
                DELETE FROM PASAJERO WHERE IDPASAJERO = p_idPasajero;
                RAISE_APPLICATION_ERROR(-20014,'No hay asientos disponibles para el avion en la categoria ingresada');
            ELSIF SQLCODE = -20015 THEN
                ROLLBACK;
                DELETE FROM PASAJERO WHERE IDPASAJERO = p_idPasajero;
                RAISE_APPLICATION_ERROR(-20015,'Se encontró más de un asiento cuando solo se esperaba uno');
            ELSIF SQLCODE = -20016 THEN
                ROLLBACK;
                DELETE FROM PASAJERO WHERE IDPASAJERO = p_idPasajero;
                RAISE_APPLICATION_ERROR(-20016,'Error de tipo o conversión al asignar el número de asiento.');
            ELSIF SQLCODE = -20017 THEN
                ROLLBACK;
                DELETE FROM PASAJERO WHERE IDPASAJERO = p_idPasajero;
                RAISE_APPLICATION_ERROR(-20017, 'Error inesperado al obtener asiento: ' || SQLERRM);
            ELSIF SQLCODE = -20023 THEN
                ROLLBACK;
                DELETE FROM PASAJERO WHERE IDPASAJERO = p_idPasajero;
                RAISE_APPLICATION_ERROR(-20023, 'Otro usuario está reservando asientos de esta categoría. Intente más tarde.');
            ELSE
                ROLLBACK;
                DELETE FROM PASAJERO WHERE IDPASAJERO = p_idPasajero;
                RAISE_APPLICATION_ERROR(-20025, 'Error inesperado al reservar el pasaje' ||SQLERRM);
            END IF; 
        
    END RESERVAR_PASAJE;
     
    
    --procedimiento para asociar monto y factura
    PROCEDURE ASOCIAR_COMPRAS_A_FACTURA
    (
        p_idVuelo IN VUELO.IDVUELO%TYPE,
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_medioPago IN FACTURA.MEDIOPAGOFACTURA%TYPE,
        p_idFacturaGenerada OUT FACTURA.IDFACTURA%TYPE
    --    p_cursorCompra SYS_REFCURSOR
    )
    IS
    --registro de los datos para el calculo
        TYPE datos_compra_type IS RECORD 
        (
            idCompra Compra.idCompra%TYPE,
            sobrecostoCategoria CategoriaAsiento.sobrecostocategoria%TYPE,
            precioBase Vuelo.precioBaseVuelo%TYPE
        );
    --coleccion de registros
        TYPE datos_compra_table IS TABLE OF datos_compra_type;
        datos_compra_t datos_compra_table;
    -- variables del total a pagar
        v_totalBruto NUMBER :=0;
        v_totalPagar NUMBER:=0;
        v_porcentaje NUMBER:=0;
        v_idFactura FACTURA.IDFACTURA%TYPE;
        
        e_fk_violation EXCEPTION;
        PRAGMA EXCEPTION_INIT(e_fk_violation, -2291); 
    BEGIN
    -- recuperar datos de la compra
        SELECT 
            c.idCompra,
            ca.sobrecostoCategoria,
            v.precioBaseVuelo
        BULK COLLECT INTO datos_compra_t
        FROM 
            Compra c
            INNER JOIN Pasaje p 
                ON c.idPasaje = p.idPasaje
            INNER JOIN Asiento a 
                ON p.numAsiento = a.numAsiento
            INNER JOIN CategoriaAsiento ca 
                ON a.idCategoria = ca.idCategoria
            INNER JOIN vuelo v 
                ON v.idVuelo = p.idVuelo
        WHERE 
            c.estadoCompra LIKE 'Sin Procesar'
            AND c.idUsuario = p_idUsuario;
             
    -- Validar que haya registros
    IF datos_compra_t.COUNT = 0 THEN
        RAISE_APPLICATION_ERROR(-20026,'No se encontraron compras registradas');
    END IF;  
    
    --calcular el total a pagar 
        v_totalBruto := CALCULAR_SUMA_PRECIO_BASE(datos_compra_t.COUNT, datos_compra_t(1).precioBase); -- recibir los parametros de suma preciio base???
        v_porcentaje := ( datos_compra_t(1).sobrecostoCategoria * v_totalBruto ) / 100;
        v_totalPagar := v_totalBruto + v_porcentaje;
        v_totalPagar := ROUND(v_totalBruto + v_porcentaje, 2);
        
    --creacion de la factura
        INSERT INTO FACTURA (FECHAFACTURA,MONTOFACTURA,MEDIOPAGOFACTURA) VALUES (CURRENT_DATE, v_totalPagar, p_medioPago)
        RETURNING idFactura INTO v_idFactura;
        
        p_idFacturaGenerada := v_idFactura;
    --asociacion de las compras con la factura
        FOR i IN 1..datos_compra_t.COUNT LOOP
            INSERT INTO GENERARFACTURA(IDFACTURA, IDCOMPRA) VALUES (v_idFactura, datos_compra_t(i).idCompra);
        END LOOP;
      
    -- Actualizar estado de las compras a "Procesado"
    FOR i IN 1 .. datos_compra_t.COUNT LOOP
        UPDATE COMPRA
        SET estadoCompra = 'Procesado'
        WHERE idCompra = datos_compra_t(i).idCompra;  
    END LOOP;
    
    COMMIT;
    
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20027, 'No se encontraron registros de compra o vuelo.');

        WHEN VALUE_ERROR THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20035, 'Error en tipo o tamaño de valor');
    
        WHEN DUP_VAL_ON_INDEX THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20028, 'Intento de insertar un registro duplicado en factura o asociación.');
        WHEN e_fk_violation THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20029, 'Violación de integridad referencial en compra o factura.');
    
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20033, 'Error inesperado al asociar compras a factura'|| SQLERRM);

    END ASOCIAR_COMPRAS_A_FACTURA;
    
    
    --funcion para generar la factura de compra
    PROCEDURE RECUPERAR_DATOS_FACTURA (
        p_idVuelo IN VUELO.IDVUELO%TYPE,
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_medioPago IN FACTURA.MEDIOPAGOFACTURA%TYPE,
        p_infoFactura OUT SYS_REFCURSOR
    )
    IS
        v_idFacturaGenerada FACTURA.IDFACTURA%TYPE;
    BEGIN
        ASOCIAR_COMPRAS_A_FACTURA
        ( p_idVuelo, p_idUsuario, p_medioPago, v_idFacturaGenerada);
        OPEN p_infoFactura FOR
        SELECT 
            f.idFactura,
            f.fechaFactura,
            f.montoFactura,
            f.medioPagoFactura
        FROM FACTURA f
        WHERE f.idFactura = v_idFacturaGenerada;

    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20036, 'No se encontró la factura generada.');
    
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20037, 'Error inesperado al recuperar los datos de la factura'|| SQLERRM);
    END RECUPERAR_DATOS_FACTURA;
    
    -- -------------------------------------------------------------------------
    -- CANCELACION DE PASAJES
    -- -------------------------------------------------------------------------
    
    --funcion para cancelar un pasaje
    PROCEDURE CANCELAR_PASAJE (p_idPasaje IN PASAJE.IDPASAJE%TYPE)
    IS
        v_idAvion Avion.idAvion%TYPE;
        v_numAsiento Asiento.numAsiento%TYPE;
        v_estadoPasaje Pasaje.estadoPasaje%TYPE;
        
        -- Excepciones personalizadas
        ex_pasaje_no_encontrado EXCEPTION;
        PRAGMA EXCEPTION_INIT(ex_pasaje_no_encontrado, -20008);
        
        ex_pasaje_ya_inactivo EXCEPTION;
        PRAGMA EXCEPTION_INIT(ex_pasaje_ya_inactivo, -20001);
            
    BEGIN
        -- 0. Nivel de aislamiento serializable
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
        
        -- 1. Obtener datos del pasaje y verificar su existencia y estado
        SELECT idAvion, numAsiento, estadoPasaje
        INTO v_idAvion, v_numAsiento, v_estadoPasaje
        FROM Pasaje
        WHERE idPasaje = p_idPasaje;
        
        -- Verificar si el pasaje ya esta inactivo (cancelado)
        IF v_estadoPasaje = 'Inactivo' THEN
            RAISE ex_pasaje_ya_inactivo;
        END IF;
    
        -- 2. Actualizar el estado del pasaje a 'Inactivo' (Cancelado)
        UPDATE Pasaje
        SET estadoPasaje = 'Inactivo'
        WHERE idPasaje = p_idPasaje;
        
        -- 3. Actualizar el asiento a 'Disponible'
        UPDATE Asiento
        SET estadoAsiento = 'Disponible'
        WHERE idAvion = v_idAvion AND numAsiento = v_numAsiento;
        
        -- 4. Confirmar transaccion
        COMMIT;
    
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20008, 'Pasaje no encontrado.');
        
        WHEN ex_pasaje_ya_inactivo THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20101, 'El pasaje ya se encuentra inactivo.');
            
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE_APPLICATION_ERROR(-20009, 'Error inesperado al cancelar el pasaje');
    END CANCELAR_PASAJE;
    
    --procediminto para retornar el ID de una categoria segun su nombre
    FUNCTION OBTENER_ID_CATEGORIA (
        p_nombreCategoria CATEGORIAASIENTO.NOMBRECATEGORIA%TYPE
    )
    RETURN CATEGORIAASIENTO.IDCATEGORIA%TYPE
    IS
        v_idCategoria CATEGORIAASIENTO.IDCATEGORIA%TYPE;
    BEGIN
        SELECT IDCATEGORIA
        INTO v_idCategoria
        FROM CATEGORIAASIENTO
        WHERE NOMBRECATEGORIA = p_nombreCategoria;
    
        RETURN v_idCategoria;
    
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20052, 'No existe la categoría con ese nombre');
        WHEN TOO_MANY_ROWS THEN
            RAISE_APPLICATION_ERROR(-20053, 'Existen múltiples categorías con ese nombre');
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20054, 'Error inesperado al obtener ID de categoría: ' || SQLERRM);
    END OBTENER_ID_CATEGORIA;

    -- retornar la informacion de todas las categorias que existen
    PROCEDURE OBTENER_CATEGORIAS_ASIENTO_NOMBRES(
        p_resultado OUT SYS_REFCURSOR
    )
    AS
    BEGIN
        OPEN p_resultado FOR
            SELECT 
                IDCATEGORIA ID,
                NOMBRECATEGORIA NOMBRE,
                SOBRECOSTOCATEGORIA SOBRECOSTO
            FROM CATEGORIAASIENTO
            ORDER BY IDCATEGORIA;
    EXCEPTION
        WHEN OTHERS THEN
            -- Si ocurre algún error, devolver cursor vacío
            OPEN p_resultado FOR
                SELECT 
                    NULL AS IDCATEGORIA,
                    NULL AS NOMBRECATEGORIA,
                    NULL AS SOBRECOSTOCATEGORIA
                FROM DUAL
                WHERE 1=0;
    END OBTENER_CATEGORIAS_ASIENTO_NOMBRES;
    
    
    -- retorna toda la informacion de un vuelo asociado a un usuario que ya haya sido procesado y que este activo
    PROCEDURE OBTENER_VUELOS_USUARIO (
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_resultado OUT SYS_REFCURSOR
    )
    AS
    BEGIN
        OPEN p_resultado FOR
            SELECT 
                v.idVuelo,
                c.idPasaje,
                v.ciuOrigenVuelo AS origen,
                v.ciuDestinoVuelo AS destino,
                v.fechaEjecucion AS fecha,
                v.precioBaseVuelo,
                a.nombreAerolinea,
                c.estadoCompra,
                f.montoFactura
            FROM 
                COMPRA c
                INNER JOIN PASAJE p ON c.idPasaje = p.idPasaje
                INNER JOIN VUELO v ON p.idVuelo = v.idVuelo
                INNER JOIN AVION av ON v.idAvion = av.idAvion
                INNER JOIN AEROLINEA a ON av.idAerolinea = a.idAerolinea
                LEFT JOIN GENERARFACTURA gf ON gf.idCompra = c.idCompra
                LEFT JOIN FACTURA f ON f.idFactura = gf.idFactura
            WHERE 
                c.idUsuario = p_idUsuario
                AND c.estadoCompra = 'Procesado'
                AND p.estadoPasaje = 'Activo'
            ORDER BY v.fechaEjecucion DESC;
    END OBTENER_VUELOS_USUARIO;
    
   
    -- retorna cuantos pasajes procesados tiene asociado un usuario
    PROCEDURE OBTENER_CANTIDAD_PASAJES_USUARIO (
        p_idUsuario IN USUARIOREGISTRADO.IDUSUARIO%TYPE,
        p_cantidad OUT NUMBER
    )
    AS
    BEGIN
        SELECT COUNT(*)
        INTO p_cantidad
        FROM COMPRA c
        INNER JOIN PASAJE p ON c.IDPASAJE = p.IDPASAJE
        WHERE c.IDUSUARIO = p_idUsuario
          AND c.ESTADOCOMPRA = 'Procesado';  -- Solo pasajes válidos
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            p_cantidad := 0;
        WHEN OTHERS THEN
            p_cantidad := 0;
    END OBTENER_CANTIDAD_PASAJES_USUARIO;
    
-- ===============================================================
-- PROCEDIMIENTO: OBTENER_INFO_VUELO_PASAJE
-- Devuelve toda la información detallada del vuelo y pasaje
-- para mostrarla en la pantalla Uc_Informacion_Vuelo
-- ===============================================================    
    
    PROCEDURE OBTENER_INFO_VUELO_PASAJE (
        p_idPasaje IN PASAJE.IDPASAJE%TYPE,
        p_resultado OUT SYS_REFCURSOR
    )
    AS
    BEGIN
        OPEN p_resultado FOR
            SELECT
                v.codVuelo AS numeroVuelo,
                v.ciuOrigenVuelo AS origen,
                v.ciuDestinoVuelo AS destino,
                TO_CHAR(v.horaSalidaVuelo, 'HH24:MI') AS horaSalida,
                TO_CHAR(v.horaLlegadaVuelo, 'HH24:MI') AS horaLlegada,
                v.duracionVuelo AS duracion,
                v.fechaEjecucion AS fechaVuelo,
                p.estadoPasaje AS estadoPasaje,
                a.nombreAerolinea,
                av.modeloAvion,
                asn.numAsiento,
                cat.nombreCategoria AS categoria,
                cat.sobrecostoCategoria,
                c.idCompra,
                f.montoFactura,
                f.idFactura,
                f.medioPagoFactura,
                -- Campos adicionales
                'En tiempo' AS estadoVuelo,
                '9' AS puertaEmbarque,
                'A6' AS zonaEmbarque,
                3 AS numPasajeros
            FROM 
                PASAJE p
                INNER JOIN VUELO v ON p.idVuelo = v.idVuelo
                INNER JOIN AVION av ON v.idAvion = av.idAvion
                INNER JOIN AEROLINEA a ON av.idAerolinea = a.idAerolinea
                INNER JOIN ASIENTO asn ON p.idAvion = asn.idAvion AND p.numAsiento = asn.numAsiento
                INNER JOIN CATEGORIAASIENTO cat ON asn.idCategoria = cat.idCategoria
                INNER JOIN COMPRA c ON c.idPasaje = p.idPasaje
                LEFT JOIN GENERARFACTURA gf ON gf.idCompra = c.idCompra
                LEFT JOIN FACTURA f ON f.idFactura = gf.idFactura
            WHERE 
                p.idPasaje = p_idPasaje;
    
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            OPEN p_resultado FOR
                SELECT NULL AS numeroVuelo FROM dual WHERE 1=0;
    END OBTENER_INFO_VUELO_PASAJE;


    -- funcion que obtiene el sobrecosto de una categoria en particular
    FUNCTION OBTENER_SOBRECOSTO_CATEGORIA (
        p_nombreCategoria CATEGORIAASIENTO.NOMBRECATEGORIA%TYPE
    )
    RETURN CATEGORIAASIENTO.SOBRECOSTOCATEGORIA%TYPE
    IS
        v_sobrecosto CATEGORIAASIENTO.SOBRECOSTOCATEGORIA%TYPE;
    BEGIN
        SELECT SOBRECOSTOCATEGORIA
        INTO v_sobrecosto
        FROM CATEGORIAASIENTO
        WHERE NOMBRECATEGORIA = p_nombreCategoria;
    
        RETURN v_sobrecosto;
    
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20055, 'No existe la categoría con ese nombre');
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20056, 'Error inesperado al obtener el sobrecosto: ' || SQLERRM);
    END OBTENER_SOBRECOSTO_CATEGORIA;

-- ================================================================
-- REAGENDAMIENTO DE PASAJES
-- ================================================================

-- ================================================================
    -- funcion que valida si un pasaje fue reagendado demasiadas veces. retorna 1 si aun se puede reagendar, 0 en caso contrario. -1 en caso de excepcion(?
    FUNCTION VERIFICAR_LIMITE_REAGENDAMIENTO(p_idPasaje PASAJE.IDPASAJE%TYPE)
    RETURN NUMBER
    IS
        v_cantReagendamientos NUMBER;
    BEGIN
        SELECT countReagendamientos
        INTO v_cantReagendamientos
        FROM PASAJE
        WHERE idPasaje = p_idPasaje;
        
        IF v_cantReagendamientos <= 2 THEN
            RETURN 1;
        ELSE
            RETURN 0;
        END IF;
        
    EXCEPTION
        WHEN OTHERS THEN
            RETURN -1;      
    END VERIFICAR_LIMITE_REAGENDAMIENTO;
    
    --Funcion que verifica que la hora actual no sobrepase la hora de salida del vuelo
    -- de sobrepasarla, retorna 0
    --de no hacerlo retorna 1
    -- en caso de excepcion, retorna -1 (SIN TERMINAR ESTE METODO)
    FUNCTION VERIFICAR_VIGENCIA_VUELO(p_idVuelo VUELO.IDVUELO%TYPE)
    RETURN NUMBER
    IS
        v_horaSalidaVuelo ;
        v_horaActual
    BEGIN
        SELECT horaSalidaVuelo
        INTO v_horaSalidaVuelo
        FROM vuelo
        WHERE idVuelo = p_idVuelo;
        
    EXCEPTION
    END VERIFICAR_VIGENCIA_VUELO;
    
-- Metodos de transaccion
    -- cacular sobrecosto de reagendamiento. Retorna el nuevo valor a cancelar
    FUNCTION CALCULAR_SOBRECOSTO_REAGENDO (
    p_idPasaje      IN Pasaje.idPasaje%TYPE,     -- ID del pasaje a reagendar
    )
    RETURN NUMBER
    IS
        -- Variables para el cálculo
        v_fecha_vuelo_anterior Pasaje.fechaUsoPasaje%TYPE; -- Fecha del vuelo original (fechaUsoPasaje)
        v_costo_base_pasaje    NUMBER(12, 2);              -- Monto original pagado por el pasaje (de la Factura)
        v_dias_antelacion      NUMBER;                     -- Días entre HOY y la fecha del vuelo original
        v_porcentaje_costo     ReglasReagendamiento.porcentajeCosto%TYPE := 0; -- Porcentaje de costo de la regla
        v_sobrecosto_final     NUMBER(12, 2) := 0;         -- Resultado final
    
    BEGIN
        -- 1. OBTENER LA FECHA DE USO ORIGINAL DEL PASAJE Y EL MONTO DE LA FACTURA
        BEGIN
            SELECT
                v.fechaEjecucion,
                f.montoFactura
            INTO
                v_fecha_vuelo_anterior,
                v_costo_base_pasaje
            FROM
                Pasaje p
            INNER JOIN
                Vuelo v ON p.idVuelo = v.idVuelo
            INNER JOIN
                Compra c ON p.idPasaje = c.idPasaje
            INNER JOIN
                GenerarFactura gf ON c.idCompra = gf.idCompra
            INNER JOIN
                Factura f ON gf.idFactura = f.idFactura
            WHERE
                p.idPasaje = p_idPasaje;
    
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                -- Si no se encuentra el pasaje o su factura asociada, retornamos 0 y lanzamos excepción
                RAISE_APPLICATION_ERROR(-20001, 'No se encontró el Pasaje ID: ' || p_idPasaje || ' o su factura asociada.');
            WHEN OTHERS THEN
                RAISE_APPLICATION_ERROR(-20002, 'Error al obtener datos del pasaje y factura: ' || SQLERRM);
        END;
    
        -- 2. CALCULAR DÍAS DE ANTELACIÓN
        -- La antelación se calcula con la fecha actual (SYSDATE) y la fecha de uso original (fechaUsoPasaje).
        v_dias_antelacion := TRUNC(v_fecha_vuelo_anterior) - TRUNC(SYSDATE);
    
        -- Si la fecha de uso ya pasó, no se permite el reagendamiento y se asume el costo total
        IF v_dias_antelacion < 0 THEN
            RAISE_APPLICATION_ERROR(-20003, 'El vuelo original (fecha ' || TO_CHAR(v_fecha_vuelo_anterior, 'DD-MON-YYYY') || ') ya ha pasado. No se puede reagendar.');
        END IF;
    
        -- 3. DETERMINAR EL PORCENTAJE DE COSTO APLICABLE
        -- Se busca la regla de reagendamiento con los DÍAS_ANTELACION más cercanos y menores o iguales
        -- a los días de antelación calculados.
    
        BEGIN
            SELECT
                porcentajeCosto
            INTO
                v_porcentaje_costo
            FROM
                ReglasReagendamiento
            WHERE
                diasAntelacion <= v_dias_antelacion -- Días de antelación debe ser menor o igual a los días disponibles
            ORDER BY
                diasAntelacion DESC -- Selecciona la regla con la antelación más alta (menor porcentaje)
            FETCH FIRST 1 ROWS ONLY;
    
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                -- Si no hay reglas de reagendamiento que cumplan (por ejemplo, si v_dias_antelacion es muy bajo
                -- y la regla más baja es 1 día o más), se podría aplicar un porcentaje por defecto alto,
                -- o lanzar un error. Aquí lanzaremos un error indicando que no hay regla aplicable.
                RAISE_APPLICATION_ERROR(-20004, 'No existe regla de reagendamiento para una antelación de ' || v_dias_antelacion || ' días.');
            WHEN OTHERS THEN
                RAISE_APPLICATION_ERROR(-20005, 'Error al buscar reglas de reagendamiento: ' || SQLERRM);
        END;
    
        -- 4. CALCULAR EL SOBRECOSTO FINAL
        -- El sobrecosto es el porcentaje de costo de la regla aplicado al monto base de la factura.
        v_sobrecosto_final := v_costo_base_pasaje * (v_porcentaje_costo / 100);
    
        -- 5. RETORNAR EL RESULTADO
        RETURN v_sobrecosto_final;
    
    EXCEPTION
        WHEN OTHERS THEN
            -- Manejo general de excepciones
            --DBMS_OUTPUT.PUT_LINE('Error en CALCULAR_SOBRECOSTO_REAGENDO: ' || SQLERRM);
            -- Re-lanzar la excepción para que sea manejada por el código que invoca la función
            RAISE;
    
    END CALCULAR_SOBRECOSTO_REAGENDO;

    -- generar factura de reagendamiento. Retorna el id de la nueva factura. 
    -- asocia la compra originar con la nueva factura en la tabla generarFactura
    
    FUNCTION GENERAR_FACTURA_REAGENDO (
    p_idPasaje      IN Pasaje.idPasaje%TYPE,         -- ID del pasaje reagendado
    p_medioPago     IN Factura.medioPagoFactura%TYPE, -- Medio de pago (ej: 'Tarjeta', 'Efectivo')
    p_idVuelo_dummy IN Vuelo.idVuelo%TYPE,           -- DUMMY: Requerido por la firma de CALCULAR_SOBRECOSTO_REAGENDO
    p_idCategoria_dummy IN CategoriaAsiento.idCategoria%TYPE -- DUMMY: Requerido por la firma de CALCULAR_SOBRECOSTO_REAGENDO
    )
    RETURN Factura.idFactura%TYPE
    IS
        v_sobrecosto  NUMBER(12, 2);             -- Monto del sobrecosto (penalización)
        v_nueva_idFactura Factura.idFactura%TYPE; -- ID de la nueva factura generada
        v_idCompra_original Compra.idCompra%TYPE; -- ID de la Compra original asociada al Pasaje
    
    BEGIN
        -- 1. CALCULAR EL SOBRECOSTO DE PENALIZACIÓN
        -- Se invoca a la función creada anteriormente para obtener el monto de la penalización.
        v_sobrecosto := CALCULAR_SOBRECOSTO_REAGENDO(
            p_idPasaje      => p_idPasaje,
        );
    
        -- Si el sobrecosto es negativo (lo cual solo ocurriría si CALCULAR_SOBRECOSTO_REAGENDO
        -- se modifica para devolver valores negativos, lo cual no es el caso), lanzamos error.
        IF v_sobrecosto < 0 THEN
            RAISE_APPLICATION_ERROR(-20006, 'El cálculo del sobrecosto arrojó un valor no válido: ' || v_sobrecosto);
        END IF;
    
        -- 2. INSERTAR EL NUEVO REGISTRO EN LA TABLA FACTURA
        -- Factura usa una secuencia GENERATED ALWAYS AS IDENTITY (START WITH 2000)
        INSERT INTO Factura (fechaFactura, montoFactura, medioPagoFactura)
        VALUES (SYSDATE, v_sobrecosto, p_medioPago)
        RETURNING idFactura INTO v_nueva_idFactura;
    
        -- 3. OBTENER LA ID DE COMPRA ORIGINAL
        BEGIN
            SELECT idCompra
            INTO v_idCompra_original
            FROM Compra
            WHERE idPasaje = p_idPasaje;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                -- Un pasaje siempre debe tener una compra asociada.
                RAISE_APPLICATION_ERROR(-20007, 'No se encontró el registro de Compra para el Pasaje ID: ' || p_idPasaje);
        END;
    
        -- 4. RELACIONAR LA NUEVA FACTURA CON LA COMPRA ORIGINAL
        -- Se inserta un registro en la tabla de relación GenerarFactura.
        -- Esto permite saber que esta nueva Factura es un cargo adicional (reagendamiento)
        -- a la compra original.
        INSERT INTO GenerarFactura (idFactura, idCompra)
        VALUES (v_nueva_idFactura, v_idCompra_original);
    
        -- 5. CONFIRMAR LA TRANSACCIÓN (COMMIT implícito si es función autónoma o COMMIT explícito si no lo es)
        -- Como esta función realiza DML, se recomienda que el COMMIT lo haga el procedimiento que la invoca.
        -- Para este ejemplo, y si no hay un procedimiento superior, forzamos el COMMIT.
        COMMIT;
    
        -- 6. RETORNAR EL ID DE LA NUEVA FACTURA
        RETURN v_nueva_idFactura;
    
    EXCEPTION
        WHEN OTHERS THEN
            -- Si algo falla, se realiza ROLLBACK y se lanza la excepción.
            ROLLBACK;
            --DBMS_OUTPUT.PUT_LINE('Error en GENERAR_FACTURA_REAGENDO: ' || SQLERRM);
            RAISE;
    
    END GENERAR_FACTURA_REAGENDO;
-- Metodos de 
 
END GESTION_PASAJES;


