using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private static readonly char[] separators
            = new [] { ' ', '.', ',', '!', '?', ':', '-', '\n', '\r' };

        private Dictionary<string, Dictionary<int, List<int>>> dictionary
            = new Dictionary<string, Dictionary<int, List<int>>>();

        public void Add(int id, string documentText)
        {
            var textWords = documentText.Split(separators);

            var documentPosition = 0;
            foreach (var e in textWords)
            {
                if (!dictionary.ContainsKey(e))
                    dictionary.Add(e, new Dictionary<int, List<int>>());

                if (!dictionary[e].ContainsKey(id))
                    dictionary[e].Add(id, new List<int>());

                dictionary[e][id].Add(documentPosition);
                documentPosition += e.Length + 1;
            }
        }

        public List<int> GetIds(string word)
        {
            var result = new List<int>();
            if (dictionary.ContainsKey(word))
                foreach (var e in dictionary[word].Keys)
                    result.Add(e);

            return result;
        }

        public List<int> GetPositions(int id, string word)
        {
            var result = new List<int>();
            if (dictionary.ContainsKey(word)
                    && dictionary[word].ContainsKey(id))
                result = dictionary[word][id];

            return result;
        }

        public void Remove(int id)
        {
            foreach (var e in dictionary.Keys.ToList())
                if (dictionary[e].ContainsKey(id))
                    dictionary[e].Remove(id);
        }
    }
}