using System.Collections.Generic;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            var average = double.NaN;
            
            foreach (var e in data)
                yield return e.WithExpSmoothedY(
                    average = double.IsNaN(average)
                        ? e.OriginalY
                        : alpha * e.OriginalY + (1 - alpha) * average);
        }
    }
}