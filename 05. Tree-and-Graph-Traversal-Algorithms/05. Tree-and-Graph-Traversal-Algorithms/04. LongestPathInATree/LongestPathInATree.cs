namespace _04.LongestPathInATree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestPathInATree
    {
        private static int totalbestSum = int.MinValue;

        public static void Main()
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            var tree = new Dictionary<int, Node>();

            for (int i = 0; i < edges; i++)
            {
                var data = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var node = data[0];
                var child = data[1];

                if (!tree.ContainsKey(node))
                {
                    tree[node] = new Node(node);
                }

                if (!tree.ContainsKey(child))
                {
                    tree[child] = new Node(child);
                }

                tree[child].HasParent = true;
                tree[node].Children.Add(tree[child]);
            }

            Node root = tree.Single(n => n.Value.HasParent == false).Value;
            FindBestSumOfLeafsWithDFS(root);

            Console.WriteLine(totalbestSum);
        }

        private static void FindBestSumOfLeafsWithDFS(Node node)
        {
            if (node.Children.Count == 0)
            {
                return;
            }

            var bestSum = int.MinValue;
            var secondBestSum = int.MinValue;
            foreach (var child in node.Children)
            {
                FindBestSumOfLeafsWithDFS(child);
                var childSum = child.BestSum + child.Value;
                if (childSum> bestSum)
                {
                    secondBestSum = bestSum;
                    bestSum = childSum;
                }
                else if (childSum > secondBestSum)
                {
                    secondBestSum = childSum;
                }
            }

            node.BestSum = bestSum;
            node.SecondBestSum = secondBestSum;

            if (bestSum != int.MinValue && secondBestSum != int.MinValue)
            {
                int nodeTotalBestSum = node.BestSum + node.SecondBestSum + node.Value;
                if ( nodeTotalBestSum > totalbestSum)
                {
                    totalbestSum = nodeTotalBestSum;
                }
            }
        }
    }
}
