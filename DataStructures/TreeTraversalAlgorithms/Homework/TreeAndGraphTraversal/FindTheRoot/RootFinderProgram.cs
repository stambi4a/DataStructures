namespace FiondTheRoot
{
    using System;
    using System.Linq;

    using FindTheRoot;

    internal class RootFinderProgram
    {
        static void Main()
        {
            Graph graph = new Graph();

            var graphRootCollection = graph.FindRoot();
            int graphRoots = graphRootCollection.Count;

            if (graphRoots == 0)
            {
                Console.WriteLine("No root!");
            }

            if (graphRoots > 1)
            {
                Console.WriteLine("Multiple root nodes!");
            }

            if (graphRoots == 1)
            {
                Console.WriteLine("Root node:{0}", graphRootCollection.First().Value);
            }
        }      
    }
}