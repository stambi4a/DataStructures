namespace BunnyWars.Core
{
    using System;

    public class Bunny : IComparable<Bunny>
    {
        private const int InitialBunnyHealth = 100;
        private const int BunnyDamage = 30;
        private const int ScoreIncreasePerKill = 1;

        public Bunny(string name, int team, int roomId)
        {
            this.Health = 100;
            this.Score = 0;
            this.Name = name;
            this.RoomId = roomId;
            this.Team = team;
        }

        public int RoomId { get; set; }

        public string Name { get; private set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Team { get; private set; }

        public int CompareTo(Bunny other)
        {
            return other.Name.CompareTo(this.Name);
        }
    }
}
