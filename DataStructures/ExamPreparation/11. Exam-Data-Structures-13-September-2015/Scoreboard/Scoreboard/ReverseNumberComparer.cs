namespace Scoreboard
{
    using System.Collections.Generic;

    public class ReverseNumberComparer : IComparer<int>
    {
        public int Compare(int score1, int score2)
        {
            return score2 - score1;
        }
    }
}
