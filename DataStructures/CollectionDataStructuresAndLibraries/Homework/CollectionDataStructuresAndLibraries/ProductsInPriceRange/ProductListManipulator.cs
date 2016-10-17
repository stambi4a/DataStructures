namespace ProductsInPriceRange
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    using Wintellect.PowerCollections;

    internal class ProductListManipulator
    {
        private const int ProductsInRangeCount = 10000;

        private const string pathSource = "../../Products.txt";
        private const string pathDest = "../../Results.txt";

        internal void FindProductsInPriceRange()
        {
            using (StreamReader reader = new StreamReader(pathSource))
            {
                OrderedBag<Product> productList = this.CreateProductList(reader);
                string line = reader.ReadLine();
                string[] input = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                double startPrice = double.Parse(input[0]);
                double endPrice = double.Parse(input[1]);
                var firstgivenProductsInRange = this.FindTopProductsByPrice(productList, startPrice, endPrice);
                this.PrintProductsInGivenRange(firstgivenProductsInRange);
            }    
        }

        private void PrintProductsInGivenRange(ICollection<Product> productList)
        {
            using (StreamWriter writer = new StreamWriter(pathDest, false))
            {
                foreach (var pair in productList)
                {
                    writer.WriteLine($"{pair.Price:F2} {pair.Name}");
                }
            }                      
        }

        private ICollection<Product> FindTopProductsByPrice(OrderedBag<Product> productList, double startPrice, double endPrice)
        {
            var firstProductStartPrice = productList.FirstOrDefault(x => x.Price >= startPrice);
            var lastProductEndPrice = productList.LastOrDefault(x => x.Price <= endPrice);
            var productsInRange = productList.Range(firstProductStartPrice, true, lastProductEndPrice, true);
            var firstgivenProductsInRange = productsInRange.Take(ProductsInRangeCount);

            return firstgivenProductsInRange.ToList();
        }

        private OrderedBag<Product> CreateProductList(StreamReader reader)
        {
            OrderedBag<Product> productList = new OrderedBag<Product>();
            int entriesCount = int.Parse(reader.ReadLine());
            int index = 0;
            while (index < entriesCount)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                string[] input = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string productName = input[0];
                double productPrice = double.Parse(input[1]);
                Product product = new Product(productName, productPrice);
                productList.Add(product);
                index++;
            }

            return productList;
        }
    }
}
