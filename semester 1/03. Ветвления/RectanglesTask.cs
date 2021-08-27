using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
        // Находит длину пересечения отрезков [a1, a2] и [b1, b2]
        private static int SegmentsIntersectionLength(int a1, int a2, int b1, int b2)
        {
            return Math.Max(0, Math.Min(a2, b2) - Math.Max(a1, b1));
        }

        // Пересекаются ли отрезки [a1, a2] и [b1, b2] (пересечение только по границе также считается пересечением)
        private static bool AreSegmentsIntersected(int a1, int a2, int b1, int b2)
        {
            return Math.Min(a2, b2) >= Math.Max(a1, b1);
        }

        // Вложен ли отрезок [int1, int2] в [ext1, ext2] (совпадающие отрезки также считаются вложенными)
        private static bool AreSegmentsNested(int int1, int int2, int ext1, int ext2)
        {
            return (ext1 <= int1) && (int2 <= ext2);
        }
        
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			return AreSegmentsIntersected(r1.Top, r1.Bottom, r2.Top, r2.Bottom)
                && AreSegmentsIntersected(r1.Left, r1.Right, r2.Left, r2.Right);
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			return SegmentsIntersectionLength(r1.Top, r1.Bottom, r2.Top, r2.Bottom)
                * SegmentsIntersectionLength(r1.Left, r1.Right, r2.Left, r2.Right);
		}

		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if (AreSegmentsNested(r1.Left, r1.Right, r2.Left, r2.Right)
                && AreSegmentsNested(r1.Top, r1.Bottom, r2.Top, r2.Bottom))
				return 0;
			if (AreSegmentsNested(r2.Left, r2.Right, r1.Left, r1.Right)
                && AreSegmentsNested(r2.Top, r2.Bottom, r1.Top, r1.Bottom))
				return 1;
			return -1;
		}
	}
}