using GenericAStarPathFinder.Interfaces;
using System;

namespace GenericAStarPathFinder.DistanceAlgorithms
{
    /// <summary>
    /// Standard 2D distance calculating algorithm.
    /// Works well if diagonal movements are allowed.
    /// If diagonal movements are not allowed it may make sense to use a different 
    /// algorithm due to the performance of Pythagoras algorithm.
    /// </summary>
    public class PythagorasAlgorithm : IDistanceAlgorithm<IntPoint2D>
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
