using System;
using System.ComponentModel.DataAnnotations;

namespace ST10028058_PROG7312_POE.Models
{
    public class AnnouncementModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Announcement Date")]
        public DateTime Date { get; set; } = DateTime.Today;

        public string Author { get; set; } = "Admin";
    }
}
