namespace ArchitectNET.Core.Collections
{
    public interface IFixedMap<in TKey, TValue>
    {
        int Count { get; }
        bool TryGetValue(TKey key, out TValue value);
    }
}