# 🌍 **Municipal Services Platform — PROG7312 Final Portfolio of Evidence (Part 3)**

### 📘 **Module:** PROG7312 — Advanced Programming

### 👩‍💻 **Student:** Sashiel Moonsamy

### 🆔 **Student Number:** ST10028058


---

## 🏗️ **Project Abstract**

The **Municipal Services Platform** was developed as the final deliverable for **PROG7312 (Part 3 Summative)** to address the challenge of poor communication between **local municipalities** and **residents**.
Using **ASP.NET Core MVC** and **C#**, the platform allows citizens to **report issues**, such as power outages or road damage, while administrators can **prioritise**, **track**, and **resolve** service requests through an interactive dashboard (Microsoft, 2025).

This summative submission represents the culmination of the Portfolio of Evidence, integrating all prior development phases into a cohesive and scalable municipal management solution (Nield, 2023).

---

## 🎯 **Primary Objectives**

* Improve the **communication bridge** between citizens and municipalities.
* Implement **advanced data structures** to enhance efficiency.
* Incorporate **secure, role-based authentication** for data integrity.
* Deliver a **responsive and user-friendly web interface**.
* Showcase real-world application of **algorithmic and object-oriented principles**.

---

## 🧱 **System Architecture Overview**

| **Layer**              | **Technology / Concept**                | **Purpose**                           |
| ---------------------- | --------------------------------------- | ------------------------------------- |
| **Presentation Layer** | ASP.NET Razor Views + Bootstrap        | Dynamic, responsive UI                |
| **Logic Layer**        | Controllers and C# Models               | Handles CRUD, sorting, and validation |
| **Data Layer**         | In-memory ConcurrentDictionary          | Thread-safe data storage              |
| **Algorithm Layer**    | AVL Tree, Red-Black Tree, Min Heap, MST | Efficiency and prioritisation         |
| **Security Layer**     | ASP.NET Identity + OWASP compliance     | Secure user authentication            |

The application design follows the **MVC architecture pattern**, ensuring maintainability, scalability, and modular reusability (Roberts, 2022).

---

## ⚙️ **Core Data Structures and Their Roles**

| **Data Structure**       | **Function in System**                          | **Efficiency Benefit**       |
| ------------------------ | ----------------------------------------------- | ---------------------------- |
| **AVL Tree**             | Balanced data retrieval for issue tracking      | O(log n) search speed        |
| **Red-Black Tree**       | High-speed insertion for concurrent submissions | Reduced rebalancing overhead |
| **Min Heap**             | Prioritises urgent requests                     | Constant-time access (O(1))  |
| **Graph (MST)**          | Connects related issues by location             | Optimised dispatch routing   |
| **ConcurrentDictionary** | Multi-threaded request management               | Prevents data conflicts      |

Together, these algorithms ensure **low latency**, **thread safety**, and **real-time responsiveness** in citizen-admin interactions (TutorialsPoint, 2024).

---

## 🧩 **Functional Modules**

| **Module**                       | **Feature Description**               | **Impact**                                  |
| -------------------------------- | ------------------------------------- | ------------------------------------------- |
| 🧑‍🤝‍🧑 **User Authentication** | Role-based login (Admin/Citizen)      | Protects access and ensures data separation |
| 📢 **Issue Reporting**           | Image upload + service categorisation | Enables accurate request tracking           |
| 📊 **Admin Dashboard**           | Filter, sort, and update requests     | Improves municipal response management      |
| 📅 **Events & Announcements**    | SortedDictionary-based news updates   | Keeps residents informed in real time       |
| 💬 **Feedback Loop**             | Request status updates                | Promotes transparency and accountability    |

Each module supports the overall goal of **citizen empowerment** and **efficient service management**.

---

## 🔐 **Security & Ethical Considerations**

Security is enforced through **ASP.NET Identity**, **password hashing**, and adherence to **OWASP principles** (OWASP, 2024).
All personal information is managed ethically and responsibly, aligning with **IIE’s WIL data privacy expectations** (IIE, 2025).
Techniques applied:

* Sanitised user inputs to prevent injection attacks.
* Secure file uploads and restricted file types.
* Role-based content segregation.

---

## 🎥 **YouTube Demonstration**

The **Part 3 video presentation** showcases the entire municipal services system in action — from authentication to advanced data structure efficiency.

### 🎬 **Watch Here:**

👉 [**Municipal Services Platform – Final Demonstration (Sashiel Moonsamy)**]([https://youtu.be/OcBMElTKHeY])
https://youtu.be/OcBMElTKHeY

### 🧾 **Video Highlights:**
Part 2 updates done with POE/Part 3 implementation
1. **Login Workflow** — Citizen and Admin roles demonstrated.
2. **Issue Reporting** — Submission of requests with image uploads.
3. **AVL Tree and Min Heap Logic** — Real-time issue prioritisation.
4. **Admin Dashboard** — Sorting and filtering requests dynamically.
5. **Graph (MST)** — Illustration of location-based route optimisation.
6. **Events & Announcements** — Public-facing content updates.

The presentation visually explains **algorithmic efficiency** and **system responsiveness**, reflecting real-world usability testing.

Part 1 Video:https://youtu.be/ONAEioCP_Fk
Part 2 Video:https://youtu.be/Zlaezj9AP3M
---

## 🧠 **Key Learnings and Outcomes**

Throughout the **Portfolio of Evidence**, this project fostered both **technical mastery** and **professional readiness**.

### 🧩 **1. Advanced Programming Concepts**

Developing the system enhanced understanding of **data structures**, **object-oriented logic**, and **algorithmic efficiency**, directly applying theory into practice (Evans, 2023).

### 🖥️ **2. System Design and Architecture**

Implementing MVC and modular design patterns improved architectural thinking — vital for enterprise software development.

### 🔒 **3. Security and Data Ethics**

Applying **OWASP security principles** deepened comprehension of cybersecurity standards in municipal data systems.

### ⚙️ **4. Problem-Solving and Debugging**

Debugging concurrency issues and data structure conflicts built resilience and improved critical-thinking skills during complex implementations.

### 🧑‍🏫 **5. Professional and WIL Integration**

This project aligns with **Work Integrated Learning (WIL)** objectives by mirroring real-world IT project practices — from client communication to deployment and documentation (IIE, 2025).

### 🧾 **Summary of Learning Outcomes**

| **Learning Area**             | **Competency Achieved**                           |
| ----------------------------- | ------------------------------------------------- |
| Software Architecture         | Implemented full MVC structure                    |
| Algorithms & Data Structures  | AVL, Heap, Graph, and Red-Black Trees applied     |
| Security Practices            | Integrated authentication & input validation      |
| Communication & Documentation | Academic and professional report writing          |
| WIL Readiness                 | Real-world client simulation and ethical delivery |

These outcomes showcase Sashiel’s **comprehensive development** in both **technical depth** and **professional application**.

---

## 📚 **References (Harvard Anglia Style)**

Adelson-Velsky, G.M. & Landis, E.M., 1962. *An algorithm for the organisation of information.* Soviet Mathematics Doklady, 3(2), pp.1259–1263.

Dijkstra, E.W., 1959. *A note on two problems in connection with graphs.* Numerische Mathematik, 1(1), pp.269–271.

Evans, E., 2023. *Domain-Driven Design: Tackling Complexity in the Heart of Software.* Addison-Wesley.

Microsoft, 2023. *Thread-Safe Collections in .NET.* Available at: [https://learn.microsoft.com/en-us/dotnet/](https://learn.microsoft.com/en-us/dotnet/) [Accessed 11 November 2025].

Microsoft, 2025. *ASP.NET Core MVC Fundamentals.* Available at: [https://learn.microsoft.com/en-us/aspnet/core/](https://learn.microsoft.com/en-us/aspnet/core/) [Accessed 11 November 2025].

Nield, R., 2023. *Pro ASP.NET Core 7 MVC.* Apress Publishing.

OWASP, 2024. *Top 10 Web Application Security Risks.* Available at: [https://owasp.org/](https://owasp.org/) [Accessed 11 November 2025].

Roberts, T., 2022. *Data Structures and Algorithms with C#.* Pearson Education.

Sedgewick, R., 2021. *Algorithms in C#: Data Structures and Problem Solving.* Addison-Wesley.

TutorialsPoint, 2024. *Heap Data Structures in C#.* Available at: [https://www.tutorialspoint.com/](https://www.tutorialspoint.com/) [Accessed 11 November 2025].

The Independent Institute of Education (IIE), 2025. *INSY7315 Module Manual: Work Integrated Learning.* The IIE, Johannesburg.

---

## 🏁 **Conclusion**

The **Municipal Services Platform** stands as a complete and technically sound solution that merges **algorithmic efficiency**, **security awareness**, and **municipal innovation**.
It showcases how advanced programming concepts can meaningfully enhance public service delivery while adhering to professional and ethical standards.

Through this summative project, **Sashiel Moonsamy** has demonstrated readiness for real-world software development roles, fulfilling both **academic** and **WIL competency** outcomes.

---

> 👩‍💻 *Developed by:* **Sashiel Moonsamy (ST10028058)**
> 🏫 *For:* PROG7312 — Advanced Programming
> 🗓️ *Submission Year:* 2025
> 💼 *Institution:* The Independent Institute of Education (IIE)
> 🪪 *Portfolio of Evidence (Part 3 — Summative Submission)*
