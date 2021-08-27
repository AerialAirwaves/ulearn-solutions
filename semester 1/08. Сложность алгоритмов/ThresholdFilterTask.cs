using System;
using System.Linq;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        private static double EvaluateThreshold(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var pictureArea = width * height;
            var whitePixelsCount = (int)(pictureArea * whitePixelsFraction);
            var result = original.Cast<double>().ToList();
            result.Sort();
            
            if (whitePixelsCount == 0)
                return double.PositiveInfinity;
            else if (whitePixelsCount == pictureArea)
                return double.NegativeInfinity;
            return result[pictureArea - whitePixelsCount];
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var result = new double[width, height];
            var threshold = EvaluateThreshold(original, whitePixelsFraction);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    result[i, j] = (original[i, j] >= threshold) ? 1 : 0;
            return result;
        }
    }
}