namespace _02RoundDance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RoundDance
    {
        private static List<int> longestPath;

        private static int totalDepth = 1;

        public static void Main(string[] args)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            Dictionary<int, int> parents = new Dictionary<int, int>();
            int edges = int.Parse(Console.ReadLine());
            int root = int.Parse(Console.ReadLine());

            for (int i = 0; i < edges; i++)
            {
                int[] parameters = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var parent = parameters[0];
                var child = parameters[1];
                if (!graph.ContainsKey(parent))
                {
                    graph.Add(parent, new List<int>());
                }
                if (!graph.ContainsKey(child))
                {
                    graph.Add(child, new List<int>());
                }
                graph[parent].Add(child);
                graph[child].Add(parent);
            }

            List<int> path = new List<int>();
            HashSet<int> visited = new HashSet<int>();
            var maxDepth = Dfs(graph, visited, path, root, 1);
            Console.WriteLine("Max depth: {0}", maxDepth);
            Console.WriteLine(string.Join(" -> ",longestPath));

        }

        private static int Dfs(Dictionary<int, List<int>> graph, HashSet<int> visited, List<int> path, int current, int depth)
        {
            visited.Add(current);
            path.Add(current);
            foreach (var child in graph[current])
            {
                if (!visited.Contains(child))
                {
                    Dfs(graph, visited, path, child, depth + 1);
                }
            }
            if(depth > totalDepth)
            {
                longestPath = new List<int>(path);
                totalDepth = depth;
            }
            path.RemoveAt(path.Count - 1);
            return totalDepth;
        }
    }
}
