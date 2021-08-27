using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable
	{
		private readonly byte[] bytes;
		private int? hashCode;

		public ReadonlyBytes(params byte[] bytesArray)
		{
			if (bytesArray is null)
				throw new ArgumentNullException("bytesArray");
			bytes = bytesArray;
		}

		public int Length => bytes.Length;

		public byte this[int index] { get => bytes[index]; }

		public override bool Equals(object obj)
		{
			if ((obj is null) || (this.GetType() != obj.GetType()))
				return false;
			var another = (ReadonlyBytes) obj;
			if (another.Length != this.Length)
				return false;
			for (var i = 0; i < this.Length; i++)
				if (this[i] != another[i])
					return false;
			return true;
		}

		public override int GetHashCode()
		{
			if (!hashCode.HasValue)
			{
				var hash = 0;
				var fnvPrime = 16777619;
				foreach (var e in this)
					unchecked
					{
						hash ^= e;
						hash *= fnvPrime;						
					}
				
				hashCode = new int?(hash);	
			}
			return hashCode.Value;
		}

		public override string ToString() => string.Format("[{0}]", string.Join(", ", bytes));

		public IEnumerator<byte> GetEnumerator()
		{
			for (int i = 0; i < bytes.Length; i++)
				yield return bytes[i];
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}