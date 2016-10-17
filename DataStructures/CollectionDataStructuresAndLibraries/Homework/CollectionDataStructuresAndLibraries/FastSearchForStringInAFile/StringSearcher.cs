namespace FastSearchForStringInAFile
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class StringSearcher
    {
        private const string PathSource = "../../Text.txt";
        private const string PathWordsToSearch = "../../MostFrequentWords.txt";
        private const string PathDest = "../../Results.txt";      

        internal void SearchSusbtringsInText()
        {
            IDictionary<string, int> substringsToSearch = this.GetSearchWords();
            this.SearchInTextWithIndexOf(substringsToSearch);
            this.PrintOccurrencesOfSubstrings(substringsToSearch);              
        }

        private void PrintOccurrencesOfSubstrings(IDictionary<string, int> substringsToSearch)
        {
            using (StreamWriter writer = new StreamWriter(PathDest, false))
            {
                foreach (var pair in substringsToSearch)
                {
                    writer.WriteLine($"{pair.Key} -> {pair.Value}");
                }
            }              
        }
        private void SearchInTextWithIndexOf(IDictionary<string, int> substringsToSearch)
        {
            var substrings = new List<string>(substringsToSearch.Keys);

            using (StreamReader reader = new StreamReader(PathSource))
            {
                while (true)
                {
                    string input = reader.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        break;
                    }

                    int length = input.Length;
                    foreach (var substring in substrings)
                    {
                        int startIndex = 0;
                        while (startIndex < length)
                        {
                            startIndex = input.IndexOf(substring, startIndex, StringComparison.CurrentCultureIgnoreCase);
                            if (startIndex == -1)
                            {
                                break;
                            }

                            substringsToSearch[substring]++;
                            startIndex++;
                        }

                    }
                }
            }
        }

        private IDictionary<string, int> GetSearchWords()
        {
            using (StreamReader reader = new StreamReader(PathWordsToSearch))
            {
                IDictionary<string, int> substringsToSearch = new Dictionary<string, int>();
                int index = 0;
                while (true)
                {
                    string substring = reader.ReadLine();
                    if (string.IsNullOrEmpty(substring))
                    {
                        break;
                    }

                    if (!substringsToSearch.ContainsKey(substring))
                    {
                        substringsToSearch.Add(substring, 0);
                    }

                    index++;
                }

                return substringsToSearch;
            }   
        }
    }
}
