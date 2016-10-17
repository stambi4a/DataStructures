namespace RoundDance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class RoundDance
    {
        internal RoundDance()
        {
            this.Dancers = new Dictionary<int, Dancer<int>>();
            this.InputGraph();
        }

        public int NumberOfFriendhips { get; set; }

        public int LeadingManNumber { get; set; }

        public Dictionary<int, Dancer<int>> Dancers { get; set; }

        private void InputGraph()
        {
            this.NumberOfFriendhips = int.Parse(Console.ReadLine());
            this.LeadingManNumber = int.Parse(Console.ReadLine());
            for (int i = 0; i < this.NumberOfFriendhips; i++)
            {
                string[] input = Console.ReadLine().Split();
                int firstDancerValue = int.Parse(input[0]);
                int secondDancerValue = int.Parse(input[1]);

                if (!this.Dancers.ContainsKey(firstDancerValue))
                {
                    Dancer<int> firstDancer = new Dancer<int>(firstDancerValue);
                    this.Dancers.Add(firstDancerValue, firstDancer);
                }

                if (!this.Dancers.ContainsKey(secondDancerValue))
                {
                    Dancer<int> secondDancer = new Dancer<int>(secondDancerValue);
                    this.Dancers.Add(secondDancerValue, secondDancer);
                }

                this.Dancers[firstDancerValue].Friends.Add(this.Dancers[secondDancerValue]);
                this.Dancers[secondDancerValue].Friends.Add(this.Dancers[firstDancerValue]);
            }
        }

        internal ICollection<Dancer<int>> FindLongestRoundDance()
        {
            Dancer<int> leadingDancer = this.Dancers[this.LeadingManNumber];
            Stack<Dancer<int>> longestRoundDance = new Stack<Dancer<int>>();
            foreach (var dancer in leadingDancer.Friends)
            {
                var currentDancer = dancer;
                Stack<Dancer<int>> roundDance = new Stack<Dancer<int>>();
                var previousDancer = currentDancer;
                roundDance.Push(leadingDancer);
                var nextDancer = currentDancer.Friends.FirstOrDefault(x => x != leadingDancer);
                while (nextDancer != default(Dancer<int>))
                {
                    currentDancer = nextDancer;
                    roundDance.Push(previousDancer);
                    var peekedDancer = roundDance.Peek();
                    nextDancer = nextDancer.Friends.FirstOrDefault(x => x != peekedDancer);
                    previousDancer = currentDancer;
                }

                roundDance.Push(previousDancer);

                if (roundDance.Count > longestRoundDance.Count)
                {
                    longestRoundDance = roundDance;
                }
            }

            return longestRoundDance.Reverse().ToList();
        }

        internal void PrintLongestRoundDance(ICollection<Dancer<int>> longestRoundDance)
        {
            int longestRoundDanceCount = longestRoundDance.Count;
            Console.WriteLine("The longest round dance is: {0} with count {1}", string.Join(" -> ", longestRoundDance), longestRoundDanceCount);         
        }
    }
}