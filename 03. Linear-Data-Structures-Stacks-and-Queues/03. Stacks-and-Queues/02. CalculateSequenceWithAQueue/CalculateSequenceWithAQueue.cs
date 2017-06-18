namespace _02.CalculateSequenceWithAQueue
{
    using System;
    using System.Collections.Generic;

    public class CalculateSequenceWithAQueue
    {
        public static void Main()
        {
            int firstNum = int.Parse(Console.ReadLine());

            var numbersHolder = new Queue<int>();
            numbersHolder.Enqueue(firstNum);

            int counter = 1;
            while (counter <= 50)
            {
                int poppedNumber = numbersHolder.Dequeue();
                if (counter != 50)
                {
                    Console.Write(poppedNumber + ", ");
                }
                else
                {
                    Console.WriteLine(poppedNumber);
                }

                numbersHolder.Enqueue(poppedNumber + 1);
                numbersHolder.Enqueue((2 * poppedNumber) + 1);
                numbersHolder.Enqueue(poppedNumber + 2);

                counter++;
            }

        }
    }
}
