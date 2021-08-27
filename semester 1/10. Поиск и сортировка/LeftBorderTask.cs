using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        /// <returns>
        /// Возвращает индекс левой границы.
        /// То есть индекс максимальной фразы, которая не начинается с prefix и меньшая prefix.
        /// Если такой нет, то возвращает -1
        /// </returns>
        public static int GetLeftBorderIndex(
            IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right - left == 1)
                return left;
            var middle = left + (right - left) / 2;
            return (StringsPartialOrderRelation(prefix, phrases[middle]))
                ? GetLeftBorderIndex(phrases, prefix, left, middle)
                : GetLeftBorderIndex(phrases, prefix, middle, right);
        }

        private static bool StringsPartialOrderRelation(string a, string b)
        {
            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) < 0
                    || b.StartsWith(a, StringComparison.OrdinalIgnoreCase);
        }
    }
}