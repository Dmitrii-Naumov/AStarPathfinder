using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Providers
{
    public class DirectNeighborProvider : INeighborProvider
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
