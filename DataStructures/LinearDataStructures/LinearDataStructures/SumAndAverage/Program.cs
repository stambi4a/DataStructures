namespace SumAndAverage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            FindSumAndAverage();
        }

        public static void FindSumAndAverage()
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

                int sumNumbers = numbers.Sum();
                Console.Write("Sum={0}; ", sumNumbers);
                double averageElements = sumNumbers / (double)length;
                Console.WriteLine("Average={0}", averageElements);
            }
        }
    }
}
