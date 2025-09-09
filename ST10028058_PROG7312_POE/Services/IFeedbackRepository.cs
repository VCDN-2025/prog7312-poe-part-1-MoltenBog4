using ST10028058_PROG7312_POE.Models;

namespace ST10028058_PROG7312_POE.Services
{
    public interface IFeedbackRepository
    {
        void Add(Feedback feedback);
        IEnumerable<Feedback> GetForIssue(Guid issueId);
        (double average, int count) GetSummary(Guid issueId);
        int TotalCount { get; }
    }
}
