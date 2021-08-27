using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var potentialMaximums = new LinkedList<double>();
			var window = new Queue<double>();
			foreach (var e in data)
			{
				while ((potentialMaximums.Count > 0) && (potentialMaximums.Last.Value < e.OriginalY))
					potentialMaximums.RemoveLast();
				potentialMaximums.AddLast(e.OriginalY);
				window.Enqueue(e.OriginalY);
				if ((window.Count > windowWidth) && (potentialMaximums.First.Value == window.Dequeue()))
					potentialMaximums.RemoveFirst();
				yield return e.WithMaxY(potentialMaximums.First.Value);
			}
		}
	}
}