using System.ComponentModel.DataAnnotations;

namespace ST10028058_PROG7312_POE.Models
{
    public class Feedback
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Link to the reported issue
        [Required]
        public Guid IssueId { get; set; }

        // 1–5 rating
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
        public int Rating { get; set; }

        // Citizen's optional message
        [StringLength(500)]
        public string? Comment { get; set; }

        // Timestamp
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        // ------ Fields used for role-based features ------

        // Who submitted this feedback (ASP.NET Identity user id)
        public string? CitizenUserId { get; set; }

        // For admin filtering (e.g., map from Issue.Category)
        public string? Department { get; set; }     // e.g., Water, Roads, Electricity
        public string? ServiceType { get; set; }    // e.g., Outage, Pothole, Leak

        // Optional SLA metric admins may set/view
        public int? ResponseTimeMinutes { get; set; }

        // Admin reply to close the feedback loop
        public string? AdminResponse { get; set; }
    }
}
