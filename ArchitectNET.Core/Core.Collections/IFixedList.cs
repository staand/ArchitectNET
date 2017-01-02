namespace ArchitectNET.Core.Collections
{
    public interface IFixedList<TItem> : IFixedCollection<TItem>
    {
        TItem this[int index] { get; }
    }
}