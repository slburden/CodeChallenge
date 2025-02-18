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
   ```sh
   curl -X 'GET'   'http://localhost:5034/api/Card/create?limit=34567'   -H 'accept: */*'
   ```
   ```json
   {
      "number": "824038779307944",
      "active": false,
      "balance": 19857.2,
      "limit": 34567
   }
   ```
- **GET /api/Card/balance** - Get the balance and limit details of a specifc card.
   ```sh
   curl -X 'POST' \
   'http://localhost:5034/api/Card/balance' \
   -H 'accept: */*' \
   -H 'Content-Type: application/json' \
   -d '{
   "cardNumber": "688720921633742"
   }'
   ```
   ```json
   {
      "cardNumber": "688720921633742",
      "balance": 19857.2,
      "creditLimit": 34567
   }
   ```
- **POST /api/Card/isauthorized** - Checks to see if a card is authorized for an amount.
   ```sh
   curl -X 'POST' \
   'http://localhost:5034/api/Card/isauthorized' \
   -H 'accept: */*' \
   -H 'Content-Type: application/json' \
   -d '{
   "cardNumber": "688720921633742",
   "amount": 300
   }'
   ```
   ```json
   {
      "authorized": true,
      "denialReason": null
   }
   ```
- **POST /api/Card/pay** - Makes a payment.
   ```sh
   curl -X 'POST' \
   'http://localhost:5034/api/Card/pay' \
   -H 'accept: */*' \
   -H 'Content-Type: application/json' \
   -d '{
   "cardNumber": "688720921633742",
   "amount": 150
   }'
   ```
   ```json
   {
      "number": "688720921633742",
      "active": true,
      "balance": 20008.6,
      "limit": 34567
   }
   ```
- **POST /api/Card/update** - Updates a card record.
   ```sh
    curl -X 'POST' \
   'http://localhost:5034/api/Card/update' \
   -H 'accept: */*' \
   -H 'Content-Type: application/json' \
   -d '{
         "number": "688720921633742",
         "active": true,
         "balance": 15.0,
         "limit": 34567
      }'
   ```
   ```json
   {
      "number": "688720921633742",
      "active": true,
      "balance": 15,
      "limit": 34567
   }
   ```

## Details
### Architecture  
RapidPay is split into four separate projects:  

- **RapidPay.Api**  
  > Provides controllers that act as an interface between the API user and the RapidPay.Business layer.  

- **RapidPay.Business**  
  > Serves as the core business logic layer, handling all application rules and operations. It utilizes repositories from the RapidPay.DataAccess layer to interact with the underlying data.  

- **RapidPay.DataAccess**  
  > Uses Dapper ORM to provide Repositorys that can be used by the Business layer to access the database.

- **RapidPay.Models**  
  > A centralized location that contains models used throughout the application.



