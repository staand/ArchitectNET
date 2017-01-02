using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core
{
    public sealed class InvariantString : IComparable, ICloneable, IConvertible, IComparable<string>, IEnumerable<char>,
        IEquatable<string>, IComparable<InvariantString>, IEquatable<InvariantString>
    {
        private readonly string _baseString;
        private int _hashCode;

        public InvariantString(string baseString)
        {
            Guard.ArgumentNotNull(baseString, "baseString");
            _baseString = baseString;
            _hashCode = -1;
        }

        public InvariantString(char[] baseStringBuffer)
            : this(new string(baseStringBuffer))
        {
        }

        public char this[int index]
        {
            get { return _baseString[index]; }
        }

        public int Length
        {
            get { return _baseString.Length; }
        }

        public object Clone()
        {
            return new InvariantString(_baseString);
        }

        public int CompareTo(InvariantString otherString)
        {
            return CompareTo(otherString, CultureInfo.InvariantCulture);
        }

        public int CompareTo(string otherString)
        {
            Guard.ArgumentNotNull(otherString, "otherString");
            return CompareTo(new InvariantString(otherString));
        }

        public bool Equals(InvariantString otherString)
        {
            if (ReferenceEquals(otherString, null))
                return false;
            if (ReferenceEquals(otherString, this))
                return true;
            if (_baseString.Length != otherString._baseString.Length)
                return false;
            return CompareTo(otherString) == 0;
        }

        public bool Equals(string otherString)
        {
            if (otherString == null)
                return false;
            return Equals(new InvariantString(otherString));
        }

        int IComparable.CompareTo(object otherObject)
        {
            Guard.ArgumentNotNull(otherObject, "otherObject");
            var otherInvariantString = otherObject as InvariantString;
            if (!ReferenceEquals(otherInvariantString, null))
                return CompareTo(otherInvariantString);
            var otherString = otherObject as string;
            if (otherString != null)
                return CompareTo(otherString);
            throw new Exception(Resources.FormatString("FBD8C857-97DE-4761-9E27-424465171F12", otherObject.GetType()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _baseString.GetEnumerator();
        }

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            return _baseString.GetEnumerator();
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToBoolean(provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToByte(provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDateTime(provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDecimal(provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDouble(provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt16(provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt32(provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt64(provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToSByte(provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToSingle(provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToString(provider);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToType(conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt16(provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt32(provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt64(provider);
        }

        public static bool operator ==(InvariantString string1, InvariantString string2)
        {
            var isString1Null = ReferenceEquals(string1, null);
            var isString2Null = ReferenceEquals(string2, null);
            if (isString1Null && isString2Null)
                return true;
            if (isString1Null || isString2Null)
                return false;
            return string1.Equals(string2);
        }

        public static implicit operator string(InvariantString invariantString)
        {
            if (ReferenceEquals(invariantString, null))
                return null;
            return invariantString._baseString;
        }

        public static implicit operator InvariantString(string baseString)
        {
            if (baseString == null)
                return null;
            return new InvariantString(baseString);
        }

        public static bool operator !=(InvariantString string1, InvariantString string2)
        {
            var isString1Null = ReferenceEquals(string1, null);
            var isString2Null = ReferenceEquals(string2, null);
            if (isString1Null && isString2Null)
                return false;
            if (isString1Null || isString2Null)
                return true;
            return !string1.Equals(string2);
        }

        public int CompareTo(InvariantString otherString, CultureInfo culture)
        {
            Guard.ArgumentNotNull(otherString, "otherString");
            var length = Length;
            var otherLength = otherString.Length;
            if (length == 0 && otherLength == 0)
                return 0;
            if (length == 0)
                return -1;
            if (otherLength == 0)
                return 1;
            var minimumLength = Math.Min(length, otherLength);
            culture = culture ?? CultureInfo.InvariantCulture;
            for (var i = 0; i < minimumLength; i++)
            {
                var character = char.ToUpper(_baseString[i], culture);
                var otherCharacter = char.ToUpper(otherString[i], culture);
                if (character != otherCharacter)
                    return character - otherCharacter;
            }
            return length - otherLength;
        }

        public override bool Equals(object otherObject)
        {
            var otherInvariantString = otherObject as InvariantString;
            if (!ReferenceEquals(otherInvariantString, null))
                return Equals(otherInvariantString);
            var otherString = otherObject as string;
            if (otherString != null)
                return Equals(otherString);
            return false;
        }

        public override int GetHashCode()
        {
            if (_hashCode != -1)
                return _hashCode;
            _hashCode = _baseString.ToUpper().GetHashCode();
            return _hashCode;
        }

        public override string ToString()
        {
            return _baseString;
        }
    }
}