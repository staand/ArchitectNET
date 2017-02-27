using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    /// <summary>
    /// A subclass of <see cref="Dictionary{TKey,TValue}" /> class which additionally implements
    /// <see cref="IFixedMap{TKey,TValue}" /> and <see cref="IVolatileMap{TKey,TValue}" /> interfaces and thus is an adapter
    /// between standard key/value collection implementation and interfaces introduced by ArchitectNET.Core.Collections
    /// namespace.
    /// </summary>
    /// <typeparam name="TKey"> The type of keys in the map. </typeparam>
    /// <typeparam name="TValue"> The type of values in the map. </typeparam>
    public class DictionaryMap<TKey, TValue> : Dictionary<TKey, TValue>, IVolatileMap<TKey, TValue>
    {
        /// <summary>
        /// Gets the number of key/value pairs in the map.
        /// </summary>
        int IFixedMap<TKey, TValue>.Count => Count;

        /// <summary>
        /// Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key"> The key to locate. </param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if the key is found;
        /// otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed
        /// uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the map contains an element with the specified key; otherwise,
        /// <see langword="false" />.
        /// </returns>
        bool IFixedMap<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            return TryGetValue(key, out value);
        }

        /// <summary>
        /// Adds the specified key and value to the map.
        /// </summary>
        /// <param name="key"> The key of the element to add. </param>
        /// <param name="value"> The value of the element to add. The value can be <see langword="null" /> for reference types. </param>
        /// <returns> <see langword="true" /> if the element is added to the map; otherwise <see langword="false" />. </returns>
        bool IVolatileMap<TKey, TValue>.Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                return false;
            Add(key, value);
            return true;
        }

        /// <summary>
        /// Removes all keys and values from the map.
        /// </summary>
        void IVolatileMap<TKey, TValue>.Clear()
        {
            Clear();
        }

        /// <summary>
        /// Removes the value with the specified key from the map.
        /// </summary>
        /// <param name="key"> The key of the element to remove. </param>
        /// <returns> <see langword="true" /> if the element is successfully found and removed; otherwise <see langword="false" />. </returns>
        bool IVolatileMap<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }
    }
}