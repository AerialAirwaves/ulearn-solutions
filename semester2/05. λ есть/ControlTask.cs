using System;

namespace func_rocket
{
	public class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
			var distance = target - rocket.Location;
			var angle = Math.Abs(distance.Angle - rocket.Velocity.Angle) < 0.5
			            || Math.Abs(distance.Angle - rocket.Direction) < 0.5
				? (distance.Angle - rocket.Direction + distance.Angle - rocket.Velocity.Angle) / 2
				: distance.Angle - rocket.Direction;

			return (angle > 0)
				? Turn.Right
				: Turn.Left;
		}
	}
}