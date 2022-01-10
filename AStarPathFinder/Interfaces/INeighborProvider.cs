using System.Collections.Generic;

namespace AStarPathFinder.Interfaces
{
    public interface INeighborProvider<TNode>
        where TNode : struct
    {
        IEnumerable<TNode> GetNeighbors(TNode tile);
    }
}
