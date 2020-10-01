\c loandb

--Creating Games table
CREATE TABLE Games (
    Id UUID PRIMARY KEY,
    Title varchar(50),
    Description varchar(200),
    Genre varchar(50),
    Platform varchar(50)
);

--Creating Friends table
CREATE TABLE Friends (
    Id UUID PRIMARY KEY,
    Name varchar(100),
    PhoneNumber varchar(50)
);

--Creating Games table
CREATE TABLE Loans (
    Id UUID PRIMARY KEY,
    FriendId UUID,
    GameId UUID,
    LoanDate TIMESTAMP,
    LoanStatus BOOLEAN
);