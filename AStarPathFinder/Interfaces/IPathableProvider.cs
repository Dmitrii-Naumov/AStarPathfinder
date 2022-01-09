using System;
using System.Collections.Generic;
using System.Text;

namespace AStarPathFinder.Interfaces
{
    public interface IPathableProvider
    {
        bool IsPathable(IntPoint2D tile);
    }
}
