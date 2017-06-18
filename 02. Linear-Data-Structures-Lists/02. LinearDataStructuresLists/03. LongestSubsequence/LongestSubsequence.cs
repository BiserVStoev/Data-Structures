using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.LongestSubsequence
{
    public class LongestSubsequence
    {
        public static void Main()
        {
            var nums = Console.ReadLine()
                   .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(int.Parse)
                   .ToList();

            var subsequence = FindLongestSubsequence(nums);

            Console.WriteLine(string.Join(" ", subsequence));

        }

        public static List<int> FindLongestSubsequence(List<int> numbers)
        {
            var longestSequence = int.MinValue;
            var startIndex = int.MinValue;

            for (int i = 0; i < numbers.Count; i++)
            {
                var currentNum = numbers[i];
                var currentLongestCount = 1;
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[j] != currentNum)
                    {
                        break;
                    }

                    currentLongestCount++;
                }

                if (currentLongestCount > longestSequence)
                {
                    longestSequence = currentLongestCount;
                    startIndex = i;
                }
            }

            var result = new List<int>(
                numbers
                    .Skip(startIndex)
                    .Take(longestSequence));

            return result;
        }
    }
}
