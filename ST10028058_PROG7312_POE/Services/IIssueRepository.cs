using ST10028058_PROG7312_POE.Models;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.Services
{
    public interface IIssueRepository
    {
        void Add(Issue issue);
        IEnumerable<Issue> GetAll();
        Issue? GetById(Guid id);

        // 🔹 NEW
        IEnumerable<Issue> GetByUser(string userId);
        int CountByUser(string userId);
    }
}
