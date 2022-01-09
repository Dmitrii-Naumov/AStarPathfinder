using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Interfaces
{
    public interface IDistanceAlgorithm
    {
        double GetDistance(IntPoint2D from, IntPoint2D to);
    }
}
