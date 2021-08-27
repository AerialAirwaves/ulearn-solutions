using System;
using System.Collections.Generic;

namespace func_rocket
{
    public class LevelsTask
    {
        private static readonly Physics standardPhysics = new Physics();
        private static readonly Vector standardTarget = new Vector(600, 200);
        private static readonly Vector customTarget1 = new Vector(700, 500);
        private static readonly Vector customTarget2 = new Vector(600, 200);
        private static readonly Vector standardLocation = new Vector(200, 500);
        private static readonly Rocket standardRocket =
            new Rocket(standardLocation, Vector.Zero, -0.5 * Math.PI);

        public static IEnumerable<Level> CreateLevels()
        {
            yield return CreateLevel("Zero", (size, v) => Vector.Zero);
            yield return CreateLevel("Heavy", (size, v) => new Vector(0, 0.9));
            yield return CreateLevel("Up", (size, v)
                => new Vector(0, -300 / (300 - v.Y + size.Height)), customTarget1);
            yield return CreateLevel("WhiteHole", GetWhiteHoleGravity(standardTarget));
            yield return CreateLevel("BlackHole", GetBlackHoleGravity(customTarget2, standardLocation));
            yield return CreateLevel("BlackAndWhite", (size, v)
                => (GetBlackHoleGravity(customTarget2, standardLocation)(size, v)
                    + GetWhiteHoleGravity(customTarget2)(size, v)) / 2, customTarget2);
        }

        private static Level CreateLevel(string name, Gravity gravity, Vector target = null, Rocket rocket = null)
            => new Level(name, rocket ?? standardRocket, target ?? standardTarget, gravity, standardPhysics);
        
        private static Gravity GetBlackHoleGravity(Vector target, Vector rocketLocation) => (size, v) =>
        {
            var blackHoleDirection = (target + rocketLocation) / 2 - v;
            return blackHoleDirection * 300 / (blackHoleDirection.Length * blackHoleDirection.Length + 1);
        };
        
        private static Gravity GetWhiteHoleGravity(Vector target) => (size, v) =>
        {
            var gravity = v - target;
            return gravity * 140 / (gravity.Length * gravity.Length + 1);
        };
    }
}