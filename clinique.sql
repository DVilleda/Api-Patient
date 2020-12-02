-- phpMyAdmin SQL Dump
-- version 4.5.4.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Dec 02, 2020 at 02:31 AM
-- Server version: 5.7.11
-- PHP Version: 5.6.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `clinique`
--

-- --------------------------------------------------------

--
-- Table structure for table `docteur`
--

CREATE TABLE `docteur` (
  `ID` int(11) NOT NULL,
  `Nom` varchar(255) NOT NULL,
  `Prenom` varchar(255) NOT NULL,
  `Num_Employe` int(11) NOT NULL,
  `Specialisation` varchar(255) DEFAULT 'Pratique Générale',
  `APIKey` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `docteur`
--

INSERT INTO `docteur` (`ID`, `Nom`, `Prenom`, `Num_Employe`, `Specialisation`, `APIKey`) VALUES
(1, 'McDonald', 'Ronald', 123456789, 'Alimentation', 'MS5Eb2N0ZXVy'),
(2, 'Smith', 'Adam', 123712893, 'Pratique générale', 'Mi5Eb2N0ZXVy');

-- --------------------------------------------------------

--
-- Table structure for table `notes`
--

CREATE TABLE `notes` (
  `ID` int(11) NOT NULL,
  `id_patient` int(11) NOT NULL,
  `id_docteur` int(11) NOT NULL,
  `Titre` varchar(255) NOT NULL,
  `Contenu` varchar(255) NOT NULL,
  `Date_Note` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `notes`
--

INSERT INTO `notes` (`ID`, `id_patient`, `id_docteur`, `Titre`, `Contenu`, `Date_Note`) VALUES
(1, 1, 1, 'Résultat X-Ray', 'Pas de fracture dans le bras', '2020-12-01'),
(2, 1, 2, 'Résultat Test Sang', 'Niveau de globule rouge est normal', '2020-12-05');

-- --------------------------------------------------------

--
-- Table structure for table `patient`
--

CREATE TABLE `patient` (
  `ID` int(11) NOT NULL,
  `Num_AssMal` varchar(15) NOT NULL,
  `Nom` varchar(255) NOT NULL,
  `Prenom` varchar(255) NOT NULL,
  `Date_Naissance` date NOT NULL,
  `Sexe` varchar(30) DEFAULT 'Préfère ne pas répondre',
  `Allergies` varchar(255) DEFAULT 'Aucune Mentionné',
  `Adresse` varchar(255) NOT NULL,
  `Num_Tel` int(15) NOT NULL,
  `Assurance` tinyint(1) DEFAULT '0',
  `APIKey` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `patient`
--

INSERT INTO `patient` (`ID`, `Num_AssMal`, `Nom`, `Prenom`, `Date_Naissance`, `Sexe`, `Allergies`, `Adresse`, `Num_Tel`, `Assurance`, `APIKey`) VALUES
(1, 'asdasd123213123', 'McMaster', 'John', '1975-10-01', 'Homme', 'Aucune Mentionné', 'St Martins House, 2 Peacock Ln, Leicester LE1 5PZ, United Kingdom', 1162615200, 0, 'MS5QYXRpZW50'),
(2, 'asdas2567567567', 'Sanchez', 'Sara', '1987-04-01', 'Femme', 'Arachides', '1624 Saint-Catherine St W, Montreal, Quebec H3H 2S7', 514569123, 1, 'Mi5QYXRpZW50'),
(3, 'uiouiou65675674', 'Richard', 'Maurice', '2000-08-31', 'Homme', 'Aucune Mentionné', '1909 Avenue des Canadiens-de-Montréal, Montréal, QC H4B 5G0', 514999999, 1, 'My5QYXRpZW50');

-- --------------------------------------------------------

--
-- Table structure for table `patient_docteur`
--

CREATE TABLE `patient_docteur` (
  `ID` int(11) NOT NULL,
  `id_patient` int(11) NOT NULL,
  `id_docteur` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `patient_docteur`
--

INSERT INTO `patient_docteur` (`ID`, `id_patient`, `id_docteur`) VALUES
(1, 1, 2);

-- --------------------------------------------------------

--
-- Table structure for table `prescriptions`
--

CREATE TABLE `prescriptions` (
  `ID` int(11) NOT NULL,
  `id_patient` int(11) NOT NULL,
  `id_docteur` int(11) NOT NULL,
  `Prescription` varchar(255) NOT NULL,
  `Medicament` varchar(255) NOT NULL,
  `Quantite` varchar(255) NOT NULL,
  `Instructions` varchar(255) NOT NULL,
  `Date_Prescription` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `prescriptions`
--

INSERT INTO `prescriptions` (`ID`, `id_patient`, `id_docteur`, `Prescription`, `Medicament`, `Quantite`, `Instructions`, `Date_Prescription`) VALUES
(1, 1, 2, 'Traitement pour le Clostridium Difficile', 'Métronizadole', '500mg', 'Prendre 1 fois par jour pendant 10 jours. Si aucune amélioration après 3 à 5 jours, contacter la clinique pour une autre consultation.', '2020-11-30'),
(2, 2, 1, 'Traitement pour l\'anémie causée par des carences', 'Supplément de fer et supplément de vitamine B12', 'Tel que spécifié dans le contenant.', 'Prendre les supplément 1 heure après un repas. Essayer de changer le plan alimentaire pour incorporer des aliments plus riches en fer.', '2020-11-10');

-- --------------------------------------------------------

--
-- Table structure for table `reference`
--

CREATE TABLE `reference` (
  `ID` int(11) NOT NULL,
  `id_patient` int(11) NOT NULL,
  `id_docteur` int(11) NOT NULL,
  `Nom_Specialisation` varchar(255) NOT NULL,
  `Lieu_Reference` varchar(255) NOT NULL,
  `Raison` varchar(255) NOT NULL,
  `Date_Reference` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `reference`
--

INSERT INTO `reference` (`ID`, `id_patient`, `id_docteur`, `Nom_Specialisation`, `Lieu_Reference`, `Raison`, `Date_Reference`) VALUES
(1, 2, 2, 'Cardiologie', 'Institut de cardiologie de Montréal', 'Test de résonnance magnétique au niveau des poumons et coeur.', '2020-09-10');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `docteur`
--
ALTER TABLE `docteur`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Num_Employé` (`Num_Employe`);

--
-- Indexes for table `notes`
--
ALTER TABLE `notes`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `NotePatient_FK` (`id_patient`),
  ADD KEY `NoteDOC_FK` (`id_docteur`);

--
-- Indexes for table `patient`
--
ALTER TABLE `patient`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Num_AssMal` (`Num_AssMal`);

--
-- Indexes for table `patient_docteur`
--
ALTER TABLE `patient_docteur`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `JointurePatient_FK` (`id_patient`),
  ADD KEY `JointureDOC_FK` (`id_docteur`);

--
-- Indexes for table `prescriptions`
--
ALTER TABLE `prescriptions`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `PresPatient_FK` (`id_patient`),
  ADD KEY `PresDOC_FK` (`id_docteur`);

--
-- Indexes for table `reference`
--
ALTER TABLE `reference`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `RefPatient_FK` (`id_patient`),
  ADD KEY `RefDOC_FK` (`id_docteur`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `docteur`
--
ALTER TABLE `docteur`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `notes`
--
ALTER TABLE `notes`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `patient`
--
ALTER TABLE `patient`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `patient_docteur`
--
ALTER TABLE `patient_docteur`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `prescriptions`
--
ALTER TABLE `prescriptions`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `reference`
--
ALTER TABLE `reference`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `notes`
--
ALTER TABLE `notes`
  ADD CONSTRAINT `NoteDOC_FK` FOREIGN KEY (`id_docteur`) REFERENCES `docteur` (`ID`),
  ADD CONSTRAINT `NotePatient_FK` FOREIGN KEY (`id_patient`) REFERENCES `patient` (`ID`);

--
-- Constraints for table `patient_docteur`
--
ALTER TABLE `patient_docteur`
  ADD CONSTRAINT `JointureDOC_FK` FOREIGN KEY (`id_docteur`) REFERENCES `docteur` (`ID`),
  ADD CONSTRAINT `JointurePatient_FK` FOREIGN KEY (`id_patient`) REFERENCES `patient` (`ID`);

--
-- Constraints for table `prescriptions`
--
ALTER TABLE `prescriptions`
  ADD CONSTRAINT `PresDOC_FK` FOREIGN KEY (`id_docteur`) REFERENCES `docteur` (`ID`),
  ADD CONSTRAINT `PresPatient_FK` FOREIGN KEY (`id_patient`) REFERENCES `patient` (`ID`);

--
-- Constraints for table `reference`
--
ALTER TABLE `reference`
  ADD CONSTRAINT `RefDOC_FK` FOREIGN KEY (`id_docteur`) REFERENCES `docteur` (`ID`),
  ADD CONSTRAINT `RefPatient_FK` FOREIGN KEY (`id_patient`) REFERENCES `patient` (`ID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
