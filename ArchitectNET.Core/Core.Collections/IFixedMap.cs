using System.Collections.Generic;

namespace ArchitectNET.Core.Collections
{
    /// <summary>
    /// Represents a generic unordered key/value collection which does not provide any capabilities for external modification.
    /// </summary>
    /// <typeparam name="TKey"> The type of keys in the read-only map. </typeparam>
    /// <typeparam name="TValue"> The type of values in the read-only map. </typeparam>
    /// <remarks>
    /// The main reason of having custom interface instead of using standard <see cref="IReadOnlyDictionary{TKey,TValue}" /> is
    /// a wish to deal with more clean and strong abstarction than BCL gives.
    /// </remarks>
    public interface IFixedMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets the number of key/value pairs in the map.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key"> The key to locate. </param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if the key is found;
        /// otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed
        /// uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the map contains an element with the specified key; otherwise,
        /// <see langword="false" />.
        /// </returns>
        bool TryGetValue(TKey key, out TValue value);
    }
}