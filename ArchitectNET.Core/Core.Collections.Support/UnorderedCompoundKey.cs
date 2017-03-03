using System;
using System.Collections;
using System.Collections.Generic;

namespace ArchitectNET.Core.Collections.Support
{
    public sealed class UnorderedCompoundKey<TElement> : IFixedCollection<TElement>,
                                                         IEquatable<UnorderedCompoundKey<TElement>>,
                                                         IEquatable<IEnumerable<TElement>>,
                                                         IEquatable<IEnumerable>
    {
        private readonly HashSet<TElement> _elements;
        private readonly int _hashCode;
        private readonly bool _hasNullElement;

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

        public UnorderedCompoundKey(params TElement[] elements)
            : this((IEnumerable<TElement>) elements)
        {
        }

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

        public static bool operator !=(UnorderedCompoundKey<TElement> key1, UnorderedCompoundKey<TElement> key2)
        {
            return !(key1 == key2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        public bool Equals(IEnumerable<TElement> otherElements)
        {
            if (otherElements == null)
                return false;
            var otherKey = otherElements as UnorderedCompoundKey<TElement>;
            if (otherKey != null)
                return Equals(otherKey);
            var elements = _elements;
            var hasNullElement = _hasNullElement;
            foreach (var otherElement in otherElements)
            {
                var isOtherElementNull = otherElement == null;
                if (isOtherElementNull && !hasNullElement)
                    return false;
                if (!isOtherElementNull && !elements.Contains(otherElement))
                    return false;
            }
            return true;
        }

        public bool Equals(IEnumerable otherElements)
        {
            if (otherElements == null)
                return false;
            var otherGenericElements = otherElements as IEnumerable<TElement>;
            if (otherGenericElements != null)
                return Equals(otherGenericElements);
            var elements = _elements;
            var hasNullElement = _hasNullElement;
            foreach (var otherElementObject in otherElements)
            {
                if (otherElementObject == null)
                {
                    if (!hasNullElement)
                        return false;
                    continue;
                }
                if (!(otherElementObject is TElement))
                    return false;
                var otherElement = (TElement) otherElementObject;
                if (!elements.Contains(otherElement))
                    return false;
            }
            return true;
        }

        public bool Equals(UnorderedCompoundKey<TElement> otherKey)
        {
            return otherKey != null
                   && _hashCode == otherKey._hashCode
                   && _hasNullElement == otherKey._hasNullElement
                   && _elements.Count == otherKey._elements.Count
                   && _elements.SetEquals(otherKey._elements);
        }

        public bool Contains(TElement element)
        {
            if (element == null)
                return _hasNullElement;
            return _elements.Contains(element);
        }

        public int Count => _elements.Count;

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
            var nullText = _hasNullElement ? "<null>" : string.Empty;
            return $"{{{string.Join(", ", _elements)}{nullText}}}";
        }
    }
}