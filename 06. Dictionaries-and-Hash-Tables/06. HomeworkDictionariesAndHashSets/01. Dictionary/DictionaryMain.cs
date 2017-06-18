namespace _01Dictionary
{
    using System;
    using System.Collections.Generic;

    public class DictionaryMain
    {
        public static void Main()
        {
            var dictionary = new HashTable<string, int>();
            dictionary.Add("test", 1);
            dictionary.Add("test2", 20);
            Console.WriteLine(dictionary["test2"]);
        }
    }
}
