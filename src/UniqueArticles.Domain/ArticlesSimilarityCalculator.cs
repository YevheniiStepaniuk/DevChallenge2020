using System;
using System.Collections.Generic;
using System.Linq;

namespace UniqueArticles.Domain
{
    public class ArticlesSimilarityCalculator
    {
        public double CalculateSimilarityPercent(string article, string duplicate)
        {
            var (articleWordCount, articleWords) = GetWords(article);
            var (duplicateWordCount, duplicateWords) = GetWords(duplicate);

            var similarWordCount = articleWords.Keys.Intersect(duplicateWords.Keys).Sum(word =>
            {
                var articleEntries = articleWords.GetValueOrDefault(word, 0);
                var duplicateEntries = duplicateWords.GetValueOrDefault(word, 0);

                return Math.Min(articleEntries, duplicateEntries);
            });

            var totalCount = Math.Max(articleWordCount, duplicateWordCount);

            return ((double) similarWordCount / totalCount) * 100;
        }

        private (int count, Dictionary<string, int> words) GetWords(string row)
        {
            var words = row.Replace(".", "").Replace("!", "".Replace(",", "")).Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var uniqueWords = words
                .GroupBy(s => s)
                .ToDictionary(s => s.Key, s => s.Count());

            return (words.Length, uniqueWords);
        }
    }
}