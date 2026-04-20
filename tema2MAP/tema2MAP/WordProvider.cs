using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tema2MAP
{
    public static class WordProvider
    {
        public static Dictionary<string, List<string>> LoadWords()
        {
            var result = new Dictionary<string, List<string>>();

            foreach (var line in File.ReadAllLines("words.txt"))
            {
                var parts = line.Split(':');
                var category = parts[0];
                var words = parts[1].Split(',').ToList();

                result[category] = words;
            }

            return result;
        }

        public static string GetRandomWord(List<string> words)
        {
            Random rnd = new Random();
            return words[rnd.Next(words.Count)];
        }
    }
}