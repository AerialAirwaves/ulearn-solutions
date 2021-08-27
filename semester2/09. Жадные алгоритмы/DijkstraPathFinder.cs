using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
    internal class DijkstraData
    {
        public Point? Previous { get; set; }
        public int Price { get; set; }
    }

    public class DijkstraPathFinder
    {
        private static readonly Size[] possibleDirections =
        {
            new Size(1, 0), new Size(0, 1),
            new Size(-1, 0), new Size(0, -1)
        };
        
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            var notVisited = new List<Point>();
            var destinations = targets.ToHashSet();
            var track = new Dictionary<Point, DijkstraData>();
            track[start] = new DijkstraData { Price = 0, Previous = default};
            notVisited.Add(start);
            while (notVisited.Count > 0)
            {
                var toOpen = notVisited.Intersect(track.Keys)
                    .OrderBy(x => track[x].Price)
                    .First();
                if (destinations.Contains(toOpen))
                {
                    var path = GetPathWithCost(track, toOpen);
                    yield return path;
                    destinations.Remove(path.End);
                }

                AddToOpens(toOpen, state, track, notVisited);
                notVisited.Remove(toOpen);
            } 
        }

        private static void AddToOpens(Point toOpen, State state,
            Dictionary<Point, DijkstraData> track, List<Point> notVisited)
        {
            foreach (var point in possibleDirections.Select(delta => toOpen + delta)
                .Where(p => !track.ContainsKey(p) && state.InsideMap(p) && !state.IsWallAt(p)))
            {
                var newPrice = track[toOpen].Price + state.CellCost[point.X, point.Y];
                track[point] = new DijkstraData { Previous = toOpen, Price = newPrice };
                notVisited.Add(point);
            }
        }

        private static PathWithCost GetPathWithCost(Dictionary<Point, DijkstraData> track, Point destination)
        {
            var result = new PathWithCost(track[destination].Price, destination);
            for (var point = track[destination].Previous; point.HasValue; point = track[point.Value].Previous)
                result.Path.Add(point.Value);
            result.Path.Reverse();
            return result;
        }
    }
}