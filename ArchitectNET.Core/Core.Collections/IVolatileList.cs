namespace ArchitectNET.Core.Collections
{
    public interface IVolatileList<TItem> : IFixedList<TItem>, IVolatileCollection<TItem>
    {
        new TItem this[int index] { get; set; }
        bool InsertAt(int index, TItem item);
        TItem RemoveAt(int index);
    }
}