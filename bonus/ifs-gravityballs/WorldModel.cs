using System;

namespace GravityBalls
{
	public class WorldModel
	{
		public double BallX;
		public double BallY;
		public double BallVelocityX = 180;
		public double BallVelocityY = 90;
		public double BallRadius;
		public double WorldWidth;
		public double WorldHeight;
		public double g = 10000;

		// task 1
		/*
		public void SimulateTimeframe(double dt)
		{
			BallY = Math.Max(BallRadius, Math.Min(BallY + BallVelocityY * dt, WorldHeight - BallRadius));
			BallX = Math.Max(BallRadius, Math.Min(BallX + BallVelocityX * dt, WorldWidth - BallRadius));
		}
		*/


		// task 2
		
		public void SimulateTimeframe(double dt)
		{
			BallY = Math.Max(BallRadius, Math.Min(BallY + BallVelocityY * dt, WorldHeight - BallRadius));
			BallX = Math.Max(BallRadius, Math.Min(BallX + BallVelocityX * dt, WorldWidth - BallRadius));
			if ((Math.Abs(BallY - BallRadius) < 1e-9) || (Math.Abs(BallY - WorldHeight + BallRadius) < 1e-9))
				BallVelocityY *= -1;
			if ((Math.Abs(BallX - BallRadius) < 1e-9) || (Math.Abs(BallX - WorldWidth + BallRadius) < 1e-9))
				BallVelocityX *= -1;
		}
		
		
		// task 3
		/*
		public void SimulateTimeframe(double dt)
		{
			BallVelocityY += g * dt;
			BallY = Math.Max(BallRadius, Math.Min(BallY + BallVelocityY * dt, WorldHeight - BallRadius));
			BallX = Math.Max(BallRadius, Math.Min(BallX + BallVelocityX * dt, WorldWidth - BallRadius));
			if ((Math.Abs(BallY - BallRadius) < 1e-9) || (Math.Abs(BallY - WorldHeight + BallRadius) < 1e-9))
				BallVelocityY *= -1;
			if ((Math.Abs(BallX - BallRadius) < 1e-9) || (Math.Abs(BallX - WorldWidth + BallRadius) < 1e-9))
				BallVelocityX *= -1;
		}
		*/
	}
}