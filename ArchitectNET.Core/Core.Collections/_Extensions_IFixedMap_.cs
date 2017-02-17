namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Provides extension methods which extend <see cref="IFixedMap{TKey,TValue}"/> implementations
    /// </summary>
    public static class _Extensions_IFixedMap_
    {
        public static bool ContainsKey<TKey, TValue>(this IFixedMap<TKey, TValue> map, TKey key)
        {
            Guard.ArgumentNotNull(key, nameof(key));
            TValue value;
            return map.TryGetValue(key, out value);
        }
    }
}