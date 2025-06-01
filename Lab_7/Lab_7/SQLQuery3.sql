USE TransportDbLab7_2;

CREATE TABLE Transports
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TransportType NVARCHAR(2) NOT NULL,
    RouteNumber NVARCHAR(10) NOT NULL,
    RouteDistance FLOAT NOT NULL,
    TravelTime INT NOT NULL
);

INSERT INTO Transports (TransportType, RouteNumber, RouteDistance, TravelTime)
VALUES
('Tr', '12a', 14.5, 30),
('Tl', '5b', 8.2, 25),
('A', '101', 20.0, 40);