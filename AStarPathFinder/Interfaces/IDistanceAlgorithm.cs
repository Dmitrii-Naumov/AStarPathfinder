namespace GenericAStarPathFinder.Interfaces
{
    public interface IDistanceAlgorithm<TNode>
        where TNode : struct
    {
        double GetDistance(TNode from, TNode to);
    }
}
