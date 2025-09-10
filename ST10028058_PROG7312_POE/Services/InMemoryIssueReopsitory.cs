using ST10028058_PROG7312_POE.DataStructures;
using ST10028058_PROG7312_POE.Models;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.Services
{
    /// <summary>
    /// Repo backed by our custom SinglyLinkedList (no built-in generic collections).
    /// </summary>
    public class InMemoryIssueRepository : IIssueRepository
    {
        private readonly SinglyLinkedList<Issue> _issues = new();

        // Total issues (all users). For per-user counts, use CountByUser.
        public int Count => _issues.Count;

        public void Add(Issue issue)
        {
            // Newest first so UI shows latest on top without sorting
            _issues.AddFirst(issue);
        }

        public IEnumerable<Issue> GetAll() => _issues;

        public Issue? GetById(Guid id) =>
            // Uses our SinglyLinkedList<T>.FirstOrDefault (not LINQ)
            _issues.FirstOrDefault(i => i.Id == id);

        // ---------- NEW: user-specific helpers ----------

        public IEnumerable<Issue> GetByUser(string userId)
        {
            foreach (var i in _issues)
            {
                if (i.CreatedByUserId == userId)
                    yield return i;
            }
        }

        public int CountByUser(string userId)
        {
            int n = 0;
            foreach (var i in _issues)
            {
                if (i.CreatedByUserId == userId) n++;
            }
            return n;
        }
    }

    public class InMemoryFeedbackRepository : IFeedbackRepository
    {
        private readonly SinglyLinkedList<Feedback> _items = new();

        public int TotalCount => _items.Count;

        public void Add(Feedback feedback)
        {
            // Newest first, consistent with Issues repo
            _items.AddFirst(feedback);
        }

        public IEnumerable<Feedback> GetForIssue(Guid issueId)
        {
            // Manual yield (no LINQ)
            foreach (var f in _items)
            {
                if (f.IssueId == issueId)
                    yield return f;
            }
        }

        public (double average, int count) GetSummary(Guid issueId)
        {
            int sum = 0, count = 0;
            foreach (var f in _items)
            {
                if (f.IssueId == issueId)
                {
                    sum += f.Rating;
                    count++;
                }
            }
            return (count == 0 ? 0.0 : (double)sum / count, count);
        }

        public Feedback? GetById(Guid id)
        {
            foreach (var f in _items)
                if (f.Id == id) return f;
            return null;
        }

        public bool SetAdminResponse(Guid feedbackId, string? response, int? responseTimeMinutes)
        {
            foreach (var f in _items)
            {
                if (f.Id == feedbackId)
                {
                    f.AdminResponse = response;
                    f.ResponseTimeMinutes = responseTimeMinutes;
                    return true;
                }
            }
            return false;
        }
    }
}
