-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le :  Dim 16 fév. 2020 à 21:19
-- Version du serveur :  5.7.26
-- Version de PHP :  7.2.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `pomodedouche`
--
CREATE DATABASE IF NOT EXISTS `pomodedouche` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `pomodedouche`;

-- --------------------------------------------------------

--
-- Structure de la table `pomodoro`
--

DROP TABLE IF EXISTS `pomodoro`;
CREATE TABLE IF NOT EXISTS `pomodoro` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `pomodoro`
--

INSERT INTO `pomodoro` (`id`, `name`, `time`) VALUES
(1, 'tester 123', '2020-02-16 19:29:08'),
(2, 'yolo', '2020-02-16 19:50:00');

-- --------------------------------------------------------

--
-- Structure de la table `pomodoro_tags`
--

DROP TABLE IF EXISTS `pomodoro_tags`;
CREATE TABLE IF NOT EXISTS `pomodoro_tags` (
  `id_pomo` int(11) NOT NULL,
  `id_tag` int(11) NOT NULL,
  UNIQUE KEY `id_pomo` (`id_pomo`,`id_tag`),
  KEY `idx_pomo` (`id_pomo`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `pomodoro_tags`
--

INSERT INTO `pomodoro_tags` (`id_pomo`, `id_tag`) VALUES
(1, 1),
(1, 2),
(1, 3),
(2, 1),
(2, 4);

-- --------------------------------------------------------

--
-- Structure de la table `tag`
--

DROP TABLE IF EXISTS `tag`;
CREATE TABLE IF NOT EXISTS `tag` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `color` varchar(6) NOT NULL DEFAULT '5DEA84',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `tag`
--

INSERT INTO `tag` (`id`, `name`, `color`) VALUES
(1, 'test1', 'FFEA84'),
(2, 'test2', '5D0084'),
(3, 'test3', '5DEAFF'),
(4, 'test 3', '33EA84');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
