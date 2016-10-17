namespace ImplementALinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    class LinkedList<T> : IEnumerable<T> where T:IComparable
    {
        private ListNode<T> first;

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (this.Count == 0)
            {
                this.first = new ListNode<T>(item);
            }
            else
            {
                ListNode<T> swap = this.first;
                this.first = new ListNode<T>(item);
                this.first.NextNode = swap;
            }

            this.Count++;
        }

        public void Remove(int index)
        {
            try
            {              
                if (index == 0)
                {
                    this.first = this.first.NextNode;
                }
                else
                {
                    int searchIndex = 1;
                    ListNode<T> currentNode = this.first;

                    while (searchIndex < index - 1)
                    {
                        currentNode = currentNode.NextNode;
                        searchIndex++;
                    }

                    currentNode.NextNode = currentNode.NextNode.NextNode;
                }

                this.Count--;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid index.");
            }          
        }

        public int FirstIndexOf(T item)
        {
            if (this.Count == 0)
            {
                return 0;
            }

            ListNode<T> current = this.first;
            int index = 1;
            while (index <= this.Count && current.Value.CompareTo(item) != 0)
            {
                current = current.NextNode;
                index++;
            }

            if (index == this.Count + 1)
            {
                return 0;
            }

            return index;
        }

        public int LastIndexOf(T item)
        {
            if (this.Count == 0)
            {
                return 0;
            }

            ListNode<T> current = this.first;
            int lastIndex = 0;
            int index = 1;
            while (index <= this.Count)
            {
                if (current.Value.CompareTo(item) == 0)
                {
                    lastIndex = index;
                }

                current = current.NextNode;
                index++;
            }
 
            return lastIndex;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.first;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.NextNode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
