using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.DistanceAlgorithms
{
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
