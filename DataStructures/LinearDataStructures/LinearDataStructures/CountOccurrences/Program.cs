namespace CountOccurrences
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            RemoveOddOccurrentNumbers();
        }

        public static void RemoveOddOccurrentNumbers()
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

                for (int i = 0; i < length; i++)
                {
                    int j = 0;
                    for (j = 0; j < i; j++)
                    {
                        if (numbers[i] == numbers[j])
                        {
                            break;
                        }
                    }

                    if (j == i)
                    {
                        int count = 1;
                        for (j = i + 1; j < length; j++)
                        {
                            if (numbers[j] == numbers[i])
                            {
                                count++;
                            }
                        }

                        Console.WriteLine("{0} -> {1} times", numbers[i], count);
                    }
                }
            }
        }
    }
}
