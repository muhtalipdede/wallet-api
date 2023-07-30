# Wallet API

## Description

This is a simple API for a wallet. It allows you to create a wallet, deposit money, withdraw money and transfer money to another wallet.

## Layers

The API is divided into 3 layers:

- **Controller**: This layer is responsible for receiving the requests and returning the responses.
- **Service**: This layer is responsible for the business rules.
- **Repository**: This layer is responsible for the database access.

## Database

The database used is InMemoryDatabase, which is a database in memory. It is used only for testing purposes.

## How to run

To run the API, you need to have the .NET 6.0 SDK installed on your machine.

To run the API, you need to run the following command in the root folder of the project:

```bash
dotnet run --project /Wallet.Api/Wallet.Api.csproj
```

## How to test

To run the tests, you need to run the following command in the root folder of the project:

```bash
dotnet test
```

## Endpoints

### Create Wallet

```http
POST /wallet
```

| Parameter | Type | Description |
| :--- | :--- | :--- |
| `name` | `string` | **Required**. The name of the wallet. |

#### Response

```javascript
{
  "id": "string",
  "name": "string",
  "balance": 0
}
```

### Get Balance

```http
GET /wallet/{id}/balance
```

| Parameter | Type | Description |
| :--- | :--- | :--- |
| `id` | `string` | **Required**. The id of the wallet. |

#### Response

```javascript
{
  "id": "string",
  "name": "string",
  "balance": 0
}
```

### Deposit

```http
POST /wallet/{id}/deposit
```

| Parameter | Type | Description |
| :--- | :--- | :--- |
| `id` | `string` | **Required**. The id of the wallet. |

#### Body

```javascript
{
  "amount": 0
}
```

#### Response

```javascript
{
  "id": "string",
  "name": "string",
  "balance": 0
}
```

### Withdraw

```http
POST /wallet/{id}/withdraw
```

| Parameter | Type | Description |
| :--- | :--- | :--- |
| `id` | `string` | **Required**. The id of the wallet. |

#### Body

```javascript
{
  "amount": 0
}
```

#### Response

```javascript
{
  "id": "string",
  "name": "string",
  "balance": 0
}
```

## Unit Tests

The unit tests are in the `Wallet.Test` project.

## Swagger

The swagger is in the `Wallet.Api` project.
