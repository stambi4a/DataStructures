namespace First_Last_List
{
    using System;
    using System.Collections.Generic;

    public class ReverseComparer<T> : Comparer<T> where T : IComparable<T>
    { 
        public override int Compare(T first, T second)
        {
            return second.CompareTo(first);
        }
    }
}
