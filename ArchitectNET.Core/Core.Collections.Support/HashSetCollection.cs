using System.Collections;
using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    public class HashSetCollection<TItem> : HashSet<TItem>, IVolatileCollection<TItem>
    {
        int IFixedCollection<TItem>.Count
        {
            get { return Count; }
        }

        bool IVolatileCollection<TItem>.Add(TItem item)
        {
            return Add(item);
        }

        void IVolatileCollection<TItem>.Clear()
        {
            Clear();
        }

        bool IFixedCollection<TItem>.Contains(TItem item)
        {
            return Contains(item);
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool IVolatileCollection<TItem>.Remove(TItem item)
        {
            return Remove(item);
        }
    }
}