namespace _05.CountOfOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CountOfOccurences
    {
        public static void Main()
        {
            var input = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var result = new SortedDictionary<int, int>();

            foreach (var number in input)
            {
                if (!result.ContainsKey(number))
                {
                    result[number] = 1;
                }
                else
                {
                    result[number]++;
                }
            }

            foreach (var element in result)
            {
                Console.WriteLine("{0} -> {1} times", element.Key, element.Value);
            }
        }
    }
}
