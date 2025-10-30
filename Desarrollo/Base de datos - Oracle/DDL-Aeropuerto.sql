DROP TABLE GenerarFactura CASCADE CONSTRAINTS;
DROP TABLE Factura CASCADE CONSTRAINTS;
DROP TABLE Compra CASCADE CONSTRAINTS;
DROP TABLE Pasaje CASCADE CONSTRAINTS;

DROP TABLE Vuelo CASCADE CONSTRAINTS;
DROP TABLE Avion CASCADE CONSTRAINTS;
DROP TABLE PuertaEmbarque CASCADE CONSTRAINTS;
DROP TABLE ZonaEmbarque CASCADE CONSTRAINTS;
DROP TABLE Aerolinea CASCADE CONSTRAINTS;

DROP TABLE UsuarioRegistrado CASCADE CONSTRAINTS;
DROP TABLE CategoriaAsiento CASCADE CONSTRAINTS;
DROP TABLE Pasajero CASCADE CONSTRAINTS;

DROP TABLE Asiento CASCADE CONSTRAINTS;

DROP TABLE ReglasReagendamiento CASCADE CONSTRAINTS;


-- SCRIPT DE CREACION DE LA BASE DE DATOS (DDL)

-- =============================
-- TABLAS DE PERSONAS
-- =============================

CREATE TABLE Pasajero (
    idPasajero       INTEGER NOT NULL,
    tipoIdPasajero     VARCHAR2(50) NOT NULL,
    nombrePasajero   VARCHAR2(50) NOT NULL,
    apellidoPasajero VARCHAR2(50) NOT NULL,
    correoPasajero  VARCHAR2(100) NOT NULL, 
  --  generoPasajero  VARCHAR2(10) NOT NULL,
  --  fechaNacPasajero  DATE NOT NULL,
  --  direccionPasajero          VARCHAR2(50) NOT NULL,
  --  observacionPasajero     VARCHAR2(100),
  --  nacionalidadPasajero   VARCHAR2(50) NOT NULL,
    
    CONSTRAINT pk_pasajero PRIMARY KEY (idPasajero),
    constraint uq_pasajero_correo unique (correoPasajero),
  --  constraint ckc_generoPasajero check (generoPasajero in ('Masculino', 'Femenino')),
    constraint ckc_tipoIdPasajero check (tipoIdPasajero in ('T.I.', 'C.C.', 'C.E.', 'P.P.')),
    constraint ckc_idPasajero check (idPasajero>0)
);

CREATE TABLE categoriaAsiento(
    idCategoria INTEGER NOT NULL,
    nombreCategoria VARCHAR2(30) NOT NULL,
    sobrecostoCategoria NUMBER NOT NULL,

    CONSTRAINT pk_Categoria PRIMARY KEY (idCategoria),
    constraint ckc_idCategoria check (idCategoria>0),
    constraint ckc_sobrecostoCategoria check (sobrecostoCategoria>0)
); 

--CREATE TABLE telefono(
  --  idTelefono INTEGER NOT NULL,
  --  numeroTelefono NUMBER NOT NULL,
    
  --  CONSTRAINT pk_Telefono PRIMARY KEY (idTelefono),
  --  constraint ckc_numeroTelefono check (numeroTelefono>0),
  --  constraint ckc_idTelefono check (idTelefono>0)
--);

CREATE TABLE UsuarioRegistrado (
    idUsuario       INTEGER GENERATED ALWAYS AS IDENTITY,
    docIdUsuario    NUMBER NOT NULL,
    tipoIdUsuario         VARCHAR2(50) NOT NULL,
    nombreUsuario   VARCHAR2(50) NOT NULL,
    apellidoUsuario VARCHAR2(50) NOT NULL,
    correoUsuario   VARCHAR2(100) NOT NULL, 
    generoUsuario    VARCHAR2(30) NOT NULL,
    fechaNacUsuario  DATE NOT NULL,
    nacionalidadUsuario    VARCHAR2(50) NOT NULL,
    estadoUsuario VARCHAR2(30) NOT NULL,
    usuarioAcceso VARCHAR2(30) NOT NULL,
    contraseniaUsuario VARCHAR2(20) NOT NULL,
    direccionUsuario       VARCHAR2(50) NOT NULL,
    observacionUsuario     VARCHAR2(100),
    telefonoUsuario INTEGER NOT NULL, 
    
    CONSTRAINT pk_UsuarioRegistrado PRIMARY KEY (idUsuario),
    constraint uq_usuarioRegistrado_correo unique (correoUsuario),
    constraint uq_usuarioRegistrado_usuarioAcceso unique (usuarioAcceso),
    constraint uq_usuarioRegistrado_docIdUsuario unique (docIdUsuario),
    constraint ckc_idUsuario check (idUsuario>0),
    constraint ckc_generoUsuario check (generoUsuario in ('Masculino', 'Femenino','No binario', 'Prefiero no decirlo', 'Otro')),
    constraint ckc_tipoIdUsuario check (tipoIdUsuario in ('T.I.', 'C.C.', 'C.E.', 'P.P.')),
    constraint ckc_estadoUsuario check (estadoUsuario in ('Activo', 'Inactivo')),
    constraint ckc_telefonoUsuario check (telefonoUsuario > 0)
);
-- =============================
-- INFRAESTRUCTURA
-- =============================
CREATE TABLE Aerolinea (
    idAerolinea     INTEGER NOT NULL,
    nombreAerolinea VARCHAR2(50) NOT NULL,
    paisOrigenAerolinea VARCHAR2(50) NOT NULL,
    contactoAerolinea VARCHAR2(100) NOT NULL,
    
    CONSTRAINT pk_Aerolinea PRIMARY KEY (idAerolinea),
    constraint ckc_idAerolinea check (idAerolinea>0)
);

CREATE TABLE ZonaEmbarque (
    idZEmbarque         INTEGER NOT NULL,
    nombreZEmbarque     VARCHAR2(50) NOT NULL,
    ubicacionZEmbarque  VARCHAR2(50) NOT NULL,
    capacidadPasajerosZ NUMBER NOT NULL,
    idAerolinea INTEGER NOT NULL,
    
    CONSTRAINT pk_ZonaEmbarque PRIMARY KEY (idZEmbarque),
    constraint fk_AeroZEmb foreign key (idAerolinea) references Aerolinea (idAerolinea),
    constraint ckc_idZEmbarque check (idZEmbarque>0),
    constraint ckc_capacidadPasajerosZ check (capacidadPasajerosZ>0)
);

CREATE TABLE PuertaEmbarque (
    idPuerta        INTEGER NOT NULL,
    numeroPuerta    VARCHAR2(10) NOT NULL,
    ubicacionPuerta VARCHAR2(50) NOT NULL,
    estadoPuerta    VARCHAR2(20) NOT NULL,
    idZEmbarque     INTEGER NOT NULL,

    CONSTRAINT pk_PuertaEmbarque PRIMARY KEY (idPuerta),
    constraint fk_ZEmbPEmb foreign key (idZEmbarque) references ZonaEmbarque (idZEmbarque),
    constraint ckc_idPuerta check (idPuerta>0),
    constraint ckc_estadoPuerta check (estadoPuerta in ('Libre', 'Ocupada', 'Mantenimiento'))
);

CREATE TABLE Avion (
    idAvion             INTEGER NOT NULL,
    matriculaAvion      VARCHAR2(20) NOT NULL,
    modeloAvion         VARCHAR2(50) NOT NULL,
    capacidadCargaAvion     NUMBER NOT NULL,
    capacidadPasajeroAvion  NUMBER NOT NULL,
    idAerolinea         INTEGER NOT NULL,
    
    
    CONSTRAINT pk_Avion PRIMARY KEY (idAvion),
    constraint fk_AeroAvio foreign key (idAerolinea) references Aerolinea (idAerolinea),
    constraint uq_Avion unique (matriculaAvion),
    constraint ckc_idAvion check (idAvion>0),
    constraint ckc_capacidadCargaAvion check (capacidadCargaAvion>0),
    constraint ckc_capacidadPasajeroAvion check (capacidadPasajeroAvion>0)

);

CREATE TABLE Asiento (
    numAsiento NUMBER NOT NULL,
    idAvion INTEGER NOT NULL,
    idCategoria INTEGER NOT NULL,
    estadoAsiento VARCHAR2(10),
    
    constraint pk_Asiento PRIMARY KEY (numAsiento, idAvion),
    constraint fk_AvioAsie foreign key (idAvion) references Avion (idAvion),
    constraint fk_cateAsie foreign key (idCategoria) references CategoriaAsiento (idCategoria),
    constraint ckc_numAsiento check (numAsiento>0),
    constraint ckc_estadoAsiento check(estadoAsiento IN('Disponible','Reservado'))
);
/*
CREATE TABLE Vuelo (
    idVuelo         INTEGER NOT NULL,
   -- origenVuelo     VARCHAR2(50) NOT NULL,
    ciuOrigenVuelo VARCHAR2(50) NOT NULL,
    paisOrigenVuelo VARCHAR2(50) NOT NULL,
  --  destinoVuelo    VARCHAR2(50) NOT NULL,
    ciuDestinoVuelo VARCHAR2(50) NOT NULL,
    paisDestinoVuelo VARCHAR2(50) NOT NULL,
    precioBaseVuelo NUMBER NOT NULL,
    estadoVuelo     VARCHAR2(15) NOT NULL,
    fechaEjecucion  TIMESTAMP NOT NULL,
    idZEmbarque     INTEGER NOT NULL,
    idPuerta        INTEGER NOT NULL,
    idAvion        INTEGER NOT NULL,

    CONSTRAINT pk_Vuelo PRIMARY KEY (idVuelo),
    constraint fk_ZEmbVuel foreign key (idZEmbarque) references ZonaEmbarque (idZEmbarque),
    constraint fk_PuerVuel foreign key (idPuerta) references PuertaEmbarque (idPuerta),
    constraint fk_AvioVuel foreign key (idAvion) references Avion (idAvion),
    constraint ckc_idVuelo check (idVuelo>0),
    constraint ckc_precioBaseVuelo check (precioBaseVuelo > 0),
    constraint ckc_estadoVuelo check (estadoVuelo in ('Adelantado', 'En tiempo', 'Atrasado'))
);
*/

CREATE TABLE Vuelo (
    idVuelo             INTEGER NOT NULL,
    codVuelo            VARCHAR2(10) NOT NULL, -- nuevo: código del vuelo (ej. AV123)
    ciuOrigenVuelo      VARCHAR2(50) NOT NULL,
    paisOrigenVuelo     VARCHAR2(50) NOT NULL,
    ciuDestinoVuelo     VARCHAR2(50) NOT NULL,
    paisDestinoVuelo    VARCHAR2(50) NOT NULL,
    precioBaseVuelo     NUMBER NOT NULL,
    estadoVuelo         VARCHAR2(15) NOT NULL,
    fechaEjecucion      DATE NOT NULL,
    horaSalidaVuelo     DATE NOT NULL,         -- nuevo: hora de salida
    horaLlegadaVuelo    DATE NOT NULL,         -- nuevo: hora de llegada
    duracionVuelo       INTERVAL DAY TO SECOND, -- nuevo: duración total del vuelo
    idZEmbarque         INTEGER NOT NULL,
    idPuerta            INTEGER NOT NULL,
    idAvion             INTEGER NOT NULL,

    CONSTRAINT pk_Vuelo PRIMARY KEY (idVuelo),
    CONSTRAINT fk_ZEmbVuel FOREIGN KEY (idZEmbarque) REFERENCES ZonaEmbarque (idZEmbarque),
    CONSTRAINT fk_PuerVuel FOREIGN KEY (idPuerta) REFERENCES PuertaEmbarque (idPuerta),
    CONSTRAINT fk_AvioVuel FOREIGN KEY (idAvion) REFERENCES Avion (idAvion),
    CONSTRAINT ckc_idVuelo CHECK (idVuelo > 0),
    CONSTRAINT ckc_precioBaseVuelo CHECK (precioBaseVuelo > 0),
    CONSTRAINT ckc_estadoVuelo CHECK (estadoVuelo IN ('Adelantado', 'En tiempo', 'Atrasado'))
);


-- =============================
-- TRANSACCIONES DE COMPRA
-- =============================

CREATE TABLE Pasaje (
    idPasaje        INTEGER GENERATED ALWAYS AS IDENTITY,
    fechaCompraPasaje DATE NOT NULL,
    fechaUsoPasaje DATE NOT NULL,
    estadoPasaje    VARCHAR2(20) NOT NULL,
    idVuelo         INTEGER NOT NULL,
    idPasajero      INTEGER NOT NULL,
    idUsuario       INTEGER NOT NULL,
    idAvion         INTEGER NOT NULL,
    numAsiento      NUMBER NOT NULL,
     
    CONSTRAINT pk_Pasaje PRIMARY KEY (idPasaje),
    constraint fk_VuelPasaj foreign key (idVuelo) references Vuelo (idVuelo),
    constraint fk_PasaPasaj foreign key (idPasajero) references Pasajero (idPasajero),
    constraint fk_UsuaPasaj foreign key (idUsuario) references UsuarioRegistrado (idUsuario),
    constraint fk_AsiePasaj foreign key (numAsiento, idAvion) references Asiento (numAsiento, idAvion),
    constraint ckc_idPasaje check (idPasaje>0),
    constraint ckc_estadoPasaje check (estadoPasaje IN ('Activo','Inactivo'))
);

CREATE TABLE Compra (
    idCompra        INTEGER GENERATED ALWAYS AS IDENTITY,
    idUsuario       INTEGER NOT NULL,
    idPasaje        INTEGER NOT NULL,
    estadoCompra VARCHAR2(30) NOT NULL,
    
    CONSTRAINT pk_Compra PRIMARY KEY (idCompra),
    constraint fk_PasaComp foreign key (idPasaje) references Pasaje (idPasaje),
    constraint fk_UsuaComp foreign key (idUsuario) references UsuarioRegistrado (idUsuario),
    constraint uq_Compra unique (idPasaje),
    constraint ckc_idCompra check (idCompra>0),
    constraint ckc_estadoCompa check (estadoCompra IN ('Procesado','Sin Procesar'))
);

CREATE TABLE Factura (
    idFactura       INTEGER GENERATED ALWAYS AS IDENTITY
    (START WITH 2000 INCREMENT BY 1 MINVALUE 2000 MAXVALUE 20000),
    fechaFactura    DATE NOT NULL,
    montoFactura    NUMBER(12,2) NOT NULL,
    medioPagoFactura VARCHAR2(20) NOT NULL,
--    idCompra        INTEGER NOT NULL, 
    
    CONSTRAINT pk_Factura PRIMARY KEY (idFactura),
 --   constraint fk_CompFact foreign key (idCompra) references Compra (idCompra),
 --   constraint uq_Factura unique (idCompra),
    constraint ckc_idFactura check (idFactura>0),
    constraint ckc_montoFactura check (montoFactura>=0)
    
);

CREATE TABLE GenerarFactura(
     idGenerarFactura INTEGER GENERATED ALWAYS AS IDENTITY,
     idFactura       INTEGER NOT NULL,
     idCompra        INTEGER NOT NULL,
  
    CONSTRAINT pk_GenerarFactura PRIMARY KEY (idGenerarFactura),
    constraint fk_CompGen foreign key (idCompra) references Compra (idCompra),
    constraint fk_facGen foreign key (idFactura) references Factura (idFactura),
    constraint uq_GenerarFactura unique (idCompra,idFactura)

);

-- =============================
-- REGLAS DE REAGENDAMIENTO
-- =============================

CREATE TABLE ReglasReagendamiento (
    idRegla         INTEGER NOT NULL,
    diasAntelacion  NUMBER NOT NULL,
    porcentajeCosto NUMBER(2) NOT NULL,
    
    CONSTRAINT pk_ReglasReagendamiento PRIMARY KEY (idRegla),
    constraint ckc_idRegla check (idRegla>0),
    constraint ckc_diasAntelacion check (diasAntelacion>=0),
    constraint ckc_porcentajeCosto check (porcentajeCosto>0)
);


commit


-- =============================
-- AEROLÍNEA
-- =============================
INSERT INTO Aerolinea (
    idAerolinea, nombreAerolinea, paisOrigenAerolinea, contactoAerolinea
) VALUES (
    1, 'Avianca', 'Colombia', 'contacto@avianca.com'
);

-- =============================
-- ZONA DE EMBARQUE
-- =============================
INSERT INTO ZonaEmbarque (
    idZEmbarque, nombreZEmbarque, ubicacionZEmbarque, capacidadPasajerosZ, idAerolinea
) VALUES (
    1, 'Zona A', 'Terminal 1', 200, 1
);

-- =============================
-- PUERTA DE EMBARQUE
-- =============================
INSERT INTO PuertaEmbarque (
    idPuerta, numeroPuerta, ubicacionPuerta, estadoPuerta, idZEmbarque
) VALUES (
    1, 'A1', 'Terminal 1 - Zona A', 'Libre', 1
);

-- =============================
-- AVIÓN
-- =============================
INSERT INTO Avion (
    idAvion, matriculaAvion, modeloAvion, capacidadCargaAvion,
    capacidadPasajeroAvion, idAerolinea
) VALUES (
    1, 'HK-4821', 'Airbus A320', 5000, 180, 1
);

-- Vuelo 1: Bogotá → Lima
INSERT INTO Vuelo (
    idVuelo, codVuelo,
    ciuOrigenVuelo, paisOrigenVuelo,
    ciuDestinoVuelo, paisDestinoVuelo,
    precioBaseVuelo, estadoVuelo,
    fechaEjecucion, horaSalidaVuelo, horaLlegadaVuelo, duracionVuelo,
    idZEmbarque, idPuerta, idAvion
) VALUES (
    1, 'AV1001',
    'Bogotá', 'Colombia',
    'Lima', 'Perú',
    850000, 'En tiempo',
    TO_DATE('2025-12-20', 'YYYY-MM-DD'),
    TO_DATE('2025-12-20 14:30', 'YYYY-MM-DD HH24:MI'),
    TO_DATE('2025-12-20 17:45', 'YYYY-MM-DD HH24:MI'),
    INTERVAL '3:15' HOUR TO MINUTE,
    1, 1, 1
);

-- Vuelo 2: Lima → Bogotá
INSERT INTO Vuelo (
    idVuelo, codVuelo,
    ciuOrigenVuelo, paisOrigenVuelo,
    ciuDestinoVuelo, paisDestinoVuelo,
    precioBaseVuelo, estadoVuelo,
    fechaEjecucion, horaSalidaVuelo, horaLlegadaVuelo, duracionVuelo,
    idZEmbarque, idPuerta, idAvion
) VALUES (
    2, 'AV1002',
    'Lima', 'Perú',
    'Bogotá', 'Colombia',
    900000, 'Adelantado',
    TO_DATE('2025-12-21', 'YYYY-MM-DD'),
    TO_DATE('2025-12-21 10:25', 'YYYY-MM-DD HH24:MI'),
    TO_DATE('2025-12-21 13:40', 'YYYY-MM-DD HH24:MI'),
    INTERVAL '3:15' HOUR TO MINUTE,
    1, 1, 1
);


INSERT INTO CategoriaAsiento (idCategoria, nombreCategoria, sobrecostoCategoria)
VALUES (1, 'Economica', 1);

INSERT INTO CategoriaAsiento (idCategoria, nombreCategoria, sobrecostoCategoria)
VALUES (2, 'Ejecutiva', 15);

INSERT INTO CategoriaAsiento (idCategoria, nombreCategoria, sobrecostoCategoria)
VALUES (3, 'Primera Clase', 30);


BEGIN
  FOR i IN 1..4 LOOP
    INSERT INTO Asiento VALUES (i, 1, 1, 'Disponible');
  END LOOP;

  FOR i IN 5..8 LOOP
    INSERT INTO Asiento VALUES (i, 1, 2, 'Disponible');
  END LOOP;

  FOR i IN 9..10 LOOP
    INSERT INTO Asiento VALUES (i, 1, 3, 'Disponible');
  END LOOP;
  COMMIT;
END;



