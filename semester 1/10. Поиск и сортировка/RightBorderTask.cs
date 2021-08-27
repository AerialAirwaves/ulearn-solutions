using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        /// <returns>
        /// Возвращает индекс правой границы. 
        /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
        /// Если такого нет, то возвращает items.Length
        /// </returns>
        public static int GetRightBorderIndex(
            IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            while (right - left > 1)
            {
                var middle = left + (right - left) / 2;
                if (StringsPartialOrderRelation(prefix, phrases[middle]))
                    left = middle;
                else
                    right = middle;
            }
            return right;
        }

        private static bool StringsPartialOrderRelation(string a, string b)
        {
            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) >= 0
                    || b.StartsWith(a, StringComparison.OrdinalIgnoreCase);
        }
    }
}