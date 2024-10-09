CREATE TABLE IF NOT EXISTS Categories (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Store BOOLEAN DEFAULT TRUE,
    IsActive BOOLEAN DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS OperationClaims (
    Id INT  NOT NULL,
    Name VARCHAR(250) NOT NULL
);

CREATE TABLE IF NOT EXISTS UserOperationClaims (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    OperationClaimId INT NOT NULL
);

CREATE TABLE IF NOT EXISTS ServiceTypes (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS Markets (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name varchar(50),
    IsActive BOOLEAN DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS ServiceProducts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS BreadPrices (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Date DATETIME NOT NULL,
    Price DECIMAL(7, 2) NOT NULL
);

CREATE TABLE IF NOT EXISTS Roles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS SystemAvailabilityTime (
    OpenTime INT NOT NULL,
    CloseTime INT NOT NULL,
    Id INT NOT NULL
);

-- Create tables with foreign key dependencies
CREATE TABLE IF NOT EXISTS Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    PasswordHash VARBINARY(500) NOT NULL,
    PasswordSalt VARBINARY(500) NOT NULL,
    Status BOOLEAN NOT NULL,
    OperationClaimId INT NOT NULL
);

CREATE TABLE IF NOT EXISTS Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    CategoryId INT NOT NULL,
    Price DECIMAL(7, 2) NOT NULL,
    status BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS MarketContracts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ServiceProductId INT NOT NULL,
    Price DECIMAL(7, 2) NOT NULL,
    MarketId INT NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) ON DELETE CASCADE,
    FOREIGN KEY (MarketId) REFERENCES Markets(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS AccumulatedMoney (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CreatedAt DATETIME NOT NULL,
    Amount DECIMAL(7, 2) NOT NULL,
    Type INT NOT NULL,
    CreatedBy INT NOT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS AccumulatedMoneyDeliveries (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CreatedAt DATETIME NOT NULL,
    Amount DECIMAL(7, 2) NOT NULL,
    AccumulatedAmount DECIMAL(7, 2) NOT NULL,
    CreatedBy INT NOT NULL,
    Type INT NOT NULL,
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS AllServices (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    Date DATE NOT NULL,
    ServiceProductId INT NOT NULL,
    Quantity INT NOT NULL,
    ServiceTypeId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS BreadCountings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Quantity INT NOT NULL,
    Date DATE NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS CashCountings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TotalMoney DECIMAL(7, 2) NOT NULL,
    RemainedMoney DECIMAL(7, 2) NOT NULL,
    Date DATE NOT NULL,
    CreditCard DECIMAL(7, 2) NOT NULL
);

CREATE TABLE IF NOT EXISTS DebtMarkets (
    id INT AUTO_INCREMENT PRIMARY KEY,
    marketId INT NOT NULL,
    date DATETIME NOT NULL,
    amount DECIMAL(6, 2) NOT NULL,
    FOREIGN KEY (marketId) REFERENCES Markets(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS DoughFactoryProducts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    BreadEquivalent FLOAT NOT NULL,
    Name VARCHAR(50) NOT NULL,
    Status BOOLEAN DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS DoughFactoryLists (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    Date DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS DoughFactoryListDetails (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DoughFactoryProductId INT NOT NULL,
    Quantity INT NOT NULL,
    DoughFactoryListId INT NOT NULL,
    FOREIGN KEY (DoughFactoryProductId) REFERENCES DoughFactoryProducts(Id) ON DELETE CASCADE,
    FOREIGN KEY (DoughFactoryListId) REFERENCES DoughFactoryLists(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Employees (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    Title VARCHAR(50) NOT NULL,
    Status BOOLEAN DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS Expenses (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Detail VARCHAR(200) NOT NULL,
    Date DATETIME NOT NULL,
    Amount DECIMAL(7, 2) NOT NULL,
    userId INT NOT NULL,
    FOREIGN KEY (userId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS GivenProductsToServices (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    Quantity INT NOT NULL,
    Date DATETIME NOT NULL,
    ServiceTypeId INT NOT NULL,
    ServiceProductId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS MoneyReceivedFromMarkets (
    id INT AUTO_INCREMENT PRIMARY KEY,
    marketId INT NOT NULL,
    date DATETIME NOT NULL,
    amount DECIMAL(6, 2) NOT NULL,
    FOREIGN KEY (marketId) REFERENCES Markets(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS MonthlyProductCounts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Count INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Month INT NOT NULL,
    Year INT NOT NULL,
    Category VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS ProductionLists (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Date DATETIME NOT NULL,
    UserId INT NOT NULL,
    CategoryId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ProductionListDetails (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT NOT NULL,
    Price DECIMAL(7, 2) NOT NULL,
    Quantity INT NOT NULL,
    ProductionListId INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductionListId) REFERENCES ProductionLists(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ProductsCountings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    Date DATETIME NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS PurchasedProductListDetails (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT NOT NULL,
    Price DECIMAL(7, 2) NOT NULL,
    Quantity INT NOT NULL,
    Date DATETIME NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ReceivedMoney (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    Amount DECIMAL(7, 2) NOT NULL,
    ServiceTypeId INT NOT NULL,
    Date DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS SalaryPayments (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeId INT NOT NULL,
    Month INT NOT NULL,
    Year INT NOT NULL,
    TotalSalary DECIMAL(18, 2) NOT NULL,
    PaidAmount DECIMAL(18, 2) NOT NULL,
    RemainingAmount DECIMAL(18, 2) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ServiceLists (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Date DATETIME NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ServiceListDetails (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ServiceListId INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(7, 2) NOT NULL,
    MarketContractId INT NOT NULL,
    FOREIGN KEY (ServiceListId) REFERENCES ServiceLists(Id) ON DELETE CASCADE,
    FOREIGN KEY (MarketContractId) REFERENCES MarketContracts(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ServiceRemindMoney (
    id INT AUTO_INCREMENT PRIMARY KEY,
    marketId INT NOT NULL,
    date DATETIME NOT NULL,
    amount DECIMAL(6, 2) NOT NULL,
    FOREIGN KEY (marketId) REFERENCES Markets(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS ServiceStaleProducts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    Date DATETIME NOT NULL,
    ServiceTypeId INT NOT NULL,
    ServiceProductId INT NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceTypeId) REFERENCES ServiceTypes(Id) ON DELETE CASCADE,
    FOREIGN KEY (ServiceProductId) REFERENCES ServiceProducts(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS StaleBread (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Quantity INT NOT NULL,
    Date DATETIME NOT NULL,
    DoughFactoryProductId INT NOT NULL,
    FOREIGN KEY (DoughFactoryProductId) REFERENCES DoughFactoryProducts(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS StaleBreadReceivedFromMarkets (
    id INT AUTO_INCREMENT PRIMARY KEY,
    marketId INT NOT NULL,
    date DATETIME NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (marketId) REFERENCES Markets(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS StaleProductsReceivedFromMarkets (
    id INT AUTO_INCREMENT PRIMARY KEY,
    marketId INT NOT NULL,
    serviceProductId INT NOT NULL,
    date DATETIME NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (marketId) REFERENCES Markets(Id) ON DELETE CASCADE,
    FOREIGN KEY (serviceProductId) REFERENCES ServiceProducts(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Advances (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeId INT NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    Year INT NOT NULL,
    Month INT NOT NULL,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS StaleProducts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    Date DATETIME NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);


-- Insert Categories default data if not exist --
INSERT INTO `Categories` (`Id`, `Name`, `Store`, `IsActive`)
SELECT 1, 'Pasta', FALSE, TRUE
WHERE NOT EXISTS (SELECT 1 FROM `Categories` WHERE `Id` = 1);
INSERT INTO `Categories` (`Id`, `Name`, `Store`, `IsActive`)
SELECT 2, 'Borek', FALSE, TRUE
WHERE NOT EXISTS (SELECT 1 FROM `Categories` WHERE `Id` = 2);
INSERT INTO `Categories` (`Id`, `Name`, `Store`, `IsActive`)
SELECT 3, 'Satin alinan', FALSE, TRUE
WHERE NOT EXISTS (SELECT 1 FROM `Categories` WHERE `Id` = 3);


-- Insert OperationClaims default data if not exist --
INSERT INTO `OperationClaims` (`Id`, `Name`)
SELECT 1, 'Hamurkar'
WHERE NOT EXISTS (SELECT 1 FROM `OperationClaims` WHERE `Id` = 1);
INSERT INTO `OperationClaims` (`Id`, `Name`)
SELECT 2, 'Pastaci'
WHERE NOT EXISTS (SELECT 1 FROM `OperationClaims` WHERE `Id` = 2);
INSERT INTO `OperationClaims` (`Id`, `Name`)
SELECT 3, 'Borekci'
WHERE NOT EXISTS (SELECT 1 FROM `OperationClaims` WHERE `Id` = 3);
INSERT INTO `OperationClaims` (`Id`, `Name`)
SELECT 4, 'Sofor'
WHERE NOT EXISTS (SELECT 1 FROM `OperationClaims` WHERE `Id` = 4);
INSERT INTO `OperationClaims` (`Id`, `Name`)
SELECT 5, 'Tezgahtar'
WHERE NOT EXISTS (SELECT 1 FROM `OperationClaims` WHERE `Id` = 5);
INSERT INTO `OperationClaims` (`Id`, `Name`)
SELECT 6, 'Admin'
WHERE NOT EXISTS (SELECT 1 FROM `OperationClaims` WHERE `Id` = 6);


-- Insert SystemAvailabilityTime default data if not exist --
INSERT INTO `SystemAvailabilityTime` (`Id`, `OpenTime`, `CloseTime`)
SELECT 1, 1, 23
WHERE NOT EXISTS (SELECT 1 FROM `SystemAvailabilityTime` WHERE `Id` = 1);


-- Insert BreadPrices default data if not exist --
INSERT INTO `BreadPrices` (`Date`, `Price`)
SELECT NOW(), 10.00
WHERE NOT EXISTS (SELECT 1 FROM `BreadPrices`);

