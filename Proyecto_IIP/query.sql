--EL QUERY ES CAPAZ DE EJECUTARSE DE UNA SOLA VEZ
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
	Hora_Ingreso TIME (0) NOT NULL
		DEFAULT GETDATE(),
	Hora_Salida TIME (0),

	Fecha DATE NOT NULL
		DEFAULT GETDATE(),
	Pago DECIMAL(10,2)
)
GO

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


INSERT INTO Cobro.Cobro (Id_Vehiculo,Hora_Ingreso,Hora_Salida)
	VALUES
	(5,'2:00',NULL),
	(3,'4:00',NULL)

GO

SELECT * FROM Cobro.Cobro
GO

/*
SELECT CONVERT (CHAR(5),Hora_Ingreso,108) AS Hora_Entrada,
CONVERT (CHAR(5),Hora_Salida,108) AS Hora_Salida,
CAST(DATEDIFF(MINUTE,Hora_Ingreso,Hora_Salida)/60 AS CHAR(2))+':'+
CAST(DATEDIFF(MINUTE,Hora_Ingreso,Hora_Salida)%60 AS CHAR(2)) AS TIEMPO,
Fecha AS FECHA
FROM Cobro.Cobro
WHERE Fecha=CAST(GETDATE() AS DATE)
GO
*/


--PROCEDIMIENTOS ALMACENADOS
CREATE PROC Vehiculo.SP_MostrarVehiculo
AS BEGIN
	SELECT a.Placa AS PLACA,b.Nombre AS TIPO, a.Id_Vehiculo, b.Id_Tipo,a.Placa FROM Vehiculo.Vehiculo a INNER JOIN Vehiculo.Tipo_Vehiculo b 
	ON a.Id_Tipo=b.Id_Tipo
END
GO

--Procedimiento para mostrar tipos de vehiculo
CREATE PROC Vehiculo.SP_MostrarTipo
AS BEGIN
	SELECT * FROM Vehiculo.Tipo_Vehiculo
END
GO

--Procedimiento para mostrar que vehiculos estan en el estacionamiento
CREATE PROC Vehiculo.SP_MostrarEstacionamiento
AS BEGIN
	SELECT a.Placa,b.Nombre AS Tipo_Vehiculo, CONVERT(CHAR(5),c.Hora_Ingreso,108) AS Hora_Ingreso 
	FROM Vehiculo.Vehiculo a INNER JOIN Vehiculo.Tipo_Vehiculo b
	ON a.Id_Tipo = b.Id_Tipo INNER JOIN Cobro.Cobro c ON a.Id_Vehiculo = c.Id_Vehiculo  
	WHERE c.Hora_Salida IS NULL
END
GO

--PROCEDIMIENTO PARA INGRESO AL PARQUEO
CREATE PROC Vehiculo.SP_IngresoVehiculo
@Placa NVARCHAR(8),
@Tipo INT 
AS
DECLARE @IdVehiculo INT
BEGIN TRANSACTION
	IF NOT EXISTS(SELECT * FROM Vehiculo.Vehiculo WHERE Placa=@Placa)
		BEGIN
			INSERT INTO Vehiculo.Vehiculo(Id_Tipo,Placa) VALUES(@Tipo,@Placa)
			SELECT @IdVehiculo =Id_Vehiculo FROM Vehiculo.Vehiculo WHERE Placa = @Placa
			INSERT INTO Cobro.Cobro (Id_Vehiculo,Hora_Ingreso) VALUES (@IdVehiculo,GETDATE())
		END
	ELSE
		BEGIN
			/*
			Este SELECT nos servira para verificar si se ingresa otro tipo de vehiculo de algun vehiculo
			ya ingresado previamente en el estacionamiento, en caso de ser asi el insert nos arrojara un
			error ya que no habrá ningun valor existente(osea NULL) para la variable @idvehiculo
			*/
			SELECT @IdVehiculo =Id_Vehiculo FROM Vehiculo.Vehiculo WHERE Placa = @Placa AND Id_Tipo=@Tipo
			/*
			La siguiente condicion nos permite verificar si el vehiculo ya se encuentra en el 
			estacionamiento
			*/
			IF NOT EXISTS(SELECT * FROM Cobro.Cobro a INNER JOIN Vehiculo.Vehiculo b
			ON a.Id_Vehiculo= b.Id_Vehiculo WHERE a.Id_Vehiculo=@IdVehiculo AND b.Id_Tipo=@Tipo 
			AND a.Hora_Salida IS NULL)
				BEGIN
					INSERT INTO Cobro.Cobro (Id_Vehiculo,Hora_Ingreso) VALUES (@IdVehiculo,GETDATE())
				END
		END
COMMIT TRANSACTION
GO

EXEC Vehiculo.SP_IngresoVehiculo 'AND6666',5
GO
--SUPER PROCEDIMIENTO PARA GENERAR EL COBRO!!

CREATE PROC Cobro.SP_SuperCobro
@Placa VARCHAR(8) 
AS 
	--Variables necesarias
	DECLARE @total DECIMAL(10,2)
	DECLARE @Horas INT
	DECLARE @Minutos INT
	DECLARE @Tipo NVARCHAR(20)
	DECLARE @Dnulo CHAR(10)
	DECLARE @IdVehiculo INT

	SELECT @IdVehiculo = Id_Vehiculo FROM Vehiculo.Vehiculo WHERE Placa=@Placa
	BEGIN TRANSACTION
	--Verificamos que su hora de salida sea nula
	SELECT @Dnulo= Hora_Salida FROM Cobro.Cobro WHERE Id_Vehiculo=@IdVehiculo
	--Actualizamos la hora de salida
	UPDATE Cobro.Cobro SET Hora_Salida = GETDATE() WHERE Id_Vehiculo=@idvehiculo
	--OBTENER EL TIEMPO QUE ESTUVO EN EL PARQUEO
	--Obtenemos las horas
	SELECT @Horas = DATEDIFF(MINUTE,Hora_Ingreso,Hora_Salida)/60 
	FROM Cobro.Cobro WHERE Id_Vehiculo=@IdVehiculo
	--Obtenemos los minutos
	SELECT @Minutos = DATEDIFF(MINUTE,Hora_Ingreso,Hora_Salida)%60 
	FROM Cobro.Cobro WHERE Id_Vehiculo=@IdVehiculo
	--Obtenemos el tipo de vehiculo
	SELECT @Tipo = a.Nombre FROM Vehiculo.Tipo_Vehiculo a INNER JOIN Vehiculo.Vehiculo b
	ON a.Id_Tipo=b.Id_Tipo
	INNER JOIN Cobro.Cobro c
	ON b.Id_Vehiculo=c.Id_Vehiculo WHERE c.Id_Vehiculo=@IdVehiculo
	--Condiciones para realizar el cobro de acuerdo a la cantidad de horas
	IF((@Horas=0 AND @Minutos<=59) OR (@Horas=1 AND @Minutos=0))
	BEGIN
		SET @total=20
	END
	ELSE IF((@Horas=1 AND @Minutos<=59) OR (@Horas=2 AND @Minutos=0))
	BEGIN
		SET @total=20+10
	END
	ELSE IF((@Horas=2 AND @Minutos<=59) OR (@Horas=3 AND @Minutos=0))
	BEGIN
		SET @total=20*3
	END
	ELSE IF((@Horas=3 AND @Minutos<=59) OR (@Horas=4 AND @Minutos=0))
	BEGIN
		SET @total=(20*3)+10
	END
	ELSE IF(@Horas>=4)
	BEGIN
		SET @total=15*@Horas
	END
	--Condiciones para realizar el cobro de acuerdo al tipo de vehiculo
	IF(@Tipo='Camion' OR @Tipo='Bus' OR @Tipo = 'Rastra')
		BEGIN
			SET @total=@total*2
		END

	IF(@Tipo='Motocicleta')
		BEGIN
			SET @total=@total*2
		END
	UPDATE Cobro.Cobro SET Pago = @total WHERE Id_Vehiculo=@IdVehiculo

	SELECT CAST(@Horas as CHAR(2)) AS HORAS ,CAST(@Minutos as CHAR(2)) AS MINUTOS, 
	+'$.'+CAST (@total AS char)  AS TOTAL, CAST (@Tipo AS NVARCHAR) AS TIPO
	IF(@Dnulo IS NULL)
		BEGIN
			COMMIT 
		END
	ELSE
		BEGIN
			ROLLBACK
		END
GO

--REGISTROS DE PRUEBA
EXEC Cobro.SP_SuperCobro 'AND6666'
EXEC Cobro.SP_SuperCobro 'VND1111'
GO

--PROCEDIMIENTO PARA EL REPORTE DE INGRESOS
CREATE PROC Cobro.SP_ReporteGeneral
AS BEGIN
	SELECT a.Placa as PLACA,CONVERT (CHAR(5),Hora_Ingreso,108) AS HORA_ENTRADA,
	CONVERT (CHAR(5),Hora_Salida,108) AS HORA_SALIDA,
	CAST(DATEDIFF(MINUTE,Hora_Ingreso,Hora_Salida)/60 AS CHAR(2))+':'+
	CAST(DATEDIFF(MINUTE,Hora_Ingreso,Hora_Salida)%60 AS CHAR(2)) AS TIEMPO,Pago AS TOTAL,
	Fecha AS FECHA
	FROM Vehiculo.Vehiculo a INNER JOIN Vehiculo.Tipo_Vehiculo b ON a.Id_Tipo=b.Id_Tipo
	INNER JOIN Cobro.Cobro c ON a.Id_Vehiculo = c.Id_Vehiculo
	WHERE Hora_Salida IS NOT NULL
END
GO

SELECT * FROM Cobro.Cobro
GO
SELECT * FROM Vehiculo.Vehiculo
GO


