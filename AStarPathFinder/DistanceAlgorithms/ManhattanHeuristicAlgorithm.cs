using GenericAStarPathFinder.Interfaces;
using System;

namespace GenericAStarPathFinder.DistanceAlgorithms
{
    /// <summary>
    /// Algorithm that calculates 2D distance by taking max distance by one axis.
    /// Works well if diagonal movement cost is considered the same as straight movement cost.
    /// </summary>
    public class MaxDimensionDistanceAlgorithm : IDistanceAlgorithm<IntPoint2D>
    {
        public double GetDistance(IntPoint2D from, IntPoint2D to)
        {
            return Math.Max(Math.Abs(from.X - to.X), Math.Abs(from.Y - to.Y));
        }
    }
}
