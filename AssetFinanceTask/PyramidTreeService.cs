using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetFinanceTask
{
    public class PyramidTreeService
    {
        private readonly IPyramidArrayProvider arrayProvider;
        private static List<List<Node>> Paths;
        private int treeDepth;

        public PyramidTreeService(IPyramidArrayProvider arrayProvider)
        {
            this.arrayProvider = arrayProvider;
            Paths = new List<List<Node>>();
        }

        public PyramidTree CreateTree()
        {
            var input = arrayProvider.GetPyramidArray();

            PyramidTree tree = new PyramidTree();

            for (int i = 0; i < input?.Length; i++)
            {
                tree.AddNode(input[i]);
            }

            return tree;
        }

        public List<List<int>> FindEvenOddPaths(PyramidTree tree)
        {
            if (tree == null || tree.Root == null)
            {
                return null;
            }

            Paths = new List<List<Node>>();
            treeDepth = tree.GetDepth();

            FindEvenOddPaths(tree.Root, new List<Node>());

            return Paths.Select(path => path.Select(node => node.Value).ToList()).ToList();
        }

        public void PrintAllPaths(List<List<int>> paths)
        {
            if (paths?.Any() == true)
            {
                foreach (var path in paths)
                {
                    path.ForEach(value => Console.Write(value + " "));
                    Console.WriteLine($"Sum: {path.Sum(value => value)}");
                }                
            }
            else
            {
                Console.WriteLine("There were no even/odd paths found in pyramid");
            }
        }

        public void PrintLongestPath(List<List<int>> paths)
        {
            if (paths?.Any() == true)
            {
                var longestPath = paths.OrderByDescending(q => q.Sum()).FirstOrDefault();
                Console.Write($"Longest path sum: {longestPath.Sum()} {Environment.NewLine}Longest path: ");
                longestPath.ForEach(value => Console.Write(value + " "));
            }
            else
            {
                Console.WriteLine("There were no even/odd paths found in pyramid");
            }
        }

        private void FindEvenOddPaths(Node node, List<Node> path)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left == null && node.Right == null)
            {
                path.Add(node);
                Paths.Add(new List<Node>(path));
                path.RemoveAt(path.Count - 1);

                return;
            }

            var leftMeetsConditions = LeftNodeMeetsConditions(node);
            var rightMeetsConditions = RightNodeMeetsConditions(node);

            if (leftMeetsConditions || rightMeetsConditions)
            {
                path.Add(node);

                if (leftMeetsConditions)
                {
                    FindEvenOddPaths(node.Left, path);
                }

                if (rightMeetsConditions)
                {
                    FindEvenOddPaths(node.Right, path);
                }

                path.RemoveAt(path.Count - 1);
            }
        }

        private bool LeftNodeMeetsConditions(Node node)
        {
            return node != null && node.Left != null && node.Value % 2 != node.Left.Value % 2;
        }

        private bool RightNodeMeetsConditions(Node node)
        {
            return node != null && node.Right != null && node.Value % 2 != node.Right.Value % 2;
        }
    }
}