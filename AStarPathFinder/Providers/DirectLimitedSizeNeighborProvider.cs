using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Providers
{
    public class DirectLimitedSizeNeighborProvider : INeighborProvider
    {
        private static readonly IntPoint2D[] NeighborOffsets = new IntPoint2D[]
        {
            new IntPoint2D(  0, -1 ),
            new IntPoint2D(  0,  1 ),
            new IntPoint2D(  1,  0 ), 
            new IntPoint2D( -1,  0 )
        };

        int MaxX;
        int MinX;
        int MaxY;
        int MinY;

        public DirectLimitedSizeNeighborProvider(int maxX, int minX, int maxY, int minY)
        {
            MaxX = maxX;
            MinX = minX;
            MaxY = maxY;
            MinY = minY;
        }

        public DirectLimitedSizeNeighborProvider(int width, int height) : this(width - 1, 0, height - 1, 0)
        { }

        public IEnumerable<IntPoint2D> GetNeighbors(IntPoint2D tile)
        {
            foreach (IntPoint2D neighborOffset in NeighborOffsets)
            {
                IntPoint2D point = tile + neighborOffset;
                if (point.X <= MaxX && point.X >= MinX && point.Y <= MaxY && point.Y >= MinY)
                {
                    yield return point;
                }
            }
        }
    }
}
