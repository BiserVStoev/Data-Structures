using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SequenceNM
{
    public class SequenceNtoM
    {
        public static void Main(string[] args)
        {
            int[] parameters = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int start = parameters[0];
            int target = parameters[1];
            if (start > target)
            {
                Console.WriteLine("(no solution)");
            }
            Queue<Item> sequence = new Queue<Item>();
            sequence.Enqueue(new Item(start));

            while (sequence.Count > 0)
            {
                Item current = sequence.Dequeue();
                if (current.Value < target)
                {
                    Item add1 = new Item(current.Value + 1,current);
                    Item add2 = new Item(current.Value + 2, current);
                    Item mult2 = new Item(current.Value * 2, current);
                    sequence.Enqueue(add1);
                    sequence.Enqueue(add2);
                    sequence.Enqueue(mult2);
                }
                else if (current.Value == target)
                {
                    Print(current);
                    break;
                }
            }

        }

        private static void Print(Item current)
        {
            Stack<string> path = new Stack<string>();
            while (current != null)
            {
                path.Push(current.Value.ToString());
                path.Push("->");
                current = current.Previous;
            }
            path.Pop();
            Console.WriteLine(string.Join(" ",path));
        }
    }
}
