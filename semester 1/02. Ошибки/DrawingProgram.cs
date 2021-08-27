using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float penPosX, penPosY;
        static Graphics graph;

        public static void Init(Graphics newGraph)
        {
            graph = newGraph;
            graph.SmoothingMode = SmoothingMode.None;
            graph.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            penPosX = x0;
            penPosY = y0;
        }

        public static void DrawLine(Pen pen, double len, double angle)
        {
            // Делает шаг длиной len в направлении angle и рисует пройденную траекторию
            var x1 = (float)(penPosX + len * Math.Cos(angle));
            var y1 = (float)(penPosY + len * Math.Sin(angle));
            graph.DrawLine(pen, penPosX, penPosY, x1, y1);
            penPosX = x1;
            penPosY = y1;
        }

        public static void ChangePosition(double len, double angle)
        {
            penPosX = (float)(penPosX + len * Math.Cos(angle));
            penPosY = (float)(penPosY + len * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        private static float k1 = 0.375f;
        private static float k2 = 0.04f;
        private static double sqrt2 = Math.Sqrt(2);
        private static double piQuarter = Math.PI / 4;
        private static double piHalf = Math.PI / 2;

        private static void DrawSide(int size, double angle, Pen color)
        {
            Drawer.DrawLine(color, size * k1, angle);
            Drawer.DrawLine(color, size * k2 * sqrt2, angle + piQuarter);
            Drawer.DrawLine(color, size * k1, angle + Math.PI);
            Drawer.DrawLine(color, size * k1 - size * k2, angle + piHalf);
            Drawer.ChangePosition(size * k2, angle - Math.PI);
            Drawer.ChangePosition(size * k2 * sqrt2, angle + 3 * piQuarter);
        }

        public static void Draw(int width, int height, double rotationAngle, Graphics graph)
        {
            Drawer.Init(graph);
            // можно задать цвет квадрата, но в аргументы метода загнать нельзя - сигнатура полетит
            Pen color = Pens.Yellow;

            int size = Math.Min(width, height);
            var diagonalLen = sqrt2 * size * (k1 + k2) / 2;

            var x0 = (float)(diagonalLen * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonalLen * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;
            Drawer.SetPosition(x0, y0);

            DrawSide(size, rotationAngle, color);
            DrawSide(size, rotationAngle - (Math.PI / 2), color);
            DrawSide(size, Math.PI + rotationAngle, color);
            DrawSide(size, (Math.PI / 2) + rotationAngle, color);
        }
    }
}