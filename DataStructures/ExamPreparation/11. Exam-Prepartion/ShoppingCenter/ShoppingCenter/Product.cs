namespace ShoppingCenter
{
    using System;
    using System.Globalization;

    public class Product : IComparable<Product>
    {
        public Product(string producer, string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
            this.Producer = producer;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }

        public int CompareTo(Product other)
        {
            if (this == other)
            {
                return 0;
            }

            int result = this.Name.CompareTo(other.Name);
            if (result == 0)
            {
                result = this.Producer.CompareTo(other.Producer);
                if (result == 0)
                {
                    return this.Price.CompareTo(other.Price);
                }
            }

            return result;
        }

        public override string ToString()
        {
            return $"{this.Name};{this.Producer};{this.Price:f2}";
        }
    }
}
