using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    class Node
    {
        public Point Position { get; set; }
        public Node CameFrom { get; set; }
        public int PathLengthFromStart { get; set; }
        public int ApproximatePathLength { get; set; }

        public Node(Point position, Node cameFrom, int pathLengthFromStart, int approximatePathLength)
        {
            Position = position;
            PathLengthFromStart = pathLengthFromStart;
            CameFrom = cameFrom;
            ApproximatePathLength = approximatePathLength;
        }

        public static List<Node> GetNeighbours(Node pathNode, Point goal, bool[,] field)
        {
            var result = new List<Node>();

            Point[] neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);

            foreach (var point in neighbourPoints)
            {
                if (point.X < 0 || point.X >= field.GetLength(0))
                    continue;
                if (point.Y < 0 || point.Y >= field.GetLength(1))
                    continue;
                if (field[point.X, point.Y])
                    continue;
                var neighbourNode = new Node(point, pathNode, pathNode.PathLengthFromStart + 1,
                    GetPathLength(point, goal));
                result.Add(neighbourNode);
            }
            return result;
        }

        public static int GetPathLength(Point begin, Point end)
        {
            return Math.Abs(begin.X - end.X) + Math.Abs(begin.Y - end.Y);
        }
    }
}