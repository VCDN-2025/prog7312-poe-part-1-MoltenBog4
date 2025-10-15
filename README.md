# 🌍 MunicipalConnect  
### PROG7312 Portfolio of Evidence – Part 1 & Part 2  
**👨‍💻 Student:** Sashiel Moonsamy  
**🆔 Student Number:** ST10028058  
**📘 Module:** Programming 3B  
**🧩 Application:** *MunicipalConnect – Local Events & Announcements Platform*  

---

## 🧾 Overview  

**MunicipalConnect** is a municipal service and engagement web application built using **ASP.NET Core MVC (C#)**.  
The system was developed across **two phases**:

- **Part 1 →** Focused on *core functionality, data handling,* and *UI foundation*.  
- **Part 2 →** Extended the project with *advanced data structures, recommendation algorithms,* and *admin management features*.  

---

## 🧩 Project Progress Summary  

| **Aspect** | **Part 1 Implementation** | **Part 2 Enhancement** |
|-------------|----------------------------|--------------------------|
| **Core Focus** | Issue Reporting System + Basic Event View | Advanced Local Events Management Module |
| **UI/UX** | Simple Bootstrap layout | Refined municipal-themed UI with consistency |
| **Data Handling** | Lists + basic validation | Dictionaries, HashSets, Queues, Priority Queues |
| **Algorithms** | None | Frequency-based Recommendation + Priority Scheduling |
| **Admin Functions** | Basic CRUD | Full Admin Dashboard with Undo functionality |
| **Video Demonstration** | Overview of core app | Walkthrough of new data structures and algorithms |
| **GitHub Repository** | [Part 1 Repo](#) | [Part 2 Repo](https://github.com/ST10028058_PROG7312_POE) |

---

## ✅ I. Core Project Requirements  

### 🧱 Part 1 – Foundational Features
- Implemented **User Authentication** and **Role Control**.  
- Developed **Report Issues** module with validation and file upload.  
- Created **navigation bar** and consistent layout.  
- Added **Events prototype** for future expansion.  

### 🚀 Part 2 – Advanced Extension
- Built a **Local Events & Announcements** module.  
- Populated **18+ sample events** for demonstration.  
- Added **search, filter, and sort** functionality.  
- Integrated **Recommendation Algorithm** based on user behavior.  
- Implemented **Priority Queue Scheduling** for upcoming events.  
- Expanded **Admin Dashboard** with CRUD and Undo operations.  

> 🎥 **Video Links:**  
> 🎬 [Part 1 Demonstration](https://youtu.be/ONAEioCP_Fk?si=VoWY-YuPxNWK0nYK)  
> 🎬 [Part 2 Demonstration](https://youtu.be/Zlaezj9AP3M)  

---

## 🎨 II. Graphical User Interface (GUI)

### 🖥️ Part 1 Highlights
- Responsive **Bootstrap layout**.  
- Clear navigation between modules.  
- Consistent municipal branding and color palette.  

### 🎨 Part 2 Enhancements
- Improved **UI consistency** using Bootswatch theme.  
- Added **alerts, cards, and responsive tables**.  
- Integrated **municipal logo and footer branding**.  
- Enhanced **form layouts** with accessible color contrast.  
- Ensured **real-time feedback** during search and filtering.

---

## 🗂 III. Feature – Local Events & Announcements  

### 🧱 Part 1
- Displayed static, hard-coded events.  
- No filtering, sorting, or algorithmic features.  

### ⚙️ Part 2
- Introduced **complete event management** with dynamic updates.  
- Added **search by keyword**, **filter by category/date**, and **sorting**.  
- Created **“Recommended for You”** event suggestions.  
- Built **Admin Dashboard** for managing events.  
- Added **Priority Queue** for nearest events.

---

### 🧮 Data Structures Implementation  

| **Feature** | **Data Structure** | **Purpose** |
|--------------|--------------------|--------------|
| Event Storage | `SortedDictionary<DateTime, EventModel>` | Automatically sorts events by date |
| Unique Categories | `HashSet<string>` | Maintains distinct event categories |
| Recently Viewed | `Queue<EventModel>` | Tracks last 5 events viewed |
| Search Tracking | `Dictionary<string,int>` | Counts frequency of searched keywords |
| Priority Events | `PriorityQueue<EventModel,int>` | Displays nearest upcoming events |

> 🧠 Each structure was selected for **efficiency, scalability,** and direct alignment with the **Part 2 data structure rubric**.

---

## ⚙️ IV. Additional Recommended Feature – Recommendation Algorithm  

### 💡 Purpose  
To analyze user search behavior and recommend events aligned with their interests.

### 🔍 Logic Flow  
1. Every search is logged in `_searchFrequency`.  
2. Dictionary values increment for each repeated term.  
3. LINQ identifies the **most frequent searches**.  
4. Recommended events are displayed based on those results.  

```csharp
private void TrackSearch(string term)
{
    if (_searchFrequency.ContainsKey(term))
        _searchFrequency[term]++;
    else
        _searchFrequency[term] = 1;
}
```

**Algorithm Type:** Frequency-Based Recommendation  
**Location:** `EventsController.cs`  

---

## 🧠 V. Admin Feature – Priority Queue Algorithm  

### 🧮 Purpose  
To highlight the **soonest upcoming events** for administrative management and user visibility.

### ⚙️ Implementation  
Each event is assigned a priority based on how soon it occurs:

```csharp
int priority = (int)(ev.Date - DateTime.Today).TotalDays;
_priorityEvents.Enqueue(ev, priority);
```

The queue sorts automatically by priority (closest dates appear first).  
Displayed as **“Top 5 Upcoming Events”** in the Admin Dashboard.

**Algorithm Type:** Priority Scheduling (PriorityQueue)  
**Location:** `AdminEventsController.cs`

---

## 🧩 VI. Algorithm Summary  

| **Algorithm** | **Data Structure** | **Purpose** | **POE Part** |
|----------------|--------------------|--------------|---------------|
| **Recommendation Algorithm** | `Dictionary<string,int>` | Recommends events by search frequency | Part 2 |
| **Priority Scheduling** | `PriorityQueue<EventModel,int>` | Ranks upcoming events by date | Part 2 |
| **Undo Functionality** | `Stack<EventModel>` | Reverts admin actions | Part 2 |
| **Recent Search History** | `Queue<string>` | Tracks recent user searches | Part 2 |

---

## 🧮 VII. Technologies Used  

🧱 **Backend:** ASP.NET Core MVC (C#)  
🎨 **Frontend:** Bootstrap 5 + Bootswatch  
🧠 **Logic:** LINQ + In-memory Collections  
📦 **Data:** Dictionaries, HashSets, Queues, PriorityQueue  
🖋️ **Views:** Razor Pages  
💡 **IDE:** Visual Studio 2022  

---

## 🏁 VIII. Conclusion  

### ✅ Part 1 Achievements
- Municipal Issue Reporting Module  
- Basic UI and event list prototype  
- Core data storage and navigation foundation  

### 🚀 Part 2 Advancements
- Fully functional **Local Events & Announcements** feature  
- Integrated **Dictionaries, Sets, Queues, Stacks, PriorityQueue**  
- Added **Recommendation Algorithm** and **Undo**  
- Maintained **UI consistency and branding**  
- Delivered a complete, optimized, and interactive system  

> Together, these phases demonstrate the student’s progress from **core feature development** to **data-driven application design** and **algorithmic enhancement.**

---

## 📚 IX. References  

> Microsoft (2024). *ASP.NET Core MVC Overview.* Available at: [https://learn.microsoft.com/en-us/aspnet/core/mvc/overview/getting-started/introduction](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview/getting-started/introduction) (Accessed: 14 Oct 2025).  
>  
> Microsoft (2024). *System.Collections.Generic Namespace.* Available at: [https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary) (Accessed: 14 Oct 2025).  
>  
> W3Schools (2024). *C# LINQ Tutorial.* Available at: [https://www.w3schools.com/cs/cs_linq.asp](https://www.w3schools.com/cs/cs_linq.asp) (Accessed: 14 Oct 2025).  
>  
> Bootswatch (2024). *Free Bootstrap Themes.* Available at: [https://bootswatch.com](https://bootswatch.com) (Accessed: 14 Oct 2025).  
>  
> Stack Overflow (2025). *LINQ and ASP.NET MVC Discussions.* Available at: [https://stackoverflow.com](https://stackoverflow.com) (Accessed: 14 Oct 2025).  
>  
> OpenStreetMap (2024). *Nominatim API Documentation.* Available at: [https://nominatim.org](https://nominatim.org) (Accessed: 14 Oct 2025).  
>  
> OpenAI (2025). *ChatGPT – Programming and Documentation Assistance.* Available at: [https://chat.openai.com](https://chat.openai.com) (Accessed: 14 Oct 2025).  

---

© **2025 – MunicipalConnect**  
Developed by **Sashiel Moonsamy (ST10028058)**  
for **PROG7312 Portfolio of Evidence – Part 1 & Part 2**
