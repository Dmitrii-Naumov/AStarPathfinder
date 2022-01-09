using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinder
{
    public static class AStarPathFinder
    {
        public static double GetBestPathLength(IntPoint2D from, IntPoint2D to, bool[,] pathingGrid)
        {
            return GetBestPathLength(from, to, pathingGrid, out _);
        }

        public static double GetBestPathLength(IntPoint2D from, IntPoint2D to, bool[,] pathingGrid, out int visitedNodesCount)
        {
            var visitedNodes = new HashSet<Node>();
            var unvisitedNodes = new HashSet<Node>();
            unvisitedNodes.Add(new Node() { Point = from, PathLength = 0 });
            while (unvisitedNodes.Count > 0)
            {
                Node bestNode = GetBestNode(unvisitedNodes, to);
                visitedNodes.Add(bestNode);
                if (bestNode.Point.Equals(to))
                {
                    visitedNodesCount = visitedNodes.Count;
                    return bestNode.PathLength;
                }
                unvisitedNodes.Remove(bestNode);
                AddNewNodes(bestNode, pathingGrid, visitedNodes, unvisitedNodes);
            }
            //for debugging
            var result = VisualizeNodes(visitedNodes, pathingGrid.GetLength(0), pathingGrid.GetLength(1));

            //no path found
            visitedNodesCount = visitedNodes.Count;
            return -1;
        }

        private static float[,] VisualizeNodes(HashSet<Node> nodes, int widht, int height)
        {
            float[,] result = new float[widht, height];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = -1;
                }
            }
            foreach (var node in nodes)
            {
                result[node.Point.X, node.Point.Y] = (float)node.PathLength;
            }
            return result;
        }

        private static Node GetBestNode(HashSet<Node> unvisitedNodes, IntPoint2D to)
        {
            Node bestNode = null;
            double bestDistance = 0;
            foreach (var node in unvisitedNodes)
            {
                double newDistance = Math.Sqrt(SquaredDistance(to, node));
                if (bestNode == null || node.PathLength + newDistance < bestNode.PathLength + bestDistance)
                {
                    bestNode = node;
                    bestDistance = newDistance;
                }
            }
            return bestNode;
        }

        private static double SquaredDistance(IntPoint2D to, Node bestNode)
        {
            return Math.Pow(bestNode.Point.X - to.X, 2) + Math.Pow(bestNode.Point.Y - to.Y, 2);
        }

        private static void AddNewNodes(Node startNode, bool[,] map, HashSet<Node> visitedNodes, HashSet<Node> unvisitedNodes)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Node newNode = new Node()
                    {
                        Point = new IntPoint2D() { X = startNode.Point.X + x, Y = startNode.Point.Y + y },
                        PathLength = startNode.PathLength + Math.Sqrt(x * x + y * y)
                    };
                    if (newNode.Point.X < 0 || newNode.Point.Y < 0 || newNode.Point.X >= map.GetLength(0) || newNode.Point.Y >= map.GetLength(1))
                    {
                        //skip
                    }
                    else if (IsPathable(newNode, map))
                    {
                        if (visitedNodes.Contains(newNode) ||
                            (unvisitedNodes.TryGetValue(newNode, out Node oldNode) && oldNode.PathLength < newNode.PathLength))
                        {
                            // skip 
                        }
                        else
                        {
                            unvisitedNodes.Add(newNode);
                        }
                    }
                }
            }
        }
        private static bool IsPathable(Node node, bool[,] map)
        {
            return IsPathable(node.Point, map);
        }
        private static bool IsPathable(IntPoint2D node, bool[,] map)
        {
            return map[(int)node.X, (int)node.Y];
        }
    }
}
