namespace AvlTreeLab
{
    using System;
    public class Node<T> where T : IComparable
    {
        public Node(T value)
        {
            this.Value = value;
            this.BalanceFactor = 0;
        } 

        public Node<T> LeftChild { get; set; }

        public Node<T> RightChild { get; set; }

        public Node<T> Parent { get; set; } 

        public int BalanceFactor { get; set; }

        public T Value { get; set; }

        public bool IsLeftChild => this.Parent != null && 
            ((
            this.Parent.BalanceFactor == 0 &&
            this.Parent.ChildrenCount == 2 &&
            this == this.Parent.LeftChild) || 
            this.Parent.BalanceFactor == 1);

        public bool IsRightChild => this.Parent != null &&
            ((
            this.Parent.BalanceFactor == 0 &&
            this.Parent.ChildrenCount == 2 &&
            this == this.Parent.RightChild) ||
            this.Parent.BalanceFactor == -1);

        public int ChildrenCount => (this.LeftChild != null ? 1 : 0) + (this.RightChild != null ? 1 : 0);

        public override string ToString() => this.Value.ToString();

    }
}

