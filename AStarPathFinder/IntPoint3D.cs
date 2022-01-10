namespace AStarPathFinder
{
	/// <summary>
	/// Represents a point on 3D grid with Integer coordinates.
	/// </summary>
    public struct IntPoint3D
	{
		public int X;
		public int Y;
		public int Z;

		public IntPoint3D(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static IntPoint3D operator +(IntPoint3D a, IntPoint3D b)
			=> new IntPoint3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		public static IntPoint3D operator -(IntPoint3D a)
			=> new IntPoint3D(-a.X, -a.Y, -a.Z);
		public static IntPoint3D operator -(IntPoint3D a, IntPoint3D b)
			=> -b + a;

		public override int GetHashCode()
		{
			unchecked// Overflow is fine
			{
				//Use prime number to calculate hash
				int hash = 17;
				hash = hash * 16777619 + X;
				hash = hash * 16777619 + Y;
				hash = hash * 16777619 + Z;
				return hash;
			}
		}
		public override bool Equals(object obj)
		{
			if (obj is IntPoint3D)
			{
				IntPoint3D secondPoint = (IntPoint3D)obj;

				return X == secondPoint.X && Y == secondPoint.Y && Z == secondPoint.Z;
			}
			return false;
		}
	}
}
