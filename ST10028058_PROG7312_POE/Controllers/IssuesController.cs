using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;
using System.IO;

namespace ST10028058_PROG7312_POE.Controllers
{
    [Authorize(Policy = "CitizenOnly")] // require login for all actions in this controller
    public class IssuesController : Controller
    {
        private readonly IIssueRepository _repo;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;

        public IssuesController(IIssueRepository repo, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _env = env;
            _userManager = userManager;
        }

        // Show ONLY my issues
        public IActionResult Index()
        {
            var uid = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(uid)) return Challenge(); // safety

            var myIssues = _repo.GetByUser(uid);
            ViewBag.TotalCount = _repo.CountByUser(uid);
            return View(myIssues);
        }

        // Report form
        [HttpGet]
        public IActionResult Create() => View(new Issue());

        // Create (sets CreatedBy fields first, then validates)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Issue model, IFormFile? attachment)
        {
            // 1) Ensure user and stamp ownership BEFORE validation
            var uid = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(uid)) return Challenge();

            model.CreatedByUserId = uid;
            model.CreatedByName = User.Identity?.Name;

            // 2) Handle attachment (no LINQ)
            if (attachment is { Length: > 0 })
            {
                var ext = Path.GetExtension(attachment.FileName).ToLowerInvariant();
                bool allowed =
                    ext == ".jpg" || ext == ".jpeg" || ext == ".png" ||
                    ext == ".gif" || ext == ".pdf" || ext == ".docx";

                if (!allowed)
                {
                    ModelState.AddModelError("", "Only images (jpg, jpeg, png, gif) or documents (pdf, docx) are allowed.");
                }
                else
                {
                    var unique = $"{Guid.NewGuid()}{ext}";
                    var path = Path.Combine(_env.WebRootPath, "uploads", unique);
                    using (var fs = System.IO.File.Create(path))
                    {
                        await attachment.CopyToAsync(fs);
                    }
                    model.AttachmentFileName = unique;
                }
            }

            // 3) Revalidate AFTER we’ve populated server-side fields
            ModelState.Clear();
            TryValidateModel(model);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 4) Save
            _repo.Add(model);

            TempData["Success"] = "Issue submitted successfully! Thank you for helping improve municipal services.";
            TempData["InviteRateIssueId"] = model.Id.ToString();
            return RedirectToAction(nameof(Index));
        }

        // Details – block access to issues that aren't mine (unless Admin)
        public IActionResult Details(Guid id)
        {
            var issue = _repo.GetById(id);
            if (issue == null) return NotFound();

            var uid = _userManager.GetUserId(User);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && issue.CreatedByUserId != uid)
                return Forbid(); // or return NotFound();

            return View(issue);
        }
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