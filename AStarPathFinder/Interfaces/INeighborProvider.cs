using System.Collections.Generic;

namespace GenericAStarPathFinder.Interfaces
{
    public interface INeighborProvider<TNode>
        where TNode : struct
    {
        IEnumerable<TNode> GetNeighbors(TNode tile);
    }
}
