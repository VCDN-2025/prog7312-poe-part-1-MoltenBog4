using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.Controllers
{
    [Authorize(Policy = "CitizenOnly")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedbackRepo;
        private readonly IIssueRepository _issueRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedbackController(
            IFeedbackRepository feedbackRepo,
            IIssueRepository issueRepo,
            UserManager<ApplicationUser> userManager)
        {
            _feedbackRepo = feedbackRepo;
            _issueRepo = issueRepo;
            _userManager = userManager;
        }

        // --- RATE (CREATE) ---

        [HttpGet]
        public IActionResult Create(Guid issueId)
        {
            var issue = _issueRepo.GetById(issueId);
            if (issue == null) return NotFound();

            ViewBag.Issue = issue;
            return View(new Feedback { IssueId = issueId, Rating = 5 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback model)
        {
            var issue = _issueRepo.GetById(model.IssueId);
            if (issue == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Issue = issue;
                return View(model);
            }

            // Identify the citizen who is rating
            var uid = _userManager.GetUserId(User);
            model.CitizenUserId = uid;

            // Auto-fill from the reported issue (citizens do not choose this)
            var cat = issue.Category.ToString();
            model.Department = cat;
            model.ServiceType = cat; // keep same value for filter convenience (or remove property if not needed)

            _feedbackRepo.Add(model);
            TempData["FeedbackSuccess"] = "Thanks for your feedback!";
            return RedirectToAction("Details", "Issues", new { id = model.IssueId });
        }

        // --- HISTORY ---

        [HttpGet]
        public IActionResult MyHistory()
        {
            var uid = _userManager.GetUserId(User);

            // Stream only this user's feedback without using LINQ or List<T>
            IEnumerable<Feedback> Mine()
            {
                foreach (var issue in _issueRepo.GetAll())
                {
                    foreach (var f in _feedbackRepo.GetForIssue(issue.Id))
                    {
                        if (f.CitizenUserId == uid)
                            yield return f;
                    }
                }
            }

            ViewBag.Issues = _issueRepo; // view uses this to show issue context
            return View(Mine());
        }
    }
}
