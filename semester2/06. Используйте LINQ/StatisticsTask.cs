using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		private static bool slideIsTheSame = false;
		private static double timeSpent = 0;

		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
			=> visits
				.GroupBy(visit => visit.UserId)
				.Select(g => g.OrderBy(x => x.DateTime).Bigrams())
				.SelectMany(g => GetVisitDurations(g, slideType))
				.Where(time=> (1 <= time) && (time <= 120))
				.DefaultIfEmpty(0)
				.Median();

		private static IEnumerable<double> GetVisitDurations(
			IEnumerable<Tuple<VisitRecord, VisitRecord>> visitsBigram, SlideType slideType)
			=> visitsBigram
				.Where(visit => (visit.Item1.SlideType == slideType) 
					&& (visit.Item1.UserId == visit.Item2.UserId) && IsSlideTheSame(visit))
				.Select(visit => timeSpent
					+ (visit.Item2.DateTime - visit.Item1.DateTime).TotalMinutes);

		private static bool IsSlideTheSame(Tuple<VisitRecord, VisitRecord> visit)
		{
			if (visit.Item1.SlideId == visit.Item2.SlideId)
			{
				timeSpent += (visit.Item2.DateTime - visit.Item1.DateTime).TotalMinutes;
				slideIsTheSame = true;
				return false;
			}
			
			if (slideIsTheSame)
			{
				slideIsTheSame = false;
				return true;
			}
			
			timeSpent = 0;
			return true;
		}
	}
}