namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Represents a generic list (indexed collection) which can be externally modified using public interface it provides.
    /// </summary>
    /// <typeparam name="TItem"> The type of the items in the list. </typeparam>
    /// <remarks>
    /// The main reason of having custom interface instead of using standard <see cref="System.Collections.Generic.IList{T}" />
    /// is a wish to deal with more clean and strong abstarction than BCL gives.
    /// </remarks>
    public interface IVolatileList<TItem> : IFixedList<TItem>, IVolatileCollection<TItem>
    {
        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index of the item to get or set. </param>
        /// <returns> The item at the specified index. </returns>
        new TItem this[int index] { get; set; }

        /// <summary>
        /// Inserts an item to the list at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index at which <paramref name="item" /> should be inserted. </param>
        /// <param name="item"> The item to insert into the list. </param>
        /// <returns>
        /// <see langword="true" /> if the item is added to the list; otherwise <see langword="false" />. If this
        /// method returns <see langword="false" /> the reason of rejecting <paramref name="item" /> is implementation specific and
        /// no assumptions about it should be made.
        /// </returns>
        bool InsertAt(int index, TItem item);

        /// <summary>
        /// Removes the item at the specified index from the list.
        /// </summary>
        /// <param name="index"> The zero-based index of the item to remove. </param>
        /// <returns> The removed item or default value of type <typeparamref name="TItem" /> if the list is not changed. </returns>
        TItem RemoveAt(int index);
    }
}