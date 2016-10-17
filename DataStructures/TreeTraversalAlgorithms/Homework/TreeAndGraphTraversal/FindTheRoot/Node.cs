namespace FindTheRoot
{
    using System.Collections.Generic;

    internal class Node<T>
    {
        public Node(T value)
        {
            this.Value = value;
            this.Children = new List<Node<T>>();
        } 

        public T Value { get; set; }

        public bool HasParent { get; set; }

        public ICollection<Node<T>> Children { get; set; }
    }
}
