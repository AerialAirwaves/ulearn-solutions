using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X, Y;
    }

    public class Geometry
    {
        public static double GetLength(Vector a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector { X = a.X + b.X, Y = a.Y + b.Y};
        }
    }
}