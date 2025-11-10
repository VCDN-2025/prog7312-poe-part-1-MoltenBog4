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
        public IActionResult Index(string? search, string? sortOption)
        {
            var announcements = _announcements.Values.AsEnumerable();

            // 🔍 Search filter
            if (!string.IsNullOrWhiteSpace(search))
            {
                announcements = announcements.Where(a =>
                    a.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.Message.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    a.Author.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            // 🔽 Sorting
            announcements = sortOption switch
            {
                "title" => announcements.OrderBy(a => a.Title),
                "author" => announcements.OrderBy(a => a.Author),
                _ => announcements.OrderByDescending(a => a.Date) // default: newest first
            };

            ViewBag.Search = search;
            ViewBag.SortOption = sortOption;

            return View(announcements.ToList());
        }

        // ===== VIEW SINGLE ANNOUNCEMENT =====
        public IActionResult ViewAnnouncement(DateTime date)
        {
            if (_announcements.TryGetValue(date, out var announcement))
                return View(announcement);

            return NotFound();
        }
    }
}
