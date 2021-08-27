using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X, Y;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector b)
        {
            return Geometry.Add(this, b);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Segment
    {
        public Vector Begin, End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }
        
        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector a)
        {
            return Math.Sqrt(GetSquare(a.X) + GetSquare(a.Y));
        }

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector { X = a.X + b.X, Y = a.Y + b.Y };
        }

        public static Vector GetSegmentStraightDirectionalVector(Segment s)
        {
            return new Vector { X = s.End.X - s.Begin.X, Y = s.End.Y - s.Begin.Y };
        }

        public static double GetLength(Segment segment)
        {
            return GetLength(GetSegmentStraightDirectionalVector(segment));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            return IsNumberInRealStraightSegment(vector.X, segment.Begin.X, segment.End.X)
                && IsNumberInRealStraightSegment(vector.Y, segment.Begin.Y, segment.End.Y)
                && IsVectorInSegmentStraight(vector, segment);
        }

        public static bool IsVectorInSegmentStraight(Vector vector, Segment segment)
        {
            var directionalVector = GetSegmentStraightDirectionalVector(segment);
            return Math.Abs((vector.X - segment.Begin.X) * directionalVector.Y
                - (vector.Y - segment.Begin.Y) * directionalVector.X) < 1e-8;
        }

        private static double GetSquare(double x)
        {
            return x * x;
        }

        private static bool IsNumberInRealStraightSegment(double x, double a, double b)
        {
            return (Math.Min(a, b) <= x) && (x <= Math.Max(a, b));
        }
    }
}
