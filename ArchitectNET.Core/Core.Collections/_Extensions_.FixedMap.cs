namespace ArchitectNET.Core.Collections
{
    public static class _Extensions_
    {
        public static bool ContainsKey<TKey, TValue>(this IFixedMap<TKey, TValue> map, TKey key)
        {
            Guard.ArgumentNotNull(key, nameof(key));
            TValue value;
            return map.TryGetValue(key, out value);
        }
    }
}