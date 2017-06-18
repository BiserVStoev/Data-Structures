using System;

namespace _02SweepAndPrune
{
    public class Item : IComparable<Item>
    {
        private const int DefaultWidth = 10;
        private const int DefaultHeight = 10;

        public Item(string name, int x, int y)
        {
            this.X1 = x;
            this.Y1 = y;
            this.Name = name;
        }

        public string Name { get; set; }

        public int X1 { get; set; }

        public int Y1 { get; set; }

        public int X2
        {
            get { return this.X1 + DefaultWidth; }
        }

        public int Y2
        {
            get { return this.Y1 + DefaultHeight; }
        }

        public void Move(int x1, int y1)
        {
            this.X1 = x1;
            this.Y1 = y1;
        }

        public int CompareTo(Item other)
        {
            return this.X1.CompareTo(other.X1);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, X1: {1}, X2: {2}, Y1: {3}, Y2: {4}", this.Name, this.X1, this.X2, this.Y1,
                this.Y2);
        }
    }
}
