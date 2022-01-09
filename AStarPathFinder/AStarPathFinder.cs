using AStarPathFinder.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            var visitedNodes = new Dictionary<IntPoint2D, double>();
            var unvisitedNodes = new Dictionary<IntPoint2D, double>();
            unvisitedNodes.Add(from, 0);
            while (unvisitedNodes.Any())
            {
                IntPoint2D bestNode = GetBestNode(unvisitedNodes, to).Value;
                double bestPathLength = unvisitedNodes[bestNode];
                if (bestNode.Equals(to))
                {
                    visitedNodesCount = visitedNodes.Count;
                    return bestPathLength;
                }

                visitedNodes.Add(bestNode, bestPathLength);
                unvisitedNodes.Remove(bestNode);

                AddNewNodes(bestNode, bestPathLength, visitedNodes, unvisitedNodes);
            }

            //no path found
            visitedNodesCount = visitedNodes.Count;
            return -1;
        }

        private IntPoint2D? GetBestNode(Dictionary<IntPoint2D, double> unvisitedNodes, IntPoint2D to)
        {
            IntPoint2D? bestNode = null;
            double bestDistance = 0;
            foreach (var node in unvisitedNodes)
            {
                double newDistance = DistanceAlgorithm.GetDistance(node.Key, to);
                if (bestNode == null || node.Value + newDistance < bestDistance)
                {
                    bestNode = node.Key;
                    bestDistance = node.Value + newDistance;
                }
            }
            return bestNode;
        }

        private void AddNewNodes(IntPoint2D startNode, double currentPathLenght, Dictionary<IntPoint2D, double> visitedNodes, Dictionary<IntPoint2D, double> unvisitedNodes)
        {
            foreach (var neighbor in NeighborProvider.GetNeighbors(startNode))
            {
                if (!PathableProvider.IsPathable(neighbor))
                {
                    continue;
                }

                if (visitedNodes.ContainsKey(neighbor))
                {
                    continue;
                }

                double newPathLength = currentPathLenght + DistanceAlgorithm.GetDistance(neighbor, startNode);
                if (unvisitedNodes.ContainsKey(neighbor))
                {
                    double oldPathLength = unvisitedNodes[neighbor];
                    if (oldPathLength >= newPathLength)
                    {
                        unvisitedNodes[neighbor] = newPathLength;
                    }
                }
                else
                {
                    unvisitedNodes.Add(neighbor, newPathLength);
                }
            }
        }
    }
}
