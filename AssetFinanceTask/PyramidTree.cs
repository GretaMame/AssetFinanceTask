using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AssetFinanceTask
{
    [DebuggerDisplay("{Value}")]
    public class Node
    {
        public int Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
        
        public Node(int value)
        {
            Value = value;
        }
    }

    public class PyramidTree
    {
        public Node Root { get; protected set; }

        private Node lastAddedNode;

        private int level;

        private int nodesInLevel;

        private Queue<Node> Queue = new Queue<Node>();

        public PyramidTree()
        {
        }

        public PyramidTree(int value)
        {
            Root = CreateNode(value);
            level = 1;
        }
        
        public void AddNode(int value)
        {
            if (nodesInLevel == level)
            {
                level++;
                nodesInLevel = 0;
            }

            if (Root == null)
            {
                Root = CreateNode(value);               
            }
            else
            {
                var node = Queue.Peek();
                if (node.Left == null)
                {
                    if (nodesInLevel == 0)
                    {
                        node.Left = CreateNode(value);
                    }
                    else
                    {
                        node.Left = lastAddedNode;
                        node.Right = CreateNode(value);
                        Queue.Dequeue();
                    }
                }
                else if (node.Right == null)
                {
                    node.Right = CreateNode(value);
                    Queue.Dequeue();
                }
            }
        }

        public int GetDepth()
        {
            return GetDepth(Root);
        }

        public void Print()
        {
            Print(Root);
        }

        private void Print(Node root)
        {
            if (root == null)
                return;

            Queue<Node> q = new Queue<Node>();

            q.Enqueue(root);

            while (true)
            {
                int nodeCount = q.Count;
                if (nodeCount == 0)
                {
                    break;
                }

                while (nodeCount > 0)
                {
                    Node node = q.Dequeue();
                    Console.Write(node.Value + " ");

                    if (node.Left != null)
                    {
                        q.Enqueue(node.Left);
                    }
                    if (node.Right != null)
                    {
                        q.Enqueue(node.Right);
                    }
                    nodeCount--;
                }
                Console.WriteLine();
            }
        }

        private int GetDepth(Node node)
        {
            if (node == null)
                return 0;
            else
            {
                int lDepth = GetDepth(node.Left);

                // Note: since this tree is always full there is not really a need to traverse the right subtree
                int rDepth = GetDepth(node.Right);

                if (lDepth > rDepth)
                    return (lDepth + 1);
                else
                    return (rDepth + 1);
            }
        }

        private Node CreateNode(int value)
        {
            var node = new Node(value);
            Queue.Enqueue(node);
            lastAddedNode = node;
            nodesInLevel++;

            return node;
        }
    }
}