using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core
{
    /// <summary>
    /// Represents a static string of characters which performs all comparison operations in the case-insensitivity context.
    /// Like standard <see cref="string" />, <see cref="InsensitiveString" /> is immutable, thus all methods performing some
    /// type of transformation return the result as new <see cref="InsensitiveString" />. As with standard strings, character
    /// positions (indices) are zero-based
    /// </summary>
    public sealed class InsensitiveString : IComparable,
                                            ICloneable,
                                            IConvertible,
                                            IComparable<string>,
                                            IEnumerable<char>,
                                            IEquatable<string>,
                                            IComparable<InsensitiveString>,
                                            IEquatable<InsensitiveString>
    {
        /// <summary>
        /// Base (underlying) string which is wrapped by this instance of <see cref="InsensitiveString" />
        /// </summary>
        private readonly string _baseString;

        /// <summary>
        /// Cashed hash code of this instance. Hash code is evaluated lazily during the first call of <see cref="GetHashCode" />
        /// method
        /// </summary>
        private int _hashCode;

        /// <summary>
        /// Initializes a new instance of <see cref="InsensitiveString" /> by wrapping the specified <paramref name="baseString" />
        /// </summary>
        /// <param name="baseString">
        /// Ordinal <see cref="string" /> which is to be wrapped into new
        /// <see cref="InsensitiveString" />
        /// </param>
        public InsensitiveString(string baseString)
        {
            Guard.ArgumentNotNull(baseString, nameof(baseString));
            _baseString = baseString;
            _hashCode = 0;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InsensitiveString" /> with characters copied from the
        /// <paramref name="baseStringBuffer" />
        /// </summary>
        /// <param name="baseStringBuffer">
        /// Array of <see cref="char" /> to copy characters from into new
        /// <see cref="InsensitiveString" />
        /// </param>
        public InsensitiveString(char[] baseStringBuffer)
            : this(new string(baseStringBuffer))
        {
        }

        /// <summary>
        /// Replaces the given <see cref="StringComparison" /> value by equivalent one which indicates the ignorance of character
        /// case during different comparison operations (e.g. <see cref="StringComparison.CurrentCulture" /> is replaced by
        /// <see cref="StringComparison.CurrentCultureIgnoreCase" />)
        /// </summary>
        /// <param name="comparison">
        /// <see cref="StringComparison" /> value which is to be replaced by a case-insensitive
        /// equivalent
        /// </param>
        /// <returns> </returns>
        public static StringComparison Insensitivify(StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparison.CurrentCultureIgnoreCase;
                case StringComparison.InvariantCulture:
                    return StringComparison.InvariantCultureIgnoreCase;
                case StringComparison.Ordinal:
                    return StringComparison.OrdinalIgnoreCase;
                default:
                    return comparison;
            }
        }

        /// <summary>
        /// Expands the given character array in such way, that for each character <code>X</code> in the initial array both upper-
        /// and lower-cased variants of <code>X</code> will be present in the result array (e.g. ['a', 'B'] will be expanded to
        /// ['a', 'A', 'b', 'B'])
        /// </summary>
        /// <param name="characters"> Array of <see cref="char" /> which is to be expanded </param>
        /// <returns>
        /// Expanded array of <see cref="char" /> which contains both upper- and lower-cased variants of each character
        /// in the initial array
        /// </returns>
        /// <remarks> This method uses <see cref="CultureInfo.CurrentCulture" /> culture to change character case </remarks>
        public static char[] Insensitivify(params char[] characters)
        {
            return Insensitivify(characters, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Expands the given character array in such way, that for each character <code>X</code> in the initial array both upper-
        /// and lower-cased variants of <code>X</code> will be present in the result array (e.g. ['a', 'B'] will be expanded to
        /// ['a', 'A', 'b', 'B'])
        /// </summary>
        /// <param name="characters"> Array of <see cref="char" /> which is to be expanded </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> which should be used to change character case. If
        /// <paramref name="culture" /> is <see langword="null" />, <see cref="CultureInfo.CurrentCulture" /> will be used
        /// </param>
        /// <returns>
        /// Expanded array of <see cref="char" /> which contains both upper- and lower-cased variants of each character
        /// in the initial array
        /// </returns>
        public static char[] Insensitivify(char[] characters, CultureInfo culture)
        {
            Guard.ArgumentNotNull(characters, nameof(characters));
            culture = culture ?? CultureInfo.CurrentCulture;
            var insensitiveCharacters = new HashSet<char>();
            foreach (var character in characters)
            {
                if (!insensitiveCharacters.Add(character))
                    continue;
                var isLowerCharacter = char.IsLower(character);
                var isUpperCharacter = !isLowerCharacter
                                       && char.IsUpper(character);
                if (!isLowerCharacter && !isUpperCharacter)
                    continue;
                var oppositeCharacter = isLowerCharacter
                                            ? char.ToUpper(character, culture)
                                            : char.ToLower(character, culture);
                insensitiveCharacters.Add(oppositeCharacter);
            }
            return insensitiveCharacters.ToArray();
        }

        /// <summary>
        /// Determines whether two specified case-insensitive strings have the same value
        /// </summary>
        /// <param name="string1"> The first case-insensitive string to compare </param>
        /// <param name="string2"> The second case-insensitive string to compare </param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="string1" /> is the same as the value of
        /// <paramref name="string2" />, otherwise <see langword="false" />
        /// </returns>
        /// <remarks> This method use <see cref="CultureInfo.CurrentCulture" /> in comparison operation </remarks>
        public static bool operator ==(InsensitiveString string1, InsensitiveString string2)
        {
            var isString1Null = ReferenceEquals(string1, null);
            var isString2Null = ReferenceEquals(string2, null);
            if (isString1Null && isString2Null)
                return true;
            if (isString1Null || isString2Null)
                return false;
            return string1.Equals(string2);
        }

        /// <summary>
        /// Converts <see cref="InsensitiveString" /> to ordinal <see cref="string" />, from which the former was created, or
        /// returns <see langword="null" /> if given <paramref name="insensitiveString" /> is <see langword="null" />
        /// </summary>
        /// <param name="insensitiveString">
        /// <see cref="InsensitiveString" /> which is to be converted to <see cref="string" />
        /// </param>
        public static implicit operator string(InsensitiveString insensitiveString)
        {
            if (ReferenceEquals(insensitiveString, null))
                return null;
            return insensitiveString._baseString;
        }

        /// <summary>
        /// Converts ordinal <see cref="string" /> to <see cref="InsensitiveString" /> by creating new instance of the latter, or
        /// returns <see langword="null" /> if given <paramref name="baseString" /> is <see langword="null" />
        /// </summary>
        /// <param name="baseString"> </param>
        public static implicit operator InsensitiveString(string baseString)
        {
            if (baseString == null)
                return null;
            return new InsensitiveString(baseString);
        }

        /// <summary>
        /// Determines whether two specified case-insensitive strings have different values
        /// </summary>
        /// <param name="string1"> The first case-insensitive string to compare </param>
        /// <param name="string2"> The second case-insensitive string to compare </param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="string1" /> is different from the value of
        /// <paramref name="string2" />, otherwise <see langword="false" />
        /// </returns>
        /// <remarks> This method use <see cref="CultureInfo.CurrentCulture" /> in comparison operation </remarks>
        public static bool operator !=(InsensitiveString string1, InsensitiveString string2)
        {
            var isString1Null = ReferenceEquals(string1, null);
            var isString2Null = ReferenceEquals(string2, null);
            if (isString1Null && isString2Null)
                return false;
            if (isString1Null || isString2Null)
                return true;
            return !string1.Equals(string2);
        }

        /// <summary>
        /// Gets the character at a specified position
        /// </summary>
        /// <param name="index"> A zero-based position in the current <see cref="InsensitiveString" /> </param>
        /// <returns> The character at position <paramref name="index" /> </returns>
        public char this[int index] => _baseString[index];

        /// <summary>
        /// Gets the number of characters in the current <see cref="InsensitiveString" />
        /// </summary>
        public int Length => _baseString.Length;

        /// <summary>
        /// Returns a referense to this <see cref="InsensitiveString" />
        /// </summary>
        /// <returns> A reference to this instance </returns>
        public object Clone()
        {
            return this;
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="object" /> and indicates whether this instance precedes, follows, or
        /// appears in the same position in the sort order as the specified <see cref="object" />
        /// </summary>
        /// <param name="otherObject">
        /// An <see cref="object" /> to compare with. If <paramref name="otherObject" /> is neither of type <see cref="string" />
        /// nor of type <see cref="InsensitiveString" />, an exception is thrown
        /// </param>
        /// <returns>
        /// A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the
        /// sort order as the <paramref name="otherObject" />:
        /// <list type="bullet">
        ///     <listheader>
        ///         <term> Value </term>
        ///         <description> Condition </description>
        ///     </listheader>
        ///     <item>
        ///         <term> Less than zero </term>
        ///         <description> This instance precedes <paramref name="otherObject" /> </description>
        ///     </item>
        ///     <item>
        ///         <term> Zero </term>
        ///         <description>
        ///         This instance has the same position in the sort order as <paramref name="otherObject" />
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term> Greater than zero </term>
        ///         <description> This instance follows  <paramref name="otherObject" /> </description>
        ///     </item>
        /// </list>
        /// </returns>
        int IComparable.CompareTo(object otherObject)
        {
            Guard.ArgumentNotNull(otherObject, nameof(otherObject));
            var otherInsensitiveString = otherObject as InsensitiveString;
            if (!ReferenceEquals(otherInsensitiveString, null))
                return CompareTo(otherInsensitiveString);
            var otherString = otherObject as string;
            if (otherString != null)
                return CompareTo(otherString);
            throw new Exception(Resources.FormatString("FBD8C857-97DE-4761-9E27-424465171F12", otherObject.GetType()));
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="InsensitiveString" /> and indicates whether this instance precedes,
        /// follows, or
        /// appears in the same position in the sort order as the specified <see cref="InsensitiveString" />
        /// </summary>
        /// <param name="otherString"> An <see cref="InsensitiveString" /> to compare with </param>
        /// <returns>
        /// A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the
        /// sort order as the <paramref name="otherString" />:
        /// <list type="bullet">
        ///     <listheader>
        ///         <term> Value </term>
        ///         <description> Condition </description>
        ///     </listheader>
        ///     <item>
        ///         <term> Less than zero </term>
        ///         <description> This instance precedes <paramref name="otherString" /> </description>
        ///     </item>
        ///     <item>
        ///         <term> Zero </term>
        ///         <description>
        ///         This instance has the same position in the sort order as <paramref name="otherString" />
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term> Greater than zero </term>
        ///         <description> This instance follows  <paramref name="otherString" /> </description>
        ///     </item>
        /// </list>
        /// </returns>
        public int CompareTo(InsensitiveString otherString)
        {
            return CompareTo(otherString, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="string" /> and indicates whether this instance precedes, follows, or
        /// appears in the same position in the sort order as the specified <see cref="string" />
        /// </summary>
        /// <param name="otherString"> An <see cref="string" /> to compare with </param>
        /// <returns>
        /// A 32-bit signed integer that indicates whether this instance precedes, follows, or appears in the same position in the
        /// sort order as the <paramref name="otherString" />:
        /// <list type="bullet">
        ///     <listheader>
        ///         <term> Value </term>
        ///         <description> Condition </description>
        ///     </listheader>
        ///     <item>
        ///         <term> Less than zero </term>
        ///         <description> This instance precedes <paramref name="otherString" /> </description>
        ///     </item>
        ///     <item>
        ///         <term> Zero </term>
        ///         <description>
        ///         This instance has the same position in the sort order as <paramref name="otherString" />
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term> Greater than zero </term>
        ///         <description> This instance follows  <paramref name="otherString" /> </description>
        ///     </item>
        /// </list>
        /// </returns>
        public int CompareTo(string otherString)
        {
            Guard.ArgumentNotNull(otherString, nameof(otherString));
            return CompareTo(new InsensitiveString(otherString));
        }

        /// <summary>
        /// Returns the <see cref="TypeCode" /> for this instance
        /// </summary>
        /// <returns> The <see cref="TypeCode.Object" /> constant </returns>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="bool" /> value using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A <see cref="bool" /> value equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToBoolean" /> implementation of <see cref="string" /> type </remarks>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToBoolean(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 8-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> An unsigned 8-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToByte" /> implementation of <see cref="string" /> type </remarks>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToByte(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="char" /> value using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A <see cref="char" /> value equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToChar" /> implementation of <see cref="string" /> type </remarks>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToChar(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="DateTime" /> value using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A <see cref="DateTime" /> value equivalent to the value of this instance </returns>
        /// <remarks>
        /// This method just calls the <see cref="IConvertible.ToDateTime" /> implementation of <see cref="string" />
        /// type
        /// </remarks>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDateTime(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="decimal" /> value using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A <see cref="decimal" /> value equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToDecimal" /> implementation of <see cref="string" /> type </remarks>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDecimal(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit IEEE-754 number using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A 64-bit IEEE-754 number equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToDouble" /> implementation of <see cref="string" /> type </remarks>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDouble(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 16-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A signed 16-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToInt16" /> implementation of <see cref="string" /> type </remarks>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt16(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 32-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A signed 32-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToInt32" /> implementation of <see cref="string" /> type </remarks>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt32(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 64-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A signed 64-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToInt64" /> implementation of <see cref="string" /> type </remarks>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt64(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 8-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A signed 8-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToSByte" /> implementation of <see cref="string" /> type </remarks>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToSByte(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit IEEE-754 number using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A 32-bit IEEE-754 number equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToSingle" /> implementation of <see cref="string" /> type </remarks>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToSingle(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="string" /> value using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> A <see cref="string" /> value equivalent to the value of this instance </returns>
        /// <remarks>
        /// This method just calls the <see cref="IConvertible.ToString(IFormatProvider)" /> implementation of
        /// <see cref="string" /> type
        /// </remarks>
        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToString(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an <see cref="object" /> of the specified <see cref="Type" /> that has an
        /// equivalent value, using the specified culture-specific formatting information
        /// </summary>
        /// <param name="conversionType"> The <see cref="Type" /> to which the value of this instance is converted </param>
        /// <param name="provider"> </param>
        /// <returns>
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific formatting
        /// information
        /// </returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToType(conversionType, provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 16-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> An unsigned 16-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToUInt16" /> implementation of <see cref="string" /> type </remarks>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt16(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 32-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> An unsigned 32-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToUInt32" /> implementation of <see cref="string" /> type </remarks>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt32(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 64-bit integer using the specified culture-specific
        /// formatting information
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information
        /// </param>
        /// <returns> An unsigned 64-bit integer equivalent to the value of this instance </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToUInt64" /> implementation of <see cref="string" /> type </remarks>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt64(provider);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _baseString.GetEnumerator();
        }

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            return _baseString.GetEnumerator();
        }

        public bool Equals(InsensitiveString otherString)
        {
            if (ReferenceEquals(otherString, null))
                return false;
            if (ReferenceEquals(otherString, this))
                return true;
            if (Length != otherString.Length)
                return false;
            return CompareTo(otherString) == 0;
        }

        public bool Equals(string otherString)
        {
            if (otherString == null)
                return false;
            return Equals(new InsensitiveString(otherString));
        }

        public override bool Equals(object otherObject)
        {
            var otherInsensitiveString = otherObject as InsensitiveString;
            if (!ReferenceEquals(otherInsensitiveString, null))
                return Equals(otherInsensitiveString);
            var otherString = otherObject as string;
            return otherString != null && Equals(otherString);
        }

        public override int GetHashCode()
        {
            var length = Length;
            if (_hashCode != 0 || length == 0)
                return _hashCode;
            var hashPart1 = 5381;
            var hashPart2 = hashPart1;
            var baseString = _baseString;
            var nextCharacter = char.ToUpperInvariant(baseString[0]);
            for (var i = 0; i < length; i++)
            {
                var character = nextCharacter;
                hashPart1 = ((hashPart1 << 5) + hashPart1) ^ character;
                if (i >= length - 1)
                    continue;
                nextCharacter = char.ToUpperInvariant(baseString[i + 1]);
                hashPart2 = ((hashPart2 << 5) + hashPart2)
                            ^ ((byte) (character >> 8) + ((byte) nextCharacter << 8));
            }
            _hashCode = hashPart1 + hashPart2 * 1566083941;
            return _hashCode;
        }

        public override string ToString()
        {
            return _baseString;
        }

        public int CompareTo(InsensitiveString otherString, CultureInfo culture)
        {
            Guard.ArgumentNotNull(otherString, nameof(otherString));
            var length = Length;
            var otherLength = otherString.Length;
            if (length == 0 && otherLength == 0)
                return 0;
            if (length == 0)
                return -1;
            if (otherLength == 0)
                return 1;
            var minimumLength = Math.Min(length, otherLength);
            culture = culture ?? CultureInfo.CurrentCulture;
            var textInfo = culture.TextInfo;
            for (var i = 0; i < minimumLength; i++)
            {
                var character = char.ToUpper(_baseString[i], culture);
                var otherCharacter = char.ToUpper(otherString[i], culture);
                if (character != otherCharacter)
                    return textInfo.ToUpper(character) - textInfo.ToUpper(otherCharacter);
            }
            return length - otherLength;
        }

        public bool Contains(string substring)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            return _baseString.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public bool EndsWith(string suffix)
        {
            return EndsWith(suffix, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool EndsWith(string suffix, StringComparison comparison)
        {
            Guard.ArgumentNotNull(suffix, nameof(suffix));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.EndsWith(suffix, insensitiveComparison);
        }

        public bool EndsWith(string suffix, CultureInfo culture)
        {
            Guard.ArgumentNotNull(suffix, nameof(suffix));
            return _baseString.EndsWith(suffix, true, culture);
        }

        public int IndexOf(char character)
        {
            return IndexOf(character, 0, Length);
        }

        public int IndexOf(char character, int startIndex)
        {
            return IndexOf(character, startIndex, Length - startIndex);
        }

        public int IndexOf(char character, int startIndex, int maximumCount)
        {
            var insensitiveCharacters = Insensitivify(character);
            if (insensitiveCharacters.Length == 1)
                return _baseString.IndexOf(character, startIndex, maximumCount);
            return _baseString.IndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        public int IndexOf(string substring)
        {
            return IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);
        }

        public int IndexOf(string substring, StringComparison comparison)
        {
            return IndexOf(substring, 0, Length, comparison);
        }

        public int IndexOf(string substring, int startIndex, StringComparison comparison)
        {
            return IndexOf(substring, startIndex, Length - startIndex, comparison);
        }

        public int IndexOf(string substring, int startIndex, int maximumCount)
        {
            return IndexOf(substring, 0, Length, StringComparison.CurrentCultureIgnoreCase);
        }

        public int IndexOf(string substring, int startIndex, int maximumCount, StringComparison comparison)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.IndexOf(substring, startIndex, maximumCount, insensitiveComparison);
        }

        public int IndexOfAny(params char[] characters)
        {
            return IndexOfAny(characters, 0, Length);
        }

        public int IndexOfAny(char[] characters, int startIndex)
        {
            return _baseString.IndexOfAny(characters, startIndex, Length - startIndex);
        }

        public int IndexOfAny(char[] characters, int startIndex, int maximumCount)
        {
            Guard.ArgumentNotNull(characters, nameof(characters));
            var insensitiveCharacters = Insensitivify(characters);
            return _baseString.IndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        public InsensitiveString Insert(int startIndex, string substring)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            var newBaseString = _baseString.Insert(startIndex, substring);
            return new InsensitiveString(newBaseString);
        }

        public bool IsNormalized(NormalizationForm normalizationForm)
        {
            return _baseString.IsNormalized(normalizationForm);
        }

        public bool IsNormalized()
        {
            return IsNormalized(NormalizationForm.FormC);
        }

        public int LastIndexOf(char character)
        {
            var length = Length;
            return LastIndexOf(character, length - 1, length);
        }

        public int LastIndexOf(char character, int startIndex)
        {
            return LastIndexOf(character, startIndex, startIndex + 1);
        }

        public int LastIndexOf(char character, int startIndex, int maximumCount)
        {
            var insensitiveCharacters = Insensitivify(character);
            if (insensitiveCharacters.Length == 1)
                return _baseString.LastIndexOf(character, startIndex, maximumCount);
            return _baseString.LastIndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        public int LastIndexOf(string substring)
        {
            var length = Length;
            return LastIndexOf(substring, length - 1, length, StringComparison.CurrentCultureIgnoreCase);
        }

        public int LastIndexOf(string substring, StringComparison comparison)
        {
            var length = Length;
            return LastIndexOf(substring, length - 1, length, comparison);
        }

        public int LastIndexOf(string substring, int startIndex, StringComparison comparison)
        {
            return LastIndexOf(substring, startIndex, startIndex + 1, comparison);
        }

        public int LastIndexOf(string substring, int startIndex, int maximumCount)
        {
            return LastIndexOf(substring, startIndex, maximumCount, StringComparison.CurrentCultureIgnoreCase);
        }

        public int LastIndexOf(string substring, int startIndex, int maximumCount, StringComparison comparison)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.LastIndexOf(substring, startIndex, maximumCount, insensitiveComparison);
        }

        public int LastIndexOfAny(params char[] characters)
        {
            var length = Length;
            return LastIndexOfAny(characters, length - 1, length);
        }

        public int LastIndexOfAny(char[] characters, int startIndex)
        {
            return LastIndexOfAny(characters, startIndex, startIndex + 1);
        }

        public int LastIndexOfAny(char[] characters, int startIndex, int maximumCount)
        {
            Guard.ArgumentNotNull(characters, nameof(characters));
            var insensitiveCharacters = Insensitivify(characters);
            return _baseString.LastIndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        public InsensitiveString Normalize(NormalizationForm normalizationForm)
        {
            var normalizedBaseString = _baseString.Normalize(normalizationForm);
            return new InsensitiveString(normalizedBaseString);
        }

        public InsensitiveString Normalize()
        {
            return Normalize(NormalizationForm.FormC);
        }

        public InsensitiveString PadLeft(int totalLength)
        {
            return PadLeft(totalLength, ' ');
        }

        public InsensitiveString PadLeft(int totalLength, char padder)
        {
            var paddedBaseString = _baseString.PadLeft(totalLength, padder);
            return new InsensitiveString(paddedBaseString);
        }

        public InsensitiveString PadRight(int totalLength)
        {
            return PadRight(totalLength, ' ');
        }

        public InsensitiveString PadRight(int totalLength, char padder)
        {
            var paddedBaseString = _baseString.PadRight(totalLength, padder);
            return new InsensitiveString(paddedBaseString);
        }

        public InsensitiveString Remove(int startIndex)
        {
            return Substring(0, startIndex);
        }

        public InsensitiveString Remove(int startIndex, int count)
        {
            var newBaseString = _baseString.Remove(startIndex, count);
            return new InsensitiveString(newBaseString);
        }

        public InsensitiveString Replace(char oldCharacter, char newCharacter)
        {
            var insensitiveOldCharacters = Insensitivify(oldCharacter);
            var newBaseString = _baseString;
            foreach (var character in insensitiveOldCharacters)
                newBaseString = newBaseString.Replace(character, newCharacter);
            return new InsensitiveString(newBaseString);
        }

        public InsensitiveString Replace(string oldSubstring, string newSubstring)
        {
            Guard.ArgumentNotNull(oldSubstring, nameof(oldSubstring));
            Guard.ArgumentNotNull(newSubstring, nameof(newSubstring));
            var baseString = _baseString;
            var lengthIncrement = Length / oldSubstring.Length * (newSubstring.Length - oldSubstring.Length);
            var buffer = new char[Length + Math.Max(0, lengthIncrement)];
            var bufferCount = 0;
            var i = 0;
            var j = 0;
            while (i >= 0)
            {
                i = baseString.IndexOf(oldSubstring,
                    j,
                    StringComparison.CurrentCultureIgnoreCase);
                var isFound = i >= 0;
                if (!isFound && j == 0)
                    return this;
                var charactersToCopyCount = (isFound ? i : Length) - j;
                baseString.CopyTo(j,
                    buffer,
                    bufferCount,
                    charactersToCopyCount);
                bufferCount += charactersToCopyCount;
                if (!isFound)
                    break;
                newSubstring.CopyTo(0,
                    buffer,
                    bufferCount,
                    newSubstring.Length);
                bufferCount += newSubstring.Length;
                j = i + newSubstring.Length;
            }
            var newBaseString = new string(buffer, 0, bufferCount);
            return new InsensitiveString(newBaseString);
        }

        public InsensitiveString[] Split(params char[] separators)
        {
            return Split(separators, int.MaxValue);
        }

        public InsensitiveString[] Split(char[] separators, StringSplitOptions options)
        {
            return Split(separators, int.MaxValue, options);
        }

        public InsensitiveString[] Split(char[] separators, int maximumCount)
        {
            return Split(separators, maximumCount, StringSplitOptions.None);
        }

        public InsensitiveString[] Split(char[] separators, int maximumCount, StringSplitOptions options)
        {
            var insensitiveSeparators = Insensitivify(separators);
            var baseStringParts = _baseString.Split(insensitiveSeparators, maximumCount, options);
            var partCount = baseStringParts.Length;
            if (partCount == 0)
                return new InsensitiveString[0];
            var insensitiveStringParts = new InsensitiveString[partCount];
            for (var i = 0; i < partCount; i++)
                insensitiveStringParts[i] = new InsensitiveString(baseStringParts[i]);
            return insensitiveStringParts;
        }

        public bool StartsWith(string prefix)
        {
            return StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool StartsWith(string prefix, StringComparison comparison)
        {
            Guard.ArgumentNotNull(prefix, nameof(prefix));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.StartsWith(prefix, insensitiveComparison);
        }

        public bool StartsWith(string prefix, CultureInfo culture)
        {
            Guard.ArgumentNotNull(prefix, nameof(prefix));
            culture = culture ?? CultureInfo.CurrentCulture;
            return _baseString.StartsWith(prefix, true, culture);
        }

        public InsensitiveString Substring(int startIndex)
        {
            return new InsensitiveString(_baseString.Substring(startIndex));
        }

        public InsensitiveString Substring(int startIndex, int length)
        {
            return new InsensitiveString(_baseString.Substring(startIndex, length));
        }

        public char[] ToCharArray()
        {
            return _baseString.ToCharArray();
        }

        public char[] ToCharArray(int startIndex, int length)
        {
            return _baseString.ToCharArray(startIndex, length);
        }

        public InsensitiveString ToLower(CultureInfo culture)
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            var lowerCasedBaseString = _baseString.ToLower(culture);
            return new InsensitiveString(lowerCasedBaseString);
        }

        public InsensitiveString ToLower()
        {
            return ToLower(CultureInfo.CurrentCulture);
        }

        public InsensitiveString ToLowerInvariant()
        {
            return ToLower(CultureInfo.InvariantCulture);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return _baseString.ToString(formatProvider);
        }

        public InsensitiveString ToUpper(CultureInfo culture)
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            var upperCasedBaseString = _baseString.ToUpper(culture);
            return new InsensitiveString(upperCasedBaseString);
        }

        public InsensitiveString ToUpper()
        {
            return ToUpper(CultureInfo.CurrentCulture);
        }

        public InsensitiveString ToUpperInvariant()
        {
            return ToUpper(CultureInfo.InvariantCulture);
        }

        public InsensitiveString Trim(params char[] trimmers)
        {
            var insensitiveTrimmers = Insensitivify(trimmers);
            var trimmedBaseString = _baseString.Trim(insensitiveTrimmers);
            return new InsensitiveString(trimmedBaseString);
        }

        public InsensitiveString TrimEnd(params char[] trimmers)
        {
            var insensitiveTrimmers = Insensitivify(trimmers);
            var trimmedBaseString = _baseString.TrimEnd(insensitiveTrimmers);
            return new InsensitiveString(trimmedBaseString);
        }

        public InsensitiveString TrimStart(params char[] trimmers)
        {
            var insensitiveTrimmers = Insensitivify(trimmers);
            var trimmedBaseString = _baseString.TrimStart(insensitiveTrimmers);
            return new InsensitiveString(trimmedBaseString);
        }
    }
}