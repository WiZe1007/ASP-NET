USE TransportDB;

CREATE TABLE Transports (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TransportType NVARCHAR(50) NOT NULL,
    RouteNumber NVARCHAR(50) NOT NULL,
    RouteDistance FLOAT NOT NULL,
    TravelTime INT NOT NULL
);