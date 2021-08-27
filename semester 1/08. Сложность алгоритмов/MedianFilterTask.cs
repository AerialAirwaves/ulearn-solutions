using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        private static double GetListMedian(List<double> list)
        {
            var result = list.ToList();
            result.Sort();
            return (result.Count % 2 == 0)
                ? (result[(result.Count / 2) - 1] + result[result.Count / 2]) / 2
                : result[result.Count / 2];
        }

        private static double GetMedianValue(double[,] original, int x, int y)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var widthLowerBound = Math.Max(0, x - 1);
            var heightLowerBound = Math.Max(0, y - 1);
            var widthUpperBound = Math.Min(width - 1, x + 1);
            var heightUpperBound = Math.Min(height - 1, y + 1);
            var result = new List<double>();
            for (int i = widthLowerBound; i <= widthUpperBound; i++)
                for (int j = heightLowerBound; j <= heightUpperBound; j++)
                        result.Add(original[i, j]);
            return GetListMedian(result);
        }

        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var result = new double[width, height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    result[i, j] = GetMedianValue(original, i, j);
            return result;
        }
    }
}