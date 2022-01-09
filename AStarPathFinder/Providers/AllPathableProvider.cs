using AStarPathFinder.Interfaces;

namespace AStarPathFinder.Providers
{
    public class AllPathableProvider : IPathableProvider
    {
        public bool IsPathable(IntPoint2D tile)
        {
            return true;
        }
    }
}
