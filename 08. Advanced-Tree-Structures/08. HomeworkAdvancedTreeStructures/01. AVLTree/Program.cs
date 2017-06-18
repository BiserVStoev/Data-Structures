namespace _01AVLTree
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            int[] values = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var tree = new AvlTree<int>();
            foreach (var value in values)
            {
                tree.Add(value);
            }
        }
    }
}
