using AStarPathFinder.Interfaces;
using System.Collections.Generic;

namespace AStarPathFinder.Providers
{
    /// <summary>
    /// Returns all direct and diagonal neighbors on 3D grid.
    /// </summary>
    public class DiagonalNeighborProvider3D : INeighborProvider<IntPoint3D>
    {
        public virtual IEnumerable<IntPoint3D> GetNeighbors(IntPoint3D point)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        yield return new IntPoint3D(point.X + x, point.Y + y, point.Z + z);
                    }
                }
            }
        }
    }
}
