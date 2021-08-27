using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        private static string[] BuildNumsArray(int start, int count)
        {
            var result = new string[count*3];
            for (int i = 0; i < count; i++)
            {
                result[i * 3] = (i + start).ToString() + "B";
                result[i * 3 + 1] = (i + start).ToString() + "D";
                result[i * 3 + 2] = (i + start).ToString() + "Z";
            }
            return result;
        }

        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var birthsPerDay = new double[31 * 3];
            foreach (var item in names)
                if ((item.Name == name) && (item.BirthDate.Day != 1))
                    if ((item.BirthDate.Year < 1964) && (item.BirthDate.Year > 1945))
                        birthsPerDay[(item.BirthDate.Day - 1) * 3]++;
                    else if ((item.BirthDate.Year < 2002) && (item.BirthDate.Year > 1964))
                        birthsPerDay[(item.BirthDate.Day - 1) * 3 + 1]++;
                    else if (item.BirthDate.Year > 2001)
                        birthsPerDay[(item.BirthDate.Day - 1) * 3 + 2]++;

            return new HistogramData(
                "зумербумердумеры", 
                BuildNumsArray(1, 31),
                birthsPerDay);
        }
    }
}