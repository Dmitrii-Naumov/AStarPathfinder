using AStarPathFinder.Interfaces;

namespace AStarPathFinder.Providers
{
    /// <summary>
    /// Pathable provider that uses bollean 2-dimension array 
    /// representing map of pathable tiles.
    /// </summary>
    public class BoolArrayPathableProvider : IPathableProvider
    {
        bool[,] PathingGrid;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathingGrid">Boolean 2-dimensional array where true value 
        /// represents pathable cell and false represents blocked cell.</param>
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
