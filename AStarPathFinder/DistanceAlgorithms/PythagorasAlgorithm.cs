using AStarPathFinder.Interfaces;
using System;

namespace AStarPathFinder.DistanceAlgorithms
{
    /// <summary>
    /// Standard distance calculating algorithm.
    /// Works well if diagonal movements are allowed.
    /// If diagonal movements are not allowed it may make sense to use a different 
    /// algorithm due to the performance of Pythagoras algorithm.
    /// </summary>
    public class PythagorasAlgorithm : IDistanceAlgorithm
    {
        public double GetDistance(IntPoint2D from, IntPoint2D to)
        {
            return Math.Sqrt(
              Math.Pow(to.X - from.X, 2) +
              Math.Pow(to.Y - from.Y, 2)
          );
        }
    }
}
