using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class AdminServiceRequestsController : Controller
    {
        // ===== INDEX (ALL REQUESTS WITH FILTERS) =====
        public IActionResult Index(string? q, string? status, int? priority, string? category)
        {
            try
            {
                // Load all requests
                var all = ServiceRequestManager.GetAllSortedByDateDescending().ToList();

                // 🔍 Keyword search (title, description, area, or category)
                if (!string.IsNullOrWhiteSpace(q))
                {
                    all = all.Where(r =>
                        r.Title.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                        r.Description.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                        r.Area.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                        r.Category.Contains(q, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                // 🎯 Status filter
                if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<RequestStatus>(status, out var parsedStatus))
                {
                    all = all.Where(r => r.Status == parsedStatus).ToList();
                }

                // ⚡ Priority filter
                if (priority.HasValue)
                {
                    all = all.Where(r => r.Priority == priority.Value).ToList();
                }

                // 🧱 Category filter
                if (!string.IsNullOrWhiteSpace(category))
                {
                    all = all.Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // 🌟 Top priority (for display banner)
                ViewBag.TopPriority = ServiceRequestManager.PeekTopPriority();

                // 💾 Preserve filter selections
                ViewBag.Search = q;
                ViewBag.SelectedStatus = status;
                ViewBag.SelectedPriority = priority;
                ViewBag.SelectedCategory = category;

                // 📋 Populate category dropdown
                ViewBag.Categories = new List<string>
                {
                    "Roads", "Sanitation", "Electricity", "Water", "Maintenance", "Utilities"
                };

                return View(all);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error loading service requests: {ex.Message}";
                return View(Enumerable.Empty<ServiceRequestModel>());
            }
        }

        // ===== UPDATE STATUS (FORM POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, RequestStatus status)
        {
            try
            {
                bool success = ServiceRequestManager.UpdateStatus(id, status);

                if (success)
                    TempData["Success"] = $"✅ Request {id} status updated to {status}.";
                else
                    TempData["Error"] = $"⚠ Could not find request {id}.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error updating request {id}: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // ===== UPDATE STATUS (AJAX) =====
        [HttpPost]
        public IActionResult UpdateStatusAjax(int id, RequestStatus status)
        {
            try
            {
                bool success = ServiceRequestManager.UpdateStatus(id, status);

                if (success)
                {
                    return Json(new
                    {
                        success = true,
                        message = $"✅ Request {id} status updated to {status}.",
                        lastUpdated = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }

                return Json(new { success = false, message = $"⚠ Could not find request {id}." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"❌ Error: {ex.Message}" });
            }
        }

        // ===== UPDATE PRIORITY (AJAX) =====
        [HttpPost]
        public IActionResult UpdatePriorityAjax(int id, int priority)
        {
            try
            {
                bool success = ServiceRequestManager.UpdatePriority(id, priority);

                if (success)
                {
                    string priorityLabel = priority switch
                    {
                        1 => "Critical",
                        2 => "High",
                        3 => "Medium",
                        4 => "Low",
                        5 => "Very Low",
                        _ => "Unspecified"
                    };

                    return Json(new
                    {
                        success = true,
                        message = $"⚡ Priority for Request {id} updated to {priorityLabel}.",
                        lastUpdated = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }

                return Json(new { success = false, message = $"⚠ Could not find request {id}." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"❌ Error updating priority: {ex.Message}" });
            }
        }

        // ===== VIEW SINGLE REQUEST =====
        public IActionResult ViewRequest(int id)
        {
            var request = ServiceRequestManager.GetById(id);
            if (request == null)
            {
                TempData["Error"] = "⚠ Request not found.";
                return RedirectToAction("Index");
            }

            return View(request);
        }

        // ===== DELETE REQUEST =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                bool removed = ServiceRequestManager.Remove(id);

                if (removed)
                    TempData["Success"] = $"🗑 Request {id} deleted successfully.";
                else
                    TempData["Error"] = $"⚠ Could not find request {id}.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error deleting request {id}: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
