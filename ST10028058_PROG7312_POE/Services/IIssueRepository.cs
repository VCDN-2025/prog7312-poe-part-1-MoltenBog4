using ST10028058_PROG7312_POE.Models;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.Services
{
    public interface IIssueRepository
    {
        void Add(Issue issue);
        IEnumerable<Issue> GetAll();
        Issue? GetById(Guid id);

        // 🔹 NEW
        IEnumerable<Issue> GetByUser(string userId);
        int CountByUser(string userId);
    }
}

//# Assistance provided by ChatGPT
//# Code and support generated with the help of OpenAI's ChatGPT.
// code attribution
// W3schools
//https://www.w3schools.com/cs/index.php

// code attribution
//Bootswatch
//https://bootswatch.com/

// code attribution
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-8.0&tabs=visual-studio

// code attribution
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio