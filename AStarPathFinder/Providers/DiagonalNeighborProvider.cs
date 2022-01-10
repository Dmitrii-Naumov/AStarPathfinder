using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Providers
{
    /// <summary>
    /// Returns all direct and diagonal neighbors.
    /// </summary>
    public class DiagonalNeighborProvider : INeighborProvider
    {
        public virtual IEnumerable<IntPoint2D> GetNeighbors(IntPoint2D point)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    yield return new IntPoint2D { X = point.X + x, Y = point.Y + y };
                }
            }
        }
    }
}
