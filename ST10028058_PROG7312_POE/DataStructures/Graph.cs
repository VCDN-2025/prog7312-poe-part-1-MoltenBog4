using System;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.DataStructures
{
    /// <summary>
    /// Graph structure representing connected municipal areas.
    /// Used for exploring nearby zones in service request filtering.
    /// </summary>
    public class Graph
    {
        private readonly Dictionary<string, HashSet<string>> _adj = new(StringComparer.OrdinalIgnoreCase);

        // Add new area node
        public void AddNode(string node)
        {
            if (!string.IsNullOrWhiteSpace(node) && !_adj.ContainsKey(node))
                _adj[node] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        // Connect two areas
        public void AddEdge(string a, string b)
        {
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b)) return;
            AddNode(a);
            AddNode(b);
            _adj[a].Add(b);
            _adj[b].Add(a);
        }

        // Return all neighboring zones
        public IEnumerable<string> Neighbors(string node)
        {
            return _adj.TryGetValue(node, out var neighbors)
                ? neighbors
                : Array.Empty<string>();
        }

        // Breadth-first search to explore nearby areas
        public List<string> BFS(string start, int maxDepth = 1)
        {
            var result = new List<string>();
            if (!_adj.ContainsKey(start)) return result;

            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var queue = new Queue<(string Node, int Depth)>();

            visited.Add(start);
            queue.Enqueue((start, 0));

            while (queue.Count > 0)
            {
                var (node, depth) = queue.Dequeue();

                if (depth > 0) result.Add(node); // Skip adding the start node itself
                if (depth >= maxDepth) continue;

                foreach (var neighbor in _adj[node])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue((neighbor, depth + 1));
                    }
                }
            }

            return result;
        }
    }
}
