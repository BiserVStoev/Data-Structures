using System;
using System.Collections.Generic;
using System.Linq;

namespace _02SweepAndPrune
{
    public class Engine
    {
        private List<Item> items;

        private Dictionary<string, int> indexesByNames; 

        private int move;

        public Engine()
        {
            this.items = new List<Item>();
            this.move = 1;
        }

        private void Add(string name, int x1, int y1)
        {
            this.items.Add(new Item(name, x1, y1));
        }

        private void SortByInsertionSort()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                int j = i;
                while (j > 0 && this.items[j].CompareTo(this.items[j - 1]) < 0)
                {
                    var prev = this.items[j];
                    this.items[j] = this.items[j - 1];
                    j--;
                    this.items[j] = prev;
                }
            }
        }

        private void CheckForCollisions()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                for (int j = i+1; j < this.items.Count; j++)
                {
                    if (this.items[i].X2 < this.items[j].X1)
                    {
                        break;                     
                    }
                    if (this.items[i].Y2 >= this.items[j].Y1 && this.items[i].Y1 <= this.items[j].Y2)
                    {
                        Console.WriteLine("({0}) {1} collides with {2}", this.move, this.items[i].Name, this.items[j].Name);
                    }
                }
            }
        }

        public void Run()
        {
            bool started = false;
            while (true)
            {
                string line = Console.ReadLine();
                string[] parameters = line.Split();
                if (parameters[0] == "start") started = true;

                if (!started)
                {
                    this.Add(parameters[1], int.Parse(parameters[2]), int.Parse(parameters[3]));
                }
                else
                {                   
                    switch (parameters[0])
                    {
                        case "tick":
                            continue;
                        case "move":
                            this.items.FirstOrDefault(x => x.Name == parameters[1]).Move(int.Parse(parameters[2]), int.Parse(parameters[3]));                         
                            break;
                    }
                    this.SortByInsertionSort();
                    this.CheckForCollisions();
                    move++;
                }              
            }
        }
    }
}
