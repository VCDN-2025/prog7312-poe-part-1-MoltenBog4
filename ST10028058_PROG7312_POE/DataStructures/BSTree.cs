using System;
using System.Collections.Generic;
using ST10028058_PROG7312_POE.Models;

namespace ST10028058_PROG7312_POE.DataStructures
{
    /// <summary>
    /// Binary Search Tree used to store Service Requests sorted by Request ID.
    /// </summary>
    public class BSTNode
    {
        public int Key { get; set; }
        public ServiceRequestModel Value { get; set; }
        public BSTNode? Left { get; set; }
        public BSTNode? Right { get; set; }

        public BSTNode(int key, ServiceRequestModel value)
        {
            Key = key;
            Value = value;
        }
    }

    public class BSTree
    {
        private BSTNode? _root;

        // ===== INSERT =====
        public void Insert(int key, ServiceRequestModel value)
        {
            _root = InsertRec(_root, key, value);
        }

        private BSTNode InsertRec(BSTNode? node, int key, ServiceRequestModel val)
        {
            if (node == null) return new BSTNode(key, val);
            if (key < node.Key) node.Left = InsertRec(node.Left, key, val);
            else if (key > node.Key) node.Right = InsertRec(node.Right, key, val);
            else node.Value = val; // Update existing request
            return node;
        }

        // ===== SEARCH =====
        public ServiceRequestModel? Search(int key)
        {
            var node = _root;
            while (node != null)
            {
                if (key == node.Key) return node.Value;
                node = key < node.Key ? node.Left : node.Right;
            }
            return null;
        }

        // ===== REMOVE =====
        public void Remove(int key)
        {
            _root = RemoveRec(_root, key);
        }

        private BSTNode? RemoveRec(BSTNode? root, int key)
        {
            if (root == null) return null;

            if (key < root.Key)
                root.Left = RemoveRec(root.Left, key);
            else if (key > root.Key)
                root.Right = RemoveRec(root.Right, key);
            else
            {
                // Node with only one child or no child
                if (root.Left == null) return root.Right;
                if (root.Right == null) return root.Left;

                // Node with two children: find inorder successor
                BSTNode successor = MinValueNode(root.Right);
                root.Key = successor.Key;
                root.Value = successor.Value;
                root.Right = RemoveRec(root.Right, successor.Key);
            }
            return root;
        }

        private BSTNode MinValueNode(BSTNode node)
        {
            BSTNode current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        // ===== IN-ORDER TRAVERSAL =====
        public List<ServiceRequestModel> InOrder()
        {
            var list = new List<ServiceRequestModel>();
            Traverse(_root, list);
            return list;
        }

        private void Traverse(BSTNode? node, List<ServiceRequestModel> list)
        {
            if (node == null) return;
            Traverse(node.Left, list);
            list.Add(node.Value);
            Traverse(node.Right, list);
        }
    }
}
