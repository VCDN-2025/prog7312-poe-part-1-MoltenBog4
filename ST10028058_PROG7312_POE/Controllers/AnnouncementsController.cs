using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class AnnouncementsController : Controller
    {
        // Reuse data from AdminAnnouncementsController
        private static SortedDictionary<DateTime, AnnouncementModel> _announcements =
            AdminAnnouncementsController.Announcements;

        // ===== INDEX (List all announcements) =====
        public IActionResult Index(string? search, string? sortOption, string? department, DateTime? startDate, DateTime? endDate)
        {
            var announcements = _announcements.Values.AsEnumerable();

            // 🔍 SEARCH
            if (!string.IsNullOrWhiteSpace(search))
            {
                announcements = announcements.Where(a =>
                    a.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.Message.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.Author.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            // 🏢 DEPARTMENT FILTER
            if (!string.IsNullOrWhiteSpace(department))
            {
                announcements = announcements.Where(a =>
                    a.Author.Equals(department, StringComparison.OrdinalIgnoreCase));
            }

            // 📅 DATE RANGE FILTER
            if (startDate.HasValue)
                announcements = announcements.Where(a => a.Date >= startDate.Value);

            if (endDate.HasValue)
                announcements = announcements.Where(a => a.Date <= endDate.Value);

            // 🔽 SORTING OPTIONS
            announcements = sortOption switch
            {
                "oldest" => announcements.OrderBy(a => a.Date),
                "title" => announcements.OrderBy(a => a.Title),
                "author" => announcements.OrderBy(a => a.Author),
                _ => announcements.OrderByDescending(a => a.Date) // Default: Newest first
            };

            // 🎯 Pass filters back to ViewBag for the View
            ViewBag.Search = search;
            ViewBag.SortOption = sortOption;
            ViewBag.Department = department;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(announcements.ToList());
        }

        // ===== RESET FILTERS =====
        [HttpGet]
        public IActionResult Reset()
        {
            // 👇 Redirects to Index() without any query parameters (clears filters)
            return RedirectToAction(nameof(Index));
        }

        // ===== VIEW SINGLE ANNOUNCEMENT =====
        public IActionResult ViewAnnouncement(DateTime date)
        {
            // 🔎 Try to find the announcement by its date key
            if (!_announcements.TryGetValue(date, out var announcement))
            {
                // If not found, return a 404 Not Found page with a message
                TempData["ErrorMessage"] = "The announcement you're looking for could not be found.";
                return RedirectToAction(nameof(Index));
            }

            // ✅ Pass the announcement to the view for display
            return View("ViewAnnouncements", announcement);
        }
    }
}
