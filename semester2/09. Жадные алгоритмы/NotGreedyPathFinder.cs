using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
    public class NotGreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var finder = new DijkstraPathFinder();
            var result = default(List<Point>);
            var bestChestsCount = 0;
            var stack = new Stack<(Point position, int energy, List<Point> path, List<Point> chestsLeft)>();
            
            stack.Push((position: state.Position,
                energy: state.Energy, 
                path: new List<Point>(), 
                chestsLeft: state.Chests.ToList()));

            while (stack.Count > 0 && bestChestsCount < state.Chests.Count)
            { // кто такая эта ваша рекурсия? ;) не, серьезно: старайтесь переносить рекурсию на стеки с циклами
                var step = stack.Pop();
                foreach (var path in finder.GetPathsByDijkstra(state, step.position, step.chestsLeft))
                {
                    if (path.Cost > step.energy)
                        continue;
                    stack.Push((position: path.End, energy: step.energy - path.Cost,
                        path: step.path.Concat(path.Path.Skip(1)).ToList(),
                        chestsLeft: step.chestsLeft.Except(new[] { path.End }).ToList()));
                }

                var chestsTakenCount = state.Chests.Count - step.chestsLeft.Count;
                if (chestsTakenCount <= bestChestsCount)
                    continue;
                
                result = step.path;
                bestChestsCount = chestsTakenCount;
            }

            return result;
        }
    }
}