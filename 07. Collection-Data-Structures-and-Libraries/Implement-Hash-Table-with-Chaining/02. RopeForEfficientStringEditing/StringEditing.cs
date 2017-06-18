using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace _02.RopeForEfficientStringEditing
{
    public class StringEditing
    {
        public static void Main(string[] args)
        {
            var data = new BigList<char>();

            string input = Console.ReadLine();

            while (input != "Exit")
            {
                var inputInfo = input.Split(' ');
                var command = inputInfo[0].Trim();
                var dataToManipulate = inputInfo[1].Trim().ToCharArray();

                switch (command)
                {
                    case "INSERT":
                        data.AddRangeToFront(dataToManipulate);
                        Console.WriteLine("OK");
                        break;
                    case "APPEND":
                        data.AddRange(dataToManipulate);
                        Console.WriteLine("OK");
                        break;
                    case "DELETE":
                        var index = int.Parse(inputInfo[2].Trim());
                        var count = int.Parse(inputInfo[3].Trim());

                        if (index < 0 || index >= data.Count || index + count >= data.Count)
                        {
                            Console.WriteLine("ERROR");
                            return;
                        }
                        data.RemoveRange(index, count);
                        Console.WriteLine("OK");
                        break;
                    case "Print":
                        Console.WriteLine(string.Join("", data));
                        break;
                    default:
                        break;
                        
                }
            }
        }
    }
}
