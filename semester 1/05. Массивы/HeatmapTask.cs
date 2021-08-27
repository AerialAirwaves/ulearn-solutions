using System;
namespace Names
{
    internal static class HeatmapTask
    {
        private static string[] BuildNumsArray(int start, int count)
        {
            var result = new string[count];
            for (int i = 0; i < count; i++)
                result[i] = (i + start).ToString();
            return result;
        }

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var heatmapMatrix = new double[30, 12];
            foreach (var item in names)
                if (item.BirthDate.Day != 1)
                    heatmapMatrix[item.BirthDate.Day - 2,
                        item.BirthDate.Month - 1]++;

            return new HeatmapData(
                "Карта интенсивности рождаемости",
                heatmapMatrix,
                BuildNumsArray(2, 30),
                BuildNumsArray(1, 12));
        }
    }
}