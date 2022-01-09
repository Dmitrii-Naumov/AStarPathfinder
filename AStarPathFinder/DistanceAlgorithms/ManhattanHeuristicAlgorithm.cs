using AStarPathFinder.Interfaces;
using System;

namespace AStarPathFinder.DistanceAlgorithms
{
    public class ManhattanHeuristicAlgorithm : IDistanceAlgorithm
    {
        public double GetDistance(IntPoint2D from, IntPoint2D to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }
    }
}
