using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var windowSum = .0;
			var window = new Queue<double>();
			foreach (var e in data)
			{
				window.Enqueue(e.OriginalY);
				windowSum += e.OriginalY;
				if (window.Count > windowWidth)
					windowSum -= window.Dequeue();
				yield return e.WithAvgSmoothedY(windowSum / window.Count);
			}
		}
	}
}