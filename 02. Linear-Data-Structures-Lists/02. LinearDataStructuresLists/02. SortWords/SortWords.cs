namespace _02.SortWords
{
    using System;
    using System.Linq;

    public class SortWords
    {
        public static void Main()
        {
            var input = Console.ReadLine().Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries).OrderBy(x => x).ToList();
            Console.WriteLine(string.Join(" ", input));
        }
    }
}
