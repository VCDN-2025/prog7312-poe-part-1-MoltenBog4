using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;

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

        // ---------------------------
        // LIST: All feedback entries
        // ---------------------------
        [HttpGet]
        public IActionResult Index()
        {
            // Build (Feedback f, Issue? i) rows without LINQ
            IEnumerable<(Feedback f, Issue? i)> All()
            {
                foreach (var issue in _issues.GetAll())
                {
                    foreach (var f in _feedback.GetForIssue(issue.Id))
                        yield return (f, issue);
                }
            }

            ViewBag.Rows = All();
            return View(); // View reads ViewBag.Rows
        }

        // -----------------------------------------
        // RESPOND (GET): Show form to write a reply
        // -----------------------------------------
        [HttpGet]
        public IActionResult Respond(Guid id)
        {
            var f = _feedback.GetById(id);
            if (f == null) return NotFound();

            var issue = _issues.GetById(f.IssueId);

            var vm = new AdminRespondVm
            {
                FeedbackId = f.Id,
                IssueId = f.IssueId,
                IssueLocation = issue?.Location ?? f.IssueId.ToString(),
                IssueCategory = issue?.Category.ToString() ?? "-",
                CitizenUserId = f.CitizenUserId ?? "-",
                Rating = f.Rating,
                Comment = f.Comment,
                AdminResponse = f.AdminResponse,
                ResponseTimeMinutes = f.ResponseTimeMinutes,
                SubmittedAt = f.SubmittedAt,
                IssueSubmittedAt = issue?.SubmittedAt ?? DateTime.MinValue
            };

            return View(vm);
        }

        // -------------------------------------------------------
        // RESPOND (POST): Save the admin reply (+ optional SLA)
        // -------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Respond(AdminRespondVm vm)
        {
            // If admin didn't provide minutes, estimate from issue submit to now
            int? minutes = vm.ResponseTimeMinutes;
            if (!minutes.HasValue && vm.IssueSubmittedAt != DateTime.MinValue)
            {
                var delta = DateTime.Now - vm.IssueSubmittedAt;
                var m = (int)Math.Round(delta.TotalMinutes);
                if (m < 0) m = 0;
                minutes = m;
            }

            var ok = _feedback.SetAdminResponse(vm.FeedbackId, vm.AdminResponse, minutes);
            if (!ok) return NotFound();

            TempData["Success"] = "Response saved.";
            return RedirectToAction(nameof(Index));
        }
    }

    // ViewModel used by Respond view
    public class AdminRespondVm
    {
        public Guid FeedbackId { get; set; }
        public Guid IssueId { get; set; }

        public string IssueLocation { get; set; } = "-";
        public string IssueCategory { get; set; } = "-";
        public string CitizenUserId { get; set; } = "-";

        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string? AdminResponse { get; set; }
        public int? ResponseTimeMinutes { get; set; }

        public DateTime SubmittedAt { get; set; }
        public DateTime IssueSubmittedAt { get; set; }
    }
}
