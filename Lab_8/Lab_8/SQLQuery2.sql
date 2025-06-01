USE User1DB;

CREATE TABLE Users (
  Id              INT IDENTITY(1,1) PRIMARY KEY,
  Name            NVARCHAR(15)  NOT NULL UNIQUE,
  Email           NVARCHAR(255) NOT NULL,
  Phone           NVARCHAR(20)  NOT NULL,
  Password        NVARCHAR(255) NOT NULL
);