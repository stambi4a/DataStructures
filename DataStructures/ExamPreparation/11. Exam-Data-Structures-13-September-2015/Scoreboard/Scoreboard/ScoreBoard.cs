namespace Scoreboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ScoreBoard
    {
        private const string DuplicatedUserMessage = "Duplicated user";
        private const string RegisteredUserMessage = "User registered";
        private const string DuplicatedGameMessage = "Duplicated game";
        private const string RegisteredGameMessage = "Game registered";
        private const string CannotAddScoreMessage = "Cannot add score";
        private const string ScoreAddedMessage = "Score added";
        private const string NoScoreMessage = "No score";
        private const string GameNotFoundMessage = "Game not found";
        private const string NoMatchesMessage = "No matches";
        private const string GameDeletedMessage = "Game deleted";
        private const string CannotDeleteGameMessage = "Cannot delete game";
        private const int ScoreResultsCount = 10;
        public ScoreBoard()
        {
            this.Games = new SortedDictionary<string, Game>();
            this.Users = new Dictionary<string, User>();
            this.Scores = new Dictionary<string, SortedDictionary<int, SortedDictionary<string, int>>>();
        }

        public SortedDictionary<string, Game> Games { get; set; }
        public Dictionary<string, User> Users { get; set; }
        public Dictionary<string, SortedDictionary<int, SortedDictionary<string, int>>> Scores { get; set; }

        public void ParseCommands(string input)
        {
            string[] commandSequence = input.Split();
            string result = null;
            switch (commandSequence[0])
            {
                case "RegisterUser" :
                    {
                        result = this.RegisterUser(commandSequence[1], commandSequence[2]);
                    }

                    break;
                case "RegisterGame":
                    {
                        result = this.RegisterGame(commandSequence[1], commandSequence[2]);
                    }

                    break;

                case "AddScore":
                    {
                        int score = int.Parse(commandSequence[5]);
                        result = this.AddScore(
                            commandSequence[1],
                            commandSequence[2],
                            commandSequence[3],
                            commandSequence[4],
                            score);
                    }

                    break;
                case "ShowScoreboard":
                    {
                        result = this.ShowScoreboard(commandSequence[1]);
                    }

                    break;

                case "ListGamesByPrefix":
                    {
                        result = this.ListGamesByPrefix(commandSequence[1]);
                    }

                    break;
                case "DeleteGame":
                    {
                        result = this.DeleteGame(commandSequence[1], commandSequence[2]);
                    }

                    break;

                default:
                    {
                        throw new ArgumentException("Invalid command!");
                    }
            }

            Console.WriteLine(result);
        }
        public string RegisterUser(string userName, string userPassword)
        {
            if (this.Users.ContainsKey(userName))
            {
                return DuplicatedUserMessage;
            }

            this.Users.Add(userName, new User(userName, userPassword));

            return RegisteredUserMessage;
        }

        public string RegisterGame(string gameName, string gamePassword)
        {
            if (this.Games.ContainsKey(gameName))
            {
                return DuplicatedGameMessage;
            }

            this.Games.Add(gameName, new Game(gameName, gamePassword));

            return RegisteredGameMessage;
        }

        public string AddScore(string userName, string userPassword, string gameName, string gamePassword, int score)
        {
            if(!this.Users.ContainsKey(userName) ||
                !this.Games.ContainsKey(gameName) ||
                !this.Users[userName].Password.Equals(userPassword) ||
                !this.Games[gameName].Password.Equals(gamePassword))
            {
                return CannotAddScoreMessage;
            }

            if (!this.Scores.ContainsKey(gameName))
            {
                this.Scores.Add(gameName, new SortedDictionary<int, SortedDictionary<string, int>>(new ReverseNumberComparer()));
            }

            if (!this.Scores[gameName].ContainsKey(score))
            {
                this.Scores[gameName].Add(score, new SortedDictionary<string, int>());
            }

            if (!this.Scores[gameName][score].ContainsKey(userName))
            {
                this.Scores[gameName][score].Add(userName, 0);
            }

            this.Scores[gameName][score][userName]++;

            return ScoreAddedMessage;
        }

        public string ShowScoreboard(string gameName)
        {
            if (!this.Games.ContainsKey(gameName))
            {
                return GameNotFoundMessage;
            }

            if (!this.Scores.ContainsKey(gameName))
            {
                return NoScoreMessage;
            }

            int index = 1;
            StringBuilder results = new StringBuilder();
            foreach (var score in this.Scores[gameName])
            {
                foreach (var user in score.Value)
                {
                    int count = user.Value;
                    for (int i = 0; i < count; i++)
                    {
                        results.Append($"#{index} {user.Key} {score.Key}{Environment.NewLine}");
                        index++;
                        if (index > ScoreResultsCount)
                        {
                            break;
                        }
                    }

                    if (index > ScoreResultsCount)
                    {
                        break;
                    }

                }

                if (index > ScoreResultsCount)
                {
                    break;
                }
            }

            results.Length -= Environment.NewLine.Length;
            return results.ToString();
        }

        public string ListGamesByPrefix(string prefix)
        {
            StringBuilder results = new StringBuilder();

           /* var gamesStartingWithGivenPrefix =
                this.Games.Keys.Where(x => x.StartsWith(prefix)).Take(ScoreResultsCount).ToArray();*/
            var matched = this.Games.Keys.FirstOrDefault(x => x.StartsWith(prefix));
            if (matched == null)
            {
                return NoMatchesMessage;
            }

            results.Append(matched + ", ");
            int index = 2;
            while (index <= ScoreResultsCount)
            {
                matched = this.Games.Keys.FirstOrDefault(x => x.CompareTo(matched) > 0 && x.StartsWith(prefix));
                if (matched == null)
                {
                    break;
                }

                results.Append(matched + ", ");
                index++;
            }
            
           /* int count = gamesStartingWithGivenPrefix.Count();
            if (count == 0)
            {
                return NoMatchesMessage;
            }


            for (int i = 0; i < count - 1; i++)
            {
                results.Append(gamesStartingWithGivenPrefix[i] + ", ");
            }

            results.Append(gamesStartingWithGivenPrefix[count - 1]);*/

            if (results.Length > 2)
            {
                results.Length -= 2;
            }

            return results.ToString();
        }

        public string DeleteGame(string gameName,string gamePassword)
        {
            if (!this.Games.ContainsKey(gameName) || !this.Games[gameName].Password.Equals(gamePassword))
            {
                return CannotDeleteGameMessage;
            }

            this.Games.Remove(gameName);
            this.Scores.Remove(gameName);

            return GameDeletedMessage;
        }
    }
}
