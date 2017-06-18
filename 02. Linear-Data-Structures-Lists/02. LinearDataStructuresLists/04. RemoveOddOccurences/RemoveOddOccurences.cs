namespace _04.RemoveOddOccurences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RemoveOddOccurences
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
                if (element.Value % 2 == 1)
                {
                    input.RemoveAll(num => num == element.Key);
                }
            }

            Console.WriteLine(string.Join(" ", input));
        }
    }
}
