namespace Mazes
{
	public static class SnakeMazeTask
	{
		private static void Move(Robot robot, int stepsCount, Direction direction)
		{
			for (int i = 0; i < stepsCount; i++)
				robot.MoveTo(direction);
		}

		public static void MoveOut(Robot robot, int width, int height)
		{
			height /= 4;
			for (int i = 0; i < height; i++)
			{
				Move(robot, width - 3,  Direction.Right);
				Move(robot, 2, Direction.Down);
				Move(robot, width - 3, Direction.Left);
				if (!robot.Finished)
					Move(robot, 2, Direction.Down);
			}
		}
	}
}