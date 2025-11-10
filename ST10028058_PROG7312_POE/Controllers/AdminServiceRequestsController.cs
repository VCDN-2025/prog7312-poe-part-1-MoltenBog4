using Microsoft.AspNetCore.Mvc;
using ST10028058_PROG7312_POE.Models;
using ST10028058_PROG7312_POE.Services;

namespace ST10028058_PROG7312_POE.Controllers
{
    public class AdminServiceRequestsController : Controller
    {
        // ===== INDEX (ALL REQUESTS) =====
        public IActionResult Index()
        {
            var all = ServiceRequestManager.GetAllSortedByDateDescending();
            ViewBag.TopPriority = ServiceRequestManager.PeekTopPriority();
            return View(all);
        }

        // ===== UPDATE STATUS =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, RequestStatus status)
        {
            ServiceRequestManager.UpdateStatus(id, status);
            TempData["Success"] = $"✅ Request {id} status updated to {status}.";
            return RedirectToAction("Index");
        }

        // ===== VIEW SINGLE =====
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

        // ===== DELETE =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (ServiceRequestManager.Remove(id))
                TempData["Success"] = $"🗑 Request {id} deleted successfully.";
            else
                TempData["Error"] = $"⚠ Could not find request {id}.";

            return RedirectToAction("Index");
        }
    }
}
