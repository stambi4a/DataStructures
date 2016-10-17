namespace ImplementReversedList
{
    using System;
    using System.Runtime.CompilerServices;

    internal class Program
    {
        private static void Main(string[] args)
        {
            ReversedList<int> revList = new ReversedList<int>();
            revList.Add(7);

            //Check if Add(T item) works
            Console.WriteLine(revList[0]);

            revList.Add(-100);
            revList.Add(23);
            revList.Add(24);

            Console.WriteLine("Count = {0}", revList.Count);
            //Check if enumerator works
            foreach (var item in revList)
            {
                Console.WriteLine("item = {0}", item);
            }

            for (int i = 0; i < revList.Count; i++)
            {
                Console.WriteLine("revList[{0}] = {1}", i, revList[i]);
            }

             //Check if remove at invalid index
            revList.Remove(5);

             //Check if Remove(int index) works properly
            revList.Remove(3);

            foreach (var item in revList)
            {
                Console.WriteLine("item = {0}", item);
            }

            for (int i = 0; i < revList.Count; i++)
            {
                Console.WriteLine("revList[{0}] = {1}", i, revList[i]);
            }


        }
    }
}
