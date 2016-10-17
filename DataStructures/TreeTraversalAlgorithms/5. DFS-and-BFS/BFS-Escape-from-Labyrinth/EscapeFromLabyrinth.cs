using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Escape_from_Labyrinth;

public class EscapeFromLabyrinth
{
    private static int width = 9;

    private static int height = 7;

    private const char VisitedCell = 's';

    private static char[,] labyrinth =
        {
            { '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '-', '-', '-', '-', '*', '*', '-', '-' },
            { '*', '*', '-', '*', '-', '-', '-', '-', '*' },
            { '*', '-', '-', '*', '-', '*', '-', '*', '*' },
            { '*', 's', '*', '-', '-', '*', '-', '*', '*' },
            { '*', '*', '-', '-', '-', '-', '-', '-', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '-', '*' },
        };

    public static void Main()
    {
        ReadLabyrinth();
        string shortestPathToExit = FindShortestPathToExit();
        if (shortestPathToExit == null)
        {
            Console.WriteLine("No exit!");
        }
        else if (shortestPathToExit == "")
        {
            Console.WriteLine("Start is at the exit.");
        }
        else
        {
            Console.WriteLine("Shortest exit: " + shortestPathToExit);
        }
    }

    private static string FindShortestPathToExit()
    {
        var queue = new Queue<Point>();
        var startPosition = FindStartPosition();
        if (startPosition == null)
        {
            return null;
        }

        queue.Enqueue(startPosition);
        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();
            if (IsExit(currentCell))
            {
                return TracePathBack(currentCell);
            }

            TryDirection(queue, currentCell, "U", 0, -1);
            TryDirection(queue, currentCell, "R", 1, 0);
            TryDirection(queue, currentCell, "D", 0, 1);
            TryDirection(queue, currentCell, "L", -1, 0);
        }

        return null;
    }

    private static Point FindStartPosition()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (labyrinth[j, i] == VisitedCell)
                {
                    return new Point() { X = i, Y = j };
                }
            }
        }

        return null;
    }

    private static bool IsExit(Point currentCell)
    {
        bool IsOnBorderX = currentCell.X == width - 1 || currentCell.X == 0;
        bool IsOnBorderY = currentCell.Y == height - 1 || currentCell.Y == 0;

        return IsOnBorderX || IsOnBorderY;
    }

    private static void TryDirection(Queue<Point> queue, Point currentCell, string direction, int deltaX, int deltaY)
    {
        int nextX = currentCell.X + deltaX;
        int nextY = currentCell.Y + deltaY;
        if (nextX >= 0 && nextX < width && nextY >= 0 && nextY < height && labyrinth[nextY, nextX] == '-')
        {
            var nextCell = new Point()
            {
                PreviousPoint = currentCell,
                X = nextX,
                Y = nextY,
                Direction = direction
            };

            queue.Enqueue(nextCell);
        }
    }

    private static string TracePathBack(Point currentCell)
    {
        var path = new StringBuilder();
        while (currentCell.PreviousPoint != null)
        {
            path.Append(currentCell.Direction);
            currentCell = currentCell.PreviousPoint;
        }

        var pathReversed = new StringBuilder(path.Length);
        for (int i = 0; i < path.Length; i++)
        {
            pathReversed.Append(path[path.Length - 1 - i]);
        }

        return pathReversed.ToString();
    }

    private static void ReadLabyrinth()
    {
        width = int.Parse(Console.ReadLine());
        height = int.Parse(Console.ReadLine());
        labyrinth = new char[height, width];
        for(int i = 0; i < height; i++)
        {
            string row = Console.ReadLine();
            for (int j = 0; j < width; j++)
            {
                labyrinth[i, j] = row[j];
            }
        }
    }
}