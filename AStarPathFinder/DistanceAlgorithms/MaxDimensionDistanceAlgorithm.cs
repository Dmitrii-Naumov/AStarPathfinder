using GenericAStarPathFinder.Interfaces;
using System;

namespace GenericAStarPathFinder.DistanceAlgorithms
{
    /// <summary>
    /// Algorithm that calculates 2D distance by the total number of squares moved horizontally 
    /// and vertically to reach the target square from the current square.
    /// Works well if diagonal movements are not allowed.
    /// </summary>
    public class ManhattanHeuristicAlgorithm : IDistanceAlgorithm<IntPoint2D>
    {
        public double GetDistance(IntPoint2D from, IntPoint2D to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }
    }
}
