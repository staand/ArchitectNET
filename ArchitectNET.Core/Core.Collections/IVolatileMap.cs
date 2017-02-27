namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Reperesents a generic unordered key/value collection which can be externally modified using public interface it
    /// provides.
    /// </summary>
    /// <typeparam name="TKey"> The type of keys in the map. </typeparam>
    /// <typeparam name="TValue"> The type of values in the map. </typeparam>
    public interface IVolatileMap<TKey, TValue> : IFixedMap<TKey, TValue>
    {
        /// <summary>
        /// Adds the specified key and value to the map.
        /// </summary>
        /// <param name="key"> The key of the element to add. </param>
        /// <param name="value"> The value of the element to add. The value can be <see langword="null" /> for reference types. </param>
        /// <returns> <see langword="true" /> if the element is added to the map; otherwise <see langword="false" />. </returns>
        bool Add(TKey key, TValue value);

        /// <summary>
        /// Removes all keys and values from the map.
        /// </summary>
        void Clear();

        /// <summary>
        /// Removes the value with the specified key from the map.
        /// </summary>
        /// <param name="key"> The key of the element to remove. </param>
        /// <returns> <see langword="true" /> if the element is successfully found and removed; otherwise <see langword="false" />. </returns>
        bool Remove(TKey key);
    }
}