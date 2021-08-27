using System.Drawing;
using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		private static double sqrt2 = Math.Sqrt(2);

		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			var random = new Random(seed);
			var (x, y) = (1.0, 0.0);

			for (int i = 0; i < iterationsCount; i++)
			{
				var tranformationNum = random.Next(2);
				var angle = Math.PI * (1 + 2 * tranformationNum) / 4;
				(x, y) = (
					(x * Math.Cos(angle) - y * Math.Sin(angle)) / sqrt2 + tranformationNum,
					(x * Math.Sin(angle) + y * Math.Cos(angle)) / sqrt2
				);
				pixels.SetPixel(x, y);
			}
		}
	}
}