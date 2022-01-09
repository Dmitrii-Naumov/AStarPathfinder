namespace AStarPathFinder
{
    public struct IntPoint2D
	{
		public int X;
		public int Y;
		public override int GetHashCode()
		{
			unchecked// Overflow is fine
			{
				//Use prime number to calculate hash

				return X.GetHashCode() * 486187739 + Y;
			}
		}
		public override bool Equals(object obj)
		{
			if (obj is IntPoint2D)
			{
				IntPoint2D secondPoint = (IntPoint2D)obj;

				return X == secondPoint.X && Y == secondPoint.Y;
			}
			return false;
		}
	}
}
