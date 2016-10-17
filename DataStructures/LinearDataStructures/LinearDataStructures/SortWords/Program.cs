namespace SortWords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            SortWords();
        }

        public static void SortWords()
        {
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                string[] input = line.Split(' ');
                int length = input.Length;
                List<string> words = new List<string>(length);

                for (int i = 0; i < length; i++)
                {
                    words.Add(input[i]);
                }

                words = words.OrderBy(x => x).ToList();
                Console.WriteLine(string.Join(" ", words));
            }
        }
    }
}
