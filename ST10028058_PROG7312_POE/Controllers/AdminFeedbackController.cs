using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminFeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedback;
        private readonly IIssueRepository _issues;

        public AdminFeedbackController(IFeedbackRepository feedback, IIssueRepository issues)
        {
            _feedback = feedback;
            _issues = issues;
        }

        // All feedback across all issues (flat list)
        public IActionResult Index()
        {
            IEnumerable<(Feedback f, Issue? i)> All()
            {
                foreach (var issue in _issues.GetAll())
                {
                    foreach (var f in _feedback.GetForIssue(issue.Id))
                        yield return (f, issue);
                }
            }

            ViewBag.Rows = All();
            return View(); // strongly-typed model not needed; we’ll read ViewBag.Rows
        }
    }
}
