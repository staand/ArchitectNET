namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Represents a generic list (indexed collection) which does not provide any capabilities for external modification.
    /// </summary>
    /// <typeparam name="TItem"> The type of the items in the list. </typeparam>
    /// <remarks>
    /// The main reason of having custom interface instead of using standard <see cref="System.Collections.Generic.IList{T}" />
    /// is a wish
    /// to deal with more clean and strong abstarction than BCL gives.
    /// </remarks>
    public interface IFixedList<TItem> : IFixedCollection<TItem>
    {
        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index of the element to get. </param>
        /// <returns> The item at the specified index. </returns>
        TItem this[int index] { get; }

        /// <summary>
        /// Searches for the specified item and returns the zero-based index of the first occurrence within the range of elements
        /// in the list that starts at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="item"> The item to locate in the list. The value can be <see langword="null" /> for reference types. </param>
        /// <param name="startIndex"> The zero-based starting index of the search. </param>
        /// <param name="maximumCount"> The number of items in the section to search. </param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="item" /> within the range of elements in the
        /// list that starts at <paramref name="startIndex" /> and contains <paramref name="maximumCount" /> number of elements, if
        /// found; otherwise, –1.
        /// </returns>
        int IndexOf(TItem item, int startIndex = 0, int maximumCount = -1);

        /// <summary>
        /// Searches for the specified item and returns the zero-based index of the last occurrence within the range of elements
        /// in the list that ends at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="item"> The item to locate in the list. The value can be <see langword="null" /> for reference types. </param>
        /// <param name="startIndex"> The zero-based starting index of the backward search. </param>
        /// <param name="maximumCount"> The number of elements in the section to search. </param>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="item" /> within the range of elements in the
        /// list that ends at <paramref name="startIndex" /> and contains <paramref name="maximumCount" /> number of elements, if
        /// found; otherwise, –1.
        /// </returns>
        int LastIndexOf(TItem item, int startIndex = -1, int maximumCount = -1);
    }
}