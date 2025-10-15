using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class AdminEventsController : Controller
    {
        // ===== Shared Data Structures =====
        private static SortedDictionary<DateTime, EventModel> _eventsByDate = EventsControllerAccessor.EventsByDate;
        private static HashSet<string> _categories = EventsControllerAccessor.Categories;

        // ===== Priority Queue for Upcoming Events =====
        // (Lower value = higher priority)
        private static PriorityQueue<EventModel, int> _priorityEvents = new();

        // ===== INDEX =====
        public IActionResult Index()
        {
            // Populate priority queue with future events
            _priorityEvents.Clear();
            foreach (var ev in _eventsByDate.Values)
            {
                int priority = (int)(ev.Date - DateTime.Today).TotalDays;
                if (priority >= 0)
                    _priorityEvents.Enqueue(ev, priority);
            }

            ViewBag.UpcomingEvents = _priorityEvents
                .UnorderedItems
                .OrderBy(x => x.Priority)
                .Take(5)
                .Select(x => x.Element)
                .ToList();

            var events = _eventsByDate.Values.OrderBy(e => e.Date).ToList();
            return View(events);
        }

        // ===== CREATE (GET) =====
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categories ?? new HashSet<string>();
            return View();
        }

        // ===== CREATE (POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categories ?? new HashSet<string>();
                return View(model);
            }

            if (_eventsByDate.ContainsKey(model.Date))
            {
                TempData["Error"] = "⚠ An event already exists on that date.";
                return RedirectToAction("Index");
            }

            _eventsByDate[model.Date] = model;

            if (!string.IsNullOrWhiteSpace(model.Category))
                _categories.Add(model.Category.Trim());

            TempData["Success"] = "✅ Event added successfully!";
            return RedirectToAction("Index");
        }

        // ===== EDIT (GET) =====
        [HttpGet]
        public IActionResult Edit(DateTime date)
        {
            if (_eventsByDate.TryGetValue(date, out var ev))
            {
                ViewBag.Categories = _categories ?? new HashSet<string>();
                return View(ev);
            }

            TempData["Error"] = "⚠ Event not found.";
            return RedirectToAction("Index");
        }

        // ===== EDIT (POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DateTime date, EventModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categories ?? new HashSet<string>();
                return View(model);
            }

            if (!_eventsByDate.ContainsKey(date))
            {
                TempData["Error"] = "⚠ Could not find event to update.";
                return RedirectToAction("Index");
            }

            _eventsByDate.Remove(date);
            _eventsByDate[model.Date] = model;

            if (!string.IsNullOrWhiteSpace(model.Category))
                _categories.Add(model.Category.Trim());

            TempData["Success"] = "✅ Event updated successfully!";
            return RedirectToAction("Index");
        }

        // ===== DELETE =====
        [HttpGet]
        public IActionResult Delete(DateTime date)
        {
            if (_eventsByDate.TryGetValue(date, out var ev))
                return View(ev);

            TempData["Error"] = "⚠ Event not found.";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(DateTime date)
        {
            if (_eventsByDate.TryGetValue(date, out var ev))
            {
                _eventsByDate.Remove(date);
                TempData["Success"] = "🗑 Event deleted successfully!";
            }
            else
            {
                TempData["Error"] = "⚠ Event not found.";
            }

            return RedirectToAction("Index");
        }
    }

    // ===== ACCESSOR CLASS =====
    public static class EventsControllerAccessor
    {
        public static SortedDictionary<DateTime, EventModel> EventsByDate =>
            (SortedDictionary<DateTime, EventModel>)
            typeof(EventsController)
                .GetField("_eventsByDate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.GetValue(null) ?? new SortedDictionary<DateTime, EventModel>();

        public static HashSet<string> Categories =>
            (HashSet<string>)
            typeof(EventsController)
                .GetField("_categories", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.GetValue(null) ?? new HashSet<string>();
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