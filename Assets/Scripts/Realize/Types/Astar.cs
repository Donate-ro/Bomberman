using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts
{
    static class Astar
    {
        public static List<Point> RunAstar(Point start, Point goal, bool[,] field)
        {
            List<Node> closedSet = new List<Node>();
            List<Node> openSet = new List<Node>();
            Node cameFrom = null, current;
            openSet.Add(new Node(start, cameFrom, 0, Node.GetPathLength(start, goal)));
            while (openSet.Count != 0)
            {
                current = openSet.OrderBy(x => x.ApproximatePathLength + x.PathLengthFromStart).First();
                if (current.Position == goal)
                    return ReconstructPath(current);
                openSet.Remove(current);
                closedSet.Add(current);
                foreach (var neighbour in Node.GetNeighbours(current, goal, field))
                {
                    if (closedSet.Count(node => node.Position == neighbour.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbour.Position);
                    if (openNode == null)
                        openSet.Add(neighbour);
                    else
                      if (openNode.PathLengthFromStart > neighbour.PathLengthFromStart)
                    {
                        openNode.CameFrom = current;
                        openNode.PathLengthFromStart = neighbour.PathLengthFromStart;
                    }
                }
            }
            return null;
        }

        static List<Point> ReconstructPath(Node current)
        {
            List<Point> result = new List<Point>();
            Node currentNode = current;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }

    }
}