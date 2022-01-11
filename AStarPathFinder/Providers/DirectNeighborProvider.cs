using GenericAStarPathFinder.Interfaces;
using System.Collections.Generic;

namespace GenericAStarPathFinder.Providers
{
    /// <summary>
    /// Returns all direct neighbors on 2D grid (diagonal neighbors are not returned).
    /// </summary>
    public class DirectNeighborProvider : INeighborProvider<IntPoint2D>
    {
        public virtual IEnumerable<IntPoint2D> GetNeighbors(IntPoint2D tile)
        {
            yield return new IntPoint2D { X = tile.X + 1, Y = tile.Y };
            yield return new IntPoint2D { X = tile.X - 1, Y = tile.Y };
            yield return new IntPoint2D { X = tile.X, Y = tile.Y + 1 };
            yield return new IntPoint2D { X = tile.X, Y = tile.Y - 1 };
        }
    }
}
