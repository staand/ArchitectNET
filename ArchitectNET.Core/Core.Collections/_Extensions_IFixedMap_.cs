namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Provides extension methods which extend <see cref="IFixedMap{TKey,TValue}" /> implementations
    /// </summary>
    public static class _Extensions_IFixedMap_
    {
        /// <summary>
        /// Determines whether the map contains the specified key
        /// </summary>
        /// <typeparam name="TKey"> The type of keys in the map </typeparam>
        /// <typeparam name="TValue"> The type of values in the map </typeparam>
        /// <param name="map"> <see cref="IFixedMap{TKey,TValue}" /> object which is treated as <see langword="this" /> instance </param>
        /// <param name="key"> The key to locate in the <paramref name="map" /> </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="map" /> contains an entry with the specifed key, otherwise
        /// <see langword="false" />
        /// </returns>
        public static bool ContainsKey<TKey, TValue>(this IFixedMap<TKey, TValue> map, TKey key)
        {
            Guard.ArgumentNotNull(key, nameof(key));
            TValue value;
            return map.TryGetValue(key, out value);
        }
    }
}