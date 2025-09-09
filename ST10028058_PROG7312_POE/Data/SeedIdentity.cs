using Microsoft.AspNetCore.Identity;
using ST10028058_PROG7312_POE.Models;

namespace ST10028058_PROG7312_POE.Data
{
    public static class SeedIdentity
    {
        public static async Task EnsureSeedAsync(
            RoleManager<IdentityRole> roleMgr,
            UserManager<ApplicationUser> userMgr)
        {
            // Roles
            if (!await roleMgr.RoleExistsAsync("Admin"))
                await roleMgr.CreateAsync(new IdentityRole("Admin"));
            if (!await roleMgr.RoleExistsAsync("Citizen"))
                await roleMgr.CreateAsync(new IdentityRole("Citizen"));

            // Admin
            var admin = await userMgr.FindByNameAsync("admin@city.gov");
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin@city.gov",
                    Email = "admin@city.gov",
                    EmailConfirmed = true,
                    FullName = "Municipality Admin"
                };
                await userMgr.CreateAsync(admin, "Admin123!");
                await userMgr.AddToRoleAsync(admin, "Admin");
            }

            // First Citizen
            var citizen1 = await userMgr.FindByNameAsync("user@demo.com");
            if (citizen1 == null)
            {
                citizen1 = new ApplicationUser
                {
                    UserName = "user@demo.com",
                    Email = "user@demo.com",
                    EmailConfirmed = true,
                    FullName = "Demo Citizen One"
                };
                await userMgr.CreateAsync(citizen1, "Citizen123!");
                await userMgr.AddToRoleAsync(citizen1, "Citizen");
            }

            // Second Citizen (new test user)
            var citizen2 = await userMgr.FindByNameAsync("second@demo.com");
            if (citizen2 == null)
            {
                citizen2 = new ApplicationUser
                {
                    UserName = "second@demo.com",
                    Email = "second@demo.com",
                    EmailConfirmed = true,
                    FullName = "Demo Citizen Two"
                };
                await userMgr.CreateAsync(citizen2, "Citizen456!");
                await userMgr.AddToRoleAsync(citizen2, "Citizen");
            }
        }
    }
}
