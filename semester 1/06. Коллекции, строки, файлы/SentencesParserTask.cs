using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        private static string[] delimiters = new[] { '.', '!', '?', ':', ';', '(', ')' };

        private static List<string> ParseSentence(string sentenceText)
        {
            var result = new List<string>();
            var word = new StringBuilder();
            foreach (var c in sentenceText + ".")
            {
                if (char.IsLetter(c) || (c == '\''))
                    word.Append(char.ToLower(c));
                else
                {
                    if (word.Length > 0)
                        result.Add(word.ToString());
                    word.Clear();
                }
            }
            return result;
        }

        public static List<List<string>> ParseSentences(string text)
        {
            
            var sentencesList = new List<List<string>>();
            foreach (var sentenceText in text.Split(delimiters))
            {
                var sentence = ParseSentence(sentenceText);
                if (sentence.Count > 0)
                    sentencesList.Add(sentence);
            }
            return sentencesList;
        }
    }
}