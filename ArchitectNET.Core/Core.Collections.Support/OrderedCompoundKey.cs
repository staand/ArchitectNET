using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectNET.Core.Collections.Support
{
    public sealed class OrderedCompoundKey<TElement> : IFixedList<TElement>,
                                                       IEquatable<OrderedCompoundKey<TElement>>,
                                                       IEquatable<IEnumerable<TElement>>,
                                                       IEquatable<IEnumerable>
    {
        private readonly TElement[] _elements;
        private readonly IEqualityComparer<TElement> _equalityComparer;
        private readonly int _hashCode;

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

        public OrderedCompoundKey(params TElement[] elements)
            : this((IEnumerable<TElement>) elements)
        {
        }

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

        public static bool operator !=(OrderedCompoundKey<TElement> key1, OrderedCompoundKey<TElement> key2)
        {
            return !(key1 == key2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            var elements = _elements;
            var elementCount = elements.Length;
            for (var i = 0; i < elementCount; i++)
                yield return elements[i];
        }

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

        public bool Contains(TElement element)
        {
            return IndexOf(element) >= 0;
        }

        public int Count => _elements.Length;

        public int IndexOf(TElement element, int startIndex = 0)
        {
            var elements = _elements;
            var elementCount = elements.Length;
            for (var i = startIndex; i < elementCount; i++)
            {
                if (_equalityComparer.Equals(element, elements[i]))
                    return i;
            }
            return -1;
        }

        public TElement this[int index] => _elements[index];

        public int LastIndexOf(TElement element, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = _elements.Length - 1;
            var elements = _elements;
            var elementCount = elements.Length;
            for (var i = startIndex; i >= 0; i--)
            {
                if (_equalityComparer.Equals(element, elements[i]))
                    return i;
            }
            return -1;
        }

        public override bool Equals(object otherObject)
        {
            var otherElements = otherObject as IEnumerable;
            return otherElements != null
                   && Equals(otherElements);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", _elements)}]";
        }
    }
}