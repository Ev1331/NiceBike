CREATE DATABASE  IF NOT EXISTS `dbNicebike` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `dbNicebike`;
-- MySQL dump 10.13  Distrib 8.0.31, for macos12 (x86_64)
--
-- Host: pat.infolab.ecam.be    Database: dbNicebike
-- ------------------------------------------------------
-- Server version	8.0.31-0ubuntu0.22.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `bike`
--

DROP TABLE IF EXISTS `bike`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bike` (
  `IdBike` int NOT NULL AUTO_INCREMENT,
  `Colour` varchar(45) DEFAULT NULL,
  `Type` varchar(45) DEFAULT NULL,
  `Size` varchar(45) DEFAULT NULL,
  `Ref` varchar(45) DEFAULT NULL,
  `Technician` int DEFAULT NULL,
  `BikeModel` int DEFAULT NULL,
  `Status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`IdBike`),
  KEY `Technician_idx` (`Technician`),
  KEY `BikeModel_idx` (`BikeModel`),
  CONSTRAINT `BikeModel` FOREIGN KEY (`BikeModel`) REFERENCES `bikemodel` (`IdBikeModel`),
  CONSTRAINT `Technician` FOREIGN KEY (`Technician`) REFERENCES `employee` (`IdEmployee`)
) ENGINE=InnoDB AUTO_INCREMENT=281 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `bikemodel`
--

DROP TABLE IF EXISTS `bikemodel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bikemodel` (
  `IdBikeModel` int NOT NULL AUTO_INCREMENT,
  `Price` int DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`IdBikeModel`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `customer`
--

DROP TABLE IF EXISTS `customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `customer` (
  `IdCustomer` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Surname` varchar(45) DEFAULT NULL,
  `Mail` varchar(45) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Street` varchar(45) DEFAULT NULL,
  `Town` varchar(45) DEFAULT NULL,
  `Number` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`IdCustomer`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `employee`
--

DROP TABLE IF EXISTS `employee`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `employee` (
  `IdEmployee` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Surname` varchar(45) DEFAULT NULL,
  `Mail` varchar(45) DEFAULT NULL,
  `JobTitle` varchar(45) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`IdEmployee`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `order`
--

DROP TABLE IF EXISTS `order`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `order` (
  `IdOrder` int NOT NULL AUTO_INCREMENT,
  `CustomerId` int DEFAULT NULL,
  `Date` varchar(45) DEFAULT NULL,
  `DeliveryDate` varchar(45) DEFAULT NULL,
  `Status` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`IdOrder`),
  KEY `CustomerId_idx` (`CustomerId`),
  CONSTRAINT `CustomerId` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`IdCustomer`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `orderdetails`
--

DROP TABLE IF EXISTS `orderdetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `orderdetails` (
  `IdOrderDetails` int NOT NULL AUTO_INCREMENT,
  `IdOrder` int DEFAULT NULL,
  `Bike` int DEFAULT NULL,
  PRIMARY KEY (`IdOrderDetails`),
  KEY `Order_idx` (`IdOrder`),
  KEY `Bike_idx` (`Bike`),
  CONSTRAINT `Bike` FOREIGN KEY (`Bike`) REFERENCES `bike` (`IdBike`),
  CONSTRAINT `Order` FOREIGN KEY (`IdOrder`) REFERENCES `order` (`IdOrder`)
) ENGINE=InnoDB AUTO_INCREMENT=251 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `part`
--

DROP TABLE IF EXISTS `part`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `part` (
  `IdPart` int NOT NULL AUTO_INCREMENT,
  `Ref` varchar(45) DEFAULT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `Quantity` int DEFAULT NULL,
  `Threshold` int DEFAULT NULL,
  `Supplier` int DEFAULT NULL,
  PRIMARY KEY (`IdPart`),
  KEY `Supplier_idx` (`Supplier`),
  CONSTRAINT `Supplier` FOREIGN KEY (`Supplier`) REFERENCES `suppliers` (`IdSupplier`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `suppliers`
--

DROP TABLE IF EXISTS `suppliers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `suppliers` (
  `IdSupplier` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Mail` varchar(45) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Street` varchar(45) DEFAULT NULL,
  `Town` varchar(45) DEFAULT NULL,
  `Number` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`IdSupplier`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-29 15:14:07
