using GenericAStarPathFinder.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AStarPathFinderTests")]
namespace GenericAStarPathFinder
{
    /// <summary>
    /// A* pathfinding algorithm that searches best path in any type of graph.
    /// </summary>
    /// <typeparam name="TNode">TNode is a type which represents a pointer
    /// to a node on the map/graph on which the algorithm searches 
    /// a path. Must be represented by a struct with a good GetHashValue algorithm.</typeparam>
    public class AStarPathFinder<TNode>
        where TNode : struct
    {
        #region State and Constructor
        IDistanceAlgorithm<TNode> DistanceAlgorithm;
        INeighborProvider<TNode> NeighborProvider;
        IPathableProvider<TNode> PathableProvider;

        /// <summary>
        /// A* pathfinding algorithm.
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
        /// var pathFinder = new AStarPathFinder<IntPoint2D>(
        ///     new BoolArrayPathableProvider(pathableMap),
        ///     new DiagonalLimitedSizeNeighborProvider(mapWidth, mapHeight),
        ///     new PythagorasAlgorithm()
        /// );
        /// var path = pathFinder.GetPath(from, to);
        /// </code>
        /// </example>
        public AStarPathFinder(IPathableProvider<TNode> pathableProvider,
            INeighborProvider<TNode> neighborProvider,
            IDistanceAlgorithm<TNode> distanceAlgorithm)
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
        public bool CanPass(TNode from, TNode to)
        {
            CalculatePath(from, to, out bool canPass, out _, out _, out _);
            return canPass;
        }

        /// <summary>
        /// Returns the shortest possible path length between <paramref name="from"/>
        /// and <paramref name="to"/> points.
        /// </summary>
        public double GetPathLength(TNode from, TNode to)
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
        public IEnumerable<TNode> GetPath(TNode from, TNode to, bool returnClosestPathIfCannotReachDestination = false)
        {
            CalculatePath(from, to, out _, out _, out _, out IEnumerable<TNode> path, returnClosestPathIfCannotReachDestination);
            return path;
        }
        #endregion

        #region Implementation
        //TODO: implement caching
        internal void CalculatePath(TNode from, TNode to,
            out bool canPass,
            out int visitedNodesCount,
            out double pathLength,
            out IEnumerable<TNode> path,
            bool returnClosestPathIfCannotReachDestination = false)
        {
            var visitedNodes = new Dictionary<TNode, double>();
            var unvisitedNodes = new Dictionary<TNode, double>();
            unvisitedNodes.Add(from, 0);


            var pathMemory = new Dictionary<TNode, TNode>();

            while (unvisitedNodes.Any())
            {
                TNode bestNode = GetBestNode(unvisitedNodes, to);
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
                TNode closestPoint = GetBestNode(visitedNodes, to);
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

        private TNode GetBestNode(Dictionary<TNode, double> unvisitedNodes, TNode to)
        {
            TNode? bestNode = null;
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
            return bestNode.Value;
        }

        private void AddNewNodes(TNode startNode, double currentPathLenght, 
            Dictionary<TNode, double> visitedNodes, 
            Dictionary<TNode, double> unvisitedNodes, 
            Dictionary<TNode, TNode> pathMemory)
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
        private IEnumerable<TNode> ReconstructPath(
            IDictionary<TNode, TNode> pathMemory,
            TNode current)
        {
            List<TNode> totalPath = new List<TNode>() { current };

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
