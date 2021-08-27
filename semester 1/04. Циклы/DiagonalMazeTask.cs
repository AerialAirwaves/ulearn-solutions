using System;
using System.Windows.Forms;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
		private static void Move(Robot robot, int stepsCount, Direction direction)
		{
			for (int i = 0; i < stepsCount; i++)
				robot.MoveTo(direction);
		}

		private static void MoveDiagonal(Robot robot, Direction longDirection,
			Direction shortDirection, int longSteps)
        {
			while (!robot.Finished)
			{
				Move(robot, longSteps, longDirection);
				if (!robot.Finished)
					Move(robot, 1, shortDirection);
			}
		}

		public static void MoveOut(Robot robot, int width, int height)
		{
			var iterSteps = Math.Min(width, height) - 2;
			var longSteps = (Math.Max(width, height) - 2) / iterSteps;

			if (width >= height)
				MoveDiagonal(robot, Direction.Right, Direction.Down, longSteps);
			else
				MoveDiagonal(robot, Direction.Down, Direction.Right, longSteps);
		}
	}
}