using System;

namespace _06ReversedList
{
    public class ReversedListTesting
    {
        public static void Main()
        {
            var reversedList = new ReversedList<int>();
            reversedList.Add(1);
            reversedList.Add(2);
            reversedList.Add(3);
            reversedList.Add(4);
            reversedList.Add(5);
            reversedList.Add(6);

            Console.WriteLine(string.Join(", ", reversedList));
            reversedList.Remove(2);
            Console.WriteLine(string.Join(", ",reversedList));
            reversedList[4] = 13;
            Console.WriteLine(string.Join(", ", reversedList));
            reversedList.Add(7);
            Console.WriteLine(string.Join(", ", reversedList));
            Console.WriteLine(reversedList[5]);
            // index out of range exceptions
            //reversedList[7] = 1;
            //Console.WriteLine(reversedList[-1]);
            //reversedList.Remove(35);
            foreach (var item in reversedList)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

            var reversedListStrings = new ReversedList<string>();
            reversedListStrings.Add("apple");
            reversedListStrings.Add("banana");
            reversedListStrings.Add("orange");
            reversedListStrings.Add("pear");
            reversedListStrings.Add("kiwi");
            reversedListStrings.Add("apricot");
            Console.WriteLine();

            Console.WriteLine(string.Join(", ", reversedListStrings));
            reversedListStrings.Remove(2);
            Console.WriteLine(string.Join(", ", reversedListStrings));
            reversedListStrings[3] = "watermelon";
            Console.WriteLine(string.Join(", ", reversedListStrings));
            reversedListStrings.Add("pineapple");
            Console.WriteLine(string.Join(", ", reversedListStrings));
            Console.WriteLine(reversedListStrings[5]);
            foreach (var item in reversedListStrings)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();

        }
    }
}
