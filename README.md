# 🏠 Real Estate Office Management System API

A RESTful Web API built with **ASP.NET Core (.NET 8)** for managing a real estate office. The system enables efficient management of properties, owners, clients, agents, rental contracts, and property images through secure and scalable API endpoints.

## 🚀 Features

- User Authentication using JWT
- Property Management
- Owner Management
- Client Management
- Agent Management
- Lease Management
- Property Image Upload
- CRUD Operations for all main entities
- SQL Server Database
- Swagger API Documentation
- 3-Tier Architecture

---

## 🛠️ Technologies Used

- ASP.NET Core Web API (.NET 8)
- C#
- SQL Server
- ADO .NET
- JWT Authentication
- LINQ
- Swagger (OpenAPI)
- Git & GitHub

---

## 📂 Project Structure

```
RealEstateOfficeAPI
│
├── Controllers
├── Business Layer
├── Data Access Layer
├── Models
├── DTOs
├── Services
├── Helpers
├── Middleware
├── wwwroot
└── Program.cs
```

---

## 🔐 Authentication

The API uses **JWT (JSON Web Token)** for authentication.

After a successful login, the client receives a JWT token that must be included in the Authorization header:

```
Authorization: Bearer YOUR_TOKEN
```

---

## 📌 Main Modules

- Authentication
- Users
- Properties
- Property Images
- Owners
- Clients
- Agents
- Leases

---

## ⚙️ Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/RealEstateOfficeAPI.git
```

### 2. Open the project

Open the solution using **Visual Studio 2022**.

### 3. Configure SQL Server

Update the connection string in:

```json
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=RealEstateOfficeDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Apply the database

Run the SQL scripts or update the database.

### 5. Run the project

```
https://localhost:xxxx/swagger
```

Swagger will open automatically.

---

## 📖 API Documentation

Swagger UI is available after running the project:

```
/swagger
```

---

## 📸 Screenshots

You can add screenshots of:

- Swagger UI
- Login Endpoint
- Property APIs
- Database Diagram

---

## 📈 Future Improvements

- Refresh Tokens
- Role-Based Authorization
- Email Verification
- Property Search & Filtering
- Favorites
- Notifications
- Pagination
- Logging
- Docker Support
- Cloud Deployment

---

## 👨‍💻 Author

**Rawda Eweda**

- GitHub:https://github.com/Rawda2007
- LinkedIn: https://www.linkedin.com/in/rawda-eweda-b1b5ab376/

---

## 📄 License

This project is created for learning and portfolio purposes.
