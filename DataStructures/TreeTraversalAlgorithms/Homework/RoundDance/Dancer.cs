namespace RoundDance
{
    using System.Collections.Generic;

    internal class Dancer<T>
    {
        public Dancer(T value)
        {
            this.Value = value;
            this.Friends = new List<Dancer<T>>();
        }


        public T Value { get; set; }

        public ICollection<Dancer<T>> Friends { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
