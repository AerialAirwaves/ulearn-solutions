using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            var letterPositions = new List<int>();
            for (int i = 0; i < lowercaseWord.Length; i++)
                if (char.IsLetter(lowercaseWord[i])
                    && (char.ToUpper(lowercaseWord[i]) != lowercaseWord[i]))
                    letterPositions.Add(i);
            AlternateCharCases(0, letterPositions, result, lowercaseWord.ToCharArray());
            return result;
        }

        static void AlternateCharCases(int position,
            List<int> letterPositions, List<string> result, char[] word)
        {
            if (position == letterPositions.Count)
            {
                result.Add(new string(word));
                return;
            }
            word[letterPositions[position]] = char.ToLower(word[letterPositions[position]]);
            AlternateCharCases(position + 1, letterPositions, result, word);
            word[letterPositions[position]] = char.ToUpper(word[letterPositions[position]]);
            AlternateCharCases(position + 1, letterPositions, result, word);
        }
    }
}