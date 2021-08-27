using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        public static MoveDirection[] FindShortestPath(Map map)
        {
            var pathInverted = (FindShortestPathViaChest(map)
                ?? FindShortestPathToExit(map) ?? Array.Empty<Point>()).Reverse();
            return pathInverted.Skip(1)
                .Zip(pathInverted, (current, next) => Walker.ConvertOffsetToDirection((Size)(current - (Size) next)))
                .ToArray();
        }

        private static IEnumerable<Point> FindShortestPathToExit(Map map)
            => BfsTask.FindPaths(map, map.InitialPosition, new[] { map.Exit })
                .OrderBy(p => p.Length).FirstOrDefault();
        
        private static IEnumerable<Point> FindShortestPathViaChest(Map map)
        {
            var pathsToChests = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
            var pathsFromExitToChests = BfsTask.FindPaths(map, map.Exit, map.Chests);
            var pathComponents = pathsToChests.Join(pathsFromExitToChests,
                    pc => pc.Value, ec => ec.First(),
                    (pc, ec) => (ec.Length + pc.Length, ec.Previous, pc))
                .OrderBy(e => e.Item1)
                .FirstOrDefault();

            if (pathComponents == default)
                return null;

            return pathComponents.Item2.Reverse().Concat(pathComponents.Item3);
        }
    }
}
