using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectNET.Core.Collections.Support
{
    /// <summary>
    /// Represents an immutable sequence of elements which implements a few specializations of <see cref="IEquatable{T}" />
    /// interface and thus can be used as a compound key in the dictionary. This class is sensitive to order of elements during
    /// equality check (this means that sequance (a, b, c) is NOT equal to (b, a, c) etc.).
    /// </summary>
    /// <typeparam name="TElement"> Type of elements which the compound key consists of. </typeparam>
    public sealed class OrderedCompoundKey<TElement> : IFixedList<TElement>,
                                                       IEquatable<OrderedCompoundKey<TElement>>,
                                                       IEquatable<IEnumerable<TElement>>,
                                                       IEquatable<IEnumerable>
    {
        /// <summary>
        /// An array of elements which this key consists of.
        /// </summary>
        private readonly TElement[] _elements;

        /// <summary>
        /// An <see cref="IEqualityComparer{T}" /> instance which is used during elements equality check.
        /// </summary>
        private readonly IEqualityComparer<TElement> _equalityComparer;

        /// <summary>
        /// Cashed hash code of this instance. Hash code is evaluated during the initialization of the instance.
        /// </summary>
        private readonly int _hashCode;

        /// <summary>
        /// Initializes a new instance of <see cref="OrderedCompoundKey{TElement}" /> with the specified elements and equality
        /// comparer for them.
        /// </summary>
        /// <param name="elements">
        /// A sequence of elements which are to be copied to the new instance of
        /// <see cref="OrderedCompoundKey{TElement}" />.
        /// </param>
        /// <param name="equalityComparer">
        /// An <see cref="IEqualityComparer{T}" /> implementation to be used during elements
        /// equality check. If this parameter is <see langword="null" /> <see cref="EqualityComparer{T}.Default" /> is used.
        /// </param>
        public OrderedCompoundKey(IEnumerable<TElement> elements, IEqualityComparer<TElement> equalityComparer = null)
        {
            Guard.ArgumentNotNull(elements, nameof(elements));
            var elementArray = elements.ToArray();
            var elementCount = elementArray.Length;
            var hashCode = 0;
            for (var i = 0; i < elementCount; i++)
            {
                var element = elementArray[i];
                if (element != null)
                    hashCode ^= element.GetHashCode() * (i + 1);
            }
            _elements = elementArray;
            _equalityComparer = equalityComparer ?? EqualityComparer<TElement>.Default;
            _hashCode = hashCode;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="OrderedCompoundKey{TElement}" /> with the specified elements.
        /// </summary>
        /// <param name="elements">
        /// An array of elements which are to be copied to the new instance of
        /// <see cref="OrderedCompoundKey{TElement}" />.
        /// </param>
        public OrderedCompoundKey(params TElement[] elements)
            : this((IEnumerable<TElement>) elements)
        {
        }

        /// <summary>
        /// Determines whether two specified compound keys have the same value. Just calls and returns result of the
        /// <see cref="Equals(OrderedCompoundKey{TElement})" /> method.
        /// </summary>
        /// <param name="key1"> The first comparing <see cref="OrderedCompoundKey{TElement}" />. </param>
        /// <param name="key2"> The second comparing <see cref="OrderedCompoundKey{TElement}" />. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="key1" /> is equal to <paramref name="key2" />, otherwise
        /// <see langword="false" />.
        /// </returns>
        public static bool operator ==(OrderedCompoundKey<TElement> key1, OrderedCompoundKey<TElement> key2)
        {
            var isKey1Null = ReferenceEquals(key1, null);
            var isKey2Null = ReferenceEquals(key2, null);
            if (isKey1Null && isKey2Null)
                return true;
            return !isKey1Null
                   && !isKey2Null
                   && key1.Equals(key2);
        }

        /// <summary>
        /// Determines whether two specified compound keys have different values. Just calls and returns an inverted result of the
        /// <see cref="Equals(OrderedCompoundKey{TElement})" /> method.
        /// </summary>
        /// <param name="key1"> The first comparing <see cref="OrderedCompoundKey{TElement}" />. </param>
        /// <param name="key2"> The second comparing <see cref="OrderedCompoundKey{TElement}" />. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="key1" /> is NOT equal to <paramref name="key2" />, otherwise
        /// <see langword="false" />.
        /// </returns>
        public static bool operator !=(OrderedCompoundKey<TElement> key1, OrderedCompoundKey<TElement> key2)
        {
            return !(key1 == key2);
        }

        /// <summary>
        /// Returns a non-generic enumerator that iterates through the elements of this compound key.
        /// </summary>
        /// <returns> A non-generic enumerator that can be used to iterate through the elements of this compound key. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns a generic enumerator that iterates through the elements of this compound key.
        /// </summary>
        /// <returns> A generic enumerator that can be used to iterate through the elements of this compound key. </returns>
        public IEnumerator<TElement> GetEnumerator()
        {
            var elements = _elements;
            var elementCount = elements.Length;
            for (var i = 0; i < elementCount; i++)
                yield return elements[i];
        }

        /// <summary>
        /// Determines whether this instance and a specified sequence of <typeparamref name="TElement" /> have the same value. This
        /// method takes into account the order of elements in the current compound key and in the specified
        /// <paramref name="otherElements" />.
        /// </summary>
        /// <param name="otherElements"> The sequence of elements to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if value of each element of <paramref name="otherElements" /> parameter is the same as the
        /// corresponding element of this instance, otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(IEnumerable<TElement> otherElements)
        {
            if (otherElements == null)
                return false;
            var otherKey = otherElements as OrderedCompoundKey<TElement>;
            if (otherKey != null)
                return Equals(otherKey);
            var elements = _elements;
            var elementCount = elements.Length;
            var elementIndex = 0;
            foreach (var otherElement in otherElements)
            {
                if (elementIndex >= elementCount)
                    return false;
                var element = elements[elementIndex++];
                if (!_equalityComparer.Equals(element, otherElement))
                    return false;
            }
            return elementIndex == elementCount;
        }

        /// <summary>
        /// Determines whether this instance and a specified sequence have the same value. This method takes into account the order
        /// of elements in the current compound key and in the specified <paramref name="otherElements" />.
        /// </summary>
        /// <param name="otherElements"> The sequence of objects to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if each element of <paramref name="otherElements" /> parameter is convertible to
        /// <typeparamref name="TElement" /> and it's value is the same as the corresponding element of this instance, otherwise
        /// <see langword="false" />.
        /// </returns>
        public bool Equals(IEnumerable otherElements)
        {
            if (otherElements == null)
                return false;
            var otherGenericElements = otherElements as IEnumerable<TElement>;
            if (otherGenericElements != null)
                return Equals(otherGenericElements);
            var elements = _elements;
            var elementCount = elements.Length;
            var elementIndex = 0;
            foreach (var otherElementObject in otherElements)
            {
                if (elementIndex >= elementCount)
                    return false;
                if (otherElementObject != null && !(otherElementObject is TElement))
                    return false;
                var element = elements[elementIndex++];
                var otherElement = (TElement) otherElementObject;
                if (!_equalityComparer.Equals(element, otherElement))
                    return false;
            }
            return elementIndex == elementCount;
        }

        /// <summary>
        /// Determines whether this instance and a specified compound key have the same value. This method takes into account the
        /// order of elements in the current compound key and in the specified <paramref name="otherKey" />.
        /// </summary>
        /// <param name="otherKey"> The compound key to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if each element of <paramref name="otherKey" /> parameter has the same value as the
        /// corresponding element of this instance, otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(OrderedCompoundKey<TElement> otherKey)
        {
            if (_hashCode != otherKey?._hashCode)
                return false;
            var elements = _elements;
            var otherElements = otherKey._elements;
            var elementCount = elements.Length;
            var otherElementCount = otherElements.Length;
            if (elementCount != otherElementCount)
                return false;
            for (var i = 0; i < elementCount; i++)
            {
                if (!_equalityComparer.Equals(elements[i], otherElements[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether this compound key contains a specific element.
        /// </summary>
        /// <param name="element"> The element to locate. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="element" /> is found in the key, otherwise
        /// <see langword="false" />.
        /// </returns>
        public bool Contains(TElement element)
        {
            return IndexOf(element) >= 0;
        }

        /// <summary>
        /// Returns a number of elements in this compound key,
        /// </summary>
        public int Count => _elements.Length;

        /// <summary>
        /// Searches for the specified element and returns the zero-based index of the first occurrence within the range of
        /// elements
        /// in the compound key that starts at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="element">
        /// The element to locate in the compound key. The value can be <see langword="null" /> for
        /// reference types.
        /// </param>
        /// <param name="startIndex"> The zero-based starting index of the search. </param>
        /// <param name="maximumCount"> The number of elements in the section to search. </param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="element" /> within the range of elements in the
        /// compound key that starts at <paramref name="startIndex" /> and contains <paramref name="maximumCount" /> number of
        /// elements, if found; otherwise, –1.
        /// </returns>
        public int IndexOf(TElement element, int startIndex = 0, int maximumCount = -1)
        {
            var elements = _elements;
            if (maximumCount < 0)
                maximumCount = elements.Length - startIndex;
            var endIndex = startIndex + maximumCount;
            for (var i = startIndex; i < endIndex; i++)
            {
                if (_equalityComparer.Equals(element, elements[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index of the element to get. </param>
        /// <returns> The element at the specified index. </returns>
        public TElement this[int index] => _elements[index];

        /// <summary>
        /// Searches for the specified element and returns the zero-based index of the last occurrence within the range of elements
        /// in the compund key that ends at the specified index and contains the specified number of elements.
        /// </summary>
        /// <param name="element">
        /// The element to locate in the compund key. The value can be <see langword="null" /> for reference
        /// types.
        /// </param>
        /// <param name="startIndex"> The zero-based starting index of the backward search. </param>
        /// <param name="maximumCount"> The number of elements in the section to search. </param>
        /// <returns>
        /// The zero-based index of the last occurrence of <paramref name="element" /> within the range of elements in the compound
        /// key that ends at <paramref name="startIndex" /> and contains <paramref name="maximumCount" /> number of elements, if
        /// found; otherwise, –1.
        /// </returns>
        public int LastIndexOf(TElement element, int startIndex = -1, int maximumCount = -1)
        {
            if (startIndex < 0)
                startIndex = _elements.Length - 1;
            if (maximumCount < 0)
                maximumCount = startIndex + 1;
            var elements = _elements;
            var endIndex = startIndex - maximumCount;
            for (var i = startIndex; i > endIndex; i--)
            {
                if (_equalityComparer.Equals(element, elements[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which should be also an
        /// <see cref="OrderedCompoundKey{TElement}" /> or of any other type which implements <see cref="IEnumerable{T}" /> or
        /// <see cref="IEnumerable" /> interface, have the same value.
        /// </summary>
        /// <param name="otherObject"> The object to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="otherObject" /> is an <see cref="OrderedCompoundKey{TElement}" /> or any
        /// other one implementing <see cref="IEnumerable{T}" /> or <see cref="IEnumerable" /> interface and it's value is the same
        /// as this instance; otherwise <see langword="false" />.
        /// </returns>
        /// <remarks>
        /// This method works in the same way as one of <see cref="Equals(OrderedCompoundKey{TElement})" />,
        /// <see cref="Equals(IEnumerable{TElement})" /> or <see cref="Equals(IEnumerable)" /> methods depending on the actual type
        /// of the <paramref name="otherObject" />.
        /// </remarks>
        public override bool Equals(object otherObject)
        {
            var otherElements = otherObject as IEnumerable;
            return otherElements != null
                   && Equals(otherElements);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="OrderedCompoundKey{TElement}" />.
        /// </summary>
        /// <returns> A 32-bit signed integer hash code. </returns>
        public override int GetHashCode()
        {
            return _hashCode;
        }

        /// <summary>
        /// Returns the string representation of this <see cref="OrderedCompoundKey{TElement}" />.
        /// </summary>
        /// <returns>
        /// <see cref="string" /> object representing this instance.
        /// </returns>
        public override string ToString()
        {
            return $"[{string.Join(", ", _elements)}]";
        }
    }
}