using System;
using System.Collections;

namespace library.Structures
{
    public class Map<K, V>
    {
        private Hashtable _items;

        public int Size => _items.Count;

        public bool IsEmpty => Size == 0;

        public Map()
        {
            _items = new Hashtable();
        }

        public bool Has(K key)
        {
            return _items.ContainsKey(key);
        }

        public void Set(K key, V value)
        {
            _items.Add(key, value);
        }

        public V Get(K key)
        {
            return (V)_items[key];
        }

        public void Delete(K key)
        {
            if (Has(key))
            {
                _items.Remove(key);
            }
        }

        public ICollection Values()
        {
            return _items.Values;
        }

        public ICollection Keys()
        {
            return _items.Keys;
        }

        public override string ToString() => String.Format("Map<{0}, {1}>: {2} Item Pair(s)", typeof(K).Name, typeof(V).Name, Size);
    }
}
