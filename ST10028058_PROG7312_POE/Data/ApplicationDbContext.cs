using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ST10028058_PROG7312_POE.Models;

namespace ST10028058_PROG7312_POE.Data
{
    // Identity-only DbContext (your Issues/Feedback still use custom DS)
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
