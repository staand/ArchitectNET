using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectNET.Core.Collections.Support
{
    /// <summary>
    /// Represents an immutable sequence of elements which implements a few specializations of <see cref="IEquatable{T}" />
    /// interface and thus can be used as a compound key in the dictionary. This class is NOT sensitive to order of elements
    /// during equality check but TAKES INTO ACCOUNT duplicates (this means that sequance (a, b, c) is equal
    /// to (b, a, c) but not equal to (a, b, c, c) etc.).
    /// </summary>
    /// <typeparam name="TElement"> Type of elements which the compound key consists of. </typeparam>
    public sealed class UnorderedCompoundKey<TElement> : IFixedCollection<TElement>,
                                                         IEquatable<UnorderedCompoundKey<TElement>>,
                                                         IEquatable<IEnumerable<TElement>>,
                                                         IEquatable<IEnumerable>
    {
        /// <summary>
        /// A dictionary that maps any particular element of the key to number of its occurrences in the initial sequence.
        /// </summary>
        private readonly Dictionary<TElement, int> _elementCountMap;

        /// <summary>
        /// The number of <see langword="null" /> elements in the initial sequence.
        /// </summary>
        private readonly int _nullElementCount;

        /// <summary>
        /// The number of elements (including <see langword="null" /> ones) in the current key. This value is equal to the number
        /// of elements in the initial sequence.
        /// </summary>
        private readonly int _count;

        /// <summary>
        /// Cashed hash code of this instance. Hash code is evaluated during the initialization of the instance.
        /// </summary>
        private readonly int _hashCode;

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
            var elementCountMap = new Dictionary<TElement, int>(equalityComparer);
            var count = 0;
            var nullElementCount = 0;
            var hashCode = 0;
            foreach (var element in elements)
            {
                count++;
                if (element == null)
                {
                    nullElementCount++;
                    continue;
                }
                int currentElementCount;
                elementCountMap.TryGetValue(element, out currentElementCount);
                elementCountMap[element] = currentElementCount + 1;
                hashCode ^= element.GetHashCode();
            }
            _elementCountMap = elementCountMap;
            _count = count;
            _nullElementCount = nullElementCount;
            _hashCode = hashCode ^ nullElementCount;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="UnorderedCompoundKey{TElement}" /> with the specified elements.
        /// </summary>
        /// <param name="elements">
        /// An array of elements which are to be copied to the new instance of
        /// <see cref="UniqueCompoundKey{TElement}" />.
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
        /// Returns a number of elements in this compound key,
        /// </summary>
        public int Count => _count;

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
            foreach (var keyValue in _elementCountMap)
            {
                var element = keyValue.Key;
                var count = keyValue.Value;
                for (var i = 0; i < count; i++)
                    yield return element;
            }
            var nullElementCount = _nullElementCount;
            for (var i = 0; i < nullElementCount; i++)
                yield return (TElement) (object) null;
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
                return _nullElementCount > 0;
            return _elementCountMap.ContainsKey(element);
        }

        /// <summary>
        /// Determines whether this instance and a specified compound key have the same value. This method IS NOT sensitive to the
        /// order of elements in the current compound key and in the specified <paramref name="otherKey" /> but TAKES INTO ACCOUNT
        /// element duplicates.
        /// </summary>
        /// <param name="otherKey"> The compound key to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if number of occurrences of each distinct element in the current instance is the same as in the
        /// <paramref name="otherKey" /> parameter and vice versa; otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(UnorderedCompoundKey<TElement> otherKey)
        {
            var isExactlyDifferent = otherKey == null
                                     || _hashCode != otherKey._hashCode
                                     || _nullElementCount != otherKey._nullElementCount
                                     || _count != otherKey._count
                                     || _elementCountMap.Count != otherKey._elementCountMap.Count;
            if (isExactlyDifferent)
                return false;
            var otherElementCountMap = otherKey._elementCountMap;
            foreach (var keyValue in _elementCountMap)
            {
                var element = keyValue.Key;
                var count = keyValue.Value;
                int otherCount;
                if (!otherElementCountMap.TryGetValue(element, out otherCount)
                    || count != otherCount)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines whether this instance and a specified sequence of <typeparamref name="TElement" /> have the same value. This
        /// method IS NOT sensitive to the order of elements in the current compound key and in the specified
        /// <paramref name="otherElements" /> but TAKES INTO ACCOUNT element duplicates.
        /// </summary>
        /// <param name="otherElements"> The sequence of elements to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if number of occurrences of each distinct element in the current key is the same as
        /// in the <paramref name="otherElements" /> parameter and vice versa; otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(IEnumerable<TElement> otherElements)
        {
            if (otherElements == null)
                return false;
            var otherKey = otherElements as UnorderedCompoundKey<TElement>;
            if (otherKey != null)
                return Equals(otherKey);
            var nullElementCount = _nullElementCount;
            var elementCountMap = _elementCountMap;
            var otherElementCountMap = new Dictionary<TElement, int>(elementCountMap.Comparer);
            var otherNullElementCount = 0;
            var otherElementCount = 0;
            foreach (var otherElement in otherElements)
            {
                otherElementCount++;
                if (otherElement == null)
                {
                    if (otherNullElementCount >= nullElementCount)
                        return false;
                    otherNullElementCount++;
                    continue;
                }
                int count;
                if (!elementCountMap.TryGetValue(otherElement, out count))
                    return false;
                int otherCount;
                otherElementCountMap.TryGetValue(otherElement, out otherCount);
                if (otherCount >= count)
                    return false;
                otherElementCountMap[otherElement] = otherCount + 1;
            }
            if (nullElementCount != otherNullElementCount
                || _count != otherElementCount)
            {
                return false;
            }
            foreach (var keyValue in elementCountMap)
            {
                var element = keyValue.Key;
                var count = keyValue.Value;
                int otherCount;
                if (!otherElementCountMap.TryGetValue(element, out otherCount)
                    || count != otherCount)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines whether this instance and a specified sequence have the same value. This method IS NOT sensitive to the
        /// order of elements in the current compound key and in the specified <paramref name="otherElements" /> but TAKES INTO
        /// ACCOUNT element duplicates.
        /// </summary>
        /// <param name="otherElements"> The sequence of objects to compare to this instance. </param>
        /// <returns>
        /// if each element of <paramref name="otherElements" /> parameter is convertible to
        /// <typeparamref name="TElement" /> and number of occurrences of each distinct element in the current key is the same as
        /// in the <paramref name="otherElements" /> parameter and vice versa; otherwise <see langword="false" />.
        /// </returns>
        public bool Equals(IEnumerable otherElements)
        {
            if (otherElements == null)
                return false;
            var otherGenericElements = otherElements as IEnumerable<TElement>;
            if (otherGenericElements != null)
                return Equals(otherGenericElements);
            var otherElementList = new List<TElement>();
            var elementCountMap = _elementCountMap;
            var nullElementCount = _nullElementCount;
            var otherNullElementCount = 0;
            foreach (var otherElementObject in otherElements)
            {
                if (otherElementObject == null)
                {
                    if (otherNullElementCount >= nullElementCount)
                        return false;
                    otherNullElementCount++;
                    otherElementList.Add((TElement) (object) null);
                    continue;
                }
                if (!(otherElementObject is TElement))
                    return false;
                var otherElement = (TElement) otherElementObject;
                if (!elementCountMap.ContainsKey(otherElement))
                    return false;
                otherElementList.Add(otherElement);
            }
            return nullElementCount == otherNullElementCount
                   && Equals(otherElementList);
        }

        /// <summary>
        /// Returns the string representation of this <see cref="UnorderedCompoundKey{TElement}" />.
        /// </summary>
        /// <returns>
        /// <see cref="string" /> object representing this instance.
        /// </returns>
        public override string ToString()
        {
            var hasNullElements = _nullElementCount > 0;
            var nullText = hasNullElements ? $", {_nullElementCount} x <null>" : string.Empty;
            return $"{{{string.Join(", ", _elementCountMap.Select(kv => $"{kv.Key} x {kv.Value}"))}{nullText}}}";
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
    }
}