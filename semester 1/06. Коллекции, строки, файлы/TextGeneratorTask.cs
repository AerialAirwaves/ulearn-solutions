using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        private static string SafeExtractFromDictionary(Dictionary<string, string> dictionary, string key)
        {
            return (dictionary.ContainsKey(key)) ? dictionary[key] : null;
        }

        private static string ChooseNextWord(
            Dictionary<string, string> nextWords,
            string previousWord, string currentWord)
        {
            var trigramKey = previousWord + " " + currentWord;
            return SafeExtractFromDictionary(nextWords, trigramKey) 
                ?? SafeExtractFromDictionary(nextWords, currentWord);
        }

        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var result = new StringBuilder();
            result.Append(phraseBeginning);
            var beginningWords = phraseBeginning.Split();
            var previousWord = (beginningWords.Length < 2)
                ? ""
                : beginningWords[beginningWords.Length - 2];
            var currentWord = (beginningWords.Length == 0) 
                ? ""
                : beginningWords[beginningWords.Length - 1];
            
            for (int i = 0; i < wordsCount; i++)
            {
                var nextWord = ChooseNextWord(nextWords, previousWord, currentWord);
                if (nextWord is null)
                    break;
                (previousWord, currentWord) = (currentWord, nextWord);
                result.Append(" ");
                result.Append(nextWord);
            }
            return result.ToString();
        }
    }
}