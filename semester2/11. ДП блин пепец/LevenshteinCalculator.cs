using System;
using System.Linq;
using System.Collections.Generic;

using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        private const double InsertRemovalCost = 1;
        
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var result = new List<ComparisonResult>();
            var documentsLeft = documents.ToList();
            
            foreach (var first in documents)
            {
                documentsLeft.Remove(first);
                foreach (var second in documentsLeft)
                    result.Add(CompareDocuments(first, second));
            }
            
            return result;
        }

        private ComparisonResult CompareDocuments(DocumentTokens first, DocumentTokens second)
        {
            var current = new double[first.Count + 1];
            for (var i = 0; i <= first.Count; i++)
                current[i] = i;
            var previous = default(double[]);

            for (var j = 1; j <= second.Count; j++)
            {
                previous = current;
                current = new double[first.Count + 1];
                current[0] = j;
                for (var i = 1; i <= first.Count; i++)
                    current[i] = first[i - 1] == second[j - 1]
                        ? previous[i - 1]
                        : Math.Min(Math.Min(current[i - 1], previous[i]) + InsertRemovalCost,
                            previous[i - 1] + TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]));
            }
            
            return new ComparisonResult(first, second, current[first.Count]);
        }
    }
}
