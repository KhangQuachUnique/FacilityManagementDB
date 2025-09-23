-- File: CreateDatabase.sql

-- Run this script in SQL Server Management Studio to create the database and all objects.

-- Create Database
CREATE DATABASE FacilityManagementDB;
GO

USE FacilityManagementDB;
GO

-- Create Tables

-- Roles Table
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- Users Table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleID INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleID),
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- Areas Table
CREATE TABLE Areas (
    AreaID INT PRIMARY KEY IDENTITY(1,1),
    AreaName NVARCHAR(100) NOT NULL
);
GO

-- Employees Table
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Position NVARCHAR(50),
    AreaID INT FOREIGN KEY REFERENCES Areas(AreaID)
);
GO

-- EquipmentTypes Table
CREATE TABLE EquipmentTypes (
    TypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(100) NOT NULL
);
GO

-- Equipment Table
CREATE TABLE Equipment (
    EquipmentID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    TypeID INT NOT NULL FOREIGN KEY REFERENCES EquipmentTypes(TypeID),
    AreaID INT NOT NULL FOREIGN KEY REFERENCES Areas(AreaID),
    Status NVARCHAR(50) NOT NULL, -- e.g., 'Operational', 'Under Maintenance', 'Broken'
    Quantity INT NOT NULL DEFAULT 1,
    Price DECIMAL(18,2) NOT NULL,
    LastMaintenanceDate DATE
);
GO

-- MaintainEquipment Table
CREATE TABLE MaintainEquipment (
    MaintenanceID INT PRIMARY KEY IDENTITY(1,1),
    EquipmentID INT NOT NULL FOREIGN KEY REFERENCES Equipment(EquipmentID),
    EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
    MaintenanceDate DATE NOT NULL,
    Cost DECIMAL(18,2) NOT NULL,
    Description NVARCHAR(MAX)
);
GO

-- EquipmentStatusLog Table
CREATE TABLE EquipmentStatusLog (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    EquipmentID INT NOT NULL FOREIGN KEY REFERENCES Equipment(EquipmentID),
    Status NVARCHAR(50) NOT NULL,
    ChangeDate DATETIME NOT NULL DEFAULT GETDATE(),
    ChangedBy INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID)
);
GO

-- Stored Procedures for CRUD

-- Roles
CREATE PROCEDURE sp_InsertRole
    @RoleName NVARCHAR(50)
AS
BEGIN
    INSERT INTO Roles (RoleName) VALUES (@RoleName);
END
GO

CREATE PROCEDURE sp_UpdateRole
    @RoleID INT,
    @RoleName NVARCHAR(50)
AS
BEGIN
    UPDATE Roles SET RoleName = @RoleName WHERE RoleID = @RoleID;
END
GO

CREATE PROCEDURE sp_DeleteRole
    @RoleID INT
AS
BEGIN
    DELETE FROM Roles WHERE RoleID = @RoleID;
END
GO

CREATE PROCEDURE sp_GetAllRoles
AS
BEGIN
    SELECT * FROM Roles;
END
GO

CREATE PROCEDURE sp_GetRoleByID
    @RoleID INT
AS
BEGIN
    SELECT * FROM Roles WHERE RoleID = @RoleID;
END
GO

-- Users
CREATE PROCEDURE sp_InsertUser
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @RoleID INT,
    @IsActive BIT
AS
BEGIN
    INSERT INTO Users (Username, PasswordHash, RoleID, IsActive) VALUES (@Username, @PasswordHash, @RoleID, @IsActive);
END
GO

CREATE PROCEDURE sp_UpdateUser
    @UserID INT,
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @RoleID INT,
    @IsActive BIT
AS
BEGIN
    UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, RoleID = @RoleID, IsActive = @IsActive WHERE UserID = @UserID;
END
GO

CREATE PROCEDURE sp_DeleteUser
    @UserID INT
AS
BEGIN
    DELETE FROM Users WHERE UserID = @UserID;
END
GO

CREATE PROCEDURE sp_GetAllUsers
AS
BEGIN
    SELECT u.*, r.RoleName FROM Users u JOIN Roles r ON u.RoleID = r.RoleID;
END
GO

CREATE PROCEDURE sp_GetUserByID
    @UserID INT
AS
BEGIN
    SELECT u.*, r.RoleName FROM Users u JOIN Roles r ON u.RoleID = r.RoleID WHERE u.UserID = @UserID;
END
GO

CREATE PROCEDURE sp_GetUserByUsername
    @Username NVARCHAR(50)
AS
BEGIN
    SELECT u.*, r.RoleName FROM Users u JOIN Roles r ON u.RoleID = r.RoleID WHERE u.Username = @Username;
END
GO

-- Areas
CREATE PROCEDURE sp_InsertArea
    @AreaName NVARCHAR(100)
AS
BEGIN
    INSERT INTO Areas (AreaName) VALUES (@AreaName);
END
GO

CREATE PROCEDURE sp_UpdateArea
    @AreaID INT,
    @AreaName NVARCHAR(100)
AS
BEGIN
    UPDATE Areas SET AreaName = @AreaName WHERE AreaID = @AreaID;
END
GO

CREATE PROCEDURE sp_DeleteArea
    @AreaID INT
AS
BEGIN
    DELETE FROM Areas WHERE AreaID = @AreaID;
END
GO

CREATE PROCEDURE sp_GetAllAreas
AS
BEGIN
    SELECT * FROM Areas;
END
GO

-- Employees
CREATE PROCEDURE sp_InsertEmployee
    @Name NVARCHAR(100),
    @Position NVARCHAR(50),
    @AreaID INT
AS
BEGIN
    INSERT INTO Employees (Name, Position, AreaID) VALUES (@Name, @Position, @AreaID);
END
GO

CREATE PROCEDURE sp_UpdateEmployee
    @EmployeeID INT,
    @Name NVARCHAR(100),
    @Position NVARCHAR(50),
    @AreaID INT
AS
BEGIN
    UPDATE Employees SET Name = @Name, Position = @Position, AreaID = @AreaID WHERE EmployeeID = @EmployeeID;
END
GO

CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeID = @EmployeeID;
END
GO

CREATE PROCEDURE sp_GetAllEmployees
AS
BEGIN
    SELECT e.*, a.AreaName FROM Employees e LEFT JOIN Areas a ON e.AreaID = a.AreaID;
END
GO

-- EquipmentTypes
CREATE PROCEDURE sp_InsertEquipmentType
    @TypeName NVARCHAR(100)
AS
BEGIN
    INSERT INTO EquipmentTypes (TypeName) VALUES (@TypeName);
END
GO

CREATE PROCEDURE sp_UpdateEquipmentType
    @TypeID INT,
    @TypeName NVARCHAR(100)
AS
BEGIN
    UPDATE EquipmentTypes SET TypeName = @TypeName WHERE TypeID = @TypeID;
END
GO

CREATE PROCEDURE sp_DeleteEquipmentType
    @TypeID INT
AS
BEGIN
    DELETE FROM EquipmentTypes WHERE TypeID = @TypeID;
END
GO

CREATE PROCEDURE sp_GetAllEquipmentTypes
AS
BEGIN
    SELECT * FROM EquipmentTypes;
END
GO

-- Equipment
CREATE PROCEDURE sp_InsertEquipment
    @Name NVARCHAR(100),
    @TypeID INT,
    @AreaID INT,
    @Status NVARCHAR(50),
    @Quantity INT,
    @Price DECIMAL(18,2),
    @LastMaintenanceDate DATE
AS
BEGIN
    INSERT INTO Equipment (Name, TypeID, AreaID, Status, Quantity, Price, LastMaintenanceDate) 
    VALUES (@Name, @TypeID, @AreaID, @Status, @Quantity, @Price, @LastMaintenanceDate);
END
GO

CREATE PROCEDURE sp_UpdateEquipment
    @EquipmentID INT,
    @Name NVARCHAR(100),
    @TypeID INT,
    @AreaID INT,
    @Status NVARCHAR(50),
    @Quantity INT,
    @Price DECIMAL(18,2),
    @LastMaintenanceDate DATE
AS
BEGIN
    UPDATE Equipment SET Name = @Name, TypeID = @TypeID, AreaID = @AreaID, Status = @Status, 
    Quantity = @Quantity, Price = @Price, LastMaintenanceDate = @LastMaintenanceDate 
    WHERE EquipmentID = @EquipmentID;
END
GO

CREATE PROCEDURE sp_DeleteEquipment
    @EquipmentID INT
AS
BEGIN
    DELETE FROM Equipment WHERE EquipmentID = @EquipmentID;
END
GO

CREATE PROCEDURE sp_GetAllEquipment
AS
BEGIN
    SELECT e.*, t.TypeName, a.AreaName FROM Equipment e 
    JOIN EquipmentTypes t ON e.TypeID = t.TypeID
    JOIN Areas a ON e.AreaID = a.AreaID;
END
GO

CREATE PROCEDURE sp_GetEquipmentByFilter
    @AreaID INT = NULL,
    @TypeID INT = NULL,
    @Status NVARCHAR(50) = NULL
AS
BEGIN
    SELECT e.*, t.TypeName, a.AreaName FROM Equipment e 
    JOIN EquipmentTypes t ON e.TypeID = t.TypeID
    JOIN Areas a ON e.AreaID = a.AreaID
    WHERE (@AreaID IS NULL OR e.AreaID = @AreaID)
    AND (@TypeID IS NULL OR e.TypeID = @TypeID)
    AND (@Status IS NULL OR e.Status = @Status);
END
GO

-- MaintainEquipment
CREATE PROCEDURE sp_InsertMaintenance
    @EquipmentID INT,
    @EmployeeID INT,
    @MaintenanceDate DATE,
    @Cost DECIMAL(18,2),
    @Description NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO MaintainEquipment (EquipmentID, EmployeeID, MaintenanceDate, Cost, Description) 
    VALUES (@EquipmentID, @EmployeeID, @MaintenanceDate, @Cost, @Description);
END
GO

CREATE PROCEDURE sp_UpdateMaintenance
    @MaintenanceID INT,
    @EquipmentID INT,
    @EmployeeID INT,
    @MaintenanceDate DATE,
    @Cost DECIMAL(18,2),
    @Description NVARCHAR(MAX)
AS
BEGIN
    UPDATE MaintainEquipment SET EquipmentID = @EquipmentID, EmployeeID = @EmployeeID, 
    MaintenanceDate = @MaintenanceDate, Cost = @Cost, Description = @Description 
    WHERE MaintenanceID = @MaintenanceID;
END
GO

CREATE PROCEDURE sp_DeleteMaintenance
    @MaintenanceID INT
AS
BEGIN
    DELETE FROM MaintainEquipment WHERE MaintenanceID = @MaintenanceID;
END
GO

CREATE PROCEDURE sp_GetAllMaintenance
AS
BEGIN
    SELECT m.*, e.Name AS EquipmentName, emp.Name AS EmployeeName 
    FROM MaintainEquipment m 
    JOIN Equipment e ON m.EquipmentID = e.EquipmentID
    JOIN Employees emp ON m.EmployeeID = emp.EmployeeID;
END
GO

CREATE PROCEDURE sp_GetMaintenanceByFilter
    @EquipmentID INT = NULL,
    @EmployeeID INT = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
    SELECT m.*, e.Name AS EquipmentName, emp.Name AS EmployeeName 
    FROM MaintainEquipment m 
    JOIN Equipment e ON m.EquipmentID = e.EquipmentID
    JOIN Employees emp ON m.EmployeeID = emp.EmployeeID
    WHERE (@EquipmentID IS NULL OR m.EquipmentID = @EquipmentID)
    AND (@EmployeeID IS NULL OR m.EmployeeID = @EmployeeID)
    AND (@StartDate IS NULL OR m.MaintenanceDate >= @StartDate)
    AND (@EndDate IS NULL OR m.MaintenanceDate <= @EndDate);
END
GO

-- Reports
CREATE PROCEDURE sp_ReportMaintenanceCostByMonthYear
    @Month INT,
    @Year INT
AS
BEGIN
    SELECT SUM(Cost) AS TotalCost 
    FROM MaintainEquipment 
    WHERE MONTH(MaintenanceDate) = @Month AND YEAR(MaintenanceDate) = @Year;
END
GO

CREATE PROCEDURE sp_ReportEquipmentValueByArea
    @AreaID INT
AS
BEGIN
    SELECT SUM(Price * Quantity) AS TotalValue 
    FROM Equipment 
    WHERE AreaID = @AreaID;
END
GO

CREATE PROCEDURE sp_ReportEquipmentValueByType
    @TypeID INT
AS
BEGIN
    SELECT SUM(Price * Quantity) AS TotalValue 
    FROM Equipment 
    WHERE TypeID = @TypeID;
END
GO

CREATE PROCEDURE sp_ReportEquipmentNeedingMaintenance
    @DaysThreshold INT -- e.g., equipment not maintained in last X days
AS
BEGIN
    SELECT * FROM Equipment 
    WHERE LastMaintenanceDate < DATEADD(DAY, -@DaysThreshold, GETDATE()) OR LastMaintenanceDate IS NULL;
END
GO

-- Functions
CREATE FUNCTION fn_GetTotalMaintenanceCost (@EquipmentID INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @Total DECIMAL(18,2);
    SELECT @Total = SUM(Cost) FROM MaintainEquipment WHERE EquipmentID = @EquipmentID;
    RETURN ISNULL(@Total, 0);
END
GO

-- Triggers
CREATE TRIGGER trg_EquipmentStatusChange
ON Equipment
AFTER UPDATE
AS
BEGIN
    IF UPDATE(Status)
    BEGIN
        INSERT INTO EquipmentStatusLog (EquipmentID, Status, ChangeDate, ChangedBy)
        SELECT i.EquipmentID, i.Status, GETDATE(), 1 -- Assume ChangedBy is 1 for demo, replace with actual
        FROM inserted i
        INNER JOIN deleted d ON i.EquipmentID = d.EquipmentID
        WHERE i.Status <> d.Status;
    END
END
GO

-- Insert Sample Data for Demo
INSERT INTO Roles (RoleName) VALUES ('Admin'), ('Manager'), ('Staff');
INSERT INTO Users (Username, PasswordHash, RoleID, IsActive) VALUES ('admin', 'hashedpass', 1, 1); -- Use proper hashing in code
INSERT INTO Areas (AreaName) VALUES ('Storage'), ('Sales Floor');
INSERT INTO Employees (Name, Position, AreaID) VALUES ('John Doe', 'Manager', 1);
INSERT INTO EquipmentTypes (TypeName) VALUES ('Shelf'), ('Computer');
INSERT INTO Equipment (Name, TypeID, AreaID, Status, Quantity, Price, LastMaintenanceDate) 
VALUES ('Bookshelf', 1, 1, 'Operational', 5, 100.00, '2023-01-01');
GO