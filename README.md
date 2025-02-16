# RapidPay

## Setup
To set up RapidPay, follow these steps:
1. Clone the repository:
   ```sh
   git clone https://github.com/slburden/CodeChallenge.git
   ```
2. Navigate to the project directory:
   ```sh
   cd CodeChallenge
   ```
3. Startup Docker Compose file:
   ```sh
   docker compose up -d
   ```
4. To check if it is up and running Navigate in a web brower to:
    ```sh
    http://localhost:5034/swagger/index.html
    ```


## Endpoints
RapidPay provides the following API endpoints:

- **GET /api/Card/create** - Create a new Card
- **GET /api/Card/balance** - Get the balance and limit details of a specifc card.
- **GET /api/Card/isauthorized** - Checks to see if a card is authorized.
- **POST /api/Card/pay** - Makes a payment.
- **POST /api/Card/update** - Updates a card record.

## Details

