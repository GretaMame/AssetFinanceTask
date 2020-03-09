using System;
using System.Linq;

namespace AssetFinanceTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Consts.Input.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            var input = arr.Select(int.Parse).ToArray();

            PyramidTree Tree = new PyramidTree();

            for (int i = 0; i < input.Length; i++)
            {
                Tree.AddNode(input[i]);
            }            
        }
    }
}
