namespace Hierarchy.Core
{
    using System.Collections.Generic;

    internal class Node<T>
    {
        public Node(T value, Node<T> parent = null)
        {
            this.Value = value;
            this.Parent = parent;
            this.Children = new List<Node<T>>();
        }

        public T Value { get; set; }

        public Node<T> Parent { get; set; }

        public IList<Node<T>> Children { get; set; }
    }
}
