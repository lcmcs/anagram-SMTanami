using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramFinderNS
{
    public class AnagramFinder : IInputChecker
    {
        public static void Main(string[] args)
        { }

        public List<List<String>> FindAllAnagramsWithDuplicateLetters(List<String> wordList)
        {
            Validate(wordList);

            var trueChain = false;
            var subList = new List<string>();
            var outerList = new List<List<string>>();
            var canonicalForm = GetCanonicalForm(wordList).ToArray();

            for (int i = 0; i < canonicalForm.Length; i++)
            {
                var currentValue = canonicalForm[i].Value;
                var currentKey = canonicalForm[i].Key;
                var nextValue = i + 1 < canonicalForm.Length ? canonicalForm[i + 1].Value : " ";

                if (currentValue == nextValue)
                {
                    subList.Add(wordList[currentKey]);
                    trueChain = true;
                }

                else if (trueChain)
                {
                    subList.Add(wordList[currentKey]);
                    outerList.Add(new List<string>(subList));
                    subList.Clear();
                    trueChain = false;
                }
            }

            outerList.ForEach(subList => subList.Sort());

            return outerList.OrderBy(list => list[0]).ToList();
        }

        private Dictionary<int, string> GetCanonicalForm(List<string> wordList)
        {
            var tracker = new HashSet<string>();
            var canonicalForm = new Dictionary<int, string>();
            var index = 0;

            foreach (var word in wordList)
            {
                if (tracker.Contains(word))
                    index++;

                else
                {
                    canonicalForm.Add(index++, GetCanonicalWord(word));
                    tracker.Add(word);
                }
            }

            return new Dictionary<int, string>(from entry in canonicalForm orderby entry.Value ascending select entry);
        }

        private string GetCanonicalWord(string s)
        {
            var array = s.ToLower().Distinct().ToArray();
            Array.Sort(array);

            if (array[0] == (' '))
            {
                return new string(array).Substring(1);
            }

            return new string(array);
        }

        public void Validate(List<string> wordList)
        {
            if (wordList is null)
            {
                throw new ArgumentException("Param 'wordList' is null or empty");
            }

            if (wordList.Contains(null) || wordList.Contains(""))
            {
                throw new ArgumentException("Param 'wordList' contains null or empty val");
            }
        }
    }

    public interface IInputChecker
    {
        void Validate(List<string> list);
    }
}
