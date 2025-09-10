# 🏙️ Municipal Services Application – PROG7312 Portfolio of Evidence

**Module:** PROG7312 – Advanced Application Development  
**Assessment:** Portfolio of Evidence (PoE) – Part 1–3  
**Author:** [Your Name]  
**Student Number:** [Your Student Number]

---

## 📖 Overview

The **Municipal Services Application** is a C# **ASP.NET Core MVC web application** developed as part of the PROG7312 PoE.  
It provides a **digital platform** for South African citizens to report municipal issues, track progress, and strengthen transparency between residents and local municipalities.

The application incorporates:
- **Citizen functionality** (report issues, leave ratings and feedback, view personal reports and feedback history)
- **Administrator functionality** (view all reported issues, see all citizen feedback, respond to feedback, filter reports)
- **Custom-built data structures** for storage (no generic collections such as `List<T>` are used)

The system contributes to improved communication, accountability, and citizen engagement.

---

## ✨ Features

### Citizens
- Submit new **service issue reports** (e.g., potholes, outages, sanitation).
- Attach optional images or documents as evidence.
- View only **their own issues** in a personal dashboard.
- Provide **ratings and written feedback** on the municipality’s response.
- Access a **feedback history** page.

### Administrators
- Log in with Admin credentials.
- View **all reported issues** across all users.
- View **all feedback and ratings** across all issues.
- **Respond** to citizen feedback and track response times.
- Filter and monitor service delivery performance by category.

---

## 🛠️ Technology Stack

- **ASP.NET Core 8.0 MVC**
- **Entity Framework Core (Identity for roles & login)**
- **SQL Server / SSMS** for authentication & persistence
- **Bootstrap (Bootswatch Brite theme)** for styling
- **Custom Data Structures**: `SinglyLinkedList<T>` used for storing issues and feedback

---

## 🚀 Getting Started

### ✅ Prerequisites
- Visual Studio 2022 (with ASP.NET and web development workload)
- .NET 8.0 SDK
- SQL Server Management Studio (SSMS) for database access

---

### 📂 Clone the Repository
```bash
git clone https://github.com/<YourUsername>/<YourRepo>.git
cd ST10028058_PROG7312_POE
