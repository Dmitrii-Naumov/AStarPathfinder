using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Providers
{
    public class DiagonalLimitedSizeNeighborProvider : INeighborProvider
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

        public IEnumerable<IntPoint2D> GetNeighbors(IntPoint2D point)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    var neighbor = new IntPoint2D { X = point.X + x, Y = point.Y + y };
                    if (neighbor.X >= MinX && neighbor.X <= MaxX && neighbor.Y >= MinY && neighbor.Y <= MaxY)
                    {
                        yield return neighbor;
                    }
                }
            }
        }
    }
}
