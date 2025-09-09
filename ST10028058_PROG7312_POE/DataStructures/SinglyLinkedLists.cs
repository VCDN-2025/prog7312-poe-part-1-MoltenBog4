using System.Collections;
using System.Diagnostics;

namespace ST10028058_PROG7312_POE.DataStructures
{
    /// <summary>
    /// A minimal, custom singly linked list (no arrays, no List<T>).
    /// Supports AddFirst, AddLast, traversal, and Count.
    /// Implements IEnumerable<T> so views can foreach it.
    /// </summary>
    [DebuggerDisplay("Count = {Count}")]
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        private sealed class Node
        {
            public T Value;
            public Node? Next;
            public Node(T value) { Value = value; }
        }

        private Node? _head;
        private Node? _tail;

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        /// <summary>Add at the front (O(1)).</summary>
        public void AddFirst(T item)
        {
            var node = new Node(item) { Next = _head };
            _head = node;
            if (_tail is null) _tail = node;
            Count++;
        }

        /// <summary>Add at the end (O(1)).</summary>
        public void AddLast(T item)
        {
            var node = new Node(item);
            if (_tail is null)
            {
                _head = _tail = node;
            }
            else
            {
                _tail.Next = node;
                _tail = node;
            }
            Count++;
        }

        /// <summary>Enumerate from head to tail (O(n)).</summary>
        public IEnumerator<T> GetEnumerator()
        {
            var cur = _head;
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Find first value matching a predicate (O(n)).</summary>
        public T? FirstOrDefault(Func<T, bool> predicate)
        {
            var cur = _head;
            while (cur != null)
            {
                if (predicate(cur.Value)) return cur.Value;
                cur = cur.Next;
            }
            return default;
        }
    }
}
