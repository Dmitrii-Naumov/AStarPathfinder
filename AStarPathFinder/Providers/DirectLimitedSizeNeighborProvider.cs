﻿using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Providers
{
    public class DirectLimitedSizeNeighborProvider : DirectNeighborProvider
    {
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

        public override IEnumerable<IntPoint2D> GetNeighbors(IntPoint2D tile)
        {
            foreach (IntPoint2D neighbor in base.GetNeighbors(tile))
            {
                if (neighbor.X <= MaxX && neighbor.X >= MinX && neighbor.Y <= MaxY && neighbor.Y >= MinY)
                {
                    yield return neighbor;
                }
            }
        }
    }
}
