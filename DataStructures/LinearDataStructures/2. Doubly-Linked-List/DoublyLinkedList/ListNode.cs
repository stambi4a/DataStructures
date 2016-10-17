namespace Double_Linked_List
{
    internal class ListNode<T>
    {
        public ListNode(T value)
        {
            this.Value = value;
        }

        public ListNode<T> NextNode { get; set; }

        public ListNode<T> PrevNode { get; set; }

        public T Value { get; private set; }
    }
}
