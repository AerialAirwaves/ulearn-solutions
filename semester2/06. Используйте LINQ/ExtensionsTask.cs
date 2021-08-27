using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        /// <summary>
        /// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
        /// Медиана списка из четного количества элементов — это среднее арифметическое 
        /// двух серединных элементов списка после сортировки.
        /// </summary>
        /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
        public static double Median(this IEnumerable<double> items)
        {
            var itemsSorted = items.OrderBy(x => x).ToArray();
            
            if (itemsSorted.Length == 0)
                throw new InvalidOperationException();
            
            return (itemsSorted.Length % 2 == 0)
                ? (itemsSorted[itemsSorted.Length / 2]
                   + itemsSorted[itemsSorted.Length / 2 - 1]) / 2
                : itemsSorted[itemsSorted.Length / 2];
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var previous = default(T);
            var isPreviousUnset = true;
            foreach (var current in items)
            {
                if (isPreviousUnset)
                {
                    previous = current;
                    isPreviousUnset = false;
                    continue;
                }
                yield return Tuple.Create(previous, previous = current);
            }
        }
    }
}