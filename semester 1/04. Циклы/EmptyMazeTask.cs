namespace Mazes
{
	public static class EmptyMazeTask
	{
		private static void Move(Robot robot, Direction direction, int stepsCount)
		{
			for (int i = 0; i < stepsCount; i++)
				robot.MoveTo(direction);
		}

		public static void MoveOut(Robot robot, int width, int height)
		{
			Move(robot, Direction.Down, height - 3);
			Move(robot, Direction.Right, width - 3);
		}
	}
}