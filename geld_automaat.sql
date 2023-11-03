-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 03 nov 2023 om 09:05
-- Serverversie: 10.4.22-MariaDB
-- PHP-versie: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `geld_automaat`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `transactions`
--

CREATE TABLE `transactions` (
  `idtransactions` int(11) NOT NULL,
  `amount` int(11) DEFAULT NULL,
  `positive` tinyint(4) DEFAULT NULL,
  `user_iduser` int(11) NOT NULL,
  `date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Gegevens worden geëxporteerd voor tabel `transactions`
--

INSERT INTO `transactions` (`idtransactions`, `amount`, `positive`, `user_iduser`, `date`) VALUES
(1, 30, 0, 1, '2023-11-01 08:03:19'),
(7, 40, 1, 1, '2023-11-01 08:23:15'),
(8, 3, 1, 1, '2023-11-01 12:59:18'),
(9, 2, 1, 2, '2023-11-01 13:25:03');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `user`
--

CREATE TABLE `user` (
  `iduser` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` longtext DEFAULT NULL,
  `saldo` int(11) DEFAULT NULL,
  `rekeningnummer` int(11) DEFAULT NULL,
  `admin` tinyint(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Gegevens worden geëxporteerd voor tabel `user`
--

INSERT INTO `user` (`iduser`, `username`, `password`, `saldo`, `rekeningnummer`, `admin`) VALUES
(1, 'Thijs', '123', 4989, 123, 1),
(2, 'Miguel', '1234', 23, 1234, 0),
(4, '696r9', '567', 0, 55555, 0),
(7, '69', '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8', 0, 567, 1),
(8, 'test3', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 0, 123456, 1);

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `transactions`
--
ALTER TABLE `transactions`
  ADD PRIMARY KEY (`idtransactions`),
  ADD KEY `fk_transactions_user_idx` (`user_iduser`);

--
-- Indexen voor tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`iduser`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `transactions`
--
ALTER TABLE `transactions`
  MODIFY `idtransactions` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT voor een tabel `user`
--
ALTER TABLE `user`
  MODIFY `iduser` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `transactions`
--
ALTER TABLE `transactions`
  ADD CONSTRAINT `fk_transactions_user` FOREIGN KEY (`user_iduser`) REFERENCES `user` (`iduser`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
