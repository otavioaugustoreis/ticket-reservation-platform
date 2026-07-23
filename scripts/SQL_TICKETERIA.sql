IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = 'SQL_TICKETERIA')
BEGIN
     CREATE DATABASE [SQL_TICKETERIA];
END;
GO

USE [SQL_TICKETERIA];
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Customer')
BEGIN
    CREATE TABLE Customer
    (
        Id INT IDENTITY(1,1) NOT NULL,
        [Name] VARCHAR(100) NOT NULL,
        CONSTRAINT Pk_Customer PRIMARY KEY CLUSTERED (Id) ON [PRIMARY]
    );
END
GO

IF(NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Event'))
BEGIN
    CREATE TABLE Event
    (
        Id INT IDENTITY(1,1) NOT NULL,
        [Name] VARCHAR(100) NOT NULL,
        [Date] DATETIME NOT NULL,
        CONSTRAINT Pk_Event PRIMARY KEY CLUSTERED (Id) ON [PRIMARY]
    );
END
GO 

IF(NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Seat'))
BEGIN
    CREATE TABLE [Seat]
    (
        Id INT IDENTITY(1,1) NOT NULL,
        Number INT NOT NULL,
        EventId INT NOT NULL,
        CONSTRAINT Pk_Seat PRIMARY KEY CLUSTERED (Id) ON [PRIMARY],
        CONSTRAINT Fk_Seat_Event FOREIGN KEY (EventId) REFERENCES [Event](Id)
    );
END
GO

IF(NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Reservation'))
BEGIN
    CREATE TABLE [Reservation]
    (
        Id INT IDENTITY(1,1) NOT NULL,
        ClientId INT NOT NULL,
        SeatId INT NOT NULL,
        CreatedAt DATETIME NOT NULL,
        ExpiresAt DATETIME NOT NULL,
        CONSTRAINT Pk_Reservation PRIMARY KEY CLUSTERED (Id) ON [PRIMARY],
        CONSTRAINT Fk_Reservation_Client FOREIGN KEY (ClientId) REFERENCES Customer(Id),
        CONSTRAINT Fk_Reservation_Seat FOREIGN KEY (SeatId) REFERENCES Seat(Id)
    );
END
GO

IF(NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Payment'))
BEGIN
    CREATE TABLE [Payment]
    (
        Id INT IDENTITY(1,1) NOT NULL,
        ReservationId INT NOT NULL,
        Amount DECIMAL(10, 2) NOT NULL,
        PaidAt DATETIME NOT NULL,
        CONSTRAINT Pk_Payment PRIMARY KEY CLUSTERED (Id) ON [PRIMARY],
        CONSTRAINT Fk_Payment_Reservation FOREIGN KEY (ReservationId) REFERENCES Reservation(Id)
    );
END
GO