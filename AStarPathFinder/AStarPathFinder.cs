using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinder
{
    public class AStarPathFinder
    {
        IDistanceAlgorithm DistanceAlgorithm;
        INeighborProvider NeighborProvider;
        IPathableProvider PathableProvider;

        //TODO: implement caching

        public AStarPathFinder(IPathableProvider pathableProvider, INeighborProvider neighborProvider, IDistanceAlgorithm distanceAlgorithm)
        {
            NeighborProvider = neighborProvider;
            PathableProvider = pathableProvider;
            DistanceAlgorithm = distanceAlgorithm;
        }

        public bool CanPass(IntPoint2D from, IntPoint2D to)
        {
            return GetBestPathLength(from, to) >= 0;
        }

        public double GetBestPathLength(IntPoint2D from, IntPoint2D to)
        {
            return GetBestPathLength(from, to, out _);
        }

        public double GetBestPathLength(IntPoint2D from, IntPoint2D to, out int visitedNodesCount)
        {
            var visitedNodes = new HashSet<Node>();
            var unvisitedNodes = new HashSet<Node>();
            unvisitedNodes.Add(new Node() { Point = from, PathLength = 0 });
            while (unvisitedNodes.Any())
            {
                Node bestNode = GetBestNode(unvisitedNodes, to);
                
                if (bestNode.Point.Equals(to))
                {
                    visitedNodesCount = visitedNodes.Count;
                    return bestNode.PathLength;
                }

                visitedNodes.Add(bestNode);
                unvisitedNodes.Remove(bestNode);

                AddNewNodes(bestNode, visitedNodes, unvisitedNodes);
            }

            //no path found
            visitedNodesCount = visitedNodes.Count;
            return -1;
        }

        private Node GetBestNode(HashSet<Node> unvisitedNodes, IntPoint2D to)
        {
            Node bestNode = null;
            double bestDistance = 0;
            foreach (var node in unvisitedNodes)
            {
                double newDistance = DistanceAlgorithm.GetDistance(to, node.Point);
                if (bestNode == null || node.PathLength + newDistance < bestNode.PathLength + bestDistance)
                {
                    bestNode = node;
                    bestDistance = newDistance;
                }
            }
            return bestNode;
        }

        private void AddNewNodes(Node startNode, HashSet<Node> visitedNodes, HashSet<Node> unvisitedNodes)
        {
            foreach (var neighbor in NeighborProvider.GetNeighbors(startNode.Point))
            {
                if (PathableProvider.IsPathable(neighbor))
                {
                    Node newNode = new Node()
                    {
                        Point = neighbor,
                        PathLength = startNode.PathLength + DistanceAlgorithm.GetDistance(neighbor, startNode.Point)
                    };

                    if (visitedNodes.Contains(newNode))
                    {
                        continue;
                    }

                    if (!unvisitedNodes.TryGetValue(newNode, out Node oldNode) || oldNode.PathLength >= newNode.PathLength)
                    {
                        unvisitedNodes.Add(newNode);
                    }
                }
            }
        }
    }
}
