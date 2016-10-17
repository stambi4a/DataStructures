namespace ProductsInPriceRange
{
    using System;
    internal class Product : IComparable
    {
        internal Product(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }

        internal string Name { get; }
        internal double Price { get; }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 0;
            }

            Product otherProduct = obj as Product;
            if (otherProduct != null)
            {
                return this.Price.CompareTo(otherProduct.Price);
            }
            else
            {
                throw new ArgumentException("Object is not a product.");
            }
        }
    }
}
