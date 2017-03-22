using System;
using System.Collections;
using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    /// <summary>
    /// Represents an immutable sequence of elements which implements a few specializations of <see cref="IEquatable{T}" />
    /// interface and thus can be used as a compound key in the dictionary. This class is NOT sensitive to order of elements
    /// during equality check (this means that sequance (a, b, c) is equal to (b, a, c) etc.).
    /// </summary>
    /// <typeparam name="TElement"> Type of elements which the compound key consists of. </typeparam>
    public sealed class UnorderedCompoundKey<TElement> : IFixedCollection<TElement>,
                                                         IEquatable<UnorderedCompoundKey<TElement>>,
                                                         IEquatable<IEnumerable<TElement>>,
                                                         IEquatable<IEnumerable>
    {
        /// <summary>
        /// A set of elements which this key consists of.
        /// </summary>
        private readonly HashSet<TElement> _elements;

        /// <summary>
        /// Cashed hash code of this instance. Hash code is evaluated during the initialization of the instance.
        /// </summary>
        private readonly int _hashCode;

        /// <summary>
        /// Determines whether the initial sequence of elements which this instance was created from had at least one
        /// <see langword="null" /> element.
        /// </summary>
        private readonly bool _hasNullElement;

        /// <summary>
        /// Initializes a new instance of <see cref="UnorderedCompoundKey{TElement}" /> with the specified elements and equality
        /// comparer for them.
        /// </summary>
        /// <param name="elements">
        /// A sequence of elements which are to be copied to the new instance of <see cref="UnorderedCompoundKey{TElement}" />.
        /// </param>
        /// <param name="equalityComparer">
        /// An <see cref="IEqualityComparer{T}" /> implementation to be used during elements
        /// equality check. If this parameter is <see langword="null" /> <see cref="EqualityComparer{T}.Default" /> is used.
        /// </param>
        public UnorderedCompoundKey(IEnumerable<TElement> elements, IEqualityComparer<TElement> equalityComparer = null)
        {
            Guard.ArgumentNotNull(elements, nameof(elements));
            equalityComparer = equalityComparer ?? EqualityComparer<TElement>.Default;
            var hashCode = 0;
            var hasNullElement = false;
            _elements = new HashSet<TElement>(equalityComparer);
            foreach (var element in elements)
            {
                var isNullElement = element == null;
                hasNullElement |= isNullElement;
                if (isNullElement)
                    continue;
                _elements.Add(element);
                hashCode ^= element.GetHashCode();
            }
            _hasNullElement = hasNullElement;
            _hashCode = hashCode ^ (hasNullElement ? 1 : 0);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UnorderedCompoundKey{TElement}" /> with the specified elements.
        /// </summary>
        /// <param name="elements">
        /// An array of elements which are to be copied to the new instance of
        /// <see cref="UnorderedCompoundKey{TElement}" />.
        /// </param>
        public UnorderedCompoundKey(params TElement[] elements)
            : this((IEnumerable<TElement>) elements)
        {
        }

        /// <summary>
        /// Determines whether two specified compound keys have the same value. Just calls and returns result of the
        /// <see cref="Equals(UnorderedCompoundKey{TElement})" /> method.
        /// </summary>
        /// <param name="key1"> The first comparing <see cref="UnorderedCompoundKey{TElement}" />. </param>
        /// <param name="key2"> The second comparing <see cref="UnorderedCompoundKey{TElement}" />. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="key1" /> is equal to <paramref name="key2" />, otherwise
        /// <see langword="false" />.
        /// </returns>
        public static bool operator ==(UnorderedCompoundKey<TElement> key1, UnorderedCompoundKey<TElement> key2)
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
        /// <see cref="Equals(UnorderedCompoundKey{TElement})" /> method.
        /// </summary>
        /// <param name="key1"> The first comparing <see cref="UnorderedCompoundKey{TElement}" />. </param>
        /// <param name="key2"> The second comparing <see cref="UnorderedCompoundKey{TElement}" />. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="key1" /> is NOT equal to <paramref name="key2" />, otherwise
        /// <see langword="false" />.
        /// </returns>
        public static bool operator !=(UnorderedCompoundKey<TElement> key1, UnorderedCompoundKey<TElement> key2)
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
            return _elements.GetEnumerator();
        }

        /// <summary>
        /// Determines whether this instance and a specified sequence of <typeparamref name="TElement" /> have the same value. This
        /// method DOES NOT take into account the order of elements in the current compound key and in the specified
        /// <paramref name="otherElements" />.
        /// </summary>
        /// <param name="otherElements"> The sequence of elements to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if symmetric difference between this instance and <paramref name="otherElements" /> parameter
        /// returns an empty set; otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(IEnumerable<TElement> otherElements)
        {
            if (otherElements == null)
                return false;
            var otherKey = otherElements as UnorderedCompoundKey<TElement>;
            if (otherKey != null)
                return Equals(otherKey);
            var hasNullElement = _hasNullElement;
            var hasOtherNullElement = false;
            var otherElementList = new List<TElement>();
            foreach (var otherElement in otherElements)
            {
                var isOtherElementNull = otherElement == null;
                if (isOtherElementNull && !hasNullElement)
                    return false;
                if (!isOtherElementNull)
                    otherElementList.Add(otherElement);
                hasOtherNullElement |= isOtherElementNull;
            }
            return hasNullElement == hasOtherNullElement
                   && _elements.SetEquals(otherElementList);
        }

        /// <summary>
        /// Determines whether this instance and a specified sequence have the same value. This method DOES NOT take into account
        /// the order of elements in the current compound key and in the specified <paramref name="otherElements" />.
        /// </summary>
        /// <param name="otherElements"> The sequence of objects to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if each element of <paramref name="otherElements" /> parameter is convertible to
        /// <typeparamref name="TElement" /> and symmetric difference between this instance and <paramref name="otherElements" />
        /// parameter returns an empty set; otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(IEnumerable otherElements)
        {
            if (otherElements == null)
                return false;
            var otherGenericElements = otherElements as IEnumerable<TElement>;
            if (otherGenericElements != null)
                return Equals(otherGenericElements);
            var hasNullElement = _hasNullElement;
            var hasOtherNullElement = false;
            var otherElementList = new List<TElement>();
            foreach (var otherElementObject in otherElements)
            {
                var isOtherElementNull = otherElementObject == null;
                hasOtherNullElement |= isOtherElementNull;
                if (isOtherElementNull)
                {
                    if (!hasNullElement)
                        return false;
                    continue;
                }
                if (!(otherElementObject is TElement))
                    return false;
                var otherElement = (TElement) otherElementObject;
                otherElementList.Add(otherElement);
            }
            return hasNullElement == hasOtherNullElement
                   && _elements.SetEquals(otherElementList);
        }

        /// <summary>
        /// Determines whether this instance and a specified compound key have the same value. This method DOES NOT take into
        /// account the order of elements in the current compound key and in the specified <paramref name="otherKey" />.
        /// </summary>
        /// <param name="otherKey"> The compound key to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if symmetric difference between this instance and <paramref name="otherKey" /> parameter
        /// returns an empty set; otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(UnorderedCompoundKey<TElement> otherKey)
        {
            return otherKey != null
                   && _hashCode == otherKey._hashCode
                   && _hasNullElement == otherKey._hasNullElement
                   && _elements.Count == otherKey._elements.Count
                   && _elements.SetEquals(otherKey._elements);
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
            if (element == null)
                return _hasNullElement;
            return _elements.Contains(element);
        }

        /// <summary>
        /// Returns a number of elements in this compound key,
        /// </summary>
        public int Count => _elements.Count;

        /// <summary>
        /// Determines whether this instance and a specified object, which should be also an
        /// <see cref="UnorderedCompoundKey{TElement}" /> or of any other type which implements <see cref="IEnumerable{T}" /> or
        /// <see cref="IEnumerable" /> interface, have the same value.
        /// </summary>
        /// <param name="otherObject"> The object to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="otherObject" /> is an <see cref="UnorderedCompoundKey{TElement}" /> or any
        /// other one implementing <see cref="IEnumerable{T}" /> or <see cref="IEnumerable" /> interface and it's value is the same
        /// as this instance; otherwise <see langword="false" />.
        /// </returns>
        /// <remarks>
        /// This method works in the same way as one of <see cref="Equals(UnorderedCompoundKey{TElement})" />,
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
        /// Returns the hash code for this <see cref="UnorderedCompoundKey{TElement}" />.
        /// </summary>
        /// <returns> A 32-bit signed integer hash code. </returns>
        public override int GetHashCode()
        {
            return _hashCode;
        }

        /// <summary>
        /// Returns the string representation of this <see cref="UnorderedCompoundKey{TElement}" />.
        /// </summary>
        /// <returns>
        /// <see cref="string" /> object representing this instance.
        /// </returns>
        public override string ToString()
        {
            var nullText = _hasNullElement ? "<null>" : string.Empty;
            return $"{{{string.Join(", ", _elements)}{nullText}}}";
        }
    }
}