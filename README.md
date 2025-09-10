[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/IbFBjHe5)

🏙️ Municipal Services Application – Part 1

Module: PROG7312 – Advanced Application Development
Assessment: Portfolio of Evidence (PoE) – Part 1

📖 Overview

The Municipal Services Application is a C# .NET Framework Windows Forms project designed to improve municipal service delivery and citizen engagement in South Africa.

Part 1 focuses on the Report Issues functionality, which enables citizens to:

Log municipal service delivery issues (e.g., sanitation, roads, water outages).

Provide location, category, and detailed descriptions.

Attach supporting media (images/documents).

Receive basic feedback through an integrated Feedback & Rating System to strengthen transparency and accountability.

⚙️ Requirements

To compile and run the application, ensure the following are installed:

Visual Studio 2022 or later

.NET Framework 8

Windows OS

🚀 Setup Instructions
1. Clone or Download the Repository
git clone https://github.com/YourGitHubUsername/MunicipalServicesApp.git


Or download as a .zip and extract.

2. Open in Visual Studio

Launch Visual Studio.

Navigate to File → Open → Project/Solution.

Open the .sln file in the project root.

3. Build the Solution

Go to Build → Build Solution.

Ensure there are no build errors.

4. Run the Application

Press F5 or click Start Debugging.

The Main Menu form will appear.

🖥️ Application Usage
Main Menu

On startup, the following options are presented:

Report Issues ✅ (implemented in Part 1)

Local Events & Announcements 🚧 (coming in Part 2)

Service Request Status 🚧 (coming in Part 3)

➡️ Only Report Issues is active for Part 1.

Report Issues Page

Features:

📍 Location Input – Enter where the issue occurred.

🗂️ Category Selection – Select the type of issue (e.g., sanitation, roads, utilities).

📝 Description Box – Provide detailed information about the issue.

📎 Media Attachment – Upload supporting images/documents.

✅ Submit Button – Save and confirm the report.

🔔 Engagement Feature – Displays a thank-you message / progress indicator.

🔙 Back to Main Menu – Return to the home screen.

📂 Data Handling

Issues are stored in a List data structure for efficient organisation.

Each report contains:

Location

Category

Description

Attachment path

🎨 Design Principles

Consistency → Uniform colour scheme and layout.

Clarity → Clear labels and buttons for ease of use.

User Feedback → MessageBox alerts for success/errors.

Responsiveness → Designed to adapt to different screen sizes.

🔑 Role-Based Feedback Management System
👤 Citizens (Users)

After logging in, users can:

Submit service requests (e.g., water outage, pothole).

Rate the municipality’s response (stars / emojis / numeric scale).

Leave written comments about satisfaction and resolution quality.

View their feedback history (past reports and ratings).

🛠️ Administrators (Municipality Staff)

Admins can:

View all submitted feedback linked to service reports.

Filter ratings by department, service type, ward, or response-time band.

Respond to citizen comments to close the feedback loop.

Access analytics dashboards (average ratings, top complaints, trends).

🔐 User Login Integration

Feedback is linked to the authenticated user account (no anonymous posts).

Users only see their own reports/feedback.

Admins have access to all records.

🔮 Future Development

Part 2 → Local Events & Announcements (with advanced data structures).

Part 3 → Service Request Status tracking (trees, graphs, heaps).

Enhancements → Dashboards, gamification, and community participation features.

👨‍💻 Author

Student Name: [Your Full Name]

Student Number: [Your Student ID]

Institution: IIE Varsity College

📚 References

Microsoft, 2023. C# documentation. Microsoft Docs. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/
 [Accessed 21 February 2025].

Microsoft, 2023. .NET Framework documentation. Microsoft Docs. Available at: https://learn.microsoft.com/en-us/dotnet/framework/
 [Accessed 21 February 2025].

Microsoft, 2023. Windows Forms documentation. Microsoft Docs. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/
 [Accessed 21 February 2025].

🎥 Demo Video

👉 Click here to watch the video demonstration: https://youtu.be/dK408b3_sqI 
