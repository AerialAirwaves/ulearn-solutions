namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			if (count % 10 == 0) || ((count % 10) >= 5)
				|| ((count % 100 >= 11) && (count % 100 <= 19))
				return "рублей";
			return ((count % 10) == 1) ? "рубль" : "рубля";
		}
	}
}