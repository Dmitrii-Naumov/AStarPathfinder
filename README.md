# AStarPathfinder
Implementation of AStar pathfinding algorithm in C# allowing to find path to closest point if the destination point is not available.
The algorithm can calculate a path on 2D grid, 3D grid or any other type of space representation for which operations of 
getting neighbor coordinates and calculating distance are defined.

## Framework version
.NET Standard 2.0

## Example
The code below is an example how to use the library.
```
using AStarPathFinder;
using AStarPathFinder.DistanceAlgorithms;
using AStarPathFinder.Providers;

var pathFinder = new AStarPathFinder<IntPoint2D>(
           new BoolArrayPathableProvider(pathableMap),
           new DiagonalLimitedSizeNeighborProvider(mapWidth, mapHeight),
           new PythagorasAlgorithm()
  );

var path = pathFinder.GetPath(from, to);
```

## Credits
The following repo has been used as an example and inspiration

https://github.com/WichardRiezebos/astar-navigator
