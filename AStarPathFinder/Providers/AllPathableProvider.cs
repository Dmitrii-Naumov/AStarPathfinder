using AStarPathFinder.Interfaces;

namespace AStarPathFinder.Providers
{
    /// <summary>
    /// Always returns true.
    /// </summary>
    public class AllPathableProvider : IPathableProvider
    {
        public bool IsPathable(IntPoint2D tile)
        {
            return true;
        }
    }
}
