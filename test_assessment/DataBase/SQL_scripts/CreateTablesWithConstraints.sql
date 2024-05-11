
use test_assessment_db

CREATE TABLE Trips (
    TripID INT IDENTITY(1,1) PRIMARY KEY,
    PickupDatetime DATETIME2 NOT NULL,
    DropoffDatetime DATETIME2 NOT NULL,
    PassangerCount INT NOT NULL,
    TripDistance DECIMAL(10, 3) NOT NULL,
    StoreAndFwdFlag NVARCHAR(255),
    PULocationID INT,
    DOLocationID INT,
    FareAmount DECIMAL(10, 3) NOT NULL,
    TipAmount DECIMAL(10, 3) NOT NULL
);

CREATE TABLE Locations (
    LocationID INT PRIMARY KEY
);

ALTER TABLE Trips
ADD CONSTRAINT FK_PULocationID FOREIGN KEY (PULocationID)
REFERENCES Locations(LocationID);

ALTER TABLE Trips
ADD CONSTRAINT FK_DOLocationID FOREIGN KEY (DOLocationID)
REFERENCES Locations(LocationID);

ALTER TABLE Trips
ADD CONSTRAINT CHK_PickupDatetime_UTC CHECK (PickupDatetime = CONVERT(DATETIME2, SWITCHOFFSET(CONVERT(DATETIMEOFFSET, PickupDatetime), '+00:00')));

ALTER TABLE Trips
ADD CONSTRAINT CHK_DropoffDatetime_UTC CHECK (DropoffDatetime = CONVERT(DATETIME2, SWITCHOFFSET(CONVERT(DATETIMEOFFSET, DropoffDatetime), '+00:00')));

go;

CREATE OR ALTER PROCEDURE GetMostExpensiveTripsByDistance
AS
BEGIN
	with topFaresPerDistance as (
		select top(100) TripID
		from Trips
		order by FareAmount / NULLIF(TripDistance, 0) desc
	)
	select * from Trips as t
	where t.TripID in (select TripID from topFaresPerDistance)
END;

go;

CREATE OR ALTER PROCEDURE GetMostExpensiveTripsByTime
AS
BEGIN
	with topFaresPerDistance as (
		select top(100) TripID
		from Trips
		order by CASE
        WHEN DATEDIFF(SECOND, PickupDatetime, DropoffDatetime) > 0 THEN FareAmount / CAST(DATEDIFF(SECOND, PickupDatetime, DropoffDatetime) AS DECIMAL(10, 3))
        ELSE NULL
    END desc
	)
	select * from Trips as t
	where t.TripID in (select TripID from topFaresPerDistance)
END;
