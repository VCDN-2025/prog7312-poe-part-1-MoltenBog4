using System;
using System.Collections.Generic;

namespace ST10028058_PROG7312_POE.DataStructures
{
    /// <summary>
    /// Custom Min-Heap (priority queue) used to store service requests
    /// based on their urgency (priority value).
    /// Lower number = higher urgency.
    /// </summary>
    public class MinHeap
    {
        private readonly List<(int RequestId, int Priority)> _items = new();

        public int Count => _items.Count;

        // ===== INSERT =====
        public void Insert(int requestId, int priority)
        {
            _items.Add((requestId, priority));
            HeapifyUp(_items.Count - 1);
        }

        // ===== PEEK (HIGHEST PRIORITY) =====
        public (int RequestId, int Priority)? Peek()
        {
            return _items.Count > 0 ? _items[0] : null;
        }

        // ===== POP (REMOVE ROOT) =====
        public (int RequestId, int Priority)? Pop()
        {
            if (_items.Count == 0) return null;

            var root = _items[0];
            _items[0] = _items[^1];
            _items.RemoveAt(_items.Count - 1);

            HeapifyDown(0);
            return root;
        }

        // ===== REMOVE BY ID =====
        public void RemoveByRequestId(int requestId)
        {
            int idx = _items.FindIndex(x => x.RequestId == requestId);
            if (idx == -1) return;

            _items[idx] = _items[^1];
            _items.RemoveAt(_items.Count - 1);

            if (idx < _items.Count)
            {
                HeapifyUp(idx);
                HeapifyDown(idx);
            }
        }

        // ===== UPDATE PRIORITY =====
        public void UpdatePriority(int requestId, int newPriority)
        {
            int idx = _items.FindIndex(x => x.RequestId == requestId);
            if (idx == -1) return;

            _items[idx] = (requestId, newPriority);
            HeapifyUp(idx);
            HeapifyDown(idx);
        }

        // ===== CONTAINS =====
        public bool Contains(int requestId)
        {
            return _items.Exists(x => x.RequestId == requestId);
        }

        // ===== PRIVATE HELPERS =====
        private void HeapifyUp(int i)
        {
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (_items[i].Priority < _items[parent].Priority)
                {
                    (_items[i], _items[parent]) = (_items[parent], _items[i]);
                    i = parent;
                }
                else break;
            }
        }

        private void HeapifyDown(int i)
        {
            int n = _items.Count;
            while (true)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                int smallest = i;

                if (left < n && _items[left].Priority < _items[smallest].Priority)
                    smallest = left;
                if (right < n && _items[right].Priority < _items[smallest].Priority)
                    smallest = right;

                if (smallest != i)
                {
                    (_items[i], _items[smallest]) = (_items[smallest], _items[i]);
                    i = smallest;
                }
                else break;
            }
        }
    }
}
