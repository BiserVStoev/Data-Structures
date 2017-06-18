namespace _01.ReverseNumbersWithStack
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReverseNumbersWithStack
    {
        static void Main()
        {
            var numbersHolder = new Stack<int>();

            var input = Console.ReadLine().Split(' ').Select(int.Parse);
            foreach (var number in input)
            {
                numbersHolder.Push(number);
            }

            while (numbersHolder.Count > 0)
            {
                Console.Write(numbersHolder.Pop() + " ");
            }
        }
    }
}
