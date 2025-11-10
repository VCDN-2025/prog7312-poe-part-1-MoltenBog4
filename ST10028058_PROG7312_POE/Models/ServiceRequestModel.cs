using System;
using System.ComponentModel.DataAnnotations;

namespace ST10028058_PROG7312_POE.Models
{
    public enum RequestStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public class ServiceRequestModel
    {
        // ===== Identification =====
        [Key]
        [Display(Name = "Request ID")]
        public int RequestId { get; set; }

        // ===== Core Details =====
        [Required(ErrorMessage = "Please enter a title for your request.")]
        [StringLength(100, ErrorMessage = "Title must be under 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please describe the problem or service request.")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        // ===== Location Details =====
        [Required(ErrorMessage = "Please specify your area.")]
        [Display(Name = "Area (e.g. Central District)")]
        public string Area { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a category.")]
        [Display(Name = "Category (e.g. Roads, Sanitation, Electricity)")]
        public string Category { get; set; } = string.Empty;

        // ===== System-Determined Fields =====
        [Display(Name = "Priority Level")]
        [Range(1, 5, ErrorMessage = "Priority must be between 1 (High) and 5 (Low).")]
        public int Priority { get; set; } = 5;

        [Display(Name = "Date Submitted")]
        [DataType(DataType.DateTime)]
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;

        [Display(Name = "Status")]
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // ===== Optional File Attachment =====
        [Display(Name = "Attached File (optional)")]
        public string? AttachedFilePath { get; set; }
    }
}
