namespace CountSymbols
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SymbolCounter
    {
        public void CountSymbols()
        {
            Dictionary.Dictionary<char, int> charactersByCount = new Dictionary.Dictionary<char, int>();
            string text = this.InputText();
            foreach (var character in text)
            {
                if (!charactersByCount.ContainsKey(character))
                {
                    charactersByCount.Add(character, 0);
                }

                charactersByCount[character]++;
            }

            var sortedCharactersByCount = charactersByCount.OrderBy(x => x.Key);
            this.PrintSymbolsCount(sortedCharactersByCount);
        }

        private string InputText()
        {
            return Console.ReadLine();
        }

        private void PrintSymbolsCount(IEnumerable<KeyValue<char, int>> charactersByCount)
        {
            foreach (var pair in charactersByCount)
            {
                Console.WriteLine($"{pair.Key} : {pair.Value} time/s");
            }
        }
    }
}
