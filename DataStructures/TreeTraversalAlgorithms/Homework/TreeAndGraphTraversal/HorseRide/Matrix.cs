namespace HorseRide
{
    using System;

    public class Matrix
    {
        internal Matrix(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            this.Array = new Cell<int>[this.Height, this.Width];
            this.InputArray();
        }

        public Cell<int>[,] Array { get; set; }

        public int Height { get; }

        public int Width { get; set; }

        public Cell<int> this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= this.Height)
                {
                    throw new ArgumentException($"i should be in the range [0..{this.Height - 1}]");
                }

                if (j < 0 || j >= this.Width)
                {
                    throw new ArgumentException($"j should be in the range [0..{this.Width - 1}]");
                }

                return this.Array[i, j];
            }

            set
            {
                if (i < 0 || i >= this.Height)
                {
                    throw new ArgumentException($"i should be in the range [0..{this.Height - 1}]");
                }

                if (j < 0 || j >= this.Width)
                {
                    throw new ArgumentException($"j should be in the range [0..{this.Width - 1}]");
                }

                if (value.Value < 0)
                {
                    throw new ArgumentException("Value should be non-negative number.");
                }

                this.Array[i, j] = value;
            }
        }

        private void InputArray()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this[i, j] = new Cell<int>(0, i, j);
                }
            }
        }
    }
}
