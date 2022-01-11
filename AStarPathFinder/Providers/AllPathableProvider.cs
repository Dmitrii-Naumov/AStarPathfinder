using GenericAStarPathFinder.Interfaces;

namespace GenericAStarPathFinder.Providers
{
    /// <summary>
    /// Always returns true.
    /// </summary>
    public class AllPathableProvider<TNode> : IPathableProvider<TNode>
        where TNode : struct
    {
        public bool IsPathable(TNode tile)
        {
            return true;
        }
    }
}
