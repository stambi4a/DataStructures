namespace PitFortress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using PitFortress.Classes;
    using PitFortress.Interfaces;
    using Wintellect.PowerCollections;

    public class PitFortressCollection : IPitFortress
    {
        private Dictionary<string, Player> players;
        private SortedDictionary<int, Mine> minesById;
        private SortedSet<Mine> mines;
        private SortedDictionary<int, SortedSet<Minion>> minionsByXCoord;
        private SortedSet<Minion> minions;
        public PitFortressCollection()
        { 
            this.players = new Dictionary<string, Player>();
            this.minesById = new SortedDictionary<int, Mine>();
            this.minionsByXCoord = new SortedDictionary<int, SortedSet<Minion>>();
            this.minions = new SortedSet<Minion>();
            this.mines = new SortedSet<Mine>();
            this.NextMineId = 1;
            this.NextMinionId = 1;
        }
        public int NextMinionId { get; private set; }

        public int NextMineId { get; private set; }

        public int PlayersCount => this.players.Count;

        public int MinionsCount => this.minions.Count;

        public int MinesCount => this.minesById.Count;

        public void AddPlayer(string name, int mineRadius)
        {
            if (this.players.ContainsKey(name))
            {
                throw new ArgumentException("Player with the same name is already in the game.");
            }

            Player player = new Player(name, mineRadius);
            this.players.Add(name, player);
        }

        public void AddMinion(int xCoordinate)
        {
            Minion minion = new Minion(this.NextMinionId++, xCoordinate);
            if (!this.minionsByXCoord.ContainsKey(xCoordinate))
            {
                this.minionsByXCoord.Add(xCoordinate, new SortedSet<Minion>());
            }

            this.minionsByXCoord[xCoordinate].Add(minion);
            this.minions.Add(minion);
        }

        public void SetMine(string playerName, int xCoordinate, int delay, int damage)
        {
            if (!this.players.ContainsKey(playerName))
            {
                throw new ArgumentException("No player with player name exists.");
            }

            if (delay > 10000 || delay < 1)
            {
                throw new ArgumentException("Delay should be in the range[1..10000]");
            }

            Mine mine = new Mine(this.NextMineId++, delay, damage, xCoordinate, this.players[playerName]);
            this.minesById.Add(mine.Id, mine);
            this.mines.Add(mine);
        }

        public IEnumerable<Minion> ReportMinions()
        {
            return this.minions;
        }

        public IEnumerable<Player> Top3PlayersByScore()
        {
            if (this.PlayersCount < 3)
            {
                throw new ArgumentException("Player count should be n; less than 3");
            }

            ICollection<Player> topThreePlayersByScore = new List<Player>();
            for (int i = 0; i < 3; i++)
            {
                var topPlayer = this.players.Values.Max();
                this.players.Remove(topPlayer.Name);
                topThreePlayersByScore.Add(topPlayer);
            }

            foreach (var player in topThreePlayersByScore)
            {
                this.players.Add(player.Name, player);
            }

            return topThreePlayersByScore;

        }

        public IEnumerable<Player> Min3PlayersByScore()
        {
            if (this.PlayersCount < 3)
            {
                throw new ArgumentException("Player count should be n; less than 3");
            }

            ICollection<Player> bottomThreePlayersByScore = new List<Player>();
            for (int i = 0; i < 3; i++)
            {
                var topPlayer = this.players.Values.Min();
                this.players.Remove(topPlayer.Name);
                bottomThreePlayersByScore.Add(topPlayer);
            }

            foreach (var player in bottomThreePlayersByScore)
            {
                this.players.Add(player.Name, player);
            }

            return bottomThreePlayersByScore;
        }

        public IEnumerable<Mine> GetMines()
        {
            return this.mines;
        }

        public void PlayTurn()
        {
            List<Mine> explodedMines = new List<Mine>();
            foreach (var mine in this.minesById.Values)
            {
                mine.Delay--;
                if (mine.Delay == 0)
                {
                    explodedMines.Add(mine);
                    int radiusBegin = mine.XCoordinate - mine.Player.Radius;
                    int radiusEnd= mine.XCoordinate + mine.Player.Radius;
                    var minionsInTheRadius = 
                        this.minionsByXCoord.Where(x => x.Key >= radiusBegin && x.Key <= radiusEnd)
                            .SelectMany(x => x.Value).ToList();
                    for (int i = 0; i < minionsInTheRadius.Count; i++)
                    {
                        minionsInTheRadius[i].Health -= mine.Damage;
                        if (!minionsInTheRadius[i].IsKilled && minionsInTheRadius[i].Health <= 0)
                        {
                            minionsInTheRadius[i].IsKilled = true;
                            this.minionsByXCoord[minionsInTheRadius[i].XCoordinate].Remove(minionsInTheRadius[i]);
                            this.minions.Remove(minionsInTheRadius[i]);
                            mine.Player.Score++;
                        }
                    }
                   
                }
            }

            foreach (var mine in explodedMines)
            {
                this.minesById.Remove(mine.Id);
                this.mines.Remove(mine);
            }
        }
    }
}
