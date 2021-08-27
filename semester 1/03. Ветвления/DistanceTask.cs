using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		private static double Sqr(double x)
        {
			return x * x;
        }
        
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
			// координаты вектора s = AB
			var sx = bx - ax;
			var sy = by - ay;
			// найдём скалярные квадраты векторов
			var squarePA = Sqr(x - ax) + Sqr(y - ay);
			var squarePB = Sqr(x - bx) + Sqr(y - by);
			var squareAB = Sqr(sx) + Sqr(sy);
			return (squareAB > Math.Abs(squarePA - squarePB))
				? Math.Abs(sy * x - sx * y + bx * ay - by * ax) / Math.Sqrt(squareAB)
				: Math.Sqrt(Math.Min(squarePA, squarePB));
		}
	}
}