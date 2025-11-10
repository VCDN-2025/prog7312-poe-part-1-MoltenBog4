using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;
using System;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class ServiceRequestsController : Controller
    {
        // ===== LIST + SEARCH =====
        public IActionResult Index(string? q, string? status, string? area)
        {
            var results = ServiceRequestManager.Search(q, status, area);
            ViewBag.Search = q;
            ViewBag.Status = status;
            ViewBag.Area = area;
            ViewBag.AreasNearby = !string.IsNullOrWhiteSpace(area)
                ? ServiceRequestManager.ExploreNearbyAreas(area)
                : null;

            return View(results);
        }

        // ===== CREATE (GET) =====
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ServiceRequestModel());
        }

        // ===== CREATE (POST) =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ServiceRequestModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var added = ServiceRequestManager.AddRequest(model);
            TempData["Success"] = $"✅ Service Request #{added.RequestId} submitted successfully.";
            return RedirectToAction("Index");
        }

        // ===== VIEW SINGLE =====
        public IActionResult ViewRequest(int id)
        {
            var r = ServiceRequestManager.GetById(id);
            if (r == null)
            {
                TempData["Error"] = "⚠ Request not found.";
                return RedirectToAction("Index");
            }

            return View(r);
        }
    }
}
