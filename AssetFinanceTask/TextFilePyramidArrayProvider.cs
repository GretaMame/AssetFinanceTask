using System;
using System.IO;
using System.Linq;

namespace AssetFinanceTask
{
    public class TextFilePyramidArrayProvider : IPyramidArrayProvider
    {
        private static readonly string workingDirectory = Environment.CurrentDirectory;
        private string defaultFileDirectory = $"{Directory.GetParent(workingDirectory).Parent.Parent.FullName}/Input.txt";
        private string fileDirectory;

        public TextFilePyramidArrayProvider()
        {
        }

        public TextFilePyramidArrayProvider(string filePath)
        {
            this.fileDirectory = filePath;
        }

        public int[] GetPyramidArray()
        {
            var directory = fileDirectory ?? defaultFileDirectory;
            if (File.Exists(directory))
            {  
                string input = File.ReadAllText(directory);
                var arr = input.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                return arr.Select(int.Parse).ToArray();
            }

            return null;
        }
    }
}
