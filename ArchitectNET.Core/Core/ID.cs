using System;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core
{
    /// <summary>
    /// Represents an immutable strongly typed identifier (ID), which supports most widely used  data types for identifiers.
    /// <br />Currently supported data types are:
    /// <list type="bullet">
    ///     <item>
    ///         <description> <see cref="byte" /> - 8-bit unsigned integer </description>
    ///     </item>
    ///     <item>
    ///         <description> <see cref="int" /> - 32-bit signed integer </description>
    ///     </item>
    ///     <item>
    ///         <description> <see cref="long" /> - 64-bit signed integer </description>
    ///     </item>
    ///     <item>
    ///         <description> <see cref="string" /> - ordinal case-sensitive string </description>
    ///     </item>
    ///     <item>
    ///         <description> <see cref="InsensitiveString" /> - case-insensitive string </description>
    ///     </item>
    ///     <item>
    ///         <description> <see cref="Guid" /> - 128-bit global unique identifier (GUID) </description>
    ///     </item>
    ///     <item>
    ///         <description> <see langword="null" /> - empty identifier </description>
    ///     </item>
    /// </list>
    /// </summary>
    public struct ID : IEquatable<ID>, IFormattable
    {
        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="byte" /> data type (is equal to <code>typeof(byte)</code>)
        /// </summary>
        private static readonly Type _byteType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="Guid" /> data type (is equal to <code>typeof(Guid)</code>)
        /// </summary>
        private static readonly Type _guidType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="ID" /> data type (is equal to <code>typeof(ID)</code>)
        /// </summary>
        private static readonly Type _idType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="InsensitiveString" /> data type (is equal to
        /// <code>typeof(InsensitiveString)</code>)
        /// </summary>
        private static readonly Type _insensitiveStringType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="int" /> data type (is equal to <code>typeof(int)</code>)
        /// </summary>
        private static readonly Type _intType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="long" /> data type (is equal to <code>typeof(long)</code>)
        /// </summary>
        private static readonly Type _longType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="string" /> data type (is equal to <code>typeof(string)</code>)
        /// </summary>
        private static readonly Type _ordinalStringType;

        /// <summary>
        /// Object representing current instance value. Can be <see langword="null" /> or of one of supported types listed above
        /// </summary>
        private readonly object _value;

        /// <summary>
        /// Initializes all of static fields cashing required <see cref="Type" /> instances (e.g. <see cref="_byteType" />,
        /// <see cref="_intType" /> etc.)
        /// </summary>
        static ID()
        {
            _idType = typeof(ID);
            _byteType = typeof(byte);
            _guidType = typeof(Guid);
            _intType = typeof(int);
            _longType = typeof(long);
            _ordinalStringType = typeof(string);
            _insensitiveStringType = typeof(InsensitiveString);
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="byte" />
        /// </summary>
        /// <param name="byteValue"> Unsigned 8-bit integer which is to be treated as an identifier </param>
        public ID(byte byteValue)
        {
            _value = byteValue;
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="byte" /> or with a
        /// <see langword="null" /> value
        /// </summary>
        /// <param name="nullableByteValue">
        /// Unsigned 8-bit integer which is to be treated as an identifier or
        /// <see langword="null" /> value
        /// </param>
        /// <remarks>
        /// If <paramref name="nullableByteValue" /> is null, the initialized instance is treated empty, thus it is equal to
        /// <see cref="Empty" /> and it's <see cref="IsEmpty" /> property returns <see langword="true" />
        /// </remarks>
        public ID(byte? nullableByteValue)
        {
            _value = nullableByteValue;
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="int" /> or with a
        /// <see langword="null" /> value
        /// </summary>
        /// <param name="nullableIntValue">
        /// Signed 32-bit integer which is to be treated as an identifier or
        /// <see langword="null" /> value
        /// </param>
        /// <remarks>
        /// If <paramref name="nullableIntValue" /> is null, the initialized instance is treated empty, thus it is equal to
        /// <see cref="Empty" /> and it's <see cref="IsEmpty" /> property returns <see langword="true" />
        /// </remarks>
        public ID(int? nullableIntValue)
        {
            _value = nullableIntValue;
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="int" />
        /// </summary>
        /// <param name="intValue"> Signed 32-bit integer which is to be treated as an identifier </param>
        public ID(int intValue)
        {
            _value = intValue;
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="long" /> or with a
        /// <see langword="null" /> value
        /// </summary>
        /// <param name="nullableLongValue">
        /// Signed 64-bit integer which is to be treated as an identifier or
        /// <see langword="null" /> value
        /// </param>
        /// <remarks>
        /// If <paramref name="nullableLongValue" /> is null, the initialized instance is treated empty, thus it is equal to
        /// <see cref="Empty" /> and it's <see cref="IsEmpty" /> property returns <see langword="true" />
        /// </remarks>
        public ID(long? nullableLongValue)
        {
            _value = nullableLongValue;
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="long" />
        /// </summary>
        /// <param name="longValue"> Signed 64-bit integer which is to be treated as an identifier </param>
        public ID(long longValue)
        {
            _value = longValue;
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="string" />
        /// </summary>
        /// <param name="ordinalStringValue"> <see cref="string" /> object which is to be treated as an identifier </param>
        /// <remarks>
        /// The initialized instance will behave as ordinal case-sensitive string during equality checks.<br />The
        /// <paramref name="ordinalStringValue" /> is allowed to be <see langword="null" />, in which case the
        /// <see cref="ContentType" /> instance is treated empty, thus it is equal to <see cref="Empty" /> and it's
        /// <see cref="IsEmpty" /> property returns <see langword="true" />
        /// </remarks>
        public ID(string ordinalStringValue)
        {
            _value = ordinalStringValue;
        }

        /// <summary>
        /// Initializes new <see cref="ContentType" /> instance with a value of type <see cref="InsensitiveString" />
        /// </summary>
        /// <param name="insensitiveStringValue"> <see cref="InsensitiveString" /> object which is to be treated as an identifier </param>
        /// <remarks>
        /// The initialized instance will behave as case-insensitive string during equality checks.<br />The
        /// <paramref name="insensitiveStringValue" /> is allowed to be <see langword="null" />, in which case the
        /// <see cref="ContentType" /> instance is treated empty, thus it is equal to <see cref="Empty" /> and it's
        /// <see cref="IsEmpty" /> property returns <see langword="true" />
        /// </remarks>
        public ID(InsensitiveString insensitiveStringValue)
        {
            _value = insensitiveStringValue;
        }

        public ID(Guid? nullableGuidValue)
        {
            _value = nullableGuidValue;
        }

        public ID(Guid guidValue)
        {
            _value = guidValue;
        }

        public static ID Empty => new ID();

        public static ID ByValue(object value)
        {
            if (value == null)
                return new ID();
            ID id;
            if (TryGetByValue(value, out id))
                return id;
            throw new Exception(Resources.FormatString("FAB8606C-BB55-4B6C-83D2-8929696A4A0C", value, value.GetType()));
        }

        public static ID InsensitiveOf(string stringValue)
        {
            return new ID(new InsensitiveString(stringValue));
        }

        public static bool operator ==(ID id1, ID id2)
        {
            return id1.Equals(id2);
        }

        public static implicit operator ID(byte? nullableByteValue)
        {
            return new ID(nullableByteValue);
        }

        public static implicit operator ID(byte byteValue)
        {
            return new ID(byteValue);
        }

        public static implicit operator ID(int? nullableIntValue)
        {
            return new ID(nullableIntValue);
        }

        public static implicit operator ID(int intValue)
        {
            return new ID(intValue);
        }

        public static implicit operator ID(long? nullableLongValue)
        {
            return new ID(nullableLongValue);
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

        public static implicit operator ID(InsensitiveString insensitiveStringValue)
        {
            if (insensitiveStringValue == null)
                return new ID();
            return new ID(insensitiveStringValue);
        }

        public static implicit operator ID(Guid? nullableGuidValue)
        {
            return new ID(nullableGuidValue);
        }

        public static implicit operator ID(Guid guidValue)
        {
            return new ID(guidValue);
        }

        public static bool operator !=(ID id1, ID id2)
        {
            return !id1.Equals(id2);
        }

        public static bool TryGetByValue(object value, out ID id)
        {
            id = new ID();
            if (value == null)
                return true;
            var valueType = value.GetType();
            if (ReferenceEquals(valueType, _idType))
                id = (ID) value;
            else if (ReferenceEquals(valueType, _guidType))
                id = new ID((Guid) value);
            else if (ReferenceEquals(valueType, _byteType))
                id = new ID((byte) value);
            else if (ReferenceEquals(valueType, _ordinalStringType))
                id = new ID((string) value);
            else if (ReferenceEquals(valueType, _intType))
                id = new ID((int) value);
            else if (ReferenceEquals(valueType, _longType))
                id = new ID((long) value);
            else if (ReferenceEquals(valueType, _insensitiveStringType))
                id = new ID((InsensitiveString) value);
            else
                return false;
            return true;
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
                       || ReferenceEquals(valueType, _insensitiveStringType);
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

        public bool IsEmpty => _value == null;

        public bool IsGuid
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _guidType);
            }
        }

        public bool IsInsensitiveString
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _insensitiveStringType);
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
                return ReferenceEquals(valueType, _intType)
                       || ReferenceEquals(valueType, _byteType)
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

        public object Value => _value;

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
            var otherValueType = otherValue.GetType();
            if (ReferenceEquals(valueType, otherValueType))
                return value.Equals(otherValue);
            var isOrdinalString = ReferenceEquals(valueType, _ordinalStringType);
            var isInsensitiveString = ReferenceEquals(valueType, _insensitiveStringType);
            var isAnyString = isOrdinalString || isInsensitiveString;
            var isOtherOrdinalString = ReferenceEquals(otherValueType, _ordinalStringType);
            var isOtherInsensitiveString = ReferenceEquals(otherValueType, _insensitiveStringType);
            var isOtherAnyString = isOtherOrdinalString || isOtherInsensitiveString;
            if (isAnyString != isOtherAnyString)
                return false;
            if (isAnyString)
            {
                return isInsensitiveString
                           ? value.Equals(otherValue)
                           : otherValue.Equals(value);
            }
            var isNumeric = ReferenceEquals(valueType, _intType)
                            || ReferenceEquals(valueType, _byteType)
                            || ReferenceEquals(valueType, _longType);
            var isOtherNumeric = ReferenceEquals(otherValueType, _intType)
                                 || ReferenceEquals(otherValueType, _byteType)
                                 || ReferenceEquals(otherValueType, _longType);
            if (!isNumeric || !isOtherNumeric)
                return false;
            return Convert.ToInt64(value) == Convert.ToInt64(otherValue);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var value = _value;
            if (value == null || string.IsNullOrWhiteSpace(format))
                return ToString();
            var formattableValue = value as IFormattable;
            if (formattableValue == null)
                return ToString();
            return formattableValue.ToString(format, formatProvider);
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
                   ?? value as InsensitiveString;
        }
    }
}