using GenericAStarPathFinder.Interfaces;
using System;

namespace GenericAStarPathFinder.DistanceAlgorithms
{
    /// <summary>
    /// Standard 3D distance calculating algorithm.
    /// Works well if diagonal movements are allowed.
    /// If diagonal movements are not allowed it may make sense to use a different 
    /// algorithm due to the performance of Pythagoras algorithm.
    /// </summary>
    public class PythagorasAlgorithm3D : IDistanceAlgorithm<IntPoint3D>
    {
        public double GetDistance(IntPoint3D from, IntPoint3D to)
        {
            return Math.Sqrt(
              Math.Pow(to.X - from.X, 2) +
              Math.Pow(to.Y - from.Y, 2) +
              Math.Pow(to.Z - from.Z, 2)
          );
        }
    }
}
