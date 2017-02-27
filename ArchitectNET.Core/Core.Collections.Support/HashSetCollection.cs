using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    /// <summary>
    /// A subclass of <see cref="HashSet{T}" /> class which additionally implements <see cref="IFixedCollection{TItem}" /> and
    /// <see cref="IVolatileCollection{TItem}" /> interfaces and thus is an adapter between standard set implementation and
    /// interfaces introduced by ArchitectNET.Core.Collections namespace.
    /// </summary>
    /// <typeparam name="TItem"> The type of the items in the set. </typeparam>
    public class HashSetCollection<TItem> : HashSet<TItem>, IVolatileCollection<TItem>
    {
        /// <summary>
        /// Determines whether the set contains a specific value.
        /// </summary>
        /// <param name="item"> The item to locate. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="item" /> is found in the collection, otherwise
        /// <see langword="false" />.
        /// </returns>
        bool IFixedCollection<TItem>.Contains(TItem item)
        {
            return Contains(item);
        }

        /// <summary>
        /// Gets the number of items currently contained in the set.
        /// </summary>
        int IFixedCollection<TItem>.Count => Count;

        /// <summary>
        /// Adds an item to the set.
        /// </summary>
        /// <param name="item"> The item to add to the set. </param>
        /// <returns>
        /// <see langword="true" /> if the item is added to the set; otherwise <see langword="false" />, which means that
        /// the specified items is already present in the set.
        /// </returns>
        bool IVolatileCollection<TItem>.Add(TItem item)
        {
            return Add(item);
        }

        /// <summary>
        /// Removes all items from the set.
        /// </summary>
        void IVolatileCollection<TItem>.Clear()
        {
            Clear();
        }

        /// <summary>
        /// Removes the specified item from the set.
        /// </summary>
        /// <param name="item"> The item to remove from the set. </param>
        /// <returns>
        /// <see langword="true" /> if the item was removed from the collection; otherwise <see langword="false" />,
        /// which means that the specified item is not present in the set.
        /// </returns>
        bool IVolatileCollection<TItem>.Remove(TItem item)
        {
            return Remove(item);
        }
    }
}