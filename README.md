# FinTrack

A simple digital wallet application built with **ASP.NET Core MVC**, **Entity Framework Core**, **PostgreSQL**, **Docker**, and **Tailwind CSS**.

FinTrack allows users to register accounts, authenticate securely, deposit funds, withdraw funds, transfer money between users, and track transaction history through a dashboard.

---

## Features

### Authentication

* User Registration
* User Login
* User Logout
* Session-Based Authentication
* Password Hashing

### Wallet Operations

* Deposit Funds
* Withdraw Funds
* Transfer Funds Between Users
* Balance Validation

### Dashboard

* View Current Balance
* View Transaction History
* View Sent Transactions
* View Received Transactions
* View Deposit Records
* View Withdrawal Records

### Transaction Management

* Transfer Transactions
* Deposit Transactions
* Withdrawal Transactions
* Timestamped Transaction History

### Infrastructure

* ASP.NET Core MVC
* Entity Framework Core
* PostgreSQL
* Docker Multi-Container Setup
* Tailwind CSS

---

## Technology Stack

### Backend

* ASP.NET Core MVC (.NET 10)
* Entity Framework Core
* PostgreSQL
* Session Authentication

### Frontend

* Razor Views
* Tailwind CSS
* HTML5
* JavaScript

### DevOps

* Docker
* Docker Compose

---

## Project Structure

```text
fintrack/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── LoggingController.cs
│   ├── SessionController.cs
│   ├── DashboardController.cs
│   └── WalletController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Models/
│   ├── User.cs
│   └── Transaction.cs
│
├── ViewModels/
│   ├── DashboardViewModel.cs
│   ├── DepositViewModel.cs
│   ├── WithdrawViewModel.cs
│   └── TransferViewModel.cs
│
├── Services/
│   └── PasswordHash.cs
│
├── Views/
│   ├── Home/
│   ├── Logging/
│   └── Wallet/
│
├── Migrations/
│
├── wwwroot/
│
├── Dockerfile
├── compose.yaml
└── README.md
```

---

## Database Design

### Users

| Column   | Type    |
| -------- | ------- |
| Id       | int     |
| Name     | string  |
| Email    | string  |
| Password | string  |
| Balance  | decimal |

### Transactions

| Column     | Type     |
| ---------- | -------- |
| Id         | int      |
| SenderId   | int      |
| ReceiverId | int      |
| Amount     | decimal  |
| Type       | string   |
| Date       | datetime |

### Relationships

* Transaction → Sender (User)
* Transaction → Receiver (User)

A transaction contains two foreign keys:

```csharp
public int SenderId { get; set; }
public User? Sender { get; set; }

public int ReceiverId { get; set; }
public User? Receiver { get; set; }
```

---

## Running the Project

### Prerequisites

* .NET 10 SDK
* Docker Desktop
* PostgreSQL (optional if using Docker)

---

### Start Using Docker

Build and run all containers:

```bash
docker compose up --build
```

Run in detached mode:

```bash
docker compose up -d
```

Stop containers:

```bash
docker compose down
```

---

## Entity Framework Commands

Create Migration:

```bash
dotnet ef migrations add InitialCreate
```

Update Database:

```bash
dotnet ef database update
```

Remove Last Migration:

```bash
dotnet ef migrations remove
```

---

## Authentication Flow

### Registration

1. User submits registration form
2. Password is hashed
3. User is stored in database
4. Initial balance is set to 0

### Login

1. User enters credentials
2. Password hash is verified
3. UserId is stored in Session
4. User is redirected to Dashboard

### Logout

1. Session is cleared
2. User is redirected to Home Page

---

## Wallet Flow

### Deposit

1. User enters amount
2. Balance increases
3. Deposit transaction is created

### Withdraw

1. User enters amount
2. System validates balance
3. Balance decreases
4. Withdrawal transaction is created

### Transfer

1. Sender selects recipient
2. System validates balance
3. Sender balance decreases
4. Receiver balance increases
5. Transaction record is created

---

## Security

Current security features:

* Password Hashing
* Session Authentication
* Server-Side Validation
* Balance Validation
* Protected Dashboard Access

Planned future improvements:

* ASP.NET Identity
* JWT Authentication
* Role-Based Authorization
* Refresh Tokens
* Email Verification
* Two-Factor Authentication

---

## Future Improvements

### Features

- User Profile Management
- Change Password
- Transaction Search
- Transaction Filtering
- Email Verification
- Two-Factor Authentication
- Email Notifications
- Admin Dashboard
- Mobile Application

### Architecture

- Repository Pattern
- Service Layer
- Unit of Work Pattern
- AutoMapper
- FluentValidation

### Security

- ASP.NET Identity
- JWT Authentication
- Refresh Tokens
- Role-Based Authorization

### Monitoring & Production

- Global Exception Handling Middleware
- Structured Logging
- Audit Logs
- Deployment Pipeline
- CI/CD Integration

---

## Author

FinTrack was built as a learning project to practice:

* ASP.NET Core MVC
* Entity Framework Core
* PostgreSQL
* Docker
* Tailwind CSS
* Financial Transaction Systems
* Software Architecture Fundamentals
