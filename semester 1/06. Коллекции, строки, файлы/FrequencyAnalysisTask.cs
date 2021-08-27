using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        private static void IncrementFrequency(
            Dictionary<string,Dictionary<string, int>> dictionary,
            string prefix, string postfix)
        {
            if (!dictionary.ContainsKey(prefix))
                dictionary[prefix] = new Dictionary<string, int>();

            if (dictionary[prefix].ContainsKey(postfix))
                dictionary[prefix][postfix]++;
            else
                dictionary[prefix][postfix] = 1;
        }

        private static Dictionary<string, Dictionary<string, int>>
            MakeNgramsFrequenciesDictionaries(List<List<string>> text)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (var i = 0; i < sentence.Count - 1; i++)
                {
                    IncrementFrequency(result, sentence[i], sentence[i+1]);
                    if (i < sentence.Count - 2)
                        IncrementFrequency(result, sentence[i] + " " + sentence[i + 1], sentence[i + 2] );
                }
            }
            return result;
        }

        private static string GetMostFrequentPostfix(Dictionary<string, int> postfixFrequenciesDict)
        {
            var maxFrequency = 0;
            var result = "";
            foreach (var postfix in postfixFrequenciesDict.Keys)
                if ((postfixFrequenciesDict[postfix] > maxFrequency)
                    || ((postfixFrequenciesDict[postfix] == maxFrequency)
                    && ((string.CompareOrdinal(postfix, result) < 0))))
                {
                    maxFrequency = postfixFrequenciesDict[postfix];
                    result = postfix;
                }
            return result;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var ngramFrequenciesDictionary = MakeNgramsFrequenciesDictionaries(text);
            var result = new Dictionary<string, string>();
            foreach (var key in ngramFrequenciesDictionary.Keys)
                result[key] = GetMostFrequentPostfix(ngramFrequenciesDictionary[key]);
            return result;
        }
    }
}