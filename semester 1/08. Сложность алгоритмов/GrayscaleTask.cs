using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace Recognizer
{
	[TestFixture]
	public class PerformanceTest
	{
		[TestCase]
		public static void WithAndWithoutDimVariables()
		{
			var payloadSize = 1000;
			var payload = new Pixel[payloadSize, payloadSize];
			for (var i = 0; i < payloadSize; i++)
				for (var j = 0; j < payloadSize; j++)
					payload[i, j] = new Pixel(0, 0, 0);

			
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			GrayscaleTask.ToGrayscale(payload);
			stopWatch.Stop();
			Console.WriteLine("Чистый тест (мс):");
			Console.WriteLine(stopWatch.ElapsedMilliseconds);

			stopWatch.Reset();
			stopWatch.Start();
			GrayscaleTask.ToGrayscaleWithutDimensionalVariables(payload);
			stopWatch.Stop();
			Console.WriteLine("Тест без выделения переменных (мс):");
			Console.WriteLine(stopWatch.ElapsedMilliseconds);

			stopWatch.Reset();
			stopWatch.Start();
			GrayscaleTask.ToGrayscaleEvalInSeparatedMethod(payload);
			stopWatch.Stop();
			Console.WriteLine("Тест с выделением в метод (мс):");
			Console.WriteLine(stopWatch.ElapsedMilliseconds);


		}
	}

    public static class GrayscaleTask
	{
		private const double GrayscaleCoefficientR = 0.299;
		private const double GrayscaleCoefficientG = 0.587;
		private const double GrayscaleCoefficientB = 0.114;

		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var result = new double[width, height];
			for (int i = 0; i < width; i++)
				for (int j = 0; j < height; j++)
					result[i, j] = (GrayscaleCoefficientR * original[i, j].R
						+ GrayscaleCoefficientG * original[i, j].G
						+ GrayscaleCoefficientB * original[i, j].B) / 255;
			return result;
		}

		public static double[,] ToGrayscaleWithutDimensionalVariables(Pixel[,] original)
		{
			var result = new double[original.GetLength(0), original.GetLength(1)];
			for (int i = 0; i < original.GetLength(0); i++)
				for (int j = 0; j < original.GetLength(1); j++)
					result[i, j] = (GrayscaleCoefficientR * original[i, j].R
						+ GrayscaleCoefficientG * original[i, j].G
						+ GrayscaleCoefficientB * original[i, j].B) / 255;
			return result;
		}


		public static double[,] ToGrayscaleEvalInSeparatedMethod(Pixel[,] original)
		{
			var width = original.GetLength(0);
			var height = original.GetLength(1);
			var result = new double[width, height];
			for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
					result[i, j] = GrayscaleEvalMethod(original, i, j);
            return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double GrayscaleEvalMethod(Pixel[,] original, int i, int j)
        {
             return (GrayscaleCoefficientR * original[i, j].R
                + GrayscaleCoefficientG * original[i, j].G
                + GrayscaleCoefficientB * original[i, j].B) / 255;
        }
    }
}