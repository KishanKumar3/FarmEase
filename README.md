# FARMEASE

_Transforming Farms, Empowering Growth Every Day_

![Last Commit](https://img.shields.io/github/last-commit/KishanKumar3/FarmEase?style=flat&logo=git&logoColor=white&color=0080ff)
![Top Language](https://img.shields.io/github/languages/top/KishanKumar3/FarmEase?style=flat&color=0080ff)
![Language Count](https://img.shields.io/github/languages/count/KishanKumar3/FarmEase?style=flat&color=0080ff)

### 🚀 Built with:
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=flat&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=dotnet&logoColor=white)
![NuGet](https://img.shields.io/badge/NuGet-004880.svg?style=flat&logo=NuGet&logoColor=white)

---

## 📚 Table of Contents

- [Overview](#overview)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Usage](#usage)
- [Architecture & Design](#architecture--design)
- [Contact](#contact)

---

## 🧩 Overview

**FarmEase** is an all-in-one farm management platform built using **Clean Architecture** principles. It streamlines operations like resource management, bookings, and user interactions with a layered and scalable backend system.

### ✅ Why FarmEase?

- 🧱 **Modular Design**: Organized into API, Application, Domain, Infrastructure, and Shared layers.
- 🔐 **Secure Authentication**: JWT-based login, registration, and role-based access control.
- 📡 **Comprehensive API Endpoints**: Easily manage farms, rooms, amenities, bookings, and users.
- 💾 **Robust Data Access**: 
  - 📌 Built using **Entity Framework Core (EF Core)**
  - 📌 Implements the **Repository Pattern** for data abstraction
  - 📌 Uses the **Unit of Work** pattern for transactional integrity
- 🧰 **Maintainable & Scalable**:
  - Follows **Clean Architecture**
  - Adheres to **SOLID** principles
  - Easy to extend and test
- 📧 **Communication Support**: Email service for booking confirmation and notifications

---

## ⚙️ Getting Started

### ✅ Prerequisites

Ensure the following are installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- NuGet CLI (included with .NET SDK)

---

### 📦 Installation

Clone the repository and install required dependencies.

```bash
# Clone the repository
git clone https://github.com/KishanKumar3/FarmEase

# Navigate to project directory
cd FarmEase

# Restore dependencies
dotnet restore
```

---

### ▶️ Usage

To run the application:

```bash
dotnet run
```

Once running, open [https://localhost:{port}/swagger](https://localhost:{port}/swagger) to explore the API via Swagger UI.

---

## 🏗️ Architecture & Design

FarmEase is designed with extensibility and testability in mind, following Clean Architecture principles.

### 📦 Layers:

- **Presentation Layer** (`Web`): ASP.NET Core API controllers.
- **Application Layer**: Business logic, services, DTOs.
- **Domain Layer**: Core entities, enums, interfaces.
- **Infrastructure Layer**: Implements repositories and data access logic.
- **Shared Layer**: Common utilities, constants, and error handling.

### 🔁 Key Patterns Used:

- **✅ Clean Architecture**
- **✅ Repository Pattern**
- **✅ Unit of Work**
- **✅ Dependency Injection**
- **✅ Logging via ILogger**

---

## 📬 Contact

Got questions or feedback? [Open an issue](https://github.com/KishanKumar3/FarmEase/issues) or connect with me on [GitHub](https://github.com/KishanKumar3).

---

[🔝 Back to Top](#farmease)
