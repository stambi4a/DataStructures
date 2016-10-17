namespace LongestPathInATree
{
    using System.Collections.Generic;

    internal class Node<T>
    {
        internal Node(T value)
        {
            this.Value = value;
            this.Children = new List<Node<T>>();
        } 

        internal T Value { get; set; }

        internal Node<int> Parent { get; set; }

        internal ICollection<Node<T>> Children { get; set; }
    }
}
