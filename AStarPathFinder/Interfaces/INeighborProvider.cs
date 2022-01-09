using System.Collections.Generic;

namespace AStarPathFinder.Interfaces
{
    public interface INeighborProvider
    {
        IEnumerable<IntPoint2D> GetNeighbors(IntPoint2D tile);
    }
}
