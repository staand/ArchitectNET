using System.Collections;
using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    public class ListCollection<TItem> : List<TItem>, IVolatileList<TItem>
    {
        public ListCollection()
        {
        }

        public ListCollection(int capacity)
            : base(capacity)
        {
        }

        public ListCollection(IEnumerable<TItem> itemsToAdd)
            : base(itemsToAdd)
        {
        }

        int IFixedCollection<TItem>.Count
        {
            get { return Count; }
        }

        TItem IVolatileList<TItem>.this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        TItem IFixedList<TItem>.this[int index]
        {
            get { return this[index]; }
        }

        bool IVolatileCollection<TItem>.Add(TItem item)
        {
            Add(item);
            return true;
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

        bool IVolatileList<TItem>.InsertAt(int index, TItem item)
        {
            Insert(index, item);
            return true;
        }

        bool IVolatileCollection<TItem>.Remove(TItem item)
        {
            return Remove(item);
        }

        TItem IVolatileList<TItem>.RemoveAt(int index)
        {
            var item = this[index];
            RemoveAt(index);
            return item;
        }
    }
}