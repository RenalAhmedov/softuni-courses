--
CREATE DATABASE CigarShop
--
USE [CigarShop]

--01.DDL

CREATE TABLE Sizes(
Id INT PRIMARY KEY IDENTITY,
[Length] INT NOT NULL CHECK ([Length] BETWEEN 10 AND 25),
RingRange DECIMAL(20,2) NOT NULL CHECK (RingRange BETWEEN 1.5 AND 7.5)
)

CREATE TABLE Tastes(
Id INT PRIMARY KEY IDENTITY,
TasteType VARCHAR(20) NOT NULL,
TasteStrength VARCHAR(15) NOT NULL,
ImageURL NVARCHAR(100) NOT NULL
)

CREATE TABLE Brands(
Id INT PRIMARY KEY IDENTITY,
BrandName VARCHAR(30) NOT NULL UNIQUE,
BrandDescription VARCHAR(MAX)
)

CREATE TABLE Cigars(
Id INT PRIMARY KEY IDENTITY,
CigarName VARCHAR(80) NOT NULL,
BrandId INT NOT NULL REFERENCES Brands(Id),
TastId INT NOT NULL REFERENCES Tastes(Id),
SizeId INT NOT NULL REFERENCES Sizes(Id),
PriceForSingleCigar MONEY NOT NULL,
ImageURL NVARCHAR(100) NOT NULL,
)

CREATE TABLE Addresses(
Id INT PRIMARY KEY IDENTITY,
Town VARCHAR(30) NOT NULL,
Country NVARCHAR(30) NOT NULL,
Streat NVARCHAR(100) NOT NULL,
ZIP VARCHAR(20) NOT NULL
)

CREATE TABLE Clients(
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(30) NOT NULL,
LastName NVARCHAR(30) NOT NULL,
Email NVARCHAR(50) NOT NULL,
AddressId INT NOT NULL REFERENCES Addresses(Id)
)

CREATE TABLE ClientsCigars(
ClientId INT NOT NULL,
CigarId INT NOT NULL,
PRIMARY KEY(ClientId, CigarId),
FOREIGN KEY (ClientId) REFERENCES Clients(Id),
FOREIGN KEY (CigarId) REFERENCES Cigars(Id)
)


--02.Insert

INSERT INTO Cigars (CigarName, BrandId, TastId, SizeId, PriceForSingleCigar, ImageURL) VALUES
('COHIBA ROBUSTO', 9, 1, 5, 15.50, 'cohiba-robusto-stick_18.jpg'),
('COHIBA SIGLO I', 9, 1, 10, 410, 'cohiba-siglo-i-stick_12.jpg'),
('HOYO DE MONTERREY LE HOYO DU MAIRE', 14, 5, 11, 7.50, 'hoyo-du-maire-stick_17.jpg'),
('HOYO DE MONTERREY LE HOYO DE SAN JUAN', 14, 4, 15, 32, 'hoyo-de-san-juan-stick_20.jpg'),
('TRINIDAD COLONIALES', 2, 3, 8, 85.21, 'trinidad-coloniales-stick_30.jpg')

INSERT INTO Addresses (Town, Country, Streat, ZIP) VALUES
('Sofia', 'Bulgaria', '18 Bul. Vasil levski', '1000'),
('Athens', 'Greece', '4342 McDonald Avenue', '10435'),
('Zagreb', 'Croatia', '4333 Lauren Drive', '10000')


--3.UPDATE
UPDATE Cigars
SET PriceForSingleCigar *= 1.20
	WHERE TastId = 1

UPDATE Brands
SET BrandDescription = 'New description'
	WHERE BrandDescription IS NULL

--4.DELETE


DELETE FROM Clients
	WHERE AddressId IN (SELECT Id FROM Addresses WHERE Country LIKE 'C%')

DELETE FROM Addresses
	WHERE Country LIKE 'C%'
	

--5.Cigars by Price
SELECT 
      CigarNameT, 
      PriceForSingleCigar,
      ImageURL
 FROM Cigars
 ORDER BY PriceForSingleCigar ASC, CigarName DESC


 --6.Cigars by Taste

 SELECT C.Id,
        C.CigarName,
		C.PriceForSingleCigar,
		T.TasteType,
		T.TasteStrength
 FROM Cigars AS C
 JOIN Tastes AS T
 ON C.TastId = T.Id
 WHERE T.TasteType = 'Earthy' OR T.TasteType = 'Woody'
 ORDER BY PriceForSingleCigar DESC


--7.Clients without Cigars
   SELECT c.Id
         ,c.FirstName + ' ' + c.LastName AS ClientName
		 ,c.Email
     FROM Clients AS c
LEFT JOIN ClientsCigars cc
       ON c.Id = cc.ClientId
    WHERE cc.ClientId IS NULL
    ORDER BY FirstName ASC


--8.First 5 Cigars 

 SELECT TOP (5) 
            CigarName
            ,PriceForSingleCigar
	        ,ImageURL
       FROM Cigars AS c
  LEFT JOIN Sizes AS s
         ON c.SizeId = s.Id
      WHERE (s.[Length] >= 12) AND (CigarName LIKE '%ci%' OR PriceForSingleCigar > 50 AND s.RingRange > 2.55)
	  ORDER BY CigarName, PriceForSingleCigar DESC 


--9.Clients with ZIP Codes

SELECT FirstName + ' ' + LastName AS FullName, 
       a.Country, 
	   a.ZIP, '$' + CAST(MAX(cig.PriceForSingleCigar) AS NVARCHAR) AS CigarPrice
  FROM Clients AS c
  JOIN Addresses AS a ON c.AddressId = a.Id
  JOIN ClientsCigars AS cc ON cc.ClientId = c.Id
  JOIN Cigars AS cig ON cig.Id = cc.CigarId
 WHERE ZIP LIKE '%[0-9]%' AND ZIP NOT LIKE '%[A-Z]%'
 GROUP BY c.FirstName, c.LastName, a.Country, a.ZIP

 --10.Cigars by Size
 SELECT LastName,
        AVG(s.[Length]) AS CigarLength,
		CEILING(AVG(s.RingRange)) AS CigarRingRange
 FROM Clients AS cl
 LEFT JOIN ClientsCigars AS cc ON cl.Id = cc.ClientId
 LEFT JOIN Cigars AS cig ON cc.CigarId = cig.Id
 LEFT JOIN Sizes AS s ON cig.SizeId = s.Id
 WHERE CigarId > 0 
 GROUP BY LastName
 ORDER BY CigarLength DESC

 --11.Client with Cigars

 CREATE FUNCTION udf_ClientWithCigars(@name VARCHAR(30))
RETURNS INT
AS
BEGIN
RETURN (SELECT COUNT(*) FROM Clients AS c
        LEFT JOIN ClientsCigars AS cc ON c.Id = cc.ClientId
		LEFT JOIN Cigars AS cig ON cc.CigarId = cig.Id
		WHERE @name = c.FirstName)
END
 

--12.Search for Cigar with Specific Taste

CREATE PROC usp_SearchByTaste(@taste VARCHAR(30))
AS
BEGIN
	SELECT CigarName,
	       '$' + CAST(PriceForSingleCigar AS VARCHAR),
	        t.TasteType,
	        b.BrandName,
	        CAST(s.[Length] AS VARCHAR) + ' ' + 'cm' AS CigarLength, 
	        CAST(s.RingRange AS VARCHAR) + ' ' + 'cm' AS CigarRingRange
	  FROM Cigars AS c
	  JOIN Tastes AS t ON t.Id = c.TastId
	  JOIN Brands AS b ON c.BrandId = b.Id
	  JOIN Sizes AS s ON c.SizeId = s.Id
	 WHERE t.TasteType = @taste
	 ORDER BY s.[Length], s.RingRange DESC
END

