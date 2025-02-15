CREATE TABLE `ufe_rates` (
  `rate_timestamp` int NOT NULL,
  `fee_rate` float NOT NULL,
  `fee_amount` float NOT NULL,
  PRIMARY KEY (`rate_timestamp`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
