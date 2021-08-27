using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var angle = shoulder;
            var elbowPos = new PointF(
                (float)(Manipulator.UpperArm * Math.Cos(angle)),
                (float)(Manipulator.UpperArm * Math.Sin(angle)));

            angle += elbow;
            var wristPos = elbowPos - new SizeF(
                (float)(Manipulator.Forearm * Math.Cos(angle)),
            (float)(Manipulator.Forearm * Math.Sin(angle)));

            angle += wrist;
            var palmEndPos = wristPos + new SizeF(
                (float)(Manipulator.Palm * Math.Cos(angle)),
                (float)(Manipulator.Palm * Math.Sin(angle)));
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI,
            0, Manipulator.UpperArm,
            Manipulator.Forearm, Manipulator.UpperArm,
            Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]

        [TestCase(Math.PI / 4, 3 * Math.PI / 4, 5 * Math.PI / 4,
            Manipulator.UpperArm * 0.7071067811865475,
            Manipulator.UpperArm * 0.7071067811865475,

            Manipulator.UpperArm * 0.7071067811865475
            + Manipulator.Forearm,
            Manipulator.UpperArm * 0.7071067811865475,

            Manipulator.UpperArm * 0.7071067811865475
            + Manipulator.Forearm
            + Manipulator.Palm * 0.7071067811865475,
            Manipulator.UpperArm * 0.7071067811865475
            + Manipulator.Palm * 0.7071067811865475)]

        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI,
            0, Manipulator.UpperArm,
            Manipulator.Forearm, Manipulator.UpperArm,
            Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]

        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI,
            0, Manipulator.UpperArm,
            Manipulator.Forearm, Manipulator.UpperArm,
            Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]


        public void TestGetJointPositions(double shoulder, double elbow, double wrist,
            double elbowEndX, double elbowEndY,
            double wristEndX, double wristEndY, 
            double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(Manipulator.UpperArm, GetDistance(joints[0], new PointF(0,0)),
                1e-4, "incorrect upper arm joints distance");
            Assert.AreEqual(Manipulator.Forearm, GetDistance(joints[1], joints[0]),
                1e-4, "incorrect forearm joints distance");
            Assert.AreEqual(Manipulator.Palm, GetDistance(joints[2], joints[1]),
                1e-4, "incorrect palm joints distance");

            Assert.AreEqual(elbowEndX, joints[0].X, 1e-5, "upper arm endX");
            Assert.AreEqual(elbowEndY, joints[0].Y, 1e-5, "upper arm endY");
            Assert.AreEqual(wristEndX, joints[1].X, 1e-5, "forearm endX");
            Assert.AreEqual(wristEndY, joints[1].Y, 1e-5, "forearm endY");
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }

        private float GetDistance(PointF a, PointF b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}