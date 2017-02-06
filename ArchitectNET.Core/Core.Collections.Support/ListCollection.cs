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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool IFixedCollection<TItem>.Contains(TItem item)
        {
            return Contains(item);
        }

        int IFixedCollection<TItem>.Count => Count;

        TItem IFixedList<TItem>.this[int index] => this[index];

        bool IVolatileCollection<TItem>.Add(TItem item)
        {
            Add(item);
            return true;
        }

        void IVolatileCollection<TItem>.Clear()
        {
            Clear();
        }

        bool IVolatileCollection<TItem>.Remove(TItem item)
        {
            return Remove(item);
        }

        bool IVolatileList<TItem>.InsertAt(int index, TItem item)
        {
            Insert(index, item);
            return true;
        }

        TItem IVolatileList<TItem>.this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        TItem IVolatileList<TItem>.RemoveAt(int index)
        {
            var item = this[index];
            RemoveAt(index);
            return item;
        }
    }
}