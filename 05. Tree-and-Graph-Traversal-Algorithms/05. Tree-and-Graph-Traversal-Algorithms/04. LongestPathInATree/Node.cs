namespace _04.LongestPathInATree
{
    using System.Collections.Generic;

    public class Node
    {
        public Node(int value)
        {
            this.Value = value;
            this.Children = new List<Node>();
        }

        public int Value { get; set; }

        public List<Node> Children { get; set; } 

        public int BestSum { get; set; }

        public int SecondBestSum { get; set; }

        public bool HasParent { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
