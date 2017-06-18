namespace _01.PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PlayWithTrees
    {
        private static readonly Dictionary<int, Tree<int>> NodeByValue = new Dictionary<int, Tree<int>>();

        public static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < nodesCount - 1; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                int parentValue = int.Parse(edge[0]);
                Tree<int> parentNode = GetTreeNodeByValue(parentValue);
                int childValue = int.Parse(edge[1]);
                Tree<int> childNode = GetTreeNodeByValue(childValue);
                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }
            
            Console.WriteLine("Root node: " + FindRootNode().Value);

            var leafs = FindAllLeafs();
            List<int> leafsValues = new List<int>();
            foreach (var node in leafs)
            {
                leafsValues.Add(node.Value);
            }

            Console.Write("Leaf nodes: ");
            Console.WriteLine(string.Join(", ", leafsValues));

            var middleNodes = FindMiddleNodes();
            List<int> middleNodesValues = new List<int>();
            foreach (var node in middleNodes)
            {
                middleNodesValues.Add(node.Value);
            }

            middleNodesValues.Sort();
            Console.Write("Middle nodes: ");
            Console.WriteLine(string.Join(", ", middleNodesValues));
        }

        public static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!NodeByValue.ContainsKey(value))
            {
                NodeByValue[value] = new Tree<int>(value);
            }

            return NodeByValue[value];
        }

        public static Tree<int> FindRootNode()
        {
            var rootNode = NodeByValue.Values.FirstOrDefault(node => node.Parent == null);

            return rootNode;
        }

        public static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = NodeByValue
                .Values
                .Where(node => node.Children.Count > 0 && node.Parent != null)
                .ToList();

            return middleNodes;
        }

        public static List<Tree<int>> FindAllLeafs()
        {
            var leafs = NodeByValue.Values
                .Where(n => n.Children.Count == 0)
                .OrderBy(n => n.Value)
                .ToList();

            return leafs;
        }

        public static string FindLongestPath(Tree<int> root)
        {
            int length = 0;

            return string.Empty;
        }
    }
}
