using AStarPathFinder.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AStarPathFinderTests")]
namespace AStarPathFinder
{
    public class AStarPathFinder
    {
        #region State and Constructor
        IDistanceAlgorithm DistanceAlgorithm;
        INeighborProvider NeighborProvider;
        IPathableProvider PathableProvider;

        /// <summary>
        /// A* pathfinding algorythm.
        /// </summary>
        /// <param name="pathableProvider">
        /// Implementation of <see cref="IPathableProvider"/> interface. 
        /// The instance is responsible for providing the pathfinding algorithm
        /// with pathable (that is not blocked) state of 
        /// points/tiles/nodes/cells or whatever we need to find a path through.
        /// </param>
        /// <param name="neighborProvider">
        /// Implementation of <see cref="INeighborProvider"/> interface. 
        /// The instance is responsible for providing the pathfinding algorithm
        /// with neigbors of points/tiles/nodes/cells or whatever we need to find a path through.
        /// </param>
        /// <param name="distanceAlgorithm">
        /// Implementation of <see cref="IDistanceAlgorithm"/> interface. 
        /// The instance is responsible for providing the pathfinding algorithm
        /// with an algorithm calculating distance between 
        /// points/tiles/nodes/cells or whatever we need to find a path through.
        /// </param>
        /// <example>
        /// <code>
        /// var pathFinder = new AStarPathFinder(
        ///     new BoolArrayPathableProvider(pathableMap),
        ///     new DiagonalLimitedSizeNeighborProvider(mapWidth, mapHeight),
        ///     new PythagorasAlgorithm()
        /// );
        /// var path = pathFinder.GetPath(from, to);
        /// </code>
        /// </example>
        public AStarPathFinder(IPathableProvider pathableProvider,
            INeighborProvider neighborProvider,
            IDistanceAlgorithm distanceAlgorithm)
        {
            NeighborProvider = neighborProvider;
            PathableProvider = pathableProvider;
            DistanceAlgorithm = distanceAlgorithm;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns true if a path exists between <paramref name="from"/>
        /// and <paramref name="to"/> points.
        /// </summary>
        public bool CanPass(IntPoint2D from, IntPoint2D to)
        {
            CalculatePath(from, to, out bool canPass, out _, out _, out _);
            return canPass;
        }

        /// <summary>
        /// Returns the shortest possible path length between <paramref name="from"/>
        /// and <paramref name="to"/> points.
        /// </summary>
        public double GetPathLength(IntPoint2D from, IntPoint2D to)
        {
            CalculatePath(from, to, out _, out _, out double bestPathLength, out _);
            return bestPathLength;
        }

        /// <summary>
        /// Returns the shortest possible path between <paramref name="from"/>
        /// and <paramref name="to"/> points.
        /// </summary>
        /// <param name="returnClosestPathIfCannotReachDestination">If set to true, forces
        /// the method to always return a path, even if the <paramref name="to"/> point 
        /// is unreachable. In that case the path to the closest reachable point is returned.
        /// </param>
        public IEnumerable<IntPoint2D> GetPath(IntPoint2D from, IntPoint2D to, bool returnClosestPathIfCannotReachDestination = false)
        {
            CalculatePath(from, to, out _, out _, out _, out IEnumerable<IntPoint2D> path, returnClosestPathIfCannotReachDestination);
            return path;
        }
        #endregion

        #region Implementation
        //TODO: implement caching
        internal void CalculatePath(IntPoint2D from, IntPoint2D to,
            out bool canPass,
            out int visitedNodesCount,
            out double pathLength,
            out IEnumerable<IntPoint2D> path,
            bool returnClosestPathIfCannotReachDestination = false)
        {
            var visitedNodes = new Dictionary<IntPoint2D, double>();
            var unvisitedNodes = new Dictionary<IntPoint2D, double>();
            unvisitedNodes.Add(from, 0);


            var pathMemory = new Dictionary<IntPoint2D, IntPoint2D>();

            while (unvisitedNodes.Any())
            {
                IntPoint2D bestNode = GetBestNode(unvisitedNodes, to).Value;
                double bestPathLength = unvisitedNodes[bestNode];
                if (bestNode.Equals(to))
                {
                    visitedNodesCount = visitedNodes.Count;
                    canPass = true;
                    pathLength = bestPathLength;
                    path = ReconstructPath(pathMemory, bestNode);
                    return;
                }

                visitedNodes.Add(bestNode, bestPathLength);
                unvisitedNodes.Remove(bestNode);

                AddNewNodes(bestNode, bestPathLength, visitedNodes, unvisitedNodes, pathMemory);
            }

            //no path found
            if (returnClosestPathIfCannotReachDestination)
            {
                visitedNodesCount = visitedNodes.Count;
                canPass = false;
                IntPoint2D closestPoint = GetBestNode(visitedNodes, to).Value;
                pathLength = visitedNodes[closestPoint];
                path = ReconstructPath(pathMemory, closestPoint);
                return;
            }
            else
            {
                visitedNodesCount = visitedNodes.Count;
                canPass = false;
                pathLength = double.NaN;
                path = null;
                return;
            }
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

        private void AddNewNodes(IntPoint2D startNode, double currentPathLenght, 
            Dictionary<IntPoint2D, double> visitedNodes, 
            Dictionary<IntPoint2D, double> unvisitedNodes, 
            Dictionary<IntPoint2D, IntPoint2D> pathMemory)
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
                        pathMemory[neighbor] = startNode;
                    }
                }
                else
                {
                    pathMemory.Add(neighbor, startNode);
                    unvisitedNodes.Add(neighbor, newPathLength);
                }
            }
        }
        private IEnumerable<IntPoint2D> ReconstructPath(
            IDictionary<IntPoint2D, IntPoint2D> pathMemory,
            IntPoint2D current)
        {
            List<IntPoint2D> totalPath = new List<IntPoint2D>() { current };

            while (pathMemory.ContainsKey(current))
            {
                current = pathMemory[current];
                totalPath.Add(current);
            }

            totalPath.Reverse();
            totalPath.RemoveAt(0);

            return totalPath;
        }
        #endregion
    }
}
