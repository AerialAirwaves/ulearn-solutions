using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Rivals
{
    public class RivalsTask
    {
        public static IEnumerable<OwnedLocation> AssignOwners(Map map)
        {
            var queue = new Queue<OwnedLocation>();
            var visited = new HashSet<Point>();
            
            for (var i = 0; i < map.Players.Length; i++)
                queue.Enqueue(new OwnedLocation(i, map.Players[i], 0));
            
            while (queue.Count > 0)
            {
                var ownLocationCandidate = queue.Dequeue();
                var point = ownLocationCandidate.Location;
                if (!map.InBounds(point) || map.Maze[point.X, point.Y] == MapCell.Wall || visited.Contains(point))
                    continue;
                
                visited.Add(point);
                yield return ownLocationCandidate;
                
                foreach (var e in NextPoints(point))
                    queue.Enqueue(new OwnedLocation(ownLocationCandidate.Owner, e, ownLocationCandidate.Distance + 1));
            }
        }

        private static readonly Size[] possibleDirections =
        {
            new Size(1, 0), new Size(0, 1),
            new Size(-1, 0), new Size(0, -1)
        };
        private static IEnumerable<Point> NextPoints(Point point) => possibleDirections.Select(delta => point + delta);
    }
}