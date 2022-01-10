using AStarPathFinder.Interfaces;
using System;

namespace AStarPathFinder.DistanceAlgorithms
{
    /// <summary>
    /// Algorithm that calculates distance by taking max distance by one axis.
    /// Works well if diagonal movement cost is considered the same as straight movement cost.
    /// </summary>
    public class MaxDimensionDistanceAlgorithm : IDistanceAlgorithm
    {
        public double GetDistance(IntPoint2D from, IntPoint2D to)
        {
            return Math.Max(Math.Abs(from.X - to.X), Math.Abs(from.Y - to.Y));
        }
    }
}
