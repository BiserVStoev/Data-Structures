namespace _01.SumAndAverage
{
    using System;
    using System.Linq;

    public class SumAndAverage
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            Console.WriteLine(
                "Sum={0}; Average={1}",
                numbers.Count == 0 ? 0 : numbers.Sum(),
                numbers.Count == 0 ? 0 : numbers.Average());
        }
    }
}
