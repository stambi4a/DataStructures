namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;

    class TupleComparer : IComparer<Tuple<int, string>>
    {
        public int Compare(Tuple<int, string> tuple1, Tuple<int, string> tuple2)
        {
            if (tuple1.Item1 - tuple2.Item1 == 0)
            {
                return tuple1.Item2.CompareTo(tuple2.Item2);
            }

            return tuple1.Item1 - tuple2.Item1;
        }
    }
}
