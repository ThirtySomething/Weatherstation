CREATE TABLE IF NOT EXISTS `mtype` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `munit` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `mvalue` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TypeID` int(11) NOT NULL,
  `Value` double NOT NULL,
  `UnitID` int(11) NOT NULL,
  `RecordTime` text NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`ID`),
  KEY `FK_MValue_MType_TypeID` (`TypeID`),
  KEY `FK_MValue_MUnit_UnitID` (`UnitID`),
  CONSTRAINT `FK_MValue_MType_TypeID` FOREIGN KEY (`TypeID`) REFERENCES `mtype` (`ID`),
  CONSTRAINT `FK_MValue_MUnit_UnitID` FOREIGN KEY (`UnitID`) REFERENCES `munit` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
