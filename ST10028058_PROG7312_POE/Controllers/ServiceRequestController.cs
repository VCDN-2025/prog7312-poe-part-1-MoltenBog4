using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class ServiceRequestsController : Controller
    {
        // ===== LIST + SEARCH + FILTERS =====
        public IActionResult Index(string? q, string? status, int? priority, string? category)
        {
            try
            {
                // Get all service requests
                var results = ServiceRequestManager.GetAllSortedByDateDescending();

                // 🔍 Keyword search (title, description, area, or category)
                if (!string.IsNullOrWhiteSpace(q))
                {
                    results = results.Where(r =>
                        r.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                        r.Description.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                        r.Area.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                        r.Category.Contains(q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                // 🟢 Filter by status
                if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<RequestStatus>(status, out var parsedStatus))
                {
                    results = results.Where(r => r.Status == parsedStatus).ToList();
                }

                // 🔺 Filter by priority (1–5)
                if (priority.HasValue)
                {
                    results = results.Where(r => r.Priority == priority.Value).ToList();
                }

                // 🧱 Filter by category
                if (!string.IsNullOrWhiteSpace(category))
                {
                    results = results.Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // 📋 Prepare dropdowns for filtering
                ViewBag.Search = q;
                ViewBag.SelectedStatus = status;
                ViewBag.SelectedPriority = priority;
                ViewBag.SelectedCategory = category;
                ViewBag.Categories = new List<string>
                {
                    "Roads", "Sanitation", "Electricity", "Water", "Maintenance", "Utilities"
                };

                return View(results);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error loading service requests: {ex.Message}";
                return View(Enumerable.Empty<ServiceRequestModel>());
            }
        }

        // ===== CREATE (GET) =====
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new List<string>
            {
                "Roads", "Sanitation", "Electricity", "Water", "Maintenance", "Utilities"
            };

            return View(new ServiceRequestModel());
        }

        // ===== CREATE (POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = new List<string>
                    {
                        "Roads", "Sanitation", "Electricity", "Water", "Maintenance", "Utilities"
                    };
                    return View(model);
                }

                var added = ServiceRequestManager.AddRequest(model);
                if (added == null)
                {
                    TempData["Error"] = "❌ Failed to submit your service request. Please try again.";
                    return View(model);
                }

                TempData["Success"] = $"✅ Your service request (ID #{added.RequestId}) has been submitted successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ An unexpected error occurred: {ex.Message}";
                return View(model);
            }
        }

        // ===== VIEW SINGLE =====
        [HttpGet]
        public IActionResult ViewRequest(int id)
        {
            try
            {
                var request = ServiceRequestManager.GetById(id);
                if (request == null)
                {
                    TempData["Error"] = "⚠ Request not found.";
                    return RedirectToAction("Index");
                }

                return View(request);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error loading request details: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
