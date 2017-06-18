namespace _03RideTheHorse
{
    using System;
    using System.Collections.Generic;

    public class RideTheHorse
    {
        private static int[,] matrix;

        public static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            int startRow = int.Parse(Console.ReadLine());
            int startCol = int.Parse(Console.ReadLine());
            matrix = new int[rows, cols];
            matrix[startRow, startCol] = 1;

            var startCell = new Cell(startRow, startCol);
            Bfs(startCell);
        
            int targetCol = cols / 2;
            Console.WriteLine("Column #{0}: ", targetCol);
            for (int row = 0; row < rows; row++)
            {
                Console.WriteLine(matrix[row, targetCol]);
            }

           
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static void Bfs(Cell startCell)
        {
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(startCell);

            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();
                AddToQueue(queue, currentCell, -1, -2);
                AddToQueue(queue, currentCell, -2, -1);
                AddToQueue(queue, currentCell, -2, 1);
                AddToQueue(queue, currentCell, -1, 2);
                AddToQueue(queue, currentCell, 1, -2);
                AddToQueue(queue, currentCell, 2, -1);
                AddToQueue(queue, currentCell, 2, 1);
                AddToQueue(queue, currentCell, 1, 2);
            }
        }

        private static void AddToQueue(Queue<Cell> queue, Cell currentCell, int rowChange, int colChange)
        {
            if (IsInMatrix(currentCell, rowChange, colChange))
            {
                queue.Enqueue(new Cell(currentCell.Row + rowChange, currentCell.Col + colChange));
                matrix[currentCell.Row + rowChange, currentCell.Col + colChange] = matrix[currentCell.Row, currentCell.Col] + 1;
            }
        }

        private static bool IsInMatrix(Cell currentCell, int rowChange, int colChange)
        {
            var rowOutside = currentCell.Row + rowChange < 0 || currentCell.Row + rowChange >= matrix.GetLength(0);
            if (rowOutside)
            {
                return false;
            }
            var colOutside = currentCell.Col + colChange < 0 || currentCell.Col + colChange >= matrix.GetLength(1);
            if (colOutside)
            {
                return false;
            }
            var visited = matrix[currentCell.Row + rowChange, currentCell.Col + colChange] != 0;
            if (visited)
            {
                return false;
            }

            return true;
        }
    }
}
