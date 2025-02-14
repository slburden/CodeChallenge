CREATE TABLE `card_details` (
  `id` int NOT NULL AUTO_INCREMENT,
  `number` varchar(15) NOT NULL,
  `active` tinyint DEFAULT '0',
  `balance` float DEFAULT '0',
  `limit` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `number_UNIQUE` (`number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
