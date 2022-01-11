using GenericAStarPathFinder.Interfaces;
using System.Collections.Generic;

namespace GenericAStarPathFinder.Providers
{
    /// <summary>
    /// Returns all direct and diagonal neighbors on 2D grid.
    /// </summary>
    public class DiagonalNeighborProvider : INeighborProvider<IntPoint2D>
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
