namespace PitFortress.Classes
{
    using System;

    using PitFortress.Interfaces;

    public class Player : IPlayer
    {
        private int mineRadius;
        public Player(string name, int radius, int score = 0)
        {
            this.Name = name;
            this.Radius = radius;
            this.Score = score;
        }
        public int CompareTo(Player other)
        {
            int result = this.Score - other.Score;
            if (result == 0)
            {
                result = this.Name.CompareTo(other.Name);
            }

            return result;
        }

        public string Name { get; private set; }

        public int Radius
        {
            get
            {
                return this.mineRadius;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Mine radius should be positive number.");
                }

                this.mineRadius = value;
            }
        }

        public int Score { get; set; }
    }
}
