create database BookSwap

CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(200),
    Role NVARCHAR(20),
    CreatedAt DATETIME DEFAULT GETDATE()
)

CREATE TABLE Books (
    BookId INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(200),
    Author NVARCHAR(100),
    Subject NVARCHAR(100),
    Description NVARCHAR(MAX),
    ImageUrl NVARCHAR(200)
)

CREATE TABLE Listings (
    ListingId INT IDENTITY PRIMARY KEY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    BookId INT FOREIGN KEY REFERENCES Books(BookId),
    Type NVARCHAR(20),
    Price DECIMAL(10,2) NULL,
    Status NVARCHAR(20),
    CreatedAt DATETIME DEFAULT GETDATE()
)

CREATE TABLE ChatMessages (
    MessageId INT IDENTITY PRIMARY KEY,
    FromUserId INT FOREIGN KEY REFERENCES Users(UserId),
    ToUserId INT FOREIGN KEY REFERENCES Users(UserId),
    ListingId INT FOREIGN KEY REFERENCES Listings(ListingId),
    MessageText NVARCHAR(MAX),
    SentAt DATETIME DEFAULT GETDATE()
)

CREATE TABLE Transactions (
    TransactionId INT IDENTITY PRIMARY KEY,
    ListingId INT FOREIGN KEY REFERENCES Listings(ListingId),
    LenderId INT FOREIGN KEY REFERENCES Users(UserId),
    BorrowerId INT FOREIGN KEY REFERENCES Users(UserId),
    Type NVARCHAR(20),
    StartedAt DATETIME DEFAULT GETDATE(),
    EndedAt DATETIME NULL
)

CREATE TABLE Ratings (
    RatingId INT IDENTITY PRIMARY KEY,
    FromUserId INT FOREIGN KEY REFERENCES Users(UserId),
    ToUserId INT FOREIGN KEY REFERENCES Users(UserId),
    Stars INT,
    Comment NVARCHAR(300),
    CreatedAt DATETIME DEFAULT GETDATE()
)

INSERT INTO Books (Title, Author, Subject, Description, ImageUrl)
VALUES
('Harry Potter', 'J.K. Rowling', 'Fantasy', 'Popular fantasy novel.', 'https://mozaik-knjiga.hr/wp-content/uploads/2024/12/Harry-Potter-1-I-KAMEN-MUDRACA-500pix.jpg.webp'),
('The Hobbit', 'J.R.R. Tolkien', 'Fantasy', 'Bilbo Baggins adventure.', 'https://znanje.hr/product-images/b6a7383c-a0b2-4c15-9ebb-330fd1da2aaa.jpg');




select * from Books