## Task Management System

---

# Overview

This project is a production-ready ASP.NET Core Web API that provides authentication, authorization, and full CRUD operations for managing tasks. The API is deployed on Microsoft Azure and connected to an Azure SQL Database.

The goal of this project is to demonstrate real-world backend development skills suitable for a .NET Developer role.

---

# Live DEMO

Base URL:

https://thaibang-dcghgrg4bfdaefgd.francecentral-01.azurewebsites.net

Swagger Documentation:

https://thaibang-dcghgrg4bfdaefgd.francecentral-01.azurewebsites.net/swagger

Health Check

https://thaibang-dcghgrg4bfdaefgd.francecentral-01.azurewebsites.net/api/health

Root Endpoint:

GET /

Returns:

{ "message": "Task Management API OK", "version": "1.0", "documentation": "/swagger", "health": "/api/health" }

Frontend:

https://proud-mud-076774a03.7.azurestaticapps.net

---

# Features

RESTful API design

JWT Authentication

Role-based Authorization

Task CRUD operations

Entity Framework Core integration

SQL Server database

Global exception handling

Logging with ILogger

Health check endpoint

Automatic database migration

Database seeding

Swagger API documentation

Production deployment on Azure

---

# Tech Stack

ASP.NET Core (.NET 8)

Entity Framework Core

SQL Server

JWT Authentication

Swagger

Azure App Service

Azure SQL Database

---

# Architecture

Controller Layer

Handles HTTP requests and responses.

Service Layer

Contains business logic.

Data Layer

Uses Entity Framework Core to interact with the database.

Database

Azure SQL Database with migrations and seeding.

---

# Project Structure

```text
TaskManagementSystem.API
├── Common
├── Controllers
├── Data
├── DTOs
│   ├── Auth
│   └── Tasks
├── Enums
├── Extensions
├── Migrations
├── Models
├── Repositories
└── Services
```

---

# Authentication

JWT-based authentication is implemented.

Roles:

Admin

User

Protected endpoints require a valid JWT token.

---

# Default Admin User

Automatically created when the database is empty.

Username:

admin

Password:

123456

Role:

Admin

---
# Running Locally

Clone the repository:

git clone https://github.com/ThaiBangHOANG/task-management-system

Navigate to project:

cd task-management-system

Update connection string in appsettings.json.

Apply migrations:

dotnet ef database update

Run the application:

dotnet run

---

# Deployment

The API is deployed on:

Microsoft Azure App Service

Deployment method:

dotnet publish -c Release

---

# Author
Thai Bang HOANG
