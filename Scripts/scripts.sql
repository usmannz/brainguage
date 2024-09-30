SET GLOBAL character_set_server = 'utf8mb4';
SET GLOBAL collation_server = 'utf8mb4_general_ci';

CREATE DATABASE brainguage
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

CREATE TABLE `users` (
  `Id` CHAR(36) PRIMARY KEY,
  `FirstName` varchar(200) DEFAULT NULL,
  `LastName` varchar(200) ,
  `Email` varchar(200) ,
  `StatusId` int DEFAULT '1',
  `IsDeleted` bit(1) DEFAULT b'0',
  `Password` varchar(200) ,
  `CreateStamp` datetime DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `DeletedBy` int DEFAULT NULL,
  `UpdateStamp` datetime DEFAULT NULL,
  `DeleteStamp` datetime DEFAULT NULL,
  `isEmail` bit(1) DEFAULT b'0',
  `ContactPhoneNumber` varchar(45) DEFAULT NULL,
  `Address` varchar(200) DEFAULT NULL
  );
  
CREATE TABLE `userroles` (
  `Id` char(36) PRIMARY KEY,
  `UsersId` char(36) DEFAULT NULL,
  `RoleId` int DEFAULT NULL
);


CREATE TABLE `roles` (
  `Id` int PRIMARY KEY AUTO_INCREMENT,
  `Name` varchar(200) DEFAULT NULL
);
INSERT INTO `brainguage`.`Roles` (`Name`) VALUES ('Admin');
INSERT INTO `brainguage`.`Roles` (`Name`) VALUES ('User');

CREATE TABLE `questions` (
  `Id` CHAR(36) PRIMARY KEY,
  `question` varchar(5000) DEFAULT NULL,
  `answer` varchar(5000) ,
  `IsDeleted` bit(1) DEFAULT b'0',
  `CreateStamp` datetime DEFAULT NULL,
  `CreatedBy` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `DeletedBy` int DEFAULT NULL,
  `UpdateStamp` datetime DEFAULT NULL,
  `DeleteStamp` datetime DEFAULT NULL
  );