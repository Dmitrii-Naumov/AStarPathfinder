using System.Collections.Generic;

namespace AStarPathFinder.Providers
{
    /// <summary>
    /// Returns all direct and diagonal cells if they are within set boundaries.
    /// </summary>
    public class DiagonalLimitedSizeNeighborProvider : DiagonalNeighborProvider
    {
        int MaxX;
        int MinX;
        int MaxY;
        int MinY;

        public DiagonalLimitedSizeNeighborProvider(int maxX, int minX, int maxY, int minY)
        {
            MaxX = maxX;
            MinX = minX;
            MaxY = maxY;
            MinY = minY;
        }

        public DiagonalLimitedSizeNeighborProvider(int width, int height) : this(width - 1, 0, height - 1, 0)
        { }

        public override IEnumerable<IntPoint2D> GetNeighbors(IntPoint2D point)
        {
            foreach (var neighbor in base.GetNeighbors(point))
            {
                if (neighbor.X >= MinX && neighbor.X <= MaxX && neighbor.Y >= MinY && neighbor.Y <= MaxY)
                {
                    yield return neighbor;
                }
            }
        }
    }
}
