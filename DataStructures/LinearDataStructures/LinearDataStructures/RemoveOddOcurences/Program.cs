namespace RemoveOddOcurences
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

                int index = 0;
                while (true)
                {
                    int lengthList = numbers.Count;
                    if (index > lengthList - 1)
                    {
                        break;
                    }

                    int count = 1;
                    for (int i = 0; i < lengthList; i++)
                    {
                        if (i != index && numbers[i] == numbers[index])
                        {
                            count++;
                        }
                    }

                    if (count % 2 == 1)
                    {
                        numbers = numbers.Where(x => x != numbers[index]).ToList();
                    }
                    else
                    {
                        index++;
                    }
                }

                Console.WriteLine(string.Join(" ", numbers));
            }
        }
    }
}
