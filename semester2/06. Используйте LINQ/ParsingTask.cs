using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
		/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
		/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
			=> lines.Select(line => ParseSlideRecord(line))
				.Where(slideRecord => slideRecord != null)
				.ToDictionary(x => x.SlideId, y => y);

		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
		/// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
		/// Такой словарь можно получить методом ParseSlideRecords</param>
		/// <returns>Список информации о посещениях</returns>
		/// <exception cref="FormatException">Если среди строк есть некорректные</exception>
		public static IEnumerable<VisitRecord> ParseVisitRecords(IEnumerable<string> lines,
			IDictionary<int, SlideRecord> slides)
			=> lines.Skip(1).Select(line => ParseVisitRecord(line, slides));

		private static SlideRecord ParseSlideRecord(string line)
		{
			var lineParams = line.Split(';');
			return lineParams.Count() == 3
					&& int.TryParse(lineParams[0], out var slideId)
					&& Enum.TryParse<SlideType>(lineParams[1], true, out var slideType)
				? new SlideRecord(slideId, slideType, lineParams[2])
				: null;
		}
		
		private static VisitRecord ParseVisitRecord(string line, IDictionary<int, SlideRecord> slides)
		{
			var lineParams = line.Split(';');
			try
			{
				var userId = int.Parse(lineParams[0]);
				var slideId = int.Parse(lineParams[1]);
				return new VisitRecord(userId,
					slideId,
					DateTime.ParseExact(lineParams[2] + " " + lineParams[3],
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture,
						DateTimeStyles.None), 
					slides[slideId].SlideType);
			}
			catch (Exception e)
			{
				throw new FormatException($"Wrong line [{line}]", e);
			}
		}
	}
}