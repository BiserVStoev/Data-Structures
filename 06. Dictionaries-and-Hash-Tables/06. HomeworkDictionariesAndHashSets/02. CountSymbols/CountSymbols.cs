namespace _02CountSymbols
{
    using System;
    using System.Linq;

    public class CountSymbols
    {
        public static void Main(string[] args)
        {
            string line = Console.ReadLine();
            var symbols = new HashTable<char, int>();
            for (int i = 0; i < line.Length; i++)
            {
                if (!symbols.ContainsKey(line[i]))
                {
                    symbols.Add(line[i],0);
                }
                symbols[line[i]]++;
            }
            foreach (var symbol in symbols.OrderBy(x=>x.Key))
            {
                Console.WriteLine("{0} -> {1} time/s", symbol.Key, symbol.Value);
            }
        }
    }
}
