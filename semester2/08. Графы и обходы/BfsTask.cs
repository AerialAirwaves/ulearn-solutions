using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace Dungeon
{
	public class BfsTask
	{
	    public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
	        var queue = new Queue<SinglyLinkedList<Point>>();
	        var visited = new HashSet<Point>();
	        var chestsHashset = chests.ToHashSet();

	        queue.Enqueue(new SinglyLinkedList<Point>(start));
	        visited.Add(start);

	        while (queue.Count > 0)
	        {
		        var path = queue.Dequeue();
		        var point = path.Value;

		        if (!map.InBounds(point) || map.Dungeon[point.X, point.Y] != MapCell.Empty)
			        continue;
		        
		        
		        foreach (var nextPoint in NextPoints(point))
		        {
			        var nextPath = new SinglyLinkedList<Point>(nextPoint, path);
			        if (visited.Contains(nextPoint))
				        continue;
				        
			        queue.Enqueue(nextPath);
			        visited.Add(nextPoint);
			        
			        if (chestsHashset.Contains(nextPoint))
				        yield return nextPath;
			    }
	        }
        }
	    
	    private static IEnumerable<Point> NextPoints(Point point)
			=> Walker.PossibleDirections.Select(delta => point + delta);
	}
}