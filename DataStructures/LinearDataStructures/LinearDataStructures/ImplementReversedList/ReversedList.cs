namespace ImplementReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        public ReversedList()
        {
            this.Capacity = 1;
            this.Arr = new T[this.Capacity];
        }

        public T[] Arr { get; set; }

        public int Count { get; set; }


        public int Capacity { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index > this.Count - 1)
                {
                    throw new InvalidOperationException("Invalid index.");
                }

                return this.Arr[this.Count - index - 1];
            }

            set
            {
                if (index > this.Count - 1)
                {
                    throw new InvalidOperationException("Invalid index.");
                }

                this.Arr[this.Count - index - 1] = value;
            }         
        }

        public void Add(T item)
        {
            if (this.Count == this.Capacity)
            {
                this.Capacity *= 2;
                T[] newArr = new T[this.Capacity];
                Array.Copy(this.Arr, newArr, this.Count);
                this.Arr = newArr;
            }

            this.Arr[this.Count] = item;
            this.Count++;
        }

        public void Remove(int index)
        {           
            try
            {
                T[] newArr = new T[this.Count - 1];
                Array.Copy(this.Arr, 0, newArr, 0, this.Count - index - 1);

                Array.Copy(this.Arr, this.Count - index, newArr, this.Count - index - 1, index);

                this.Count--;
                this.Arr = newArr;
            }
            catch (ArgumentException ae)
            {                
                Console.WriteLine(ae.Message);
            }          
        }

        public IEnumerator<T> GetEnumerator()
        {
            int index = 0;
            while (index <= this.Count - 1)
            {
                var current = this[index];
                yield return current;
                index++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}