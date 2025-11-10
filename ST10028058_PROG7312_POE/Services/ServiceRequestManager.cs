using System;
using System.Collections.Generic;
using System.Linq;
using ST10028058_PROG7312_POE.Models;

namespace ST10028058_PROG7312_POE.Services
{
    public static class ServiceRequestManager
    {
        // In-memory store: keeps requests sorted by RequestId
        private static readonly SortedDictionary<int, ServiceRequestModel> _requests = new();

        // Priority queue: smaller number = higher priority
        private static readonly PriorityQueue<ServiceRequestModel, int> _priorityQueue = new();

        // Graph of municipal areas (for ExploreNearbyAreas feature)
        private static readonly Dictionary<string, List<string>> _areaGraph = new()
        {
            { "Central District", new List<string> { "North Zone", "South Zone" } },
            { "North Zone", new List<string> { "Central District", "West End" } },
            { "South Zone", new List<string> { "Central District", "East Ward" } },
            { "East Ward", new List<string> { "South Zone" } },
            { "West End", new List<string> { "North Zone" } }
        };

        private static int _nextId = 1;
        private static readonly object _lock = new(); // 🔒 Thread safety

        // ====== STATIC CONSTRUCTOR TO SEED DATA ======
        static ServiceRequestManager()
        {
            if (!_requests.Any())
            {
                SeedSampleRequests();
            }
        }

        // ====== SEED SAMPLE DATA ======
        private static void SeedSampleRequests()
        {
            AddRequest(new ServiceRequestModel
            {
                Title = "Pothole Repair on Main Street",
                Description = "Large pothole near the intersection causing traffic delays.",
                Area = "Central District",
                Category = "Roads",
                Priority = 1,
                Status = RequestStatus.InProgress
            });

            AddRequest(new ServiceRequestModel
            {
                Title = "Water Leakage in North Zone",
                Description = "Continuous water leak reported near residential block A12.",
                Area = "North Zone",
                Category = "Utilities",
                Priority = 2,
                Status = RequestStatus.Pending
            });

            AddRequest(new ServiceRequestModel
            {
                Title = "Streetlight Not Working",
                Description = "The streetlight opposite the park is not functioning.",
                Area = "West End",
                Category = "Electricity",
                Priority = 3,
                Status = RequestStatus.Pending
            });

            AddRequest(new ServiceRequestModel
            {
                Title = "Illegal Dumping in East Ward",
                Description = "Garbage piles accumulating near the old bus stop.",
                Area = "East Ward",
                Category = "Sanitation",
                Priority = 2,
                Status = RequestStatus.Completed
            });

            AddRequest(new ServiceRequestModel
            {
                Title = "Broken Water Pipe",
                Description = "Burst pipe flooding street near Elm Avenue.",
                Area = "South Zone",
                Category = "Utilities",
                Priority = 1,
                Status = RequestStatus.InProgress
            });

            AddRequest(new ServiceRequestModel
            {
                Title = "Park Fence Repair",
                Description = "Damaged fencing along the children’s play area.",
                Area = "Central District",
                Category = "Maintenance",
                Priority = 4,
                Status = RequestStatus.Pending
            });

            AddRequest(new ServiceRequestModel
            {
                Title = "Traffic Light Malfunction",
                Description = "Signal lights stuck on red at Oak Street intersection.",
                Area = "Central District",
                Category = "Electricity",
                Priority = 1,
                Status = RequestStatus.InProgress
            });

            AddRequest(new ServiceRequestModel
            {
                Title = "Sewage Overflow",
                Description = "Foul smell and overflow from drain behind shopping center.",
                Area = "South Zone",
                Category = "Sanitation",
                Priority = 1,
                Status = RequestStatus.Pending
            });
        }

        // ====== CORE CRUD OPERATIONS ======
        public static ServiceRequestModel AddRequest(ServiceRequestModel request)
        {
            lock (_lock)
            {
                request.RequestId = _nextId++;
                request.DateSubmitted = DateTime.UtcNow;
                request.LastUpdated = request.DateSubmitted;

                _requests[request.RequestId] = request;
                _priorityQueue.Enqueue(request, request.Priority);
                return request;
            }
        }

        public static IEnumerable<ServiceRequestModel> GetAllSortedByDateDescending()
        {
            lock (_lock)
            {
                return _requests.Values.OrderByDescending(r => r.DateSubmitted).ToList();
            }
        }

        public static ServiceRequestModel? GetById(int id)
        {
            lock (_lock)
            {
                return _requests.TryGetValue(id, out var r) ? r : null;
            }
        }

        public static bool Remove(int id)
        {
            lock (_lock)
            {
                if (_requests.ContainsKey(id))
                {
                    _requests.Remove(id);
                    return true;
                }
                return false;
            }
        }

        // ====== UPDATE STATUS ======
        public static bool UpdateStatus(int id, RequestStatus newStatus)
        {
            lock (_lock)
            {
                if (_requests.TryGetValue(id, out var r))
                {
                    if (r.Status != newStatus)
                    {
                        r.Status = newStatus;
                        r.LastUpdated = DateTime.UtcNow;
                    }
                    return true;
                }
                return false;
            }
        }

        // ====== UPDATE PRIORITY ======
        public static bool UpdatePriority(int id, int newPriority)
        {
            lock (_lock)
            {
                if (_requests.TryGetValue(id, out var r))
                {
                    if (r.Priority != newPriority)
                    {
                        r.Priority = newPriority;
                        r.LastUpdated = DateTime.UtcNow;
                    }
                    return true;
                }
                return false;
            }
        }

        // ====== SEARCH + FILTER ======
        public static IEnumerable<ServiceRequestModel> Search(string? keyword, string? status, string? area)
        {
            lock (_lock)
            {
                var query = _requests.Values.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(keyword))
                    query = query.Where(r => r.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                                           || r.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<RequestStatus>(status, out var parsedStatus))
                    query = query.Where(r => r.Status == parsedStatus);

                if (!string.IsNullOrWhiteSpace(area))
                    query = query.Where(r => r.Area.Equals(area, StringComparison.OrdinalIgnoreCase));

                return query.OrderByDescending(r => r.DateSubmitted).ToList();
            }
        }

        // ====== PRIORITY QUEUE FEATURE ======
        public static ServiceRequestModel? PeekTopPriority()
        {
            lock (_lock)
            {
                return _priorityQueue.TryPeek(out var top, out _) ? top : null;
            }
        }

        // ====== GRAPH: Nearby Area Feature ======
        public static List<string> ExploreNearbyAreas(string area)
        {
            if (_areaGraph.TryGetValue(area, out var neighbors))
                return neighbors;
            return new List<string> { "No nearby areas found." };
        }
    }
}
