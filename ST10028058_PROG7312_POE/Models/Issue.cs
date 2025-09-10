using System.ComponentModel.DataAnnotations;

namespace ST10028058_PROG7312_POE.Models
{
    public enum IssueCategory { Sanitation, Roads, Electricity, Water, Parks, Other }

    public class Issue
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public IssueCategory Category { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        public string? AttachmentFileName { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        // 🔹 NEW: who submitted this issue
        [Required]
        public string CreatedByUserId { get; set; } = string.Empty;

        // (optional, handy for admin lists)
        public string? CreatedByName { get; set; }
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