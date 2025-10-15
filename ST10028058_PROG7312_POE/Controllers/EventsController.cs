using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class EventsController : Controller
    {
        // ===== DATA STRUCTURES =====
        private static SortedDictionary<DateTime, EventModel> _eventsByDate = new();
        private static HashSet<string> _categories = new();
        private static Queue<EventModel> _recentlyViewed = new();
        private static Dictionary<string, int> _searchFrequency = new();

        // ===== CONSTRUCTOR: SEED SAMPLE DATA =====
        static EventsController()
        {
            if (!_eventsByDate.Any())
            {
                // --- Community & Environment ---
                AddEvent("Community Clean-Up Drive", "Community", DateTime.Today.AddDays(2),
                    "Join our community clean-up drive to promote a healthy, clean environment.", "Central Park");

                AddEvent("Tree Planting Day", "Environment", DateTime.Today.AddDays(4),
                    "Help plant trees to support our green city initiative.", "Eco Park");

                AddEvent("Riverbank Restoration", "Environment", DateTime.Today.AddDays(6),
                    "Assist in cleaning and restoring the local riverbank to preserve biodiversity.", "Riverside Zone");

                // --- Education & Innovation ---
                AddEvent("Youth Coding Bootcamp", "Education", DateTime.Today.AddDays(3),
                    "Free workshop introducing youth to coding, problem-solving, and innovation.", "Tech Library");

                AddEvent("Math Olympiad", "Education", DateTime.Today.AddDays(9),
                    "Test your problem-solving skills in the municipal math and science challenge.", "City School Hall");

                AddEvent("Digital Literacy Workshop", "Education", DateTime.Today.AddDays(12),
                    "Learn basic computer and internet skills in this free community course.", "Municipal ICT Centre");

                // --- Health & Safety ---
                AddEvent("Blood Donation Camp", "Health", DateTime.Today.AddDays(5),
                    "Donate blood to help save lives and strengthen community health support.", "City Health Centre");

                AddEvent("Fire Safety Workshop", "Safety", DateTime.Today.AddDays(7),
                    "Interactive workshop to learn fire prevention and emergency response.", "Fire HQ Auditorium");

                AddEvent("Wellness & Mental Health Seminar", "Health", DateTime.Today.AddDays(14),
                    "Discover ways to manage stress and build mental resilience.", "Community Hall");

                AddEvent("First Aid Training Session", "Safety", DateTime.Today.AddDays(8),
                    "Become certified in basic first aid and emergency response.", "Municipal Clinic");

                // --- Arts & Culture ---
                AddEvent("Art and Culture Fair", "Arts", DateTime.Today.AddDays(10),
                    "A celebration of art, dance, and local creativity with live performances.", "City Square");

                AddEvent("Local Film Screening", "Arts", DateTime.Today.AddDays(13),
                    "Short film showcase promoting social awareness and storytelling.", "Civic Theatre");

                AddEvent("Music Under the Stars", "Entertainment", DateTime.Today.AddDays(16),
                    "Enjoy live music performances under the night sky.", "Town Amphitheatre");

                // --- Sports & Recreation ---
                AddEvent("Community Soccer League Finals", "Sports", DateTime.Today.AddDays(11),
                    "Watch local teams compete for the championship title.", "Municipal Stadium");

                AddEvent("Fun Run & Family Day", "Sports", DateTime.Today.AddDays(15),
                    "Participate in the city-wide charity fun run and family activities.", "East Green Zone");

                // --- Government & Sustainability ---
                AddEvent("Town Budget Consultation", "Government", DateTime.Today.AddDays(17),
                    "Public consultation meeting for the annual municipal budget.", "Town Hall");

                AddEvent("Renewable Energy Expo", "Sustainability", DateTime.Today.AddDays(18),
                    "Explore innovative energy solutions and meet green tech experts.", "Eco Expo Centre");

                AddEvent("Citizen Innovation Challenge", "Community", DateTime.Today.AddDays(20),
                    "Pitch your ideas to improve city living and win project funding.", "Innovation Hub");
            }
        }

        // ===== SHARED DATA ACCESS =====
        public static SortedDictionary<DateTime, EventModel> EventsByDate => _eventsByDate;
        public static HashSet<string> Categories => _categories;

        // ===== ADD NEW CATEGORY =====
        public static void AddCategory(string newCategory)
        {
            if (!string.IsNullOrWhiteSpace(newCategory))
                _categories.Add(newCategory.Trim());
        }

        // ===== INTERNAL UTILITY: Add Event =====
        private static void AddEvent(string title, string category, DateTime date, string description, string location)
        {
            var ev = new EventModel
            {
                Title = title,
                Category = category,
                Date = date,
                Description = description,
                Location = location
            };

            _eventsByDate[date] = ev;
            _categories.Add(category);
        }

        // ===== INTERNAL UTILITY: Track Searches =====
        private void TrackSearch(string term)
        {
            if (_searchFrequency.ContainsKey(term))
                _searchFrequency[term]++;
            else
                _searchFrequency[term] = 1;
        }

        // ===== INTERNAL UTILITY: Recommend Events =====
        private List<EventModel> RecommendEvents()
        {
            var topCategories = _searchFrequency
                .OrderByDescending(x => x.Value)
                .Take(3)
                .Select(x => x.Key)
                .ToList();

            return _eventsByDate.Values
                .Where(e => topCategories.Contains(e.Category))
                .ToList();
        }

        // ===== USER VIEW: Index (Browse Events) =====
        public IActionResult Index(string? keyword, string? searchCategory, DateTime? fromDate, DateTime? toDate, string? sortOption)
        {
            var events = _eventsByDate.Values.AsEnumerable();

            // 🔍 Keyword search
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                events = events.Where(e =>
                    e.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    e.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));
                TrackSearch(keyword);
            }

            // 🏷 Category filter
            if (!string.IsNullOrEmpty(searchCategory))
            {
                events = events.Where(e => e.Category.Equals(searchCategory, StringComparison.OrdinalIgnoreCase));
                TrackSearch(searchCategory);
            }

            // 📅 Date range filter
            if (fromDate.HasValue)
                events = events.Where(e => e.Date >= fromDate.Value);

            if (toDate.HasValue)
                events = events.Where(e => e.Date <= toDate.Value);

            // 🔽 Sorting
            if (!string.IsNullOrEmpty(sortOption))
            {
                events = sortOption switch
                {
                    "date" => events.OrderBy(e => e.Date),
                    "category" => events.OrderBy(e => e.Category),
                    "title" => events.OrderBy(e => e.Title),
                    _ => events
                };
            }

            ViewBag.Categories = _categories;
            ViewBag.Recommendations = RecommendEvents();

            return View(events.ToList());
        }

        // ===== USER VIEW: Single Event =====
        public IActionResult ViewEvent(DateTime date)
        {
            if (_eventsByDate.TryGetValue(date, out var ev))
            {
                // Track recent views (limit 5)
                if (_recentlyViewed.Count >= 5)
                    _recentlyViewed.Dequeue();

                _recentlyViewed.Enqueue(ev);
                return View(ev);
            }
            return NotFound();
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