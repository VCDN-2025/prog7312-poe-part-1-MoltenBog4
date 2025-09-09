using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ST10028058_PROG7312_POE.Data;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Identity on SQL Server (auth + roles)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CitizenOnly", p => p.RequireRole("Citizen"));
    options.AddPolicy("AdminOnly", p => p.RequireRole("Admin"));
});

// Your custom Part 1 repositories (no built-in collections)
builder.Services.AddSingleton<IIssueRepository, InMemoryIssueRepository>();
builder.Services.AddSingleton<IFeedbackRepository, InMemoryFeedbackRepository>();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath = "/Account/Login";
    o.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

Directory.CreateDirectory(Path.Combine(app.Environment.WebRootPath, "uploads"));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed roles + demo users
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;
    await SeedIdentity.EnsureSeedAsync(
        sp.GetRequiredService<RoleManager<IdentityRole>>(),
        sp.GetRequiredService<UserManager<ApplicationUser>>());
}

app.Run();
