✅ Distinguishes **Part 1 vs Part 2** clearly.
✅ Highlights exactly how **each POE requirement** was tackled.
✅ Lists all **implemented features** per part (including your new algorithms).
✅ Includes **Harvard Anglia–style references**, OpenAI attribution, and polished formatting.
✅ Uses icons and visual section breaks for readability on GitHub.

---

````markdown
# 🌍 MunicipalConnect  
### PROG7312 Portfolio of Evidence – Part 1 & Part 2  
**Student:** Sashiel Moonsamy  
**Student Number:** ST10028058  
**Module:** Programming 3B  
**Application:** *MunicipalConnect – Local Events & Announcements Platform*  

---

## 🧾 Overview  

**MunicipalConnect** is a municipal service and engagement web application built using **ASP.NET Core MVC (C#)**.  
The system was developed across **two phases**:

- **Part 1 →** Focused on *core functionality, data handling,* and *UI foundation*.  
- **Part 2 →** Extended the project with *data-structure algorithms*, *recommendation systems*, *priority scheduling*, and *enhanced admin control*.

---

## 🧩 Project Progress Summary  

| **Aspect** | **Part 1 Implementation** | **Part 2 Enhancement** |
|-------------|----------------------------|--------------------------|
| **Core Focus** | Issue Reporting System + Basic Event View | Advanced Local Events Management Module |
| **UI/UX** | Simple Bootstrap layout | Refined green theme, consistent branding, responsive cards/tables |
| **Data Handling** | Lists + basic validation | Dictionaries, HashSets, Queues, and Priority Queue |
| **Algorithms** | None required in brief | Frequency-based Recommendation Algorithm + Priority Scheduling |
| **Admin Functions** | Basic Create/Edit/Delete | Full Admin Dashboard with category dropdowns & sorting |
| **Video Demonstration** | Recorded overview of Part 1 | Final walk-through showing search, filter, and algorithms |
| **GitHub Repository** | [Part 1 Repo Link](#) | [Part 2 Repo Link](https://github.com/ST10028058_PROG7312_POE) |

---

## ✅ I. Core Project Requirements  

### 🧱 Part 1 – Foundational Features
- Implemented **User Authentication & Role Control**.  
- Created **Report Issues** module with form validation.  
- Built **base navigation bar** and consistent theme.  
- Designed **Events page prototype** (early concept for Part 2).  

### 🚀 Part 2 – Advanced Extension
- Extended application to include **Local Events & Announcements**.  
- Populated 18 + sample events across multiple categories.  
- Added search by keyword, category, and date range.  
- Implemented sorting and real-time UI feedback.  
- Developed algorithms for **Recommendation and Priority Scheduling**.  
- Added Admin CRUD (Create/Edit/Delete) with category dropdowns.  

> 🎥 **Video Links**  
> • [Part 1 Demonstration](#)  
> • [Part 2 Demonstration](#)  

---

## 🎨 II. Graphical User Interface (GUI)

### Part 1 Highlights
- Introduced responsive Bootstrap layout.  
- Developed navigation menu linking home, reports, and feedback.  
- Early consistent green theme and municipal branding.

### Part 2 Enhancements
- Upgraded all pages to maintain **visual consistency and feedback**.  
- Added dynamic alerts for success/error messages.  
- Used **Bootswatch theme** for a modern look and consistent style.  
- Improved form input fields with green borders and centered content.  
- Added category dropdown and calendar date selectors.

---

## 🗂 III. Feature – Local Events and Announcements  

### Part 1 Base Feature
- Displayed hard-coded events in a simple list format.  
- No sorting or filtering logic implemented yet.  

### Part 2 Implementation
- Added complete event management module for citizens and admins.  
- Integrated search, filter, sort, and priority features.  
- Introduced dynamic recommendation section for users.  
- Added Admin Dashboard with event creation and editing views.  
- Used PriorityQueue to display the Top 5 upcoming events.  

---

### 🧱 Data Structures Implementation  

| **Feature** | **Data Structure** | **Purpose** |
|--------------|--------------------|--------------|
| Event Storage | `SortedDictionary<DateTime, EventModel>` | Automatically sort events by date |
| Unique Categories | `HashSet<string>` | Maintain distinct event categories |
| Recently Viewed | `Queue<EventModel>` | Track last 5 events user opened |
| Search Tracking | `Dictionary<string,int>` | Count how often a keyword/category is searched |
| Priority Events | `PriorityQueue<EventModel,int>` | Display nearest upcoming events first |

Each structure was chosen for efficiency and clarity, fulfilling the **“Data Structures Implementation”** section of the brief.

---

## ⚙ IV. Additional Recommended Feature – Recommendation Algorithm  

### 💡 Purpose
To analyze the user’s search behavior and recommend events that match their interests.

### 🧩 How It Works
1. Every search (keyword or category) is recorded in `_searchFrequency`.  
2. Each term’s value increments every time it is used.  
3. LINQ queries identify the **top 3 most-frequent** searches.  
4. Events matching those categories are recommended to the user.  

```csharp
private void TrackSearch(string term)
{
    if (_searchFrequency.ContainsKey(term))
        _searchFrequency[term]++;
    else
        _searchFrequency[term] = 1;
}
````

**Algorithm Type:** *Frequency-Based Recommendation Algorithm (Dictionary + LINQ)*
**Controller:** `EventsController`

---

## 🧠 V. Admin Feature – Priority Queue Algorithm

### 🧮 Purpose

To ensure events happening soonest are highlighted for administrative action and citizen visibility.

### ⚙ Implementation

Each event is assigned a numerical priority based on how soon it occurs:

```csharp
int priority = (int)(ev.Date - DateTime.Today).TotalDays;
_priorityEvents.Enqueue(ev, priority);
```

The queue automatically sorts events by lowest priority value (closest date).
This is displayed in the Admin Dashboard as “Upcoming Top 5 Events”.

**Algorithm Type:** *Priority Scheduling Algorithm (PriorityQueue)*
**Controller:** `AdminEventsController`

---

## 🧮 VI. Algorithm Summary

| **Algorithm**                | **Data Structure**              | **Purpose**                                   | **POE Part** |
| ---------------------------- | ------------------------------- | --------------------------------------------- | ------------ |
| **Recommendation Algorithm** | `Dictionary<string,int>`        | Tracks search frequency and recommends events | Part 2       |
| **Priority Queue Algorithm** | `PriorityQueue<EventModel,int>` | Ranks upcoming events by date                 | Part 2       |
| **Stack / Queue Use**        | `Queue<EventModel>`             | Tracks user’s recently viewed events          | Part 1 & 2   |

These algorithms fulfill both the **Event Management** and **Additional Recommended Feature** requirements in the official Part 2 brief.

---

## 🧮 VII. Technologies Used

* **ASP.NET Core MVC (C#)** – Backend logic and controller framework
* **Bootstrap 5 (Bootswatch)** – Consistent UI theme and responsive design
* **LINQ** – Efficient filtering and data analysis
* **Razor Views** – Dynamic frontend rendering
* **In-Memory Collections** – Efficient runtime data management

---

## 🏁 VIII. Conclusion

### Part 1 Achievements

✅ Core municipal reporting features implemented
✅ Basic events view and authentication
✅ Foundational UI and data model

### Part 2 Enhancements

✅ Local Events & Announcements module
✅ Efficient data structures (HashSet, Dictionary, PriorityQueue)
✅ Recommendation & Priority Algorithms implemented
✅ Admin management and category dropdowns
✅ Consistent UI and video demonstration

Together, these phases demonstrate the student’s growth from *functional design in Part 1* to *algorithmic thinking and optimization in Part 2.*

---

## 📚 IX. References

> Microsoft (2024). *ASP.NET Core MVC Overview.* Available at: [https://learn.microsoft.com/en-us/aspnet/core/mvc/overview/getting-started/introduction/getting-started](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview/getting-started/introduction/getting-started) (Accessed: 14 Oct 2025).
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

© 2025 – *MunicipalConnect* | Developed by **Sashiel Moonsamy (ST10028058)**
for **PROG7312 Portfolio of Evidence – Part 1 & Part 2**

```

---
