using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminIssuesController : Controller
    {
        private readonly IIssueRepository _issues;
        private readonly IFeedbackRepository _feedback;

        public AdminIssuesController(IIssueRepository issues, IFeedbackRepository feedback)
        {
            _issues = issues;
            _feedback = feedback;
        }

        // All issues across all users
        public IActionResult Index()
        {
            var all = _issues.GetAll();
            return View(all);
        }

        // Per-issue view + feedback thread
        public IActionResult Details(Guid id)
        {
            var issue = _issues.GetById(id);
            if (issue == null) return NotFound();

            var summary = _feedback.GetSummary(id);
            ViewBag.AverageRating = summary.average;
            ViewBag.RatingCount = summary.count;

            // Stream feedback entries (no LINQ)
            IEnumerable<Feedback> Thread()
            {
                foreach (var f in _feedback.GetForIssue(id)) yield return f;
            }

            ViewBag.Feedback = Thread(); // the view will enumerate it
            return View(issue);
        }
    }
}
