using AStarPathFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Providers
{
    public class BoolArrayPathableProvider : IPathableProvider
    {
        bool[,] PathingGrid;
        public BoolArrayPathableProvider(bool[,] pathingGrid)
        {
            PathingGrid = pathingGrid;
        }
        public bool IsPathable(IntPoint2D tile)
        {
            return PathingGrid[tile.X, tile.Y];
        }
    }
}
