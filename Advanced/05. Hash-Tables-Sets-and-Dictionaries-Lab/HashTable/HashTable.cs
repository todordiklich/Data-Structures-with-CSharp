namespace HashTable
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DEFAULT_CAPACITY = 100;
        private List<KeyValue<TKey, TValue>>[] buckets;
        public int Count { get; private set; }

        public int Capacity => this.buckets.Length;

        public HashTable(int capacity = DEFAULT_CAPACITY)
        {
            buckets = new List<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            if (this.ContainsKey(key))
            {
                throw new ArgumentException();
            }

            var item = new KeyValue<TKey, TValue>(key, value);
            this.AddItem(item);
            this.Count++;

            this.ResizeAndRehash();
        }

        private void AddItem(KeyValue<TKey, TValue> item)
        {
            int index = this.GetIndex(item.Key);

            if (this.buckets[index] == null)
            {
                this.buckets[index] = new List<KeyValue<TKey, TValue>>();
            }

            this.buckets[index].Add(item);
        }

        private int GetIndex(TKey key)
        {
            var hash = Math.Abs(key.GetHashCode());
            return hash % this.Capacity;
        }

        private void ResizeAndRehash()
        {
            if (this.Count / (double)this.Capacity >= 0.75)
            {
                var oldBuckets = this.buckets;
                buckets = new List<KeyValue<TKey, TValue>>[this.Capacity * 2];

                foreach (var bucket in oldBuckets)
                {
                    if (bucket != null)
                    {
                        foreach (var item in bucket)
                        {
                            this.AddItem(item);
                        }
                    }
                }
            }
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            bool toReturn;

            int index = GetIndex(key);
            var element = new KeyValue<TKey, TValue>(key, value);

            if (this.ContainsKey(key))
            {
                toReturn = false;
                foreach (var item in buckets[index])
                {
                    if (item.Key.Equals(key))
                    {
                        item.Value = value;
                    }
                }
            }
            else
            {
                toReturn = true;
                if (this.buckets[index] == null)
                {
                    this.buckets[index] = new List<KeyValue<TKey, TValue>>();
                }

                this.buckets[index].Add(element);
                this.Count++;
            }
            

            this.ResizeAndRehash();

            return toReturn;
        }

        public TValue Get(TKey key)
        {
            var item = Find(key);
            if (item == null)
            {
                throw new KeyNotFoundException();
            }

            return item.Value;
        }

        public TValue this[TKey key]
        {
            get
            {
                var item = Find(key);
                if (item == null)
                {
                    throw new KeyNotFoundException();
                }

                return item.Value;
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var item = Find(key);
            if (item == null)
            {
                value = default;
                return false;
            }

            value = item.Value;
            return true;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = GetIndex(key);

            return this.buckets[index]?.FirstOrDefault(item => item.Key.Equals(key));
        }

        public bool ContainsKey(TKey key)
        {
            var item = this.Find(key);

            return item != null;
        }

        public bool Remove(TKey key)
        {
            var item = this.Find(key);

            if (item != null)
            {
                int index = GetIndex(key);
                this.buckets[index].Remove(item);
                this.Count--;
                if (this.buckets[index].Count == 0)
                {
                    this.buckets[index] = null;
                }

                return true;
            }            

            return false;
        }

        public void Clear()
        {
            this.buckets = new List<KeyValue<TKey, TValue>>[DEFAULT_CAPACITY];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return this.buckets.Where(b => b != null).SelectMany(i => i).Select(i => i.Key);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return this.buckets.Where(b => b != null).SelectMany(i => i).Select(i => i.Value);
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in buckets)
            {
                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        yield return item;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
