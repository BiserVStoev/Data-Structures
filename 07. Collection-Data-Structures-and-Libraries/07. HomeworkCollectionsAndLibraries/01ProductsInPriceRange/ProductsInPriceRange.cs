using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01ProductsInPriceRange
{
    public class ProductsInPriceRange
    {
        public static void Main(string[] args)
        {
            var products = new OrderedMultiDictionary<double, string>(true);
            // 500 000 Example
            //Example500000Elements(products);

            ReadInput(products);
        }

        private static void ReadInput(OrderedMultiDictionary<double,string> products)
        {
            int elements = int.Parse(Console.ReadLine());

            for (int i = 0; i < elements; i++)
            {

                string[] parameters = Console.ReadLine().Split();
                string name = parameters[0];
                double price = double.Parse(parameters[1]);
                products.Add(price, name);
            }

            string line = Console.ReadLine();
            double[] ranges = line.Split().Select(double.Parse).ToArray();
            double lowerBound = ranges[0];
            double upperBound = ranges[1];

            var range = products.Range(lowerBound, true, upperBound, true).Take(20);
            foreach (var pair in range)
            {
                Console.WriteLine("{0} {1}", pair.Key, string.Join(", ", pair.Value));
            }
        }

        private static void Example500000Elements(OrderedMultiDictionary<double,string> products )
        {
            Random rnd = new Random();
            int products500000 = 500000;
            string[] prodcutNames = new[] { "apples", "bananas", "milk", "cheese", "water", "beer", "muffin" };

            for (int i = 0; i < products500000; i++)
            {
                int index = i % prodcutNames.Length;
                string name = prodcutNames[index];
                products.Add(rnd.NextDouble() * 10, name);
            }

            for (int i = 0; i < 10000; i++)
            {
                var test = products.Range(5, true, 10, true).Take(20);
                foreach (var pair in test)
                {
                    Console.WriteLine("{0} {1}", pair.Key, string.Join(", ", pair.Value));
                }
                Console.WriteLine();
            }
        }
    }
}
