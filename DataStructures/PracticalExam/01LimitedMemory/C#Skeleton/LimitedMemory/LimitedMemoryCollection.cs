using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace LimitedMemory
{
    using System.Resources;

    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private Dictionary<K, Pair<K, V>> pairs;
        private LinkedList<Pair<K, V>> pairsByPriority;
        public LimitedMemoryCollection(int capacity)
        {
            this.Capacity = capacity;
            this.pairs = new Dictionary<K, Pair<K, V>>();
            this.pairsByPriority = new LinkedList<Pair<K, V>>();
        } 

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            var pair = this.pairsByPriority.Last;
           
            while (pair != null)
            {
                yield return pair.Value;
                pair = pair.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get; private set; }

        public int Count => this.pairsByPriority.Count;

        public void Set(K key, V value)
        {
            Pair<K, V> newPair = new Pair<K, V>(key, value);

            if (this.pairs.ContainsKey(key))
            {
                var oldPair = this.pairs[key];
                this.pairs.Remove(key);
                this.pairs.Add(key, newPair);

                this.pairsByPriority.Remove(oldPair);
                this.pairsByPriority.AddLast(newPair);
            }

            if (!this.pairs.ContainsKey(key))
            {
                if (this.Count < this.Capacity)
                {
                    this.pairsByPriority.AddLast(newPair);
                }
                else
                {
                    var oldPair = this.pairsByPriority.First.Value;
                    this.pairs.Remove(oldPair.Key);
                    this.pairsByPriority.RemoveFirst();
                    this.pairsByPriority.AddLast(newPair);
                }

                this.pairs.Add(key, newPair);
            }

           
        }

        public V Get(K key)
        {
            if (!this.pairs.ContainsKey(key))
            {
                throw new KeyNotFoundException("Searched key does not exist.");
            }

            var pair = this.pairs[key];
            this.pairsByPriority.Remove(pair);
            this.pairsByPriority.AddLast(pair);
            return this.pairs[key].Value;
        }
    }
}
