namespace ArchitectNET.Core.Collections
{
    public interface IVolatileCollection<TItem> : IFixedCollection<TItem>
    {
        bool Add(TItem item);
        void Clear();
        bool Remove(TItem item);
    }
}