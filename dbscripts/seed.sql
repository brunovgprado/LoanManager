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

--Creating Loans table
CREATE TABLE Loans (
    Id UUID PRIMARY KEY,
    FriendId UUID,
    GameId UUID,
    LoanDate TIMESTAMP,
    Returned BOOLEAN
);

--Creating Users table
CREATE TABLE Users (
    Id UUID PRIMARY KEY,
    Email varchar(100),
    Password varchar(200)
);

ALTER TABLE "friends" OWNER TO loanuser;
ALTER TABLE "games" OWNER TO loanuser;
ALTER TABLE "users" OWNER TO loanuser;

-- Pushing some mocked data to database
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('c8400148-79d5-4d25-8dd8-5b2bd217c6a6', 'Luiz Helmont', '21985487958');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('20cbeabb-b16b-4e64-889c-2441caca602b', 'Bruno Prado', '21985487958');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('10ed49d1-5c65-4197-bb07-98c2f886d0b7', 'Felipe Silveira', '21965968547');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('2898d57d-6f0e-47cd-a3b9-7dcf99b97040', 'Jessica Santos', '21985457896');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('a0f69093-c658-4738-b103-5965b907136d', 'Patrick Narciso', '21998547859');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('98cd51ed-3aab-4dcd-8aad-53757153c4f3', 'Sonia Prado', '21975848968');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('6d25efab-3ab5-47d1-9401-3fd880261066', 'Fernando de Jesus', '21998547585');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('de4019e6-3f02-4a20-a6cb-b6aa3e0ec41d', 'Patricia Souza', '21965210352');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('aaf34e57-204f-45a2-b338-acfc725e2706', 'João Gabriel', '21965520102');
INSERT INTO Friends (Id, Name, PhoneNumber) VALUES ('0d54a6b4-4b46-46a0-bf72-23576c61ff49', 'Gabriel Moura', '51977485478');

INSERT INTO Games (Id, Title, Description, Genre, Platform) VALUES ('7d613abf-367b-45a9-92b3-0d2374d9e523', 'Dino Crisis', 'Amazing survival horror game', 'survival', 'Console');
INSERT INTO Games (Id, Title, Description, Genre, Platform) VALUES ('ebe22480-e47b-409c-af0b-0d1ec697a71a', 'Dino Crisis 2', 'The best game ever!', 'survival', 'Console');
INSERT INTO Games (Id, Title, Description, Genre, Platform) VALUES ('35c210a4-1fd6-4403-a692-368995fb393a', 'Driver', 'Take your license and be a bad boy', 'Action', 'Console');
INSERT INTO Games (Id, Title, Description, Genre, Platform) VALUES ('a62debf5-feb0-47b8-becc-580bad5648ef', 'Driver 2', 'Now you can steal some cars while the police don''t show up', 'Action', 'Console');
INSERT INTO Games (Id, Title, Description, Genre, Platform) VALUES ('e02945e9-f3fc-476f-be06-93aecf6f1689', 'Driver 3', 'Oh yes! this is it!', 'Action', 'Console');
INSERT INTO Games (Id, Title, Description, Genre, Platform) VALUES ('889383d8-8f27-493f-883e-ad6fde0c9e9b', 'Golden Eye 007', 'The best 007 video game ever', 'Action', 'Console');
