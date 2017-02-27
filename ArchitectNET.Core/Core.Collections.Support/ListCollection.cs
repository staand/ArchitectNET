using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    /// <summary>
    /// A subclass of <see cref="List{T}" /> class which additionally implements <see cref="IFixedCollection{TItem}" />,
    /// <see cref="IFixedList{TItem}" />, <see cref="IVolatileCollection{TItem}" /> and <see cref="IVolatileList{TItem}" />
    /// interfaces and thus is an adapter between standard array-based list implementation and interfaces introduced by
    /// ArchitectNET.Core.Collections namespace.
    /// </summary>
    /// <typeparam name="TItem"> The type of the items in the list. </typeparam>
    public class ListCollection<TItem> : List<TItem>, IVolatileList<TItem>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ListCollection{TItem}" /> that is empty and has default initial capacity.
        /// </summary>
        public ListCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ListCollection{TItem}" /> that is empty and has the specified initial
        /// capacity.
        /// </summary>
        /// <param name="capacity"> The number of elements that the new list can initially store. </param>
        public ListCollection(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ListCollection{TItem}" /> that contains items copied from the specified
        /// collection and has sufficient capacity to accommodate the number of items copied.
        /// </summary>
        /// <param name="itemsToAdd"> The collection whose items are copied to the new list. </param>
        public ListCollection(IEnumerable<TItem> itemsToAdd)
            : base(itemsToAdd)
        {
        }

        /// <summary>
        /// Determines whether the list contains a specific value.
        /// </summary>
        /// <param name="item"> The item to locate. </param>
        /// <returns> <see langword="true" /> if <paramref name="item" /> is found in the list, otherwise <see langword="false" />. </returns>
        bool IFixedCollection<TItem>.Contains(TItem item)
        {
            return Contains(item);
        }

        /// <summary>
        /// Gets the number of items currently contained in the collection.
        /// </summary>
        int IFixedCollection<TItem>.Count => Count;

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index of the element to get. </param>
        /// <returns> The item at the specified index. </returns>
        TItem IFixedList<TItem>.this[int index] => this[index];

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item"> The item to add to the list. </param>
        /// <returns> This method always returns <see langword="true" />. </returns>
        bool IVolatileCollection<TItem>.Add(TItem item)
        {
            Add(item);
            return true;
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        void IVolatileCollection<TItem>.Clear()
        {
            Clear();
        }

        /// <summary>
        /// Removes the specified item from the list.
        /// </summary>
        /// <param name="item"> The item to remove from the list. </param>
        /// <returns>
        /// <see langword="true" /> if the item was found and removed from the list; otherwise <see langword="false" />.
        /// </returns>
        bool IVolatileCollection<TItem>.Remove(TItem item)
        {
            return Remove(item);
        }

        /// <summary>
        /// Inserts an item to the list at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index at which <paramref name="item" /> should be inserted. </param>
        /// <param name="item"> The item to insert into the list. </param>
        /// <returns> This method always returns <see langword="true" />. </returns>
        bool IVolatileList<TItem>.InsertAt(int index, TItem item)
        {
            Insert(index, item);
            return true;
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index of the item to get or set. </param>
        /// <returns> The item at the specified index. </returns>
        TItem IVolatileList<TItem>.this[int index]
        {
            get { return this[index]; }
            set { this[index] = value; }
        }

        /// <summary>
        /// Removes the item at the specified index from the list.
        /// </summary>
        /// <param name="index"> The zero-based index of the item to remove. </param>
        /// <returns> The removed item or default value of type <typeparamref name="TItem" /> if the list is not changed. </returns>
        TItem IVolatileList<TItem>.RemoveAt(int index)
        {
            var item = this[index];
            RemoveAt(index);
            return item;
        }
    }
}