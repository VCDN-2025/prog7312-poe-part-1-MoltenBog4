# 🏙️ Municipal Services Application – PROG7312 Portfolio of Evidence

**Module:** PROG7312 – Advanced Application Development  
**Assessment:** Portfolio of Evidence (PoE) – Part 1
**Author:** Sashiel Moonsamy  
**Student Number:** ST10028058  

👉 [**Watch Demo Video on YouTube**](https://youtu.be/ONAEioCP_Fk)
https://youtu.be/ONAEioCP_Fk

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

⚙️ Setup Instructions

Open the solution in Visual Studio:
ST10028058_PROG7312_POE.sln

Restore NuGet packages (Visual Studio should do this automatically).

Database setup:

Update appsettings.json with your SQL Server connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=MunicipalServicesDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Run the following in Package Manager Console:

Update-Database


Seed data: The system automatically seeds roles and users on first run:

Admin → admin@city.gov / Admin123!

Citizen 1 → user@demo.com / Citizen123!

Citizen 2 → second@demo.com / Citizen456!

▶️ Run the Application

Press F5 in Visual Studio, or run:

dotnet run


👥 Usage
For Citizens

Login with a citizen account.

Click Report Issues to submit a new issue.

View your own reports under My Reports.

After submitting, provide a rating/feedback.

Check past submissions under My Feedback.

For Admins

Login with the Admin account.

Navigate to All Issues to see every report.

Navigate to All Feedback to review all citizen feedback.

Use the Respond option to provide an official reply.

🎥 Demo Video

A full walkthrough of the system (citizen & admin features, reporting, feedback, and responses) is available here:

👉 Watch Demo Video on YouTube:https://youtu.be/ONAEioCP_Fk


📜 Academic Notes

Developed for PROG7312 Advanced Application Development.

Implements user engagement strategies such as:

Ratings & feedback for improved transparency.

Demonstrates role-based access (Citizen vs Admin).

Uses custom data structures instead of built-in collections.

📚 References

Microsoft Docs – ASP.NET Core MVC

Microsoft Docs – ASP.NET Core Identity

Bootswatch – Brite Theme

PROG7312 PoE Specification Document (2025)

Gido, Clements & Baker – Successful Project Management, 7th Edition

👨‍💻 Author

Name: Sashiel Moonsamy

Student Number: ST10028058
