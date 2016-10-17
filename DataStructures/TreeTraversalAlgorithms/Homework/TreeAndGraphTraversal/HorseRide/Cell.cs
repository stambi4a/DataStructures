namespace HorseRide
{
    using System.Collections.Generic;

    public class Cell<T>
    {
        public Cell(T value, int row, int column)
        {
            this.Value = value;
            this.Row = row;
            this.Column = column;
            this.Children = new List<Cell<T>>();
        } 

        public int Row { get; }

        public int Column { get; }

        public T Value { get; set; }

        public List<Cell<T>> Children { get; set; } 
    }
}
