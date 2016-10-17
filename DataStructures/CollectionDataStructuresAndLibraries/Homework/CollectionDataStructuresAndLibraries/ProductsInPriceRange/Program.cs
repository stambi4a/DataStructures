namespace ProductsInPriceRange
{
    using System;
    using System.Diagnostics;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch watch = Stopwatch.StartNew();          
            ProductListManipulator manipulator = new ProductListManipulator();
            manipulator.FindProductsInPriceRange();
            watch.Stop();
            var timeElapsed = watch.Elapsed;
            Console.WriteLine($"Elapsed time is:{timeElapsed}");
        }
    }
}
