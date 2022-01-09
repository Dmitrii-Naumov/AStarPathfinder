using System.Diagnostics;

namespace AStarPathFinder
{
    [DebuggerDisplay("{PathLength} - ({Point.X},{Point.Y})")]
    internal class Node
    {
        public double PathLength;
        public IntPoint2D Point;
       
        public override int GetHashCode()
        {
            //We ignore PathLenght comparison in order to easily replace nodes in collection
            return Point.GetHashCode();
        }
       
        public override bool Equals(object obj)
        {
            //We ignore PathLenght comparison in order to easily replace nodes in collection
            if (obj is Node)
                return Point.Equals((obj as Node).Point);
            else return false;
        }
    }
}
