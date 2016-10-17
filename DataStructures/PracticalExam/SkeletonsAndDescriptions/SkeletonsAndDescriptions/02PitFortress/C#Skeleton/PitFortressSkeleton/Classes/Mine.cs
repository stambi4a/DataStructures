namespace PitFortress.Classes
{
    using System;
    using System.Collections.Generic;

    using PitFortress.Interfaces;

    public class Mine : IMine
    {
        private int xCoordinate;
        private int delay;
        private int damage;

        public Mine(int id, int delay, int damage, int xCoordinate, Player player)
        {
            this.Id = id;
            this.Delay = delay;
            this.Damage = damage;
            this.XCoordinate = xCoordinate;
            this.Player = player;
        }

        public int Id { get; private set; }

        public int Delay { get; set; }

        public int Damage
        {
            get
            {
                return this.damage;
            }

            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Damage should be in the range [1..100]");
                }

                this.damage = value;
            }
        }

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

        public Player Player { get; private set; }

        public int CompareTo(Mine other)
        {
            int result = this.Delay - other.Delay;
            if (result == 0)
            {
                result = this.Id - other.Id;
            }

            return result;
        }

    }
}
