namespace StringEditor
{
    using System;

    using Wintellect.PowerCollections;
    internal class Rope : BigList<char>
    {
        internal void Insert(string word)
        {
            int length = word.Length;
            for (int i = 0; i < length; i++)
            {
                this.AddToFront(word[length - 1 - i]);
            }
        }

        internal void Append(string word)
        {
            int length = word.Length;
            for (int i = 0; i < length; i++)
            {
                this.Add(word[i]);
            }
        }

        internal void Delete(int startIndex, int count)
        {

            for (int i = 0; i < count; i++)
            {
                base.RemoveAt(startIndex);
            }
        }

        internal void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
}
