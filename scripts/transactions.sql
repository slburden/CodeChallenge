CREATE TABLE `transaction` (
  `id` int NOT NULL,
  `card_id` int NOT NULL,
  `charge_time` varchar(45) NOT NULL DEFAULT 'NOW()',
  `amount` float DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_transaction_01_idx` (`card_id`),
  CONSTRAINT `fk_transaction_01` FOREIGN KEY (`card_id`) REFERENCES `card_details` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
