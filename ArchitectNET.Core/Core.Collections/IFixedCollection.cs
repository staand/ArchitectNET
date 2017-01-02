using System.Collections.Generic;

namespace ArchitectNET.Core.Collections
{
    public interface IFixedCollection<TItem> : IEnumerable<TItem>
    {
        int Count { get; }
        bool Contains(TItem item);
    }
}