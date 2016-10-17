namespace FindTheRoot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Graph
    {
        internal Graph()
        {
            this.Nodes = new Dictionary<int, Node<int>>();
            this.InputGraph();
        }

        public int NumberOfNodes { get; set; }

        public int NumberOfEdges { get; set; }

        public Dictionary<int, Node<int>> Nodes { get; set; }

        private void InputGraph()
        {
            this.NumberOfNodes = int.Parse(Console.ReadLine());
            this.NumberOfEdges = int.Parse(Console.ReadLine());
            for (int i = 0; i < this.NumberOfEdges; i++)
            {
                string[] input = Console.ReadLine().Split();
                int parentNodeValue = int.Parse(input[0]);
                int childNodeValue = int.Parse(input[1]);

                if (!this.Nodes.ContainsKey(childNodeValue))
                {
                    Node<int> childNode = new Node<int>(childNodeValue);
                    this.Nodes.Add(childNodeValue, childNode);
                }

                if (!this.Nodes.ContainsKey(parentNodeValue))
                {
                    Node<int> parentNode = new Node<int>(parentNodeValue);
                    this.Nodes.Add(parentNodeValue, parentNode);
                }

                this.Nodes[childNodeValue].HasParent = true;
                this.Nodes[parentNodeValue].Children.Add(this.Nodes[childNodeValue]);
            }
        }

        internal ICollection<Node<int>> FindRoot()
        {
            var graphRoots = this.Nodes.Values.Where(x => !x.HasParent).ToList();

            return graphRoots;
        } 
    }
}