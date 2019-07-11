USE tempdb
GO
--VERIFICAMOS QUE LA BASE DE DATOS EXISTA
IF EXISTS(SELECT * FROM sys.databases WHERE name='DB_ESTACIONAMIENTO')
BEGIN
	DROP DATABASE DB_ESTACIONAMIENTO
END
GO

CREATE DATABASE DB_ESTACIONAMIENTO
GO

USE DB_ESTACIONAMIENTO
GO

--CREANDO LOS SCHEMAS QUE SERAN DE AYUDA
CREATE SCHEMA Vehiculo
GO
CREATE SCHEMA Cobro
GO
--CREACION DE LAS TABLAS
--Creacion de la tabla tipo_vehiculo
CREATE TABLE Vehiculo.Tipo_Vehiculo(
	Id_Tipo INT IDENTITY (1,1) NOT NULL
		CONSTRAINT PK_Id_Tipo PRIMARY KEY CLUSTERED,
	Nombre NVARCHAR (20) NOT NULL
)
GO

--Creacion de la tabla vehiculo
CREATE TABLE Vehiculo.Vehiculo(
	Id_Vehiculo INT IDENTITY (1,1) NOT NULL
		CONSTRAINT PK_Id_Vehiculo PRIMARY KEY CLUSTERED,
	Placa NVARCHAR(8) NOT NULL,
	Id_Tipo INT NOT NULL
)
GO

--Creacion de la tabla cobro
CREATE TABLE Cobro.Cobro(
	Id_Cobro INT IDENTITY (1,1) NOT NULL
		CONSTRAINT PK_Id_Cobro PRIMARY KEY CLUSTERED,
	Id_Vehiculo INT NOT NULL,
	Hora_Ingreso TIME NOT NULL
		DEFAULT GETDATE(),
	Hora_Salida TIME,
	Fecha DATETIME NOT NULL
)

--CREACION DE LAS LLAVES FORANEAS
ALTER TABLE Vehiculo.Vehiculo
	ADD CONSTRAINT
	FK_Un_TipoTiene_Varios_Vehiculos
	FOREIGN KEY (Id_Tipo) REFERENCES Vehiculo.Tipo_Vehiculo(Id_Tipo)
	ON UPDATE CASCADE
	ON DELETE NO ACTION
GO
ALTER TABLE Cobro.Cobro
	ADD CONSTRAINT
	Fk_Un_VehiculoTiene_Muchos_Cobros
	FOREIGN KEY (Id_Vehiculo) REFERENCES Vehiculo.Vehiculo(Id_Vehiculo)
	ON UPDATE CASCADE
	ON DELETE NO ACTION
GO
INSERT INTO Vehiculo.Tipo_Vehiculo
VALUES
	('Turismo'),
	('Pick-up'),
	('Camioneta'),
	('Camion'),
	('Bus'),
	('Rastra'),
	('Motocicleta')
GO

INSERT INTO Vehiculo.Vehiculo
VALUES
	('AND1234',1),
	('BND5678',2),
	('VND1111',3),
	('CND2222',4),
	('DND3333',5),
	('END4444',6),
	('TND5555',7),
	('FND1234',1),
	('GND5678',2),
	('HND1111',3),
	('JND2222',4),
	('KND3333',5),
	('LND4444',6),
	('QND5555',7),
	('WND6666',1),
	('RED7777',2),
	('YND8888',3),
	('TND9999',4),
	('YND6512',5),
	('UND6112',6),
	('ZND9643',7)
GO

--PROCEDIMIENTOS ALMACENADOS

CREATE PROC Vehiculo.SP_MostrarVehiculo
AS BEGIN
	SELECT a.Placa AS PLACA,b.Nombre AS TIPO, a.Id_Vehiculo, b.Id_Tipo,a.Placa FROM Vehiculo.Vehiculo a INNER JOIN Vehiculo.Tipo_Vehiculo b 
	ON a.Id_Tipo=b.Id_Tipo
END
GO

SELECT * FROM Vehiculo.Vehiculo
GO

