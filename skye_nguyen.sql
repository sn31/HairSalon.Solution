-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Sep 21, 2018 at 09:54 PM
-- Server version: 5.7.21
-- PHP Version: 7.2.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `skye_nguyen`
--
CREATE DATABASE IF NOT EXISTS `skye_nguyen` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `skye_nguyen`;

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

CREATE TABLE `clients` (
  `id` int(32) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `stylist_id` int(11) NOT NULL,
  `phone` varchar(255) NOT NULL DEFAULT '000-000-0000'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `clients`
--

INSERT INTO `clients` (`id`, `name`, `stylist_id`, `phone`) VALUES
(7, 'Matt', 6, '123456789'),
(8, 'Elmer\'s Glue', 6, '123491737'),
(9, 'Happy Client', 9, '127137123'),
(10, 'Another person', 6, '123712'),
(11, 'Tester', 6, '1231283'),
(12, 'Matthew', 6, '3487138'),
(13, 'ImBored', 7, '12314'),
(14, 'IsThisWorking?', 7, '32412341'),
(15, 'Probably', 7, '123717231'),
(16, 'RandomLady', 6, '7431843431');

-- --------------------------------------------------------

--
-- Table structure for table `stylists`
--

CREATE TABLE `stylists` (
  `id` int(32) NOT NULL,
  `name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stylists`
--

INSERT INTO `stylists` (`id`, `name`) VALUES
(6, 'Elaine'),
(7, 'Helen'),
(8, 'Linda'),
(9, 'Some RandomDude');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id`),
  ADD KEY `Stylist` (`stylist_id`);

--
-- Indexes for table `stylists`
--
ALTER TABLE `stylists`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `clients`
--
ALTER TABLE `clients`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `stylists`
--
ALTER TABLE `stylists`
  MODIFY `id` int(32) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `clients`
--
ALTER TABLE `clients`
  ADD CONSTRAINT `Stylist` FOREIGN KEY (`stylist_id`) REFERENCES `stylists` (`id`) ON UPDATE CASCADE;
