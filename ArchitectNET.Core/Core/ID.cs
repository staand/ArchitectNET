using System;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core
{
    public struct ID : IEquatable<ID>
    {
        private static readonly Type _byteType;
        private static readonly Type _guidType;
        private static readonly Type _idType;
        private static readonly Type _intType;
        private static readonly Type _invariantStringType;
        private static readonly Type _longType;
        private static readonly Type _ordinalStringType;
        private readonly object _value;

        static ID()
        {
            _idType = typeof(ID);
            _byteType = typeof(byte);
            _guidType = typeof(Guid);
            _intType = typeof(int);
            _longType = typeof(long);
            _ordinalStringType = typeof(string);
            _invariantStringType = typeof(InvariantString);
        }

        public ID(byte byteValue)
        {
            _value = byteValue;
        }

        public ID(int intValue)
        {
            _value = intValue;
        }

        public ID(long longValue)
        {
            _value = longValue;
        }

        public ID(string ordinalStringValue)
        {
            Guard.ArgumentNotNull(ordinalStringValue, "ordinalStringValue");
            _value = ordinalStringValue;
        }

        public ID(InvariantString invariantStringValue)
        {
            Guard.ArgumentNotNull(invariantStringValue, "invariantStringValue");
            _value = invariantStringValue;
        }

        public ID(Guid guidValue)
        {
            _value = guidValue;
        }

        public static ID Empty
        {
            get { return new ID(); }
        }

        public bool IsAnyString
        {
            get
            {
                var value = _value;
                if (value == null)
                    return false;
                var valueType = value.GetType();
                return ReferenceEquals(valueType, _ordinalStringType)
                       || ReferenceEquals(valueType, _invariantStringType);
            }
        }

        public bool IsByte
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _byteType);
            }
        }

        public bool IsEmpty
        {
            get { return _value == null; }
        }

        public bool IsGuid
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _guidType);
            }
        }

        public bool IsInt
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _intType);
            }
        }

        public bool IsInvariantString
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _invariantStringType);
            }
        }

        public bool IsLong
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _longType);
            }
        }

        public bool IsNumeric
        {
            get
            {
                var value = _value;
                if (value == null)
                    return false;
                var valueType = value.GetType();
                return ReferenceEquals(valueType, _byteType)
                       || ReferenceEquals(valueType, _intType)
                       || ReferenceEquals(valueType, _longType);
            }
        }

        public bool IsOrdinalString
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _ordinalStringType);
            }
        }

        public object Value
        {
            get { return _value; }
        }

        public static ID ByValue(object value)
        {
            if (value == null)
                return new ID();
            ID id;
            if (TryGetByValue(value, out id))
                return id;
            throw new Exception(Resources.FormatString("FAB8606C-BB55-4B6C-83D2-8929696A4A0C", value, value.GetType()));
        }

        public static bool TryGetByValue(object value, out ID id)
        {
            id = new ID();
            if (value == null)
                return true;
            var valueType = value.GetType();
            if (ReferenceEquals(valueType, _idType))
                id = (ID)value;
            else if (ReferenceEquals(valueType, _guidType))
                id = new ID((Guid)value);
            else if (ReferenceEquals(valueType, _byteType))
                id = new ID((byte)value);
            else if (ReferenceEquals(valueType, _ordinalStringType))
                id = new ID((string)value);
            else if (ReferenceEquals(valueType, _intType))
                id = new ID((int)value);
            else if (ReferenceEquals(valueType, _longType))
                id = new ID((long)value);
            else if (ReferenceEquals(valueType, _invariantStringType))
                id = new ID((InvariantString)value);
            else
                return false;
            return true;
        }

        public bool Equals(ID otherID)
        {
            var value = _value;
            var otherValue = otherID._value;
            if (value == null && otherValue == null)
                return true;
            if (value == null || otherValue == null)
                return false;
            if (ReferenceEquals(value, otherValue))
                return true;
            var valueType = value.GetType();
            var otherValueType = value.GetType();
            return ReferenceEquals(valueType, otherValueType)
                   && value.Equals(otherValue);
        }

        public static bool operator ==(ID id1, ID id2)
        {
            return id1.Equals(id2);
        }

        public static implicit operator ID(byte byteValue)
        {
            return new ID(byteValue);
        }

        public static implicit operator ID(int intValue)
        {
            return new ID(intValue);
        }

        public static implicit operator ID(long longValue)
        {
            return new ID(longValue);
        }

        public static implicit operator ID(string ordinalStringValue)
        {
            if (ordinalStringValue == null)
                return new ID();
            return new ID(ordinalStringValue);
        }

        public static implicit operator ID(InvariantString invariantStringValue)
        {
            if (invariantStringValue == null)
                return new ID();
            return new ID(invariantStringValue);
        }

        public static implicit operator ID(Guid guidValue)
        {
            return new ID(guidValue);
        }

        public static bool operator !=(ID id1, ID id2)
        {
            return !id1.Equals(id2);
        }

        public override bool Equals(object otherObject)
        {
            var otherID = otherObject as ID?;
            return otherID.HasValue
                   && Equals(otherID.Value);
        }

        public override int GetHashCode()
        {
            var value = _value;
            if (value == null)
                return 0;
            return value.GetHashCode();
        }

        public override string ToString()
        {
            if (IsEmpty)
                return "<Empty ID>";
            if (IsGuid)
                return Value.ToString().ToUpper();
            return Value.ToString();
        }

        public string TryExtractString()
        {
            var value = _value;
            if (value == null)
                return null;
            return value as string
                   ?? value as InvariantString;
        }
    }
}