namespace AStarPathFinder.Interfaces
{
    public interface IPathableProvider<TNode>
        where TNode : struct
    {
        bool IsPathable(TNode node);
    }
}
