namespace ShoppingCenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ShoppingCenter
    {
        private const string ProductAddedMessage = "Product added";
        private const string NoProductsFoundMessage = "No products found";
        private const string XProductsDeleted = "{0} products deleted";

        private Dictionary<string, Dictionary<string, SortedDictionary<Product, int>>> productsByProducerAndName;
/*        private Dictionary<string, SortedDictionary<Product, int>> productsByName;*/
        private Dictionary<string, SortedDictionary<Product, int>> productsByProducer;
        private SortedDictionary<decimal, Dictionary<Product, int>> productsByPrice;

        public ShoppingCenter()
        {
            this.productsByProducerAndName = new Dictionary<string, Dictionary<string, SortedDictionary<Product, int>>>();
            /*this.productsByName = new Dictionary<string, SortedDictionary<Product, int>>();*/
            this.productsByProducer = new Dictionary<string, SortedDictionary<Product, int>>();
            this.productsByPrice = new SortedDictionary<decimal, Dictionary<Product, int>>();
        }

        public void ExecuteCommands(List<string> commandParams)
        {
            string result = null;
            switch (commandParams[0])
            {
                case "AddProduct":
                    {
                        decimal price = decimal.Parse(commandParams[2]);
                        result = this.AddProduct(commandParams[1], commandParams[3], price);
                    }

                    break;

                case "DeleteProducts":
                    {
                        result = commandParams.Count == 3 
                            ? this.DeleteProducts(commandParams[1], commandParams[2]) 
                            : this.DeleteProducts(commandParams[1]);
                    }

                    break;
                case "FindProductsByName":
                    {
                        result = this.FindProductsByName(commandParams[1]);
                    }

                    break;

                case "FindProductsByProducer":
                    {
                        result = this.FindProductsByProducer(commandParams[1]);
                    }

                    break;

                case "FindProductsByPriceRange":
                    {
                        decimal fromPrice = decimal.Parse(commandParams[1]);
                        decimal toPrice = decimal.Parse(commandParams[2]);
                        result = this.FindProductsByPriceRange(fromPrice, toPrice);
                    }

                    break;

                default:
                    {
                        throw new ArgumentException("Invalid command name");
                    }
            }

            if (!string.IsNullOrEmpty(result))
            {
                Console.WriteLine(result);
            }
        }

        public string AddProduct(string name, string producer, decimal price)
        {
            Product product = new Product(producer, name, price);
            if (!this.productsByProducerAndName.ContainsKey(producer))
            {
                this.productsByProducerAndName.Add(producer, new Dictionary<string, SortedDictionary<Product, int>>());
                this.productsByProducer.Add(producer, new SortedDictionary<Product, int>());
            }

            if (!this.productsByProducerAndName[producer].ContainsKey(name))
            {
                this.productsByProducerAndName[producer].Add(name, new SortedDictionary<Product, int>());
            }

            /*if (!this.productsByName.ContainsKey(name))
            {
                this.productsByName.Add(name, new SortedDictionary<Product, int>());
            }*/

            if (!this.productsByProducerAndName[producer][name].ContainsKey(product))
            {
                this.productsByProducerAndName[producer][name].Add(product, 0);
                this.productsByProducer[producer].Add(product, 0);
            }
/*
            if (!this.productsByName[name].ContainsKey(product))
            {
                this.productsByName[name].Add(product, 0);
            }*/

            this.productsByProducerAndName[producer][name][product]++;
            this.productsByProducer[producer][product]++;
           /* this.productsByName[name][product]++;*/

            if (!this.productsByPrice.ContainsKey(price))
            {
                this.productsByPrice.Add(price, new Dictionary<Product, int>());
            }

            if (!this.productsByPrice[price].ContainsKey(product))
            {
                this.productsByPrice[price].Add(product, 0);
            }

            this.productsByPrice[price][product]++;

            return ProductAddedMessage;
        }

        public string DeleteProducts(string producer)
        {
            if (!this.productsByProducer.ContainsKey(producer))
            {
                return NoProductsFoundMessage;
            }

            var deletedProducts = this.productsByProducer[producer].Values.Sum();

            foreach (var product in this.productsByProducer[producer].Keys)
            {
                this.productsByPrice[product.Price].Remove(product);
            }

            this.productsByProducer.Remove(producer);

            /*foreach (var name in this.productsByProducerAndName[producer].Keys)
            {
                var productsToDelete = this.productsByName[name].Keys.Where(x => x.Producer.Equals(producer));
                foreach (var productToDelete in productsToDelete)
                {
                    this.productsByName[name].Remove(productToDelete);
                }
            }*/

            this.productsByProducerAndName.Remove(producer);

            return string.Format(XProductsDeleted, deletedProducts);
        }

        public string DeleteProducts(string name, string producer)
        {
            if (!this.productsByProducer.ContainsKey(producer))
            {
                return NoProductsFoundMessage;
            }

            if (!this.productsByProducerAndName[producer].ContainsKey(name))
            {
                return NoProductsFoundMessage;
            }

            foreach (var product in this.productsByProducerAndName[producer][name].Keys)
            {
                this.productsByPrice[product.Price].Remove(product);
                this.productsByProducer[producer].Remove(product);
            }

            var deletedProducts = this.productsByProducerAndName[producer][name].Values.Sum();
            this.productsByProducerAndName[producer].Remove(name);

            /*var productsToDelete = this.productsByName[name].Keys.Where(x => x.Producer.Equals(producer));
            foreach (var productToDelete in productsToDelete)
            {
                this.productsByName[name].Remove(productToDelete);
            }*/

            return string.Format(XProductsDeleted, deletedProducts);
        }

        public string FindProductsByName(string name)
        {/*
            if (!this.productsByName.ContainsKey(name))
            {
                return NoProductsFoundMessage;
            }

            string producer = this.productsByName[name].First().Key.Producer;
            var foundProducts = this.productsByName[name];*/
            var foundProducts =
                this.productsByProducerAndName.Values.Where(x => x.ContainsKey(name)).SelectMany(x => x[name]).OrderBy(x=>x.Key);
            if (!foundProducts.Any())
            {
                return NoProductsFoundMessage;
            }

            StringBuilder result = new StringBuilder();
            foreach (var product in foundProducts)
            {
                int count = product.Value;
                for (int i = 0; i < count; i++)
                {
                    result.Append("{" + product.Key + "}" + Environment.NewLine);
                }
            }

            result.Length -= Environment.NewLine.Length;
           
            return result.ToString();
        }

        public string FindProductsByProducer(string producer)
        {
            if (!this.productsByProducer.ContainsKey(producer))
            {
                return NoProductsFoundMessage;
            }

            var foundProducts = this.productsByProducer[producer];
            StringBuilder result = new StringBuilder();
            foreach (var product in foundProducts)
            {
                int count = product.Value;
                for (int i = 0; i < count; i++)
                {
                    result.Append("{" + product.Key + "}" + Environment.NewLine);
                }
            }

            result.Length -= Environment.NewLine.Length;
            return result.ToString();
        }

        public string FindProductsByPriceRange(decimal fromPrice, decimal toPrice)
        {
            var foundProducts = this.productsByPrice.Where(x => x.Key >= fromPrice && x.Key <= toPrice).SelectMany(x=>x.Value).OrderBy(x=>x.Key);
            if (!foundProducts.Any())
            {
                return NoProductsFoundMessage;
            }

            StringBuilder result = new StringBuilder();
            foreach (var product in foundProducts)
            {
                int count = product.Value;
                for (int i = 0; i < count; i++)
                {
                    result.Append("{" + product.Key + "}" + Environment.NewLine);
                }
            }

            result.Length -= Environment.NewLine.Length;

            return result.ToString();
        }
    }
}
