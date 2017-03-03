namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Represents a generic list (indexed collection) which does not provide any capabilities for external modification.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the list.</typeparam>
    /// <remarks>
    /// The main reason of having custom interface instead of using standard <see cref="System.Collections.Generic.IList{T}" /> is a wish
    /// to deal with more clean and strong abstarction than BCL gives.
    /// </remarks>
    public interface IFixedList<TItem> : IFixedCollection<TItem>
    {
        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The item at the specified index.</returns>
        TItem this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        int IndexOf(TItem item, int startIndex = 0, int maximumCount = -1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        int LastIndexOf(TItem item, int startIndex = -1, int maximumCount = -1);
    }
}