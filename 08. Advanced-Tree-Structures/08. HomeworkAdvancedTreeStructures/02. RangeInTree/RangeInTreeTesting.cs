using System;
using System.Collections.Generic;
using System.Linq;

namespace _02RangeInTree
{
    public class RangeInTreeTesting
    {
        public static void Main(string[] args)
        {
            int[] values = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] rangeTokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var lowerBound = rangeTokens[0];
            var upperBound = rangeTokens[1];
            var tree = new AvlTree<int>();
            foreach (var value in values)
            {
                tree.Add(value);
            }
            var range = tree.FindRange(lowerBound, upperBound);
            Console.WriteLine(string.Join(", ", range));
        }
    }
}
