using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;
using System.Linq;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var searchDefect = state.Chests.Count - state.Goal;
            var pathFinder = new DijkstraPathFinder();

            var result = new List<Point>();
            while (state.Chests.Count > searchDefect)
            {
                var pathToChest = pathFinder.GetPathsByDijkstra(state, state.Position, state.Chests)
                    .FirstOrDefault();
                
                if (pathToChest == default || state.Energy < pathToChest.Cost)
                    return new List<Point>();
                
                result.AddRange(pathToChest.Path.Skip(1));
                state.Chests.Remove(pathToChest.End);
                state.Position = pathToChest.End;
                state.Energy -= pathToChest.Cost;
            }
            
            return result;
        }
    }
}