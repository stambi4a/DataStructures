namespace LongestSubsequence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            FindLongestSubsequence();
        }

        public static void FindLongestSubsequence()
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
                List<int> numbers = new List<int>(length);

                for (int i = 0; i < length; i++)
                {
                    numbers.Add(int.Parse(input[i]));
                }

                int indexBegin = 0;
                int count = 1;
                int maxCount = 1;
                for (int i = 1; i < length; i++)
                {
                    if (numbers[i] == numbers[i - 1])
                    {
                        count++;
                    }
                    else
                    {
                        if (maxCount < count)
                        {
                            maxCount = count;
                            indexBegin = i - count;
                        }

                        count = 1;
                    }

                    if (i == length - 1 && count > maxCount)
                    {
                        maxCount = count;
                        indexBegin = i - count + 1;
                    }
                }

                List<int> subsequence = new List<int>();
                for (int i = indexBegin; i < indexBegin + maxCount; i++)
                {
                    subsequence.Add(numbers[i]);
                }

                Console.WriteLine(string.Join(" ", subsequence));
            }
        }
    }
}
