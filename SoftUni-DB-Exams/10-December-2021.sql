CREATE DATABASE Airport

USE [Airport]

--01.DDL
CREATE TABLE [Passengers](
[Id] INT PRIMARY KEY IDENTITY ,
[FullName] VARCHAR(100) UNIQUE NOT NULL,
[Email] VARCHAR(50) UNIQUE NOT NULL
)

CREATE TABLE [Pilots](
[Id] INT PRIMARY KEY IDENTITY ,
[FirstName] VARCHAR(30) UNIQUE NOT NULL,
[LastName] VARCHAR (30) UNIQUE NOT NULL,
[Age] TINYINT NOT NULL CHECK(Age BETWEEN 21 AND 62),
[Rating] FLOAT CHECK(Rating BETWEEN 0 AND 10)
)

CREATE TABLE [AircraftTypes](
[Id] INT PRIMARY KEY IDENTITY,
[TypeName] VARCHAR(30) UNIQUE NOT NULL
)

CREATE TABLE [Aircraft](
[Id] INT PRIMARY KEY IDENTITY,
[Manufacturer] VARCHAR(25) NOT NULL,
[Model] VARCHAR(30) NOT NULL,
[Year] INT NOT NULL,
[FlightHours] INT,
[Condition] CHAR(1) NOT NULL,
[TypeId] INT NOT NULL REFERENCES AircraftTypes(Id)
)

CREATE TABLE [PilotsAircraft](
[AircraftId] INT NOT NULL,
[PilotId] INT NOT NULL,
PRIMARY KEY([AircraftId], [PilotId]),
FOREIGN KEY ([AircraftId]) REFERENCES [Aircraft](Id),
FOREIGN KEY ([PilotId]) REFERENCES [Pilots](Id)
)

CREATE TABLE [Airports](
[Id] INT PRIMARY KEY IDENTITY,
[AirportName] VARCHAR(70) UNIQUE NOT NULL,
[Country] VARCHAR (100) UNIQUE NOT NULL
)

CREATE TABLE [FlightDestinations](
[Id] INT PRIMARY KEY IDENTITY,
[AirportId] INT NOT NULL REFERENCES Airports(Id),
[Start] DATETIME NOT NULL,
[AircraftId] INT NOT NULL REFERENCES Aircraft(Id),
[PassengerId] INT NOT NULL REFERENCES Passengers(Id),
[TicketPrice] DECIMAL(18,2) NOT NULL DEFAULT 15
)

--02.INSERT
    SELECT * 
    FROM [Pilots] 
	WHERE Id BETWEEN 5 AND 15 
	INSERT INTO [Passengers] (FullName, Email) VALUES
('Krystal Cuckson', 'KrystalCuckson@gmail.com'),
('Susy Borrel', 'SusyBorrel@gmail.com'),
('Saxon Veldman', 'SaxonVeldman@gmail.com'),
('Lenore Romera', 'LenoreRomera@gmail.com'),
('Enrichetta Jeremiah', 'EnrichettaJeremiah@gmail.com'),
('Delaney Stove', 'DelaneyStove@gmail.com'),
('Ilaire Tomaszewicz', 'IlaireTomaszewicz@gmail.com'),
('Genna Jaquet', 'GennaJaquet@gmail.com'),
('Carlotta Dykas', 'CarlottaDykas@gmail.com'),
('Viki Oneal', 'VikiOneal@gmail.com'),
('Anthe Larne', 'AntheLarne@gmail.com')



--3.UPDATE
UPDATE Aircraft
	SET Condition = 'A'
	WHERE (Condition = 'C' OR Condition = 'B') AND
	(FlightHours IS NULL OR FlightHours <= 100) AND
	Year >= 2013

	
--4.DELETE
DELETE FROM Passengers
	  WHERE LEN(FullName) <= 10



--5.Aircraft
SELECT [Manufacturer],
       [Model],
	   [FlightHours],
	   [Condition]
  FROM [Aircraft]
 ORDER BY [FlightHours] DESC


 --6.Pilots and Aircraft
SELECT 
       FirstName,
	   LastName, 
       a.Manufacturer,
       a.Model,
       a.FlightHours 
  FROM Pilots AS p
  JOIN PilotsAircraft AS pa ON p.Id = pa.PilotId
  JOIN Aircraft AS a ON pa.AircraftId = a.Id
 WHERE a.FlightHours IS NOT NULL AND
	   a.FlightHours <= 304
 ORDER BY a.FlightHours DESC, p.FirstName


 --7.Top 20 Flight Destinations
 SELECT TOP(20) 
        fd.Id AS DestinationId, 
        [Start], p.FullName, 
        a.AirportName, 
        fd.TicketPrice 
   FROM FlightDestinations AS fd
   JOIN Passengers AS p ON fd.PassengerId = p.Id
   JOIN Airports AS a ON a.Id = fd.AirportId
  WHERE DAY([Start]) % 2 = 0
  ORDER BY TicketPrice DESC, AirportName



  --8.Number of Flights for Each Aircraft
SELECT DISTINCT a.Id AS AircraftId, a.Manufacturer, a.FlightHours,
	(
		SELECT COUNT(*) FROM FlightDestinations 
		WHERE AircraftId = a.Id
	) AS FlightDestinationsCount, 
	(
		SELECT ROUND(AVG(TicketPrice), 2) FROM FlightDestinations
		WHERE AircraftId = a.Id
	) AS AvgPrice
	FROM Aircraft AS a
		JOIN FlightDestinations AS fd ON fd.AircraftId = a.Id
		WHERE 
			(
				SELECT COUNT(*) FROM FlightDestinations 
				WHERE AircraftId = a.Id
			) >= 2
		ORDER BY FlightDestinationsCount DESC, a.Id

	

--9.Regular Passengers
   SELECT p.FullName, 
          COUNT(*) AS CountOfAircraft, 
          SUM(TicketPrice) AS TotalPayed 
     FROM FlightDestinations AS fd
	 JOIN Passengers AS p ON fd.PassengerId = p.Id
	 JOIN Aircraft AS a ON fd.AircraftId = a.Id
	WHERE SUBSTRING(p.FullName, 2, 1) = 'a'
	GROUP BY p.FullName
   HAVING COUNT(*) >= 2
    ORDER BY p.FullName



--10.Full Info for Flight Destinations
SELECT ap.AirportName, 
       fd.[Start] AS DayTime, 
	   fd.TicketPrice, 
	   p.FullName, 
	   ac.Manufacturer, 
	   ac.Model FROM FlightDestinations AS fd
  JOIN Airports AS ap ON fd.AirportId = ap.Id
  JOIN Passengers AS p ON fd.PassengerId = p.Id
  JOIN Aircraft AS ac ON fd.AircraftId = ac.Id
 WHERE (DATEPART(HOUR, fd.[Start]) BETWEEN 6 AND 20) AND fd.TicketPrice > 2500
 ORDER BY ac.Model

 
 --11.Find all Destinations by Email Address
CREATE FUNCTION udf_FlightDestinationsByEmail(@email VARCHAR(30))
RETURNS INT
AS
BEGIN
	RETURN (SELECT COUNT(*) FROM FlightDestinations AS fd
	JOIN Passengers AS p ON fd.PassengerId = p.Id
	WHERE @email = p.Email)
END


-- 12. Full Info for Airports
CREATE PROC usp_SearchByAirportName (@airportName VARCHAR(70))
AS
BEGIN
	SELECT a.AirportName, p.FullName, 
	(
		CASE
			WHEN fd.TicketPrice <= 400 THEN 'Low'
			WHEN fd.TicketPrice BETWEEN 401 AND 1500 THEN 'Medium'
			ELSE 'High'
		END
	) AS LevelOfTickerPrice, ac.Manufacturer, ac.Condition, [at].TypeName

		FROM Airports AS a
			JOIN FlightDestinations AS fd ON fd.AirportId = a.Id
			JOIN Passengers AS p ON fd.PassengerId = p.Id
			JOIN Aircraft AS ac ON fd.AircraftId = ac.Id
			JOIN AircraftTypes AS [at] ON ac.TypeId = [at].Id
			WHERE a.AirportName = @airportName
			ORDER BY ac.Manufacturer, p.FullName
END












	         