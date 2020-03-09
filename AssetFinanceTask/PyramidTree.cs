using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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