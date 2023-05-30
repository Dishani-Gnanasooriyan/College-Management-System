-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Feb 17, 2023 at 06:34 AM
-- Server version: 10.1.16-MariaDB
-- PHP Version: 5.6.24

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `college_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `departmenttable`
--

CREATE TABLE `departmenttable` (
  `depname` varchar(25) NOT NULL,
  `depdesc` varchar(30) NOT NULL,
  `depduration` int(11) NOT NULL,
  `amount` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `departmenttable`
--

INSERT INTO `departmenttable` (`depname`, `depdesc`, `depduration`, `amount`) VALUES
('ACCOUNT', 'MANAGEMENT', 4, 45000),
('BCOM', 'BUSINESS', 5, 44000),
('PHYSICS', 'SCIENCE', 6, 63000);

-- --------------------------------------------------------

--
-- Table structure for table `fees`
--

CREATE TABLE `fees` (
  `feesnum` int(11) NOT NULL,
  `stuid` int(11) NOT NULL,
  `stuname` varchar(20) NOT NULL,
  `payment_date` varchar(20) NOT NULL,
  `Dep_Fees` int(11) NOT NULL,
  `payment_fees` int(10) NOT NULL,
  `payment` int(10) NOT NULL,
  `balance` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `fees`
--

INSERT INTO `fees` (`feesnum`, `stuid`, `stuname`, `payment_date`, `Dep_Fees`, `payment_fees`, `payment`, `balance`) VALUES
(1, 1, 'SAMAR', '2022-04-06', 45000, 21000, 21000, 24000),
(2, 2, 'ANTONY', '2022-04-06', 44000, 14000, 14000, 30000);

-- --------------------------------------------------------

--
-- Table structure for table `studenttable`
--

CREATE TABLE `studenttable` (
  `stuid` int(11) NOT NULL,
  `stuname` varchar(20) NOT NULL,
  `stugender` varchar(7) NOT NULL,
  `studob` varchar(15) NOT NULL,
  `stuphone` varchar(10) NOT NULL,
  `studep` varchar(20) NOT NULL,
  `stufees` varchar(20) NOT NULL,
  `Teacher` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `studenttable`
--

INSERT INTO `studenttable` (`stuid`, `stuname`, `stugender`, `studob`, `stuphone`, `studep`, `stufees`, `Teacher`) VALUES
(1, 'SAMAR', 'FEMALE', '1998-04-02', '0758415236', 'ACCOUNT', '45000', 'NITHAN'),
(2, 'ANTONY', 'MALE', '2000-06-03', '0754565123', 'BCOM', '44000', 'MAALINI');

-- --------------------------------------------------------

--
-- Table structure for table `teachertable`
--

CREATE TABLE `teachertable` (
  `teacherid` int(20) NOT NULL,
  `teachername` varchar(20) NOT NULL,
  `teachergen` varchar(7) NOT NULL,
  `teacherdob` varchar(15) NOT NULL,
  `teacherphone` varchar(10) NOT NULL,
  `teacherdep` varchar(20) NOT NULL,
  `teacheraddress` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `teachertable`
--

INSERT INTO `teachertable` (`teacherid`, `teachername`, `teachergen`, `teacherdob`, `teacherphone`, `teacherdep`, `teacheraddress`) VALUES
(1, 'KUMAR', 'MALE', '1988-06-03', '0778545698', 'PHYSICS', 'KANDY'),
(2, 'MAALINI', 'FEMALE', '1978-05-03', '0754852147', 'BCOM', 'COLOMBO'),
(3, 'NITHAN', 'MALE', '1991-03-05', '0775412365', 'ACCOUNT', 'KURUNAKALA');

-- --------------------------------------------------------

--
-- Table structure for table `usertable`
--

CREATE TABLE `usertable` (
  `userid` int(11) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usertable`
--

INSERT INTO `usertable` (`userid`, `username`, `password`) VALUES
(1, 'DISHANI', '990714');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `departmenttable`
--
ALTER TABLE `departmenttable`
  ADD PRIMARY KEY (`depname`);

--
-- Indexes for table `fees`
--
ALTER TABLE `fees`
  ADD PRIMARY KEY (`feesnum`),
  ADD KEY `stuid` (`stuid`);

--
-- Indexes for table `studenttable`
--
ALTER TABLE `studenttable`
  ADD PRIMARY KEY (`stuid`),
  ADD KEY `fk2` (`studep`);

--
-- Indexes for table `teachertable`
--
ALTER TABLE `teachertable`
  ADD PRIMARY KEY (`teacherid`),
  ADD KEY `fk1` (`teacherdep`);

--
-- Indexes for table `usertable`
--
ALTER TABLE `usertable`
  ADD PRIMARY KEY (`userid`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `fees`
--
ALTER TABLE `fees`
  ADD CONSTRAINT `fees_ibfk_1` FOREIGN KEY (`stuid`) REFERENCES `studenttable` (`stuid`);

--
-- Constraints for table `studenttable`
--
ALTER TABLE `studenttable`
  ADD CONSTRAINT `fk2` FOREIGN KEY (`studep`) REFERENCES `departmenttable` (`depname`);

--
-- Constraints for table `teachertable`
--
ALTER TABLE `teachertable`
  ADD CONSTRAINT `fk1` FOREIGN KEY (`teacherdep`) REFERENCES `departmenttable` (`depname`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
