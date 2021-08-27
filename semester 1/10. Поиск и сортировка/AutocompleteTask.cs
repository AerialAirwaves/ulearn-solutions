using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            count = Math.Min(count, GetCountByPrefix(phrases, prefix));
            var result = new string[count];
            var startPosition = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            Array.Copy(phrases.ToArray(), startPosition, result, 0, count);
            return result;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            return RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count)
                - LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var result = AutocompleteTask.GetTopByPrefix(new List<string> { }, "", 1337);
            Assert.AreEqual(new string[0], result);
        }

        [Test]
        public void TopByPrefix_IsOutputCorrect_WhenEmptyPrefix()
        {
            var phrases = new List<string> { "aaa_every", "aba_string", "abc_startswith", "acc_empty" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, "", phrases.Count);
            Assert.AreEqual(phrases.ToArray(), result);
        }

        [Test]
        public void TopByPrefix_IsOutputCorrect_WhenResultPhrasesCountLessThanCount()
        {
            var phrases = new List<string> { "rakunda", "rakukaja", "rakk", "reaction" };
            var expectedResult = new[] { "rakunda", "rakukaja", "rakk" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, "ra", 6);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TopByPrefix_IsOutputCorrect_OnSampleInput()
        {
            var phrases = new List<string> { "alpine", "alpha", "already" };
            var expectedResult = new[] { "alpine", "alpha" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, "alp", 2);
            Assert.AreEqual(expectedResult, result);
        }



        [Test]
        public void CountByPrefix_IsZero_WhenNoPhrases()
        {
            var phrases = new List<string>();
            var result = AutocompleteTask.GetCountByPrefix(phrases, "");
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string> { "aaa_every", "aba_string", "abc_startswith", "acc_empty" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "");
            Assert.AreEqual(phrases.Count, result);
        }

        [Test]
        public void CountByPrefix_IsOutputCorrect_OnSampleInput()
        {
            var phrases = new List<string> { "rakunda", "rakukaja", "rakk", "reaction" };
            var result = AutocompleteTask.GetCountByPrefix(phrases, "ra");
            Assert.AreEqual(3, result);
        }
    }
}
