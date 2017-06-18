namespace _03Phonebook
{
    using System;

    public class Phonebook
    {
        public static void Main(string[] args)
        {
            var phonebook = new HashTable<string, string>();

            string line = Console.ReadLine();
            while (line != "search")
            {
                string[] parameters = line.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                string name = parameters[0];
                string phone = parameters[1];

                phonebook.AddOrReplace(name, phone);
                line = Console.ReadLine();
            }

            line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {

                if (phonebook.ContainsKey(line))
                {
                    Console.WriteLine("{0} -> {1}", line, phonebook[line]);
                }
                else
                {
                    Console.WriteLine("Contact {0} does not exist.", line);
                }
                line = Console.ReadLine();
            }
        }
    }
}
