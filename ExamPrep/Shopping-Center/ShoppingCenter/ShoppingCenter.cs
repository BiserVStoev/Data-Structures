namespace ShoppingCenter
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Wintellect.PowerCollections;

    public class ShoppingCenter
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            //var center = new ShoppingCenterSlow();
            var center = new ShoppingCenterFast();

            int commandsCount = int.Parse(Console.ReadLine());
            for (int i = 1; i <= commandsCount; i++)
            {
                string command = Console.ReadLine();
                string commandResult = center.ProcessCommand(command);  
                Console.WriteLine(commandResult);
            }
        }
    }

    public class ShoppingCenterSlow
    {
        private const string PRODUCT_ADDED = "Product added";
        private const string X_PRODUCTS_DELETED = " products deleted";
        private const string NO_PRODUCTS_FOUND = "No products found";
        private const string INCORRECT_COMMAND = "Incorrect command";

        private readonly List<Product> products = new List<Product>();

        private string AddProduct(string name, string price, string producer)
        {
            Product product = new Product()
            {
                Name = name,
                Price = decimal.Parse(price),
                Producer = producer
            };

            this.products.Add(product);

            return PRODUCT_ADDED;
        }

        private string FindProductsByName(string name)
        {
            var products = this.products
                .Where(p => p.Name == name)
                .OrderBy(p => p);
            return this.PrintProducts(products);
        }

        private string FindProductsByProducer(string producer)
        {
            var products = this.products
                .Where(p => p.Producer == producer)
                .OrderBy(p => p);

            return this.PrintProducts(products);
        }

        private string FindProductsByPriceRange(string from, string to)
        {
            decimal rangeStart = decimal.Parse(from);
            decimal rangeEnd = decimal.Parse(to);
            var products = this.products
                .Where(p => p.Price >= rangeStart && p.Price <= rangeEnd)
                .OrderBy(p => p);

            return this.PrintProducts(products);
        }

        private string PrintProducts(IEnumerable<Product> products)
        {
            if (products.Any())
            {
                var builder = new StringBuilder();
                foreach (var product in products)
                {
                    builder.AppendLine(product.ToString());
                }

                string formattedProducts = builder.ToString().TrimEnd();

                return formattedProducts;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string DeleteProductsByNameAndProducer(string name, string producer)
        {
            int countOfRemovedProducts = this.products.RemoveAll(p => p.Name == name && p.Producer == producer);
            if (countOfRemovedProducts > 0)
            {
                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string DeleteProductsByProducer(string producer)
        {
            int countOfRemovedProducts = this.products.RemoveAll(p => p.Producer == producer);
            if (countOfRemovedProducts > 0)
            {
                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;
        }

        public string ProcessCommand(string command)
        {
            int indexOfFirstSpace = command.IndexOf(' ');
            string method = command.Substring(0, indexOfFirstSpace);
            string parameterValues = command.Substring(indexOfFirstSpace + 1);
            string[] parameters =
                parameterValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            switch (method)
            {
                case "AddProduct":
                    return this.AddProduct(parameters[0], parameters[1], parameters[2]);
                case "DeleteProducts":
                    if (parameters.Length == 1)
                    {
                        return this.DeleteProductsByProducer(parameters[0]);
                    }

                    return this.DeleteProductsByNameAndProducer(parameters[0], parameters[1]);
                case "FindProductsByName":
                    return this.FindProductsByName(parameters[0]);
                case "FindProductsByPriceRange":
                    return this.FindProductsByPriceRange(parameters[0], parameters[1]);
                case "FindProductsByProducer":
                    return this.FindProductsByProducer(parameters[0]);
                default:
                    return INCORRECT_COMMAND;
            }
        }
    }

    public class ShoppingCenterFast
    {
        private const string PRODUCT_ADDED = "Product added";
        private const string X_PRODUCTS_DELETED = " products deleted";
        private const string NO_PRODUCTS_FOUND = "No products found";
        private const string INCORRECT_COMMAND = "Incorrect command";

        private readonly MultiDictionary<string, Product> productsByName =
            new MultiDictionary<string, Product>(true);
        private readonly MultiDictionary<string, Product> productsByNameAndProducer =
            new MultiDictionary<string, Product>(true);
        private readonly OrderedMultiDictionary<decimal, Product> productsByPrice =
            new OrderedMultiDictionary<decimal, Product>(true);
        private readonly MultiDictionary<string, Product> productsByProducer =
            new MultiDictionary<string, Product>(true);

        private string AddProduct(string name, string price, string producer)
        {
            Product product = new Product()
            {
                Name = name,
                Price = decimal.Parse(price),
                Producer = producer
            };

            this.productsByName.Add(name, product);
            string nameAndProducerKey = this.CombineKeys(name, producer);
            this.productsByNameAndProducer.Add(nameAndProducerKey, product);
            this.productsByPrice.Add(product.Price, product);
            this.productsByProducer.Add(producer, product);

            return PRODUCT_ADDED;
        }

        private string CombineKeys(string name, string producer)
        {
            string key = name + ";" + producer;

            return key;
        }

        private string FindProductsByName(string name)
        {
            var productsFound = this.productsByName[name];

            return this.SortAndPrintProducts(productsFound);
        }

        private string SortAndPrintProducts(IEnumerable<Product> products)
        {
            if (products.Any())
            {
                var sortedProducts = new List<Product>(products);
                sortedProducts.Sort();
                var builder = new StringBuilder();
                foreach (var product in sortedProducts)
                {
                    builder.AppendLine(product.ToString());
                }

                // Remove the undneeded last "new line"
                builder.Length -= Environment.NewLine.Length;

                string formattedProducts = builder.ToString();

                return formattedProducts;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string FindProductsByProducer(string producer)
        {
            var productsFound = this.productsByProducer[producer];

            return this.SortAndPrintProducts(productsFound);
        }

        private string FindProductsByPriceRange(string from, string to)
        {
            decimal rangeStart = decimal.Parse(from);
            decimal rangeEnd = decimal.Parse(to);
            var productsFound = this.productsByPrice.Range(rangeStart, true, rangeEnd, true).Values;

            return this.SortAndPrintProducts(productsFound);
        }

        private string DeleteProductsByNameAndProducer(string name, string producer)
        {
            string nameAndProducerKey = name + ";" + producer;
            var productsToBeRemoved = this.productsByNameAndProducer[nameAndProducerKey];
            if (productsToBeRemoved.Any())
            {
                int countOfRemovedProducts = productsToBeRemoved.Count;
                foreach (var product in productsToBeRemoved)
                {
                    this.productsByName.Remove(product.Name, product);
                    this.productsByProducer.Remove(product.Producer, product);
                    this.productsByPrice.Remove(product.Price, product);
                }

                this.productsByNameAndProducer.Remove(nameAndProducerKey);

                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;
        }

        private string DeleteProductsByProducer(string producer)
        {
            var productsToBeRemoved = this.productsByProducer[producer];
            if (productsToBeRemoved.Any())
            {
                foreach (var product in productsToBeRemoved)
                {
                    this.productsByName.Remove(product.Name, product);
                    string nameAndProducerKey = this.CombineKeys(product.Name, producer);
                    this.productsByNameAndProducer.Remove(nameAndProducerKey, product);
                    this.productsByPrice.Remove(product.Price, product);
                }

                int countOfRemovedProducts = this.productsByProducer[producer].Count;
                this.productsByProducer.Remove(producer);

                return countOfRemovedProducts + X_PRODUCTS_DELETED;
            }

            return NO_PRODUCTS_FOUND;
        }

        public string ProcessCommand(string command)
        {
            int indexOfFirstSpace = command.IndexOf(' ');
            string method = command.Substring(0, indexOfFirstSpace);
            string parameterValues = command.Substring(indexOfFirstSpace + 1);
            string[] parameters =
                parameterValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            switch (method)
            {
                case "AddProduct":
                    return this.AddProduct(parameters[0], parameters[1], parameters[2]);
                case "DeleteProducts":
                    if (parameters.Length == 1)
                    {
                        return this.DeleteProductsByProducer(parameters[0]);
                    }

                    return this.DeleteProductsByNameAndProducer(parameters[0], parameters[1]);
                case "FindProductsByName":
                    return this.FindProductsByName(parameters[0]);
                case "FindProductsByPriceRange":
                    return this.FindProductsByPriceRange(parameters[0], parameters[1]);
                case "FindProductsByProducer":
                    return this.FindProductsByProducer(parameters[0]);
                default:
                    return INCORRECT_COMMAND;
            }
        }
    }

    public class Product : IComparable<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }

        public int CompareTo(Product other)
        {
            int resultOfCompare = this.Name.CompareTo(other.Name);
            if (resultOfCompare == 0)
            {
                resultOfCompare = this.Producer.CompareTo(other.Producer);
            }

            if (resultOfCompare == 0)
            {
                resultOfCompare = this.Price.CompareTo(other.Price);
            }

            return resultOfCompare;
        }

        public override string ToString()
        {
            string toString =
                "{" +
                this.Name + ";" +
                this.Producer + ";" +
                this.Price.ToString("0.00") +
                "}";

            return toString;
        }
    }
}