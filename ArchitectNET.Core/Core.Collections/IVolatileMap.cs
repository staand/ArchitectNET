namespace ArchitectNET.Core.Collections
{
    public interface IVolatileMap<in TKey, TValue> : IFixedMap<TKey, TValue>
    {
        bool Add(TKey key, TValue value);
        void Clear();
        bool Remove(TKey key);
    }
}