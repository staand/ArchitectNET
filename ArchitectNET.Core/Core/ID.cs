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
        /// Cashed <see cref="Type" /> instance for <see cref="byte" /> data type (is equal to <code>typeof(byte)</code>).
        /// </summary>
        private static readonly Type _byteType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="char" /> data type (is equal to <code>typeof(char)</code>).
        /// </summary>
        private static readonly Type _charType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="Guid" /> data type (is equal to <code>typeof(Guid)</code>).
        /// </summary>
        private static readonly Type _guidType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="ID" /> data type (is equal to <code>typeof(ID)</code>).
        /// </summary>
        private static readonly Type _idType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="InsensitiveString" /> data type (is equal to
        /// <code>typeof(InsensitiveString)</code>).
        /// </summary>
        private static readonly Type _insensitiveStringType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="int" /> data type (is equal to <code>typeof(int)</code>).
        /// </summary>
        private static readonly Type _intType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="long" /> data type (is equal to <code>typeof(long)</code>).
        /// </summary>
        private static readonly Type _longType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="string" /> data type (is equal to <code>typeof(string)</code>).
        /// </summary>
        private static readonly Type _ordinalStringType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="sbyte" /> data type (is equal to <code>typeof(sbyte)</code>).
        /// </summary>
        private static readonly Type _sbyteType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="short" /> data type (is equal to <code>typeof(short)</code>).
        /// </summary>
        private static readonly Type _shortType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="uint" /> data type (is equal to <code>typeof(ulong)</code>).
        /// </summary>
        private static readonly Type _uintType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="ulong" /> data type (is equal to <code>typeof(uint)</code>).
        /// </summary>
        private static readonly Type _ulongType;

        /// <summary>
        /// Cashed <see cref="Type" /> instance for <see cref="ushort" /> data type (is equal to <code>typeof(ushort)</code>).
        /// </summary>
        private static readonly Type _ushortType;

        /// <summary>
        /// Object representing current instance value. Can be <see langword="null" /> or of one of supported types listed above.
        /// </summary>
        private readonly object _value;

        /// <summary>
        /// Initializes all of static fields cashing required <see cref="Type" /> instances (e.g. <see cref="_byteType" />,
        /// <see cref="_intType" /> etc.).
        /// </summary>
        static ID()
        {
            _idType = typeof(ID);
            _guidType = typeof(Guid);
            _charType = typeof(char);
            _byteType = typeof(byte);
            _sbyteType = typeof(sbyte);
            _shortType = typeof(short);
            _ushortType = typeof(ushort);
            _intType = typeof(int);
            _uintType = typeof(uint);
            _longType = typeof(long);
            _ulongType = typeof(ulong);
            _ordinalStringType = typeof(string);
            _insensitiveStringType = typeof(InsensitiveString);
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="byte" />.
        /// </summary>
        /// <param name="byteValue"> Unsigned 8-bit integer which is to be treated as an identifier. </param>
        public ID(byte byteValue)
        {
            _value = byteValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="byte" /> or with a
        /// <see langword="null" /> value.
        /// </summary>
        /// <param name="nullableByteValue">
        /// Unsigned 8-bit integer which is to be treated as an identifier or
        /// <see langword="null" /> value.
        /// </param>
        /// <remarks>
        /// If <paramref name="nullableByteValue" /> is null, the initialized instance is treated empty, thus it is equal to
        /// <see cref="Empty" /> and it's <see cref="IsEmpty" /> property returns <see langword="true" />.
        /// </remarks>
        public ID(byte? nullableByteValue)
        {
            _value = nullableByteValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="int" /> or with a
        /// <see langword="null" /> value.
        /// </summary>
        /// <param name="nullableIntValue">
        /// Signed 32-bit integer which is to be treated as an identifier or
        /// <see langword="null" /> value.
        /// </param>
        /// <remarks>
        /// If <paramref name="nullableIntValue" /> is null, the initialized instance is treated empty, thus it is equal to
        /// <see cref="Empty" /> and it's <see cref="IsEmpty" /> property returns <see langword="true" />.
        /// </remarks>
        public ID(int? nullableIntValue)
        {
            _value = nullableIntValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="int" />.
        /// </summary>
        /// <param name="intValue"> Signed 32-bit integer which is to be treated as an identifier. </param>
        public ID(int intValue)
        {
            _value = intValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="long" /> or with a
        /// <see langword="null" /> value.
        /// </summary>
        /// <param name="nullableLongValue">
        /// Signed 64-bit integer which is to be treated as an identifier or
        /// <see langword="null" /> value.
        /// </param>
        /// <remarks>
        /// If <paramref name="nullableLongValue" /> is null, the initialized instance is treated empty, thus it is equal to
        /// <see cref="Empty" /> and it's <see cref="IsEmpty" /> property returns <see langword="true" />.
        /// </remarks>
        public ID(long? nullableLongValue)
        {
            _value = nullableLongValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="long" />.
        /// </summary>
        /// <param name="longValue"> Signed 64-bit integer which is to be treated as an identifier. </param>
        public ID(long longValue)
        {
            _value = longValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="string" />.
        /// </summary>
        /// <param name="ordinalStringValue"> <see cref="string" /> object which is to be treated as an identifier. </param>
        /// <remarks>
        /// The initialized instance will behave as ordinal case-sensitive string during equality checks.<br />The
        /// <paramref name="ordinalStringValue" /> is allowed to be <see langword="null" />, in which case the
        /// <see cref="ID" /> instance is treated empty, thus it is equal to <see cref="Empty" /> and it's
        /// <see cref="IsEmpty" /> property returns <see langword="true" />.
        /// </remarks>
        public ID(string ordinalStringValue)
        {
            _value = ordinalStringValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="InsensitiveString" />.
        /// </summary>
        /// <param name="insensitiveStringValue"> <see cref="InsensitiveString" /> object which is to be treated as an identifier. </param>
        /// <remarks>
        /// The initialized instance will behave as case-insensitive string during equality checks.<br />The
        /// <paramref name="insensitiveStringValue" /> is allowed to be <see langword="null" />, in which case the
        /// <see cref="ID" /> instance is treated empty, thus it is equal to <see cref="Empty" /> and it's
        /// <see cref="IsEmpty" /> property returns <see langword="true" />.
        /// </remarks>
        public ID(InsensitiveString insensitiveStringValue)
        {
            _value = insensitiveStringValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="Guid" /> or with a
        /// <see langword="null" /> value.
        /// </summary>
        /// <param name="nullableGuidValue">
        /// Global Unique Identifier (GUID) which is to be treated as an identifier or
        /// <see langword="null" /> value.
        /// </param>
        /// <remarks>
        /// If <paramref name="nullableGuidValue" /> is null, the initialized instance is treated empty, thus it is equal to
        /// <see cref="Empty" /> and it's <see cref="IsEmpty" /> property returns <see langword="true" />.
        /// </remarks>
        public ID(Guid? nullableGuidValue)
        {
            _value = nullableGuidValue;
        }

        /// <summary>
        /// Initializes new <see cref="ID" /> instance with a value of type <see cref="Guid" />.
        /// </summary>
        /// <param name="guidValue"> Global Unique Identifier (GUID) which is to be treated as an identifier. </param>
        public ID(Guid guidValue)
        {
            _value = guidValue;
        }

        /// <summary>
        /// Returns an instance of <see cref="ID" />  which is treated empty. The "emptiness" of an identifier means that
        /// it has no actual value (value is <see langword="null" />).
        /// </summary>
        public static ID Empty => new ID();

        /// <summary>
        /// Wraps the given <paramref name="value" /> into <see cref="ID" /> which has it's <see cref="Value" /> equal to the
        /// passed argument (a conversion to one of supported data types may be automatically performed).
        /// </summary>
        /// <param name="value"> A value which is to be wrapped into a new <see cref="ID" /> instance. </param>
        /// <returns>
        /// New <see cref="ID" /> instance which has it's <see cref="Value" /> equal to the given
        /// <paramref name="value" />.
        /// </returns>
        /// <remarks>
        /// If the <paramref name="value" /> can't be wrapped into <see cref="ID" /> due to unsupported type, an
        /// exception is thrown.
        /// </remarks>
        public static ID ByValue(object value)
        {
            if (value == null)
                return new ID();
            ID id;
            if (TryGetByValue(value, out id))
                return id;
            throw new Exception(Resources.FormatString("FAB8606C-BB55-4B6C-83D2-8929696A4A0C", value, value.GetType()));
        }

        /// <summary>
        /// Wraps the given <paramref name="stringValue" /> into <see cref="ID" /> which treats the passed string as a
        /// case-insensitive one. This method is a shortcut to "
        /// <code>new <see cref="ID" />(new <see cref="InsensitiveString" />(<paramref name="stringValue" />))"</code>.
        /// </summary>
        /// <param name="stringValue">
        /// A <see cref="string" /> which is to be wrapped into <see cref="ID" /> as a case-insensitive
        /// string.
        /// </param>
        /// <returns>
        /// New <see cref="ID" /> instance having it's <see cref="Value" /> equal to
        /// <code>new <see cref="InsensitiveString" />(<paramref name="stringValue" />)</code>.
        /// </returns>
        public static ID InsensitiveOf(string stringValue)
        {
            if (stringValue == null)
                return Empty;
            return new ID(new InsensitiveString(stringValue));
        }

        /// <summary>
        /// Determines whether two specified IDs have the same value. Just calls and returns result of the
        /// <see cref="Equals(ArchitectNET.Core.ID)" /> method.
        /// </summary>
        /// <param name="id1"> The first comparing <see cref="ID" />. </param>
        /// <param name="id2"> The second comparing <see cref="ID" />. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="id1" /> is equal to <paramref name="id2" />,
        /// otherwise <see langword="false" />.
        /// </returns>
        public static bool operator ==(ID id1, ID id2)
        {
            return id1.Equals(id2);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="nullableByteValue" /> as it's argument.
        /// </summary>
        /// <param name="nullableByteValue">
        /// Unsigned 8-bit integer or <see langword="null" /> value which is to be converted to
        /// <see cref="ID" />.
        /// </param>
        public static implicit operator ID(byte? nullableByteValue)
        {
            return new ID(nullableByteValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="byteValue" /> as it's argument.
        /// </summary>
        /// <param name="byteValue"> Unsigned 8-bit integer which is to be converted to <see cref="ID" />. </param>
        public static implicit operator ID(byte byteValue)
        {
            return new ID(byteValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="nullableIntValue" /> as it's argument.
        /// </summary>
        /// <param name="nullableIntValue">
        /// Signed 32-bit integer or <see langword="null" /> value which is to be converted to
        /// <see cref="ID" />.
        /// </param>
        public static implicit operator ID(int? nullableIntValue)
        {
            return new ID(nullableIntValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="intValue" /> as it's argument.
        /// </summary>
        /// <param name="intValue"> Signed 32-bit integer which is to be converted to <see cref="ID" />. </param>
        public static implicit operator ID(int intValue)
        {
            return new ID(intValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="nullableLongValue" /> as it's argument.
        /// </summary>
        /// <param name="nullableLongValue">
        /// Signed 64-bit integer or <see langword="null" /> value which is to be converted to
        /// <see cref="ID" />.
        /// </param>
        public static implicit operator ID(long? nullableLongValue)
        {
            return new ID(nullableLongValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="longValue" /> as it's argument.
        /// </summary>
        /// <param name="longValue"> Signed 64-bit integer which is to be converted to <see cref="ID" />. </param>
        public static implicit operator ID(long longValue)
        {
            return new ID(longValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="ordinalStringValue" /> as it's argument.
        /// </summary>
        /// <param name="ordinalStringValue"> Ordinal string which is to be converted to <see cref="ID" />. </param>
        public static implicit operator ID(string ordinalStringValue)
        {
            if (ordinalStringValue == null)
                return new ID();
            return new ID(ordinalStringValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="insensitiveStringValue" /> as it's argument.
        /// </summary>
        /// <param name="insensitiveStringValue"> Case-insensitive string which is to be converted to <see cref="ID" />. </param>
        public static implicit operator ID(InsensitiveString insensitiveStringValue)
        {
            if (insensitiveStringValue == null)
                return new ID();
            return new ID(insensitiveStringValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="nullableGuidValue" /> as it's argument.
        /// </summary>
        /// <param name="nullableGuidValue">
        /// Global Unique Identifier or <see langword="null" /> value which is to be converted to
        /// <see cref="ID" />.
        /// </param>
        public static implicit operator ID(Guid? nullableGuidValue)
        {
            return new ID(nullableGuidValue);
        }

        /// <summary>
        /// Creates a new <see cref="ID" /> instance which is initialized in the same way as after the call of <see cref="ID" />
        /// constructor passing <paramref name="guidValue" /> as it's argument.
        /// </summary>
        /// <param name="guidValue"> Global Unique Identifier which is to be converted to <see cref="ID" />. </param>
        public static implicit operator ID(Guid guidValue)
        {
            return new ID(guidValue);
        }

        /// <summary>
        /// Determines whether two specified IDs have different values. Just calls and returns an inverted result of the
        /// <see cref="Equals(ArchitectNET.Core.ID)" /> method.
        /// </summary>
        /// <param name="id1"> The first comparing <see cref="ID" />. </param>
        /// <param name="id2"> The second comparing <see cref="ID" />. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="id1" /> is NOT equal to <paramref name="id2" />,
        /// <see langword="false" /> otherwise.
        /// </returns>
        public static bool operator !=(ID id1, ID id2)
        {
            return !id1.Equals(id2);
        }

        /// <summary>
        /// Tries to wrap the given <paramref name="value" /> into <see cref="ID" /> which has it's <see cref="Value" /> equal to
        /// the passed argument (a conversion to one of supported data types may be automatically performed).
        /// </summary>
        /// <param name="value"> A value which is to be wrapped into a new <see cref="ID" /> instance. </param>
        /// <param name="id">
        /// When this method returns, the <see cref="ID" /> instance having it's <see cref="Value" /> equal to
        /// the passed value, if the latter has one of supported data types, <see cref="Empty" /> otherwise.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the specified <paramref name="value" /> was successfully wrapped into
        /// <see cref="ID" />, <see langword="false" /> otherwise.
        /// </returns>
        /// <remarks>
        /// Wrapping process is performed according to the following rules:
        /// <list type="number">
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> is <see langword="null" />, method succeeds with
        ///         <paramref name="id" /> assigned <see cref="Empty" />
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> has <see cref="ID" /> type, method succeeds with <paramref name="id" /> assigned
        ///         <code>(ID)value</code>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> has <see cref="Guid" /> type, method succeeds and
        ///         <paramref name="id" /> is assigned <code>new ID((Guid)value)</code>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> has <see cref="byte" /> type, method succeeds and
        ///         <paramref name="id" /> is assigned <code>new ID((byte)value)</code>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> has <see cref="char" />, <see cref="sbyte" />, <see cref="short" />,
        ///         <see cref="ushort" /> or <see cref="int" /> type, method succeeds and <paramref name="id" /> is assigned
        ///         <code>new ID(Convert.ToInt32(value))</code>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> has <see cref="uint" />, <see cref="long" /> or <see cref="ulong" />
        ///         type, method succeeds and <paramref name="id" /> is assigned <code>new ID(Convert.ToInt64(value))</code>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> has <see cref="string" /> type, method succeeds and
        ///         <paramref name="id" /> is assigned <code>new ID((string)value)</code>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If <paramref name="value" /> has <see cref="InsensitiveString" /> type, method succeeds and
        ///         <paramref name="id" /> is assigned <code>new ID((InsensitiveString)value)</code>
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         Method fails in all other cases and <paramref name="id" /> is assigned <see cref="Empty" />
        ///         </description>
        ///     </item>
        /// </list>
        /// </remarks>
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
            else if (ReferenceEquals(valueType, _charType)
                     || ReferenceEquals(valueType, _sbyteType)
                     || ReferenceEquals(valueType, _shortType)
                     || ReferenceEquals(valueType, _ushortType))
            {
                id = new ID(Convert.ToInt32(value));
            }
            else if (ReferenceEquals(valueType, _uintType)
                     || ReferenceEquals(valueType, _ulongType))
            {
                id = new ID(Convert.ToInt64(value));
            }
            else
                return false;
            return true;
        }

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents a string (no matter, if ordinal or
        /// case-insensitive one), otherwise <see langword="false" />.
        /// </summary>
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

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents an unsigned 8-bit integer, otherwise
        /// <see langword="false" />.
        /// </summary>
        public bool IsByte
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _byteType);
            }
        }

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> has no actual value and is treated empty, otherwise
        /// <see langword="false" />.
        /// </summary>
        public bool IsEmpty => _value == null;

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents a 128-bit global unique idenitifier (ID),
        /// otherwise <see langword="false" />.
        /// </summary>
        public bool IsGuid
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _guidType);
            }
        }

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents a case-insensitive string (
        /// <see cref="InsensitiveString" /> instance), otherwise <see langword="false" />.
        /// </summary>
        public bool IsInsensitiveString
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _insensitiveStringType);
            }
        }

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents a 32-bit signed integer, otherwise
        /// <see langword="false" />.
        /// </summary>
        public bool IsInt
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _intType);
            }
        }

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents a 64-bit signed integer, otherwise
        /// <see langword="false" />.
        /// </summary>
        public bool IsLong
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _longType);
            }
        }

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents a number (no matter, if of <see cref="byte" />,
        /// <see cref="int" /> or <see cref="long" /> type), otherwise <see langword="false" />.
        /// </summary>
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

        /// <summary>
        /// Returns <see langword="true" /> if this <see cref="ID" /> represents an ordinal case-sensitive string (standard
        /// <see cref="string" /> instance), otherwise <see langword="false" />.
        /// </summary>
        public bool IsOrdinalString
        {
            get
            {
                var value = _value;
                return value != null && ReferenceEquals(value.GetType(), _ordinalStringType);
            }
        }

        /// <summary>
        /// Returns an actual value of this <see cref="ID" />(<see langword="null" /> if empty).
        /// </summary>
        public object Value => _value;

        /// <summary>
        /// Determines whether this instance and <paramref name="otherID" /> have the same value.
        /// </summary>
        /// <param name="otherID"> The <see cref="ID" /> instance to compare to this instance. </param>
        /// <returns> <see langword="true" /> if <paramref name="otherID" /> is equal to this instance. </returns>
        /// <remarks>
        /// This methods works according to the following rules:
        /// <list type="number">
        ///     <item>
        ///         <description>
        ///         If this instance and <paramref name="otherID" /> are both empty, <see langword="true" /> is
        ///         immediately returned
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If this instance or <paramref name="otherID" /> is empty (but NOT both), <see langword="false" />
        ///         is immediately returned
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If values of this instance and <paramref name="otherID" /> have exactly the same type, the result
        ///         of <see cref="object.Equals(object)" /> is returned
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If this instance and <paramref name="otherID" /> both represent a string, their values are
        ///         converted to <see cref="InsensitiveString" /> and the result of equality check is returned
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description>
        ///         If this instance and <paramref name="otherID" /> both represent a number, their values are
        ///         converted to <see cref="long" /> and the result of equality check is returned
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <description> In all other cases <see langword="false" /> is returned </description>
        ///     </item>
        /// </list>
        /// </remarks>
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

        /// <summary>
        /// Formats the value of this <see cref="ID" /> using the specified format.
        /// </summary>
        /// <param name="format"> The format to use or <see langword="null" /> to use the default format. </param>
        /// <param name="formatProvider">
        /// The provider to use to format the value or <see langword="null" /> to obtain the format
        /// information from the current locale setting of the operating system.
        /// </param>
        /// <returns> The value of this <see cref="ID" /> in the specified format. </returns>
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

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be wrappable into <see cref="ID" /> using
        /// <see cref="TryGetByValue" /> method, have the same value.
        /// </summary>
        /// <param name="otherObject"> The object to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="otherObject" /> is wrappable into <see cref="ID" /> and its value is
        /// the same as this instance, otherwise <see langword="false" />.
        /// </returns>
        public override bool Equals(object otherObject)
        {
            var otherID = otherObject as ID?;
            return otherID.HasValue
                   && Equals(otherID.Value);
        }

        /// <summary>
        /// Returns a hash code of this <see cref="ID" />.
        /// </summary>
        /// <returns> A 32-bit signed integer hash code. </returns>
        public override int GetHashCode()
        {
            var value = _value;
            if (value == null)
                return 0;
            return value.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of this <see cref="ID" />.
        /// </summary>
        /// <returns>
        /// <see cref="string" /> object representing this instance.
        /// </returns>
        public override string ToString()
        {
            if (IsEmpty)
                return "<Empty ID>";
            if (IsGuid)
                return Value.ToString().ToUpper();
            return Value.ToString();
        }

        /// <summary>
        /// Tries to extract a <see cref="string" /> from this <see cref="ID" />. This is possible if and only if
        /// <see cref="IsAnyString" /> returns <see langword="true" />.
        /// </summary>
        /// <returns>
        /// This instance's value converted to <see cref="string" /> if <see cref="IsAnyString" /> is
        /// <see langword="true" />, otherwise <see langword="false" />.
        /// </returns>
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