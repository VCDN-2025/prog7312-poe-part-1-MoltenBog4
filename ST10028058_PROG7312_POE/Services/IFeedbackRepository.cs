using ST10028058_PROG7312_POE.Models;

namespace ST10028058_PROG7312_POE.Services
{
    public interface IFeedbackRepository
    {
        void Add(Feedback feedback);
        IEnumerable<Feedback> GetForIssue(Guid issueId);
        (double average, int count) GetSummary(Guid issueId);
        int TotalCount { get; }

        Feedback? GetById(Guid id);
        bool SetAdminResponse(Guid feedbackId, string? response, int? responseTimeMinutes);
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