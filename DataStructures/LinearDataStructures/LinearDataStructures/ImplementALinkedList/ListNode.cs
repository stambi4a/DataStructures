namespace ImplementALinkedList
{
    using System;

    internal class ListNode<T> where T : IComparable
    {
        public ListNode(T value)
        {
            this.Value = value;
        }

        public ListNode<T> NextNode { get; set; }

        public T Value { get; private set; }
    }
}
