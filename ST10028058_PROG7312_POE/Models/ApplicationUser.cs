using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ST10028058_PROG7312_POE.Models
{
    // Extend if you want profile fields later
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string? FullName { get; set; }
    }
}
