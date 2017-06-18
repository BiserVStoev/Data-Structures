using System;
using System.Collections.Generic;
using System.Linq;

namespace _08DistanceInLabyrinth
{
    public class DistanceInLabyrinth
    {
        private class Position
        {
            public Position(int row, int col, int step)
            {
                this.Row = row;
                this.Col = col;
                this.Step = step;
            }
            public int Row { get; set; }
            public int Col { get; set; }
            public int Step { get; set; }
        }

        public static void Main(string[] args)
        {

            //Sample case
            var matrix = ReturnTestInput();

            //Use this one if you want console input
            //Console.WriteLine("Write input matrix each line representing a line from the matrix in this format \"000x0x\" (an empty line signifies end of input):");
            //var matrix = ReadInput();

            var startingPosition = GetStartingPosition(matrix);
            BFS(matrix, startingPosition);
            Print(matrix);
        }

        private static void Print(List<List<string>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (matrix[i][j] == "0")
                    {
                        matrix[i][j] = "u";
                    }
                    Console.Write("{0,4}", matrix[i][j]);
                }
                Console.WriteLine();
            }
        }

        private static Position GetStartingPosition(List<List<string>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (matrix[i][j] == "*")
                    {
                        return new Position(i, j, 0);
                    }
                }
            }
            throw new ArgumentException("No starting position!");
        }

        private static List<List<string>> ReadInput()
        {
            List<List<string>> matrix = new List<List<string>>();
            string line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                var lineAsStringList = line.ToCharArray().Select(x => x.ToString()).ToList();
                matrix.Add(lineAsStringList);
                line = Console.ReadLine();
            }
            return matrix;
        }

        private static List<List<string>> ReturnTestInput()
        {
            return new List<List<string>>()
            {
                new List<string>() {"0","0","0","x","0","x" },
                new List<string>() {"0","x","0","x","0","x" },
                new List<string>() {"0","*","x","0","x","0" },
                new List<string>() {"0","x","0","0","0","0" },
                new List<string>() {"0","0","0","x","x","0" },
                new List<string>() {"0","0","0","x","0","x" }
            };
        }

        private static void BFS(List<List<string>> matrix, Position startPosition)
        {
            bool[,] visited = new bool[matrix.Count, matrix[0].Count];
            Queue<Position> queue = new Queue<Position>();
            AddNeighbours(matrix, queue, startPosition);

            while (queue.Count > 0)
            {
                Position currentPosition = queue.Dequeue();
                if (!visited[currentPosition.Row, currentPosition.Col])
                {
                    matrix[currentPosition.Row][currentPosition.Col] = currentPosition.Step.ToString();
                    AddNeighbours(matrix, queue, currentPosition);
                    visited[currentPosition.Row, currentPosition.Col] = true;
                }
            }

        }

        //could optimise with inverse checks and ORs
        private static void AddNeighbours(List<List<string>> matrix, Queue<Position> queue, Position current)
        {
            if (current.Row - 1 >= 0 && matrix[current.Row - 1][current.Col] != "x" && matrix[current.Row - 1][current.Col] != "*")
            {
                queue.Enqueue(new Position(current.Row - 1, current.Col, current.Step + 1));
            }
            if (current.Row + 1 < matrix.Count && matrix[current.Row + 1][current.Col] != "x" && matrix[current.Row + 1][current.Col] != "*")
            {
                queue.Enqueue(new Position(current.Row + 1, current.Col, current.Step + 1));
            }
            if (current.Col - 1 >= 0 && matrix[current.Row][current.Col - 1] != "x" && matrix[current.Row][current.Col - 1] != "*")
            {
                queue.Enqueue(new Position(current.Row, current.Col - 1, current.Step + 1));
            }
            if (current.Col + 1 < matrix[current.Row].Count && matrix[current.Row][current.Col + 1] != "x" && matrix[current.Row][current.Col + 1] != "*")
            {
                queue.Enqueue(new Position(current.Row, current.Col + 1, current.Step + 1));
            }
        }
    }
}
