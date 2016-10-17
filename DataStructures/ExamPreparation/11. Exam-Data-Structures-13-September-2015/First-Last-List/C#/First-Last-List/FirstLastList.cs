using System;
using System.Collections.Generic;
using System.Linq;

using Wintellect.PowerCollections;

namespace First_Last_List
{
    public class FirstLastList<T> : IFirstLastList<T>
        where T : IComparable<T>
    {
        private LinkedList<T> elements = new LinkedList<T>();
        private OrderedBag<T> sortedAscendingElements = new OrderedBag<T>();
        private OrderedBag<T> sortedDescendingElements = new OrderedBag<T>((p1, p2) => -p1.CompareTo(p2));

        public int Count => this.elements.Count;

        public void Add(T newElement)
        {
            this.elements.AddLast(newElement);
            this.sortedAscendingElements.Add(newElement);
            this.sortedDescendingElements.Add(newElement);
        }

        public IEnumerable<T> First(int count)
        {
            if (count > this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.elements.Take(count);
        }

        public IEnumerable<T> Last(int count)
        {
            if (count > this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var node = this.elements.Last;
            while (count > 0 && node != null)
            {
                count--;
                yield return node.Value;
                node = node.Previous;
            }
        }

        public IEnumerable<T> Min(int count)
        {
            if (count > this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.sortedAscendingElements.Take(count);
        }

        public IEnumerable<T> Max(int count)
        {
            if (count > this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return this.sortedDescendingElements.Take(count);
        }

        public int RemoveAll(T element)
        {
            var elementsToGetRidOf = new List<T>(this.sortedAscendingElements.Range(element, true, element, true));
           
            foreach (var item in elementsToGetRidOf)
            {
                this.elements.Remove(item);
            }

            this.sortedAscendingElements.RemoveMany(elementsToGetRidOf);
            this.sortedDescendingElements.RemoveMany(elementsToGetRidOf);
            return elementsToGetRidOf.Count;
        }

        public void Clear()
        {
            this.elements = new LinkedList<T>();
            this.sortedAscendingElements.Clear();
            this.sortedDescendingElements.Clear();
        }
    }
}
