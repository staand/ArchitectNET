namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Represents a generic unordered collection which can be externally modified using public interface it provides.
    /// </summary>
    /// <typeparam name="TItem"> The type of the items in the collection. </typeparam>
    /// <remarks>
    /// The main reason of having custom interface instead of using standard
    /// <see cref="System.Collections.Generic.ICollection{T}" /> is a wish to deal with more clean and strong abstarction than
    /// BCL gives.
    /// </remarks>
    public interface IVolatileCollection<TItem> : IFixedCollection<TItem>
    {
        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item"> The item to add to the collection. </param>
        /// <returns>
        /// <see langword="true" /> if the item is added to the collection; otherwise <see langword="false" />. If this
        /// method returns <see langword="false" /> the reason of rejecting <paramref name="item" /> is implementation specific and
        /// no assumptions about it should be made.
        /// </returns>
        bool Add(TItem item);

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Removes the specified item from the collection.
        /// </summary>
        /// <param name="item"> The item to remove from the collection. </param>
        /// <returns>
        /// <see langword="true" /> if the item was removed from the collection; otherwise <see langword="false" />. If
        /// this method returns <see langword="false" /> the reason of rejecting <paramref name="item" /> is implementation
        /// specific and no assumptions about it should be made.
        /// </returns>
        bool Remove(TItem item);
    }
}