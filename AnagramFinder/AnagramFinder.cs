using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramFinderNS
{
    public class AnagramFinder : IInputChecker
    {
        public static void Main(string[] args)
        {

        }

        public List<List<String>> FindAllAnagramsWithDuplicateLetters(List<String> wordList)
        {
            Validate(wordList);

            var subList = new List<string>();
            var outerList = new List<List<string>>();
            bool trueChain = false;

            var canonicalForm = GetCanonicalForm(wordList);
            var iter = canonicalForm.ToList();

            for (int i = 0; i < iter.Count; i++)
            {
                var currentValue = iter[i].Value;
                var currentKey = iter[i].Key;

                if ((i+1 != iter.Count) && currentValue == iter[i + 1].Value)
                {
                    subList.Add(wordList[currentKey]);
                    trueChain = true;
                }

                else if (trueChain)
                {
                    subList.Add(wordList[currentKey]); 
                    subList.Sort();
                    outerList.Add(new List<string>(subList));
                    subList.Clear();
                    trueChain = false;
                }
            }

            return outerList.OrderBy(list => list[0]).ToList();
        }

        private Dictionary<int, string> GetCanonicalForm(List<string> wordList)
        {
            Dictionary<string, int> tracker = new Dictionary<string, int>();
            Dictionary<int, string> d = new Dictionary<int, string>(); 

            var sortedWords = new List<string>();
            int index = 0;

            foreach (string s in wordList)
            {
                if (tracker.ContainsKey(s))
                {
                    
                }
                else
                { 
                    sortedWords.Add(new string(GetCanonicalWord(s.ToLower())));
                    tracker.Add(s, 1);
                }
            }

            foreach (string s in sortedWords)
            {
                d.Add(index, s);
                index++;
            }

            var sortedDictTEnumerable = from entry in d orderby 
                entry.Value ascending select entry;

            return new Dictionary<int, string>(sortedDictTEnumerable);
        }

        private string GetCanonicalWord(string s)
        {
            var arr = (s.Distinct().ToArray());
            Array.Sort(arr);
            s = new string(arr);

            if (s.StartsWith(" "))
            {
                s = s.Remove(0, 1);
            }

            return s;
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
