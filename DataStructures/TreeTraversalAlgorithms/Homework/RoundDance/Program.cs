namespace RoundDance
{
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main(string[] args)
        {
            RoundDance roundDance = new RoundDance();
            ICollection<Dancer<int>> longestRoundDance = roundDance.FindLongestRoundDance();
            roundDance.PrintLongestRoundDance(longestRoundDance);
        }
    }
}
