using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class AdminAnnouncementsController : Controller
    {
        // === In-memory store ===
        private static SortedDictionary<DateTime, AnnouncementModel> _announcements = new();

        // === STATIC CONSTRUCTOR: Seed sample announcements ===
        static AdminAnnouncementsController()
        {
            if (!_announcements.Any())
            {
                AddAnnouncement(
                    "Water Supply Interruption Notice",
                    "Residents are advised that water supply will be temporarily unavailable in the Central District on Saturday due to essential maintenance. Please store sufficient water beforehand.",
                    DateTime.Today.AddDays(-1),
                    "Municipal Water Department"
                );

                AddAnnouncement(
                    "Free Health Screening Day",
                    "Join our community health initiative this Friday for free blood pressure, diabetes, and wellness screenings at your nearest clinic.",
                    DateTime.Today.AddDays(1),
                    "City Health Department"
                );

                AddAnnouncement(
                    "Traffic Diversion for Road Repairs",
                    "Main Street will be closed for resurfacing between 8–15 November. Motorists are advised to use alternate routes via Green Avenue or Oak Drive.",
                    DateTime.Today,
                    "Traffic and Transport Authority"
                );

                AddAnnouncement(
                    "Load Shedding Schedule Update",
                    "An updated load shedding schedule has been released for November. Visit the municipal website for your zone's timing and stay prepared.",
                    DateTime.Today.AddDays(-3),
                    "Energy Management Office"
                );

                AddAnnouncement(
                    "Public Library Renovations",
                    "The Downtown Public Library will be closed from 12–25 November for renovations. All due dates for borrowed books will be extended automatically.",
                    DateTime.Today.AddDays(2),
                    "Department of Education & Culture"
                );

                AddAnnouncement(
                    "Holiday Market Applications Open",
                    "Local vendors can now apply for stalls at the annual Holiday Market event taking place in December. Limited spots available!",
                    DateTime.Today.AddDays(5),
                    "Community Development Office"
                );

                AddAnnouncement(
                    "Emergency Contact Number Update",
                    "The city’s emergency contact number has changed to 0800-555-911. Please update your records and share this with your community.",
                    DateTime.Today.AddDays(-7),
                    "Municipal Communications"
                );
            }
        }

        // === UTILITY METHOD: Adds an announcement ===
        private static void AddAnnouncement(string title, string message, DateTime date, string author)
        {
            _announcements[date] = new AnnouncementModel
            {
                Title = title,
                Message = message,
                Date = date,
                Author = author
            };
        }

        // === INDEX ===
        public IActionResult Index()
        {
            var list = _announcements.Values.OrderByDescending(a => a.Date).ToList();
            return View(list);
        }

        // === CREATE (GET) ===
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // === CREATE (POST) ===
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AnnouncementModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _announcements[model.Date] = model;
            TempData["Success"] = "✅ Announcement added successfully!";
            return RedirectToAction("Index");
        }

        // === DELETE (GET) ===
        [HttpGet]
        public IActionResult Delete(DateTime date)
        {
            if (_announcements.TryGetValue(date, out var announcement))
                return View(announcement);

            TempData["Error"] = "⚠ Announcement not found.";
            return RedirectToAction("Index");
        }

        // === DELETE (POST) ===
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(DateTime date)
        {
            if (_announcements.Remove(date))
                TempData["Success"] = "🗑 Announcement deleted successfully!";
            else
                TempData["Error"] = "⚠ Announcement not found.";

            return RedirectToAction("Index");
        }

        // === VIEW SINGLE ===
        public IActionResult ViewAnnouncement(DateTime date)
        {
            if (_announcements.TryGetValue(date, out var announcement))
                return View(announcement);

            return NotFound();
        }

        // === Shared accessor for other controllers ===
        public static SortedDictionary<DateTime, AnnouncementModel> Announcements => _announcements;
    }
}
