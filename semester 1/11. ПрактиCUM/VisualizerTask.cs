using System;
using System.Drawing;
using System.Windows.Forms;

namespace Manipulation
{
    public static class VisualizerTask
    {
        public static double ElbowStep = Math.PI / 256;
        public static double ShoulderStep = Math.PI / 256;
        public static double AlphaStep = Math.PI / 1536;
        public static float  JointPointSize = 10;

        public static double X = 220;
        public static double Y = -100;
        public static double Alpha = 0.05;
        public static double Wrist = 2 * Math.PI / 3;
        public static double Elbow = 3 * Math.PI / 4;
        public static double Shoulder = Math.PI / 2;

        public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
        public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
        public static Pen ManipulatorPen = new Pen(Color.Black, 3);
        public static Brush JointBrush = Brushes.Gray;

        public static void KeyDown(Form form, KeyEventArgs key)
        {
            var (deltaShoulder, deltaElbow) = (0d, 0d);
            // switch case сломался, несите новый
            if (key.KeyCode == Keys.Q)
                deltaShoulder += 0.1;
            else if (key.KeyCode == Keys.A)
                deltaShoulder -= 0.1;
            else if (key.KeyCode == Keys.W)
                deltaElbow += 0.1;
            else if (key.KeyCode == Keys.S)
                deltaElbow -= 0.1;

            var timer = new Timer();
            timer.Interval = 1;
            timer.Tick += (o, ev) =>
            {
                var tmp = true;
                if (deltaShoulder > 0 && tmp)
                {
                    Shoulder += ShoulderStep;
                    deltaShoulder -= ShoulderStep;
                    if (deltaShoulder <= 0)
                        tmp = false;
                }
                if (deltaShoulder < 0 && tmp)
                {
                    Shoulder -= ShoulderStep;
                    deltaShoulder += ShoulderStep;
                    if (deltaShoulder >= 0)
                        tmp = false;
                }

                if (deltaElbow > 0 && tmp)
                {
                    Elbow += ElbowStep;
                    deltaElbow -= ElbowStep;
                    if (deltaElbow <= 0)
                        tmp = false;
                }
                if (deltaElbow < 0 && tmp)
                {
                    Elbow -= ElbowStep;
                    deltaElbow += ElbowStep;
                    if (deltaElbow >= 0)
                        tmp = false;
                }

                Wrist = -(Alpha + Shoulder + Elbow);
                form.Invalidate();

                if (!tmp)
                {
                    var t = o as Timer;
                    t.Stop();
                }
            };
            timer.Start();
        }

        public static void MouseMove(Form form, MouseEventArgs e)
        {
            var mathPoint = ConvertWindowToMath(new PointF(e.X, e.Y), GetShoulderPos(form));
            (X, Y) = (mathPoint.X, mathPoint.Y);
            UpdateManipulator();
            if (!(double.IsNaN(Shoulder + Elbow + Wrist)))
                form.Invalidate();
        }

        public static void MouseWheel(Form form, MouseEventArgs e)
        {
            Alpha += AlphaStep * e.Delta;
            UpdateManipulator();
            if (!(double.IsNaN(Shoulder + Elbow + Wrist)))
                form.Invalidate();
        }

        public static void UpdateManipulator()
        {
            var angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
            (Shoulder, Elbow, Wrist) = (angles[0], angles[1], angles[2]);
        }

        private static void DrawManipulatorLine(Graphics graphics,
            PointF shoulderPos, PointF startJoint, PointF endJoint)
        {
            graphics.DrawLine(ManipulatorPen,
                shoulderPos.X + startJoint.X, shoulderPos.Y - startJoint.Y,
                shoulderPos.X + endJoint.X,   shoulderPos.Y - endJoint.Y);
        }

        private static void DrawManipulatorJoint(Graphics graphics,
            PointF shoulderPos, PointF jointPoint)
        {
            graphics.FillEllipse(JointBrush,
                shoulderPos.X - (JointPointSize / 2) + jointPoint.X,
                shoulderPos.Y - (JointPointSize / 2) - jointPoint.Y,
                JointPointSize, JointPointSize);
        }

        public static void DrawManipulator(Graphics graphics, PointF shoulderPos)
        {
            var jointsPoints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

            graphics.DrawString($"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
                new Font(SystemFonts.DefaultFont.FontFamily, 12),
                Brushes.DarkRed, 10, 10);

            DrawReachableZone(graphics, ReachableAreaBrush,
                UnreachableAreaBrush, shoulderPos, jointsPoints);

            for (int i = 0; i < 3; i++)
            {
                var startJoint = (i == 0) ? new PointF(0, 0) : jointsPoints[i - 1];
                DrawManipulatorJoint(graphics, shoulderPos, startJoint);
                DrawManipulatorLine(graphics, shoulderPos, startJoint, jointsPoints[i]);
            }
        }

        private static void DrawReachableZone(
            Graphics graphics,
            Brush reachableBrush,
            Brush unreachableBrush,
            PointF shoulderPos,
            PointF[] joints)
        {
            var mathCenter = new PointF(joints[2].X - joints[1].X,
                joints[2].Y - joints[1].Y);
            var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);

            var radius = Manipulator.Forearm + Manipulator.UpperArm;
            graphics.FillEllipse(reachableBrush,
                windowCenter.X - radius, windowCenter.Y - radius,
                2 * radius, 2 * radius);
            
            radius = Math.Abs(Manipulator.Forearm - Manipulator.UpperArm);
            graphics.FillEllipse(unreachableBrush,
                windowCenter.X - radius, windowCenter.Y - radius,
                2 * radius, 2 * radius);
        }

        public static PointF GetShoulderPos(Form form)
        {
            return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
        }

        public static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
        {
            return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
        }

        public static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
        {
            return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
        }
    }
}