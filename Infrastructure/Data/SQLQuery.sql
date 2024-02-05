CREATE TABLE Currencies 
(
	Code char(3) PRIMARY KEY NOT NULL,
	Currency nvarchar(20) NOT NULL
)

CREATE TABLE Manufacturers 
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Manufacture nvarchar(50) unique NOT NULL
)

CREATE TABLE Categories 
(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Categoryname nvarchar(50) NOT NULL
)

CREATE TABLE Products 
(
	ArticleNumber INT PRIMARY KEY IDENTITY NOT NULL,
	Title nvarchar(250) NOT NULL,
	ManufacturerId INT REFERENCES Manufacturers(Id) NOT NULL,
	CategoryId INT REFERENCES Categories(Id) NOT NULL,
	Preamble NVARCHAR(200),
	Description NVARCHAR(MAX),
	Specification NVARCHAR(MAX)
)

CREATE TABLE ProductPrices 
(
	ArticleNumber INT PRIMARY KEY REFERENCES Products(ArticleNumber) NOT NULL,
	Price MONEY NOT NULL,
	CurrencyCode CHAR(3) REFERENCES Currencies(Code) NOT NULL
)