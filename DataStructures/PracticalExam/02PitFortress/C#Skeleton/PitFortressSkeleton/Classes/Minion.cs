namespace PitFortress.Classes
{
    using System;

    using PitFortress.Interfaces;

    public class Minion : IMinion
    {
        private int xCoordinate;
        public Minion(int id, int xCoordinate, int health = 100)
        {
            this.Id = id;
            this.XCoordinate = xCoordinate;
            this.Health = health;
            this.IsKilled = false;
        }

        public int Id { get; private set; }

        public int XCoordinate
        {
            get
            {
                return this.xCoordinate;
            }

            private set
            {
                if (value < 0 || value > 1000000)
                {
                    throw new ArgumentException("X coordinate should be in  the range[0..1000000]");
                }

                this.xCoordinate = value;
            }
        }

        public bool IsKilled { get; set; }

        public int Health { get; set; }

        public int CompareTo(Minion other)
        {
            if (this == other)
            {
                return 0;
            }

            int result = this.XCoordinate - other.XCoordinate;
            if (result == 0)
            {
                result = this.Id - other.Id;
            }

            return result;
        }

    }
}
