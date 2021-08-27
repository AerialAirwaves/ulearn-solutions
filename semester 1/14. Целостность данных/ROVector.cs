namespace ReadOnlyVectorTask
{
	public class ReadOnlyVector
	{
		public readonly double X, Y;

		public ReadOnlyVector(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		public ReadOnlyVector Add(ReadOnlyVector other)
		{
			return new ReadOnlyVector(this.X + other.X, this.Y + other.Y);
		}

		public ReadOnlyVector WithX(double x) => new ReadOnlyVector(x, this.Y);
		public ReadOnlyVector WithY(double y) => new ReadOnlyVector(this.X, y);
	}
}