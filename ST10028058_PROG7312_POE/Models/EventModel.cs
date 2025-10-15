using System;

namespace ST10028058_PROG7312_POE.Models
{
    public class EventModel
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // Optional: image or location (you can add later)
        public string Location { get; set; }
    }
}
