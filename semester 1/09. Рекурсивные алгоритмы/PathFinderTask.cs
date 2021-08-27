using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
		public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
			var size = checkpoints.Length;
			var bestOrder = new int[size];
			var bestCost = double.PositiveInfinity;
			MakePermutations(
				new int[size],
				bestOrder,
				1,
				0,
				ref bestCost,
				checkpoints);
			return bestOrder;
		}

		private static void MakePermutations(
			int[] permutation,
			int[] bestPermutation,
			int position,
			double currentCost,
			ref double bestCost,
			Point[] checkpoints)
        {
			if (currentCost >= bestCost)
				return;
			if (position == permutation.Length)
			{
				permutation.CopyTo(bestPermutation, 0);
				bestCost = currentCost;
				return;
			}
			for (int i = 0; i < permutation.Length; i++)
			{
				if (Array.IndexOf(permutation, i, 0, position) != -1)
					continue;
				permutation[position] = i;
				MakePermutations(permutation,
					bestPermutation,
					position + 1,
					currentCost
						+ PointExtensions.DistanceTo(checkpoints[permutation[position]],
							checkpoints[permutation[position - 1]]),
					ref bestCost,
					checkpoints);
			}
		}
	}
}