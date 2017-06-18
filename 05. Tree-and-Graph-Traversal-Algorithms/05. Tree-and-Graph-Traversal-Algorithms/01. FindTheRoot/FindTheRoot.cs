namespace _01.FindTheRoot
{
    using System;
    using System.Linq;

    public class FindTheRoot
    {
        public static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            
            bool[] hasParent = new bool[nodes];

            for (int i = 0; i < edges; i++)
            {
                var edgeData = Console.ReadLine().Split(' ');
                
                int child = int.Parse(edgeData[1]);

                hasParent[child] = true;
            }

            if (hasParent.Any(b => b == false))
            {
                if (hasParent.Where(b => b == false).Count() > 1)
                {
                    Console.WriteLine("Multiple root nodes!");
                }
                else
                {
                    for (int i = 0; i < edges; i++)
                    {
                        if (!hasParent[i])
                        {
                            Console.WriteLine(i);
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No root!");
            }
        }
    }
}
