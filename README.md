# MyApp - NTier Architecture with ASP.NET Core and Entity Framework Core

A sample .NET application demonstrating **NTier Architecture** with **ASP.NET Core** and **Entity Framework Core**.

This project follows a layered architecture pattern with separation of concerns:

- **Domain**: Contains business logic and domain entities.
- **Infrastructure**: Provides data access and external services.
- **Application**: Implements service layer and business logic.
- **Server**: ASP.NET Core WebAPI that exposes application services.

## Table of Contents

- [Project Overview](#project-overview)
- [Technologies Used](#technologies-used)
- [Setup Instructions](#setup-instructions)
- [Folder Structure](#folder-structure)
- [Migrations](#migrations)
- [Testing the API](#testing-the-api)
- [Contributing](#contributing)
- [License](#license)

## Project Overview

This application is built using **ASP.NET Core WebAPI** as the backend and **Entity Framework Core** for database management. It demonstrates the following key principles:

- **Layered Architecture**: Clear separation of concerns.
- **Dependency Injection**: For managing dependencies.
- **Entity Framework Core**: To handle database interaction.
- **ASP.NET Core WebAPI**: To expose endpoints for consuming the application logic.

## Technologies Used

- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0**
- **SQL Server**
- **C# 8.0**
- **Dependency Injection**
- **Migrations for EF Core**

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK
- SQL Server (local or remote)

### Step 1: Clone the repository

Clone the repository to your local machine:

```bash
git clone https://github.com/yourusername/MyApp.git
cd MyApp
