using System.Collections.Generic;

namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Represents a generic not ordered collection which does not provide any capabilities for external modification
    /// </summary>
    /// <typeparam name="TItem"> The type of the items in the collection. </typeparam>
    /// <remarks>
    /// The main reason of having custom interface instead of using standard <see cref="ICollection{T}" /> is a wish
    /// to deal with more clean and strong abstarction than BCL gives
    /// </remarks>
    public interface IFixedCollection<TItem> : IEnumerable<TItem>
    {
        /// <summary>
        /// Gets the number of elements currently contained in the collection.
        /// </summary>
        /// <remarks>
        /// Although the collection does not provide any methods for modification, <see cref="Count" /> property may
        /// return different values as a result of two different calls
        /// </remarks>
        int Count { get; }

        /// <summary>
        /// Determines whether the collection contains a specific value
        /// </summary>
        /// <param name="item"> </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="item" /> is found in the collection, otherwise
        /// <see langword="false" />
        /// </returns>
        /// <remarks> Equality determination is an implementation specific behaviour </remarks>
        bool Contains(TItem item);
    }
}