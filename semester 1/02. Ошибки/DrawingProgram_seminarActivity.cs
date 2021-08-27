using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RefactorMe
{
    class Painter
    {
        static float x, y;
        static Graphics graphics;

        public static void Initialize(Graphics newGraphics)
        {
            graphics = newGraphics;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void MakeStep(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void ChangePosition(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, double rotationAngle, Graphics graphics)
        {
            // rotationAngle пока не используется, но будет использоваться в будущем
            Painter.Initialize(graphics);
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                DrawRectangle(width, height, graphics, i * 150, i * 100, new Pen (Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256))), (float)rotationAngle + (i * (1f / 16f)));
            }
            //DrawRectangle(width, height, graphics, 150, 200, Pens.Blue, (float)rotationAngle + (1.0f / 2.0f));
        }

        private static void DrawRectangle(int width, int height, Graphics graphics, int xCenter, int yCenter, Pen pen, float rotationAngle)
        {
            var size = Math.Min(width, height);
            float pi = Convert.ToSingle(Math.PI);
            float sqrtOfTwo = Convert.ToSingle(Math.Sqrt(2));
            
            var diagonalLength = sqrtOfTwo * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(pi / 4 + pi)) + width / 2f - 250 + xCenter;
            var y0 = (float)(diagonalLength * Math.Sin(pi / 4 + pi)) + height / 2f - 175 + yCenter;
            Painter.SetPosition(x0, y0);

            //Рисуем 1-ую сторону
            PrintSide(size, 0 + rotationAngle, pen);
            //Рисуем 2-ую сторону
            PrintSide(size, -pi / 2 + rotationAngle, pen);
            //Рисуем 3-ю сторону
            PrintSide(size, pi + rotationAngle, pen);
            //Рисуем 4-ую сторону
            PrintSide(size, pi / 2 + rotationAngle, pen);
        }

        private static void PrintSide(int size, float angle, Pen pen)
        {
            float pi = Convert.ToSingle(Math.PI);
            float sqrtOfTwo = Convert.ToSingle(Math.Sqrt(2));

            Painter.MakeStep(pen, size * 0.375f, angle);
            Painter.MakeStep(pen, size * 0.04f * sqrtOfTwo, angle + pi / 4);
            Painter.MakeStep(pen, size * 0.375f, angle + pi);
            Painter.MakeStep(pen, size * 0.375f - size * 0.04f, angle + pi / 2);

            Painter.ChangePosition(size * 0.04f, angle - pi);
            Painter.ChangePosition(size * 0.04f * sqrtOfTwo, angle + 3 * pi / 4);
        }
    }
}