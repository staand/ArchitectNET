using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    public class DictionaryMap<TKey, TValue> : Dictionary<TKey, TValue>, IVolatileMap<TKey, TValue>
    {
        int IFixedMap<TKey, TValue>.Count => Count;

        bool IFixedMap<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return TryGetValue(key, out value);
        }

        bool IVolatileMap<TKey, TValue>.Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                return false;
            Add(key, value);
            return true;
        }

        void IVolatileMap<TKey, TValue>.Clear()
        {
            Clear();
        }

        bool IVolatileMap<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }
    }
}