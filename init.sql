-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Bakery')
BEGIN
    CREATE DATABASE Bakery;
END
GO

-- Use the Bakery database
USE Bakery;
GO

-- Create Categories table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE Categories (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL,
        Store BIT DEFAULT 1,
        IsActive BIT DEFAULT 1
    );
END

-- Create OperationClaims table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OperationClaims')
BEGIN
    CREATE TABLE OperationClaims (
        Id INT NOT NULL,
        Name NVARCHAR(250) NOT NULL
    );
END

-- Create UserOperationClaims table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UserOperationClaims')
BEGIN
    CREATE TABLE UserOperationClaims (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        OperationClaimId INT NOT NULL
    );
END

-- Create ServiceTypes table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ServiceTypes')
BEGIN
    CREATE TABLE ServiceTypes (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL
    );
END

-- Create Markets table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Markets')
BEGIN
    CREATE TABLE Markets (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(50),
        IsActive BIT DEFAULT 1
    );
END

-- Create ServiceProducts table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ServiceProducts')
BEGIN
    CREATE TABLE ServiceProducts (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL
    );
END

-- Create BreadPrices table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BreadPrices')
BEGIN
    CREATE TABLE BreadPrices (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Date DATETIME NOT NULL,
        Price DECIMAL(7, 2) NOT NULL
    );
END

-- Create Roles table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
BEGIN
    CREATE TABLE Roles (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL
    );
END

-- Create SystemAvailabilityTime table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SystemAvailabilityTime')
BEGIN
    CREATE TABLE SystemAvailabilityTime (
        OpenTime INT NOT NULL,
        CloseTime INT NOT NULL,
        Id INT NOT NULL
    );
END

-- Create Users table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        Email NVARCHAR(50) NOT NULL,
        PasswordHash VARBINARY(500) NOT NULL,
        PasswordSalt VARBINARY(500) NOT NULL,
        Status BIT NOT NULL,
        OperationClaimId INT NOT NULL
    );
END

-- Create Products table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL,
        CategoryId INT NOT NULL,
        Price DECIMAL(7, 2) NOT NULL,
        status BIT DEFAULT 1,
        FOREIGN KEY (CategoryId) REFERENCES Categories(Id) 
    );
END

-- Create MarketContracts table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MarketContracts')
BEGIN
    CREATE TABLE MarketContracts (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ServiceProductId INT NOT NULL,
        Price DECIMAL(7, 2) NOT NULL,
        MarketId INT NOT NULL,
        IsActive BIT DEFAULT 1,
        FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) ,
        FOREIGN KEY (MarketId) REFERENCES Markets(Id) 
    );
END

-- Create AccumulatedMoney table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AccumulatedMoney')
BEGIN
    CREATE TABLE AccumulatedMoney (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CreatedAt DATETIME NOT NULL,
        Amount DECIMAL(7, 2) NOT NULL,
        Type INT NOT NULL,
        CreatedBy INT NOT NULL,
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id) 
    );
END

-- Create AccumulatedMoneyDeliveries table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AccumulatedMoneyDeliveries')
BEGIN
    CREATE TABLE AccumulatedMoneyDeliveries (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CreatedAt DATETIME NOT NULL,
        Amount DECIMAL(7, 2) NOT NULL,
        AccumulatedAmount DECIMAL(7, 2) NOT NULL,
        CreatedBy INT NOT NULL,
        Type INT NOT NULL,
        FOREIGN KEY (CreatedBy) REFERENCES Users(Id) 
    );
END

-- Create AllServices table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AllServices')
BEGIN
    CREATE TABLE AllServices (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Date DATE NOT NULL,
        ServiceProductId INT NOT NULL,
        Quantity INT NOT NULL,
        ServiceTypeId INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) ,
        FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) ,
        FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) 
    );
END

-- Create BreadCountings table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BreadCountings')
BEGIN
    CREATE TABLE BreadCountings (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Quantity INT NOT NULL,
        Date DATE NOT NULL,
        UserId INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) 
    );
END

-- Create CashCountings table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CashCountings')
BEGIN
    CREATE TABLE CashCountings (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        TotalMoney DECIMAL(7, 2) NOT NULL,
        RemainedMoney DECIMAL(7, 2) NOT NULL,
        Date DATE NOT NULL,
        CreditCard DECIMAL(7, 2) NOT NULL
    );
END

-- Create DebtMarkets table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DebtMarkets')
BEGIN
    CREATE TABLE DebtMarkets (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MarketId INT NOT NULL,
        Date DATETIME NOT NULL,
        Amount DECIMAL(6, 2) NOT NULL,
        FOREIGN KEY (MarketId) REFERENCES Markets(Id) 
    );
END

-- Create DoughFactoryProducts table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DoughFactoryProducts')
BEGIN
    CREATE TABLE DoughFactoryProducts (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        BreadEquivalent FLOAT NOT NULL,
        Name NVARCHAR(50) NOT NULL,
        Status BIT DEFAULT 1
    );
END

-- Create DoughFactoryLists table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DoughFactoryLists')
BEGIN
    CREATE TABLE DoughFactoryLists (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Date DATETIME NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) 
    );
END

-- Create DoughFactoryListDetails table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DoughFactoryListDetails')
BEGIN
    CREATE TABLE DoughFactoryListDetails (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        DoughFactoryProductId INT NOT NULL,
        Quantity INT NOT NULL,
        DoughFactoryListId INT NOT NULL,
        FOREIGN KEY (DoughFactoryProductId) REFERENCES DoughFactoryProducts(Id) ,
        FOREIGN KEY (DoughFactoryListId) REFERENCES DoughFactoryLists(Id) 
    );
END

-- Create Employees table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE Employees (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Salary DECIMAL(18, 2) NOT NULL,
        CreatedAt DATETIME NOT NULL,
        Title NVARCHAR(50) NOT NULL,
        Status BIT DEFAULT 1
    );
END

-- Create Expenses table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Expenses')
BEGIN
    CREATE TABLE Expenses (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Detail NVARCHAR(200) NOT NULL,
        Date DATETIME NOT NULL,
        Amount DECIMAL(7, 2) NOT NULL,
        UserId INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) 
    );
END

-- Create GivenProductsToServices table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'GivenProductsToServices')
BEGIN
    CREATE TABLE GivenProductsToServices (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Quantity INT NOT NULL,
        Date DATETIME NOT NULL,
        ServiceTypeId INT NOT NULL,
        ServiceProductId INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) ,
        FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) ,
        FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) 
    );
END

-- Create MoneyReceivedFromMarkets table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MoneyReceivedFromMarkets')
BEGIN
    CREATE TABLE MoneyReceivedFromMarkets (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MarketId INT NOT NULL,
        Date DATETIME NOT NULL,
        Amount DECIMAL(6, 2) NOT NULL,
        FOREIGN KEY (MarketId) REFERENCES Markets(Id) 
    );
END

-- Create MonthlyProductCounts table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MonthlyProductCounts')
BEGIN
    CREATE TABLE MonthlyProductCounts (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL,
        Count INT NOT NULL,
        Price DECIMAL(10, 2) NOT NULL,
        Month INT NOT NULL,
        Year INT NOT NULL,
        Category NVARCHAR(255) NOT NULL
    );
END

-- Create ProductionLists table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductionLists')
BEGIN
    CREATE TABLE ProductionLists (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Date DATETIME NOT NULL,
        UserId INT NOT NULL,
        CategoryId INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) ,
        FOREIGN KEY (CategoryId) REFERENCES Categories(Id) 
    );
END

-- Create ProductionListDetails table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductionListDetails')
BEGIN
    CREATE TABLE ProductionListDetails (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL,
        Price DECIMAL(7, 2) NOT NULL,
        Quantity INT NOT NULL,
        ProductionListId INT NOT NULL,
        FOREIGN KEY (ProductId) REFERENCES Products(Id) ,
        FOREIGN KEY (ProductionListId) REFERENCES ProductionLists(Id) 
    );
END

-- Create ProductsCountings table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductsCountings')
BEGIN
    CREATE TABLE ProductsCountings (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL,
        Quantity INT NOT NULL,
        Date DATETIME NOT NULL,
        FOREIGN KEY (ProductId) REFERENCES Products(Id) 
    );
END

-- Create PurchasedProductListDetails table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'PurchasedProductListDetails')
BEGIN
    CREATE TABLE PurchasedProductListDetails (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL,
        Price DECIMAL(7, 2) NOT NULL,
        Quantity INT NOT NULL,
        Date DATETIME NOT NULL,
        UserId INT NOT NULL,
        FOREIGN KEY (ProductId) REFERENCES Products(Id) ,
        FOREIGN KEY (UserId) REFERENCES Users(Id) 
    );
END

-- Create ReceivedMoney table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ReceivedMoney')
BEGIN
    CREATE TABLE ReceivedMoney (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Amount DECIMAL(7, 2) NOT NULL,
        ServiceTypeId INT NOT NULL,
        Date DATETIME NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) ,
        FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) 
    );
END

-- Create SalaryPayments table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SalaryPayments')
BEGIN
    CREATE TABLE SalaryPayments (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        Month INT NOT NULL,
        Year INT NOT NULL,
        TotalSalary DECIMAL(18, 2) NOT NULL,
        PaidAmount DECIMAL(18, 2) NOT NULL,
        RemainingAmount DECIMAL(18, 2) NOT NULL,
        CreatedAt DATETIME NOT NULL,
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) 
    );
END

-- Create ServiceLists table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ServiceLists')
BEGIN
    CREATE TABLE ServiceLists (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Date DATETIME NOT NULL,
        UserId INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) 
    );
END

-- Create ServiceListDetails table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ServiceListDetails')
BEGIN
    CREATE TABLE ServiceListDetails (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ServiceListId INT NOT NULL,
        Quantity INT NOT NULL,
        Price DECIMAL(7, 2) NOT NULL,
        MarketContractId INT NOT NULL,
        FOREIGN KEY (ServiceListId) REFERENCES ServiceLists(Id) ,
        FOREIGN KEY (MarketContractId) REFERENCES MarketContracts(Id) 
    );
END

-- Create ServiceRemindMoney table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ServiceRemindMoney')
BEGIN
    CREATE TABLE ServiceRemindMoney (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MarketId INT NOT NULL,
        Date DATETIME NOT NULL,
        Amount DECIMAL(6, 2) NOT NULL,
        FOREIGN KEY (MarketId) REFERENCES Markets(Id) 
    );
END

-- Create ServiceStaleProducts table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ServiceStaleProducts')
BEGIN
    CREATE TABLE ServiceStaleProducts (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Date DATETIME NOT NULL,
        ServiceTypeId INT NOT NULL,
        ServiceProductId INT NOT NULL,
        Quantity INT NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(Id) ,
        FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) ,
        FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) 
    );
END

-- Create StaleBread table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StaleBread')
BEGIN
    CREATE TABLE StaleBread (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Quantity INT NOT NULL,
        Date DATETIME NOT NULL,
        DoughFactoryProductId INT NOT NULL,
        FOREIGN KEY (DoughFactoryProductId) REFERENCES DoughFactoryProducts(Id) 
    );
END

-- Create StaleBreadReceivedFromMarkets table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StaleBreadReceivedFromMarkets')
BEGIN
    CREATE TABLE StaleBreadReceivedFromMarkets (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MarketId INT NOT NULL,
        Date DATETIME NOT NULL,
        Quantity INT NOT NULL,
        FOREIGN KEY (MarketId) REFERENCES Markets(Id) 
    );
END

-- Create StaleProductsReceivedFromMarkets table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StaleProductsReceivedFromMarkets')
BEGIN
    CREATE TABLE StaleProductsReceivedFromMarkets (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MarketId INT NOT NULL,
        ServiceProductId INT NOT NULL,
        Date DATETIME NOT NULL,
        Quantity INT NOT NULL,
        FOREIGN KEY (MarketId) REFERENCES Markets(Id) ,
        FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) 
    );
END

-- Create Advances table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Advances')
BEGIN
    CREATE TABLE Advances (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeId INT NOT NULL,
        Amount DECIMAL(18, 2) NOT NULL,
        CreatedAt DATETIME NOT NULL,
        Year INT NOT NULL,
        Month INT NOT NULL,
        FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) 
    );
END

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StaleProducts')
BEGIN
    CREATE TABLE StaleProducts (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL,
        Quantity INT NOT NULL,
        Date DATETIME NOT NULL,
        FOREIGN KEY (ProductId) REFERENCES Products(Id) 
    );
END

-- Insert default data
-- Categories
IF NOT EXISTS (SELECT 1 FROM Categories WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT Categories ON;
    INSERT INTO Categories (Id, Name, Store, IsActive)
    VALUES (1, 'Pasta', 0, 1);
    SET IDENTITY_INSERT Categories OFF;
END

IF NOT EXISTS (SELECT 1 FROM Categories WHERE Id = 2)
BEGIN
    SET IDENTITY_INSERT Categories ON;
    INSERT INTO Categories (Id, Name, Store, IsActive)
    VALUES (2, 'Borek', 0, 1);
    SET IDENTITY_INSERT Categories OFF;
END

IF NOT EXISTS (SELECT 1 FROM Categories WHERE Id = 3)
BEGIN
    SET IDENTITY_INSERT Categories ON;
    INSERT INTO Categories (Id, Name, Store, IsActive)
    VALUES (3, 'Satin alinan', 0, 1);
    SET IDENTITY_INSERT Categories OFF;
END

-- OperationClaims
IF NOT EXISTS (SELECT 1 FROM OperationClaims WHERE Id = 1)
    INSERT INTO OperationClaims (Id, Name) VALUES (1, 'Hamurkar');

IF NOT EXISTS (SELECT 1 FROM OperationClaims WHERE Id = 2)
    INSERT INTO OperationClaims (Id, Name) VALUES (2, 'Pastaci');

IF NOT EXISTS (SELECT 1 FROM OperationClaims WHERE Id = 3)
    INSERT INTO OperationClaims (Id, Name) VALUES (3, 'Borekci');

IF NOT EXISTS (SELECT 1 FROM OperationClaims WHERE Id = 4)
    INSERT INTO OperationClaims (Id, Name) VALUES (4, 'Sofor');

IF NOT EXISTS (SELECT 1 FROM OperationClaims WHERE Id = 5)
    INSERT INTO OperationClaims (Id, Name) VALUES (5, 'Tezgahtar');

IF NOT EXISTS (SELECT 1 FROM OperationClaims WHERE Id = 6)
    INSERT INTO OperationClaims (Id, Name) VALUES (6, 'Admin');

-- SystemAvailabilityTime
IF NOT EXISTS (SELECT 1 FROM SystemAvailabilityTime WHERE Id = 1)
    INSERT INTO SystemAvailabilityTime (Id, OpenTime, CloseTime) VALUES (1, 1, 23);

-- BreadPrices
IF NOT EXISTS (SELECT 1 FROM BreadPrices)
    INSERT INTO BreadPrices (Date, Price) VALUES (GETDATE(), 10.00);