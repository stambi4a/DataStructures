namespace Scoreboard
{
    using System;

    public class Program
    {
        private static void Main(string[] args)
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                if (input.Equals("End"))
                {
                    break;
                }

                scoreBoard.ParseCommands(input);
            }          
        }
    }
}
