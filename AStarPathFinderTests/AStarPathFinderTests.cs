using GenericAStarPathFinder;
using GenericAStarPathFinder.DistanceAlgorithms;
using GenericAStarPathFinder.Providers;
using System.Runtime.CompilerServices;
using Xunit;

[assembly: InternalsVisibleTo("AStarPathFinderTests")]
namespace AStarPathFinderTests
{
	public class AStarPathFinderTests
	{
		[Fact]
		public void CanCalculateTrivialPath()
		{
			IntPoint3D from = new IntPoint3D(0, 0, 0);
			IntPoint3D to = new IntPoint3D(0, 0, 0);

			var distanceAlgorithm = new PythagorasAlgorithm3D();

			var pathFinder = new AStarPathFinder<IntPoint3D>(
					new AllPathableProvider<IntPoint3D>(),
					new DiagonalNeighborProvider3D(),
					distanceAlgorithm
				);
			Assert.Equal(distanceAlgorithm.GetDistance(from, to), pathFinder.GetPathLength(from, to));
		}

		[Fact]
		public void CanCalculateStraightPath()
		{
			IntPoint2D from = new IntPoint2D() { X = 0, Y = 0 };
			IntPoint2D to = new IntPoint2D() { X = 0, Y = 2 };

			var distanceAlgorithm = new PythagorasAlgorithm();

			var pathFinder = new AStarPathFinder<IntPoint2D>(
					new AllPathableProvider<IntPoint2D>(),
					new DiagonalNeighborProvider(),
					distanceAlgorithm
				);
			Assert.Equal(distanceAlgorithm.GetDistance(from, to), pathFinder.GetPathLength(from, to));
		}

		[Fact]
		public void CannotGoThroughWalls()
		{
			bool[,] map = new bool[3, 3];
			map[0, 0] = true;
			map[0, 1] = true;
			map[0, 2] = true;

			map[2, 0] = true;
			map[2, 1] = true;
			map[2, 2] = true;
			IntPoint2D from = new IntPoint2D() { X = 0, Y = 0 };
			IntPoint2D to = new IntPoint2D() { X = 2, Y = 2 };

			var pathFinder = new AStarPathFinder<IntPoint2D>(
				new BoolArrayPathableProvider(map),
				new DiagonalLimitedSizeNeighborProvider(3, 3),
				new PythagorasAlgorithm()
			);
			Assert.False(pathFinder.CanPass(from, to));
		}


		[Fact]
		public void CanCalculateComplexPath()
		{
			bool[,] map = new bool[5, 5];
			map[0, 0] = true;
			map[0, 1] = true;
			map[0, 2] = true;
			map[0, 3] = true;


			map[1, 2] = true;

			map[2, 0] = true;
			map[2, 1] = true;
			map[2, 2] = true;
			map[2, 3] = true;

			map[3, 0] = true;
			map[3, 1] = true;
			map[3, 2] = true;
			map[3, 3] = true;
			IntPoint2D from = new IntPoint2D() { X = 0, Y = 0 };
			IntPoint2D to = new IntPoint2D() { X = 3, Y = 0 };

			var pathFinder = new AStarPathFinder<IntPoint2D>(
				new BoolArrayPathableProvider(map),
				new DiagonalLimitedSizeNeighborProvider(5, 5),
				new PythagorasAlgorithm()
			);

			var pathLength = pathFinder.GetPathLength(from, to);
			Assert.True(pathLength > 5 && pathLength < 6);
		}

		[Fact]
		public void CalculatesPathEfficiently()
		{

			IntPoint2D from = new IntPoint2D() { X = 0, Y = 0 };
			IntPoint2D to = new IntPoint2D() { X = 4, Y = 4 };

			var pathFinder = new AStarPathFinder<IntPoint2D>(
				new AllPathableProvider<IntPoint2D>(),
				new DiagonalLimitedSizeNeighborProvider(5, 5),
				new PythagorasAlgorithm()
			);

			int visitedNodesCount;
			pathFinder.CalculatePath(from, to, out _, out visitedNodesCount, out double pathLength, out _);
			Assert.True(pathLength > 5 && pathLength < 6, "Wrong Path Calculated");
			Assert.True(visitedNodesCount >= 4 && visitedNodesCount <= 8, "Path Calculated Inefficiently");
		}
	}
}
