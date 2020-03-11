using System;

namespace AssetFinanceTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new PyramidTreeService(new StaticPyramidArrayProvider());

            PyramidTree tree = service.CreateTree();
            //tree.Print();
            //Console.WriteLine();

            var paths = service.FindEvenOddPaths(tree);
            service.PrintAllPaths(paths);
            service.PrintLongestPath(paths);

            Console.ReadLine();
        }
    }
}
