namespace AStarPathFinder
{
	/// <summary>
	/// Represents a point on 2D grid with Integer coordinates.
	/// </summary>
    public struct IntPoint2D
	{
		public int X;
		public int Y;

		public IntPoint2D(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static IntPoint2D operator +(IntPoint2D a, IntPoint2D b)
			=> new IntPoint2D(a.X + b.X, a.Y + b.Y);
		public static IntPoint2D operator -(IntPoint2D a)
			=> new IntPoint2D(-a.X, -a.Y);
		public static IntPoint2D operator -(IntPoint2D a, IntPoint2D b)
			=> -b + a;

		public override int GetHashCode()
		{
			unchecked// Overflow is fine
			{
				//Use prime number to calculate hash

				return X * 486187739 + Y;
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
