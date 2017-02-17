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
    /// positions (indices) are zero-based.
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
        /// Base (underlying) string which is wrapped by this instance of <see cref="InsensitiveString" />.
        /// </summary>
        private readonly string _baseString;

        /// <summary>
        /// Cashed hash code of this instance. Hash code is evaluated lazily during the first call of <see cref="GetHashCode" />
        /// method.
        /// </summary>
        private int _hashCode;

        /// <summary>
        /// Initializes a new instance of <see cref="InsensitiveString" /> by wrapping the specified <paramref name="baseString" />
        /// .
        /// </summary>
        /// <param name="baseString">
        /// Ordinal <see cref="string" /> which is to be wrapped into new <see cref="InsensitiveString" />.
        /// </param>
        public InsensitiveString(string baseString)
        {
            Guard.ArgumentNotNull(baseString, nameof(baseString));
            _baseString = baseString;
            _hashCode = 0;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InsensitiveString" /> with characters copied from the
        /// <paramref name="baseStringBuffer" />.
        /// </summary>
        /// <param name="baseStringBuffer">
        /// Array of <see cref="char" /> to copy characters from into new <see cref="InsensitiveString" />.
        /// </param>
        public InsensitiveString(char[] baseStringBuffer)
            : this(new string(baseStringBuffer))
        {
        }

        /// <summary>
        /// Replaces the given <see cref="StringComparison" /> value by equivalent one which indicates the ignorance of character
        /// case during different comparison operations (e.g. <see cref="StringComparison.CurrentCulture" /> is replaced by
        /// <see cref="StringComparison.CurrentCultureIgnoreCase" />).
        /// </summary>
        /// <param name="comparison">
        /// <see cref="StringComparison" /> value which is to be replaced by a case-insensitive
        /// equivalent.
        /// </param>
        /// <returns> <see cref="StringComparison" /> value indifferent to character case. </returns>
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
        /// ['a', 'A', 'b', 'B']).
        /// </summary>
        /// <param name="characters"> Array of <see cref="char" /> which is to be expanded. </param>
        /// <returns>
        /// Expanded array of <see cref="char" /> which contains both upper- and lower-cased variants of each character
        /// in the initial array.
        /// </returns>
        /// <remarks> This method uses <see cref="CultureInfo.CurrentCulture" /> culture to change character case. </remarks>
        public static char[] Insensitivify(params char[] characters)
        {
            return Insensitivify(characters, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Expands the given character array in such way, that for each character <code>X</code> in the initial array both upper-
        /// and lower-cased variants of <code>X</code> will be present in the result array (e.g. ['a', 'B'] will be expanded to
        /// ['a', 'A', 'b', 'B']).
        /// </summary>
        /// <param name="characters"> Array of <see cref="char" /> which is to be expanded. </param>
        /// <param name="culture">
        /// <see cref="CultureInfo" /> which should be used to change character case. If
        /// <paramref name="culture" /> is <see langword="null" />, <see cref="CultureInfo.CurrentCulture" /> will be used.
        /// </param>
        /// <returns>
        /// Expanded array of <see cref="char" /> which contains both upper- and lower-cased variants of each character
        /// in the initial array.
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
        /// Determines whether two specified case-insensitive strings have the same value.
        /// </summary>
        /// <param name="string1"> The first case-insensitive string to compare. </param>
        /// <param name="string2"> The second case-insensitive string to compare. </param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="string1" /> is the same as the value of
        /// <paramref name="string2" />, otherwise <see langword="false" />.
        /// </returns>
        /// <remarks> This method use <see cref="CultureInfo.CurrentCulture" /> in comparison operation. </remarks>
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
        /// returns <see langword="null" /> if given <paramref name="insensitiveString" /> is <see langword="null" />.
        /// </summary>
        /// <param name="insensitiveString">
        /// <see cref="InsensitiveString" /> which is to be converted to <see cref="string" />.
        /// </param>
        public static implicit operator string(InsensitiveString insensitiveString)
        {
            if (ReferenceEquals(insensitiveString, null))
                return null;
            return insensitiveString._baseString;
        }

        /// <summary>
        /// Converts ordinal <see cref="string" /> to <see cref="InsensitiveString" /> by creating new instance of the latter, or
        /// returns <see langword="null" /> if given <paramref name="baseString" /> is <see langword="null" />.
        /// </summary>
        /// <param name="baseString"> <see cref="string" /> which is to be converted to <see cref="InsensitiveString" />. </param>
        public static implicit operator InsensitiveString(string baseString)
        {
            if (baseString == null)
                return null;
            return new InsensitiveString(baseString);
        }

        /// <summary>
        /// Determines whether two specified case-insensitive strings have different values.
        /// </summary>
        /// <param name="string1"> The first case-insensitive string to compare. </param>
        /// <param name="string2"> The second case-insensitive string to compare. </param>
        /// <returns>
        /// <see langword="true" /> if the value of <paramref name="string1" /> is different from the value of
        /// <paramref name="string2" />, otherwise <see langword="false" />.
        /// </returns>
        /// <remarks> This method use <see cref="CultureInfo.CurrentCulture" /> in comparison operation. </remarks>
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
        /// Gets the character at a specified position.
        /// </summary>
        /// <param name="index"> A zero-based position in the current <see cref="InsensitiveString" />. </param>
        /// <returns> The character at position <paramref name="index" />. </returns>
        public char this[int index] => _baseString[index];

        /// <summary>
        /// Gets the number of characters in the current <see cref="InsensitiveString" />.
        /// </summary>
        public int Length => _baseString.Length;

        /// <summary>
        /// Returns a referense to this <see cref="InsensitiveString" />.
        /// </summary>
        /// <returns> A reference to this instance. </returns>
        public object Clone()
        {
            return this;
        }

        /// <summary>
        /// Compares this instance with a specified <see cref="object" /> and indicates whether this instance precedes, follows, or
        /// appears in the same position in the sort order as the specified <see cref="object" />.
        /// </summary>
        /// <param name="otherObject">
        /// An <see cref="object" /> to compare with. If <paramref name="otherObject" /> is neither of type <see cref="string" />
        /// nor of type <see cref="InsensitiveString" />, an exception is thrown.
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
        /// appears in the same position in the sort order as the specified <see cref="InsensitiveString" />.
        /// </summary>
        /// <param name="otherString"> An <see cref="InsensitiveString" /> to compare with. </param>
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
        /// appears in the same position in the sort order as the specified <see cref="string" />.
        /// </summary>
        /// <param name="otherString"> An <see cref="string" /> to compare with. </param>
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
        /// Returns the <see cref="TypeCode" /> for this instance.
        /// </summary>
        /// <returns> The <see cref="TypeCode.Object" /> constant. </returns>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="bool" /> value using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A <see cref="bool" /> value equivalent to the value of this instance. </returns>
        /// <remarks>
        /// This method just calls the <see cref="IConvertible.ToBoolean" /> implementation of <see cref="string" />
        /// type.
        /// </remarks>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToBoolean(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 8-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> An unsigned 8-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToByte" /> implementation of <see cref="string" /> type. </remarks>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToByte(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="char" /> value using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A <see cref="char" /> value equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToChar" /> implementation of <see cref="string" /> type. </remarks>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToChar(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="DateTime" /> value using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A <see cref="DateTime" /> value equivalent to the value of this instance. </returns>
        /// <remarks>
        /// This method just calls the <see cref="IConvertible.ToDateTime" /> implementation of <see cref="string" />
        /// type.
        /// </remarks>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDateTime(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="decimal" /> value using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A <see cref="decimal" /> value equivalent to the value of this instance. </returns>
        /// <remarks>
        /// This method just calls the <see cref="IConvertible.ToDecimal" /> implementation of <see cref="string" />
        /// type.
        /// </remarks>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDecimal(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 64-bit IEEE-754 number using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A 64-bit IEEE-754 number equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToDouble" /> implementation of <see cref="string" /> type. </remarks>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToDouble(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 16-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A signed 16-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToInt16" /> implementation of <see cref="string" /> type. </remarks>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt16(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 32-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A signed 32-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToInt32" /> implementation of <see cref="string" /> type. </remarks>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt32(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 64-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A signed 64-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToInt64" /> implementation of <see cref="string" /> type. </remarks>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToInt64(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent signed 8-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A signed 8-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToSByte" /> implementation of <see cref="string" /> type. </remarks>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToSByte(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent 32-bit IEEE-754 number using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A 32-bit IEEE-754 number equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToSingle" /> implementation of <see cref="string" /> type. </remarks>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToSingle(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="string" /> value using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> A <see cref="string" /> value equivalent to the value of this instance. </returns>
        /// <remarks>
        /// This method just calls the <see cref="IConvertible.ToString(IFormatProvider)" /> implementation of
        /// <see cref="string" /> type.
        /// </remarks>
        string IConvertible.ToString(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToString(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an <see cref="object" /> of the specified <see cref="Type" /> that has an
        /// equivalent value, using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="conversionType"> The <see cref="Type" /> to which the value of this instance is converted. </param>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns>
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific formatting
        /// information.
        /// </returns>
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToType(conversionType, provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 16-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> An unsigned 16-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToUInt16" /> implementation of <see cref="string" /> type. </remarks>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt16(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 32-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> An unsigned 32-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToUInt32" /> implementation of <see cref="string" /> type. </remarks>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt32(provider);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent unsigned 64-bit integer using the specified culture-specific
        /// formatting information.
        /// </summary>
        /// <param name="provider">
        /// An <see cref="IFormatProvider" /> interface implementation that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns> An unsigned 64-bit integer equivalent to the value of this instance. </returns>
        /// <remarks> This method just calls the <see cref="IConvertible.ToUInt64" /> implementation of <see cref="string" /> type. </remarks>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            IConvertible baseConvertible = _baseString;
            return baseConvertible.ToUInt64(provider);
        }

        /// <summary>
        /// Returns an enumerator that iterates through characters of this <see cref="InsensitiveString" />.
        /// </summary>
        /// <returns> An <see cref="IEnumerator" /> object that can be used to iterate through characters of the current instance. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _baseString.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through characters of this <see cref="InsensitiveString" />.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}" /> object that can be used to iterate through characters of the current
        /// instance.
        /// </returns>
        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            return _baseString.GetEnumerator();
        }

        /// <summary>
        /// Determines whether this instance and another <see cref="InsensitiveString" /> have the same value.
        /// </summary>
        /// <param name="otherString"> The <see cref="InsensitiveString" /> to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if the value of the <paramref name="otherString" /> is the same as the value of this
        /// instance, otherwise <see langword="false" />.
        /// </returns>
        /// <remarks> This method performs case insensitive comparison using <see cref="CultureInfo.CurrentCulture" /> culture. </remarks>
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

        /// <summary>
        /// Determines whether this instance and another <see cref="string" /> have the same value.
        /// </summary>
        /// <param name="otherString"> The <see cref="string" /> to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if the value of the <paramref name="otherString" /> is the same as the value of this
        /// instance, otherwise <see langword="false" />.
        /// </returns>
        /// <remarks>
        /// This method first wraps <paramref name="otherString" /> into <see cref="InsensitiveString" /> and then
        /// performs case insensitive comparison using <see cref="CultureInfo.CurrentCulture" /> culture.
        /// </remarks>
        public bool Equals(string otherString)
        {
            if (otherString == null)
                return false;
            return Equals(new InsensitiveString(otherString));
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which be of <see cref="string" /> or
        /// <see cref="InsensitiveString" /> type, have the same value.
        /// </summary>
        /// <param name="otherObject"> The object to compare to this instance. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="otherObject" /> is a <see cref="string" /> or
        /// <see cref="InsensitiveString" /> and it's value is the same as this instance, otherwise <see langword="false" />.
        /// </returns>
        /// <remarks>
        /// This methods first tries to convert <paramref name="otherObject" /> to <see cref="InsensitiveString" /> (maybe with
        /// additional cast to <see cref="string" />) and then performs case insensitive comparison using
        /// <see cref="CultureInfo.CurrentCulture" /> culture.
        /// </remarks>
        public override bool Equals(object otherObject)
        {
            var otherInsensitiveString = otherObject as InsensitiveString;
            if (!ReferenceEquals(otherInsensitiveString, null))
                return Equals(otherInsensitiveString);
            var otherString = otherObject as string;
            return otherString != null && Equals(otherString);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="InsensitiveString" />.
        /// </summary>
        /// <returns> A 32-bit signed integer hash code. </returns>
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

        /// <summary>
        /// Returns the string from which this instance was created. No actual conversion is performed.
        /// </summary>
        /// <returns> The string from which this instance was created. </returns>
        public override string ToString()
        {
            return _baseString;
        }

        /// <summary>
        /// Compares this instance with the specified <see cref="InsensitiveString" /> in case-insensitive fasion using the given
        /// <see cref="CultureInfo" />.
        /// </summary>
        /// <param name="otherString"> An <see cref="InsensitiveString" /> to compare with. </param>
        /// <param name="culture">
        /// A <see cref="CultureInfo" /> to be used during comparison. If <paramref name="culture" /> is
        /// <see langword="null" />, <see cref="CultureInfo.CurrentCulture" /> is used.
        /// </param>
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

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this <see cref="InsensitiveString" />.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <returns>
        /// <see langword="true" /> if the <paramref name="substring" /> parameter occurs within this
        /// <see cref="InsensitiveString" />, or if value is the empty string (""); otherwise, <see langword="false" />.
        /// </returns>
        public bool Contains(string substring)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            return _baseString.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Determines whether the end of this <see cref="InsensitiveString" /> matches the specified <see cref="string" />.
        /// </summary>
        /// <param name="suffix"> The <see cref="string" /> to compare to the substring at the end of this instance. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="suffix" /> the end of this instance; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool EndsWith(string suffix)
        {
            return EndsWith(suffix, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Determines whether the end of this <see cref="InsensitiveString" /> matches the specified <see cref="string" /> when
        /// compared using the specified comparison option.
        /// </summary>
        /// <param name="suffix"> The <see cref="string" /> to compare to the substring at the end of this instance. </param>
        /// <param name="comparison">
        /// One of the enumeration values that determines how this string and
        /// <paramref name="comparison" /> are compared. This parameter is always "insensitivified" using
        /// <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="suffix" /> the end of this instance; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool EndsWith(string suffix, StringComparison comparison)
        {
            Guard.ArgumentNotNull(suffix, nameof(suffix));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.EndsWith(suffix, insensitiveComparison);
        }

        /// <summary>
        /// Determines whether the end of this <see cref="InsensitiveString" /> matches the specified <see cref="string" /> when
        /// compared using the specified culture.
        /// </summary>
        /// <param name="suffix"> The <see cref="string" /> to compare to the substring at the end of this instance. </param>
        /// <param name="culture">
        /// Cultural information that determines how this instance and <paramref name="suffix" /> are
        /// compared. If <paramref name="culture" /> is <see langword="null" />, <see cref="CultureInfo.CurrentCulture" /> is used.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="suffix" /> the end of this instance; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool EndsWith(string suffix, CultureInfo culture)
        {
            Guard.ArgumentNotNull(suffix, nameof(suffix));
            return _baseString.EndsWith(suffix, true, culture);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified Unicode character in this
        /// <see cref="InsensitiveString" />.
        /// </summary>
        /// <param name="character"> A Unicode character to seek. </param>
        /// <returns> The zero-based index position of <paramref name="character" /> if it is found, or -1 if it is not. </returns>
        public int IndexOf(char character)
        {
            return IndexOf(character, 0, Length);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified Unicode character in this
        /// <see cref="InsensitiveString" />. The search starts at a specified character position.
        /// </summary>
        /// <param name="character"> A Unicode character to seek. </param>
        /// <param name="startIndex"> The search starting position. </param>
        /// <returns> The zero-based index position of <paramref name="character" /> if it is found, or -1 if it is not. </returns>
        public int IndexOf(char character, int startIndex)
        {
            return IndexOf(character, startIndex, Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified Unicode character in this
        /// <see cref="InsensitiveString" />. The search starts at a specified character position and examines a specified number
        /// of character positions.
        /// </summary>
        /// <param name="character"> A Unicode character to seek. </param>
        /// <param name="startIndex"> The search starting position. </param>
        /// <param name="maximumCount"> The number of character positions to examine. </param>
        /// <returns> The zero-based index position of <paramref name="character" /> if it is found, or -1 if it is not. </returns>
        public int IndexOf(char character, int startIndex, int maximumCount)
        {
            var insensitiveCharacters = Insensitivify(character);
            if (insensitiveCharacters.Length == 1)
                return _baseString.IndexOf(character, startIndex, maximumCount);
            return _baseString.IndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified <see cref="string" /> in this instance.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is 0.
        /// </returns>
        public int IndexOf(string substring)
        {
            return IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified <see cref="string" /> in this instance. A
        /// parameter specifies the type of search to use for the specified <see cref="string" />.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="comparison">
        /// One of the enumeration values that specifies the rules for the search. This parameter is
        /// always "insensitivified" using <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is 0.
        /// </returns>
        public int IndexOf(string substring, StringComparison comparison)
        {
            return IndexOf(substring, 0, Length, comparison);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified <see cref="string" /> in this instance. The
        /// search starts at a specified character position and examines a specified number of character positions.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex"> The search starting position. </param>
        /// <param name="comparison">
        /// One of the enumeration values that specifies the rules for the search. This parameter is
        /// always "insensitivified" using <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is 0.
        /// </returns>
        public int IndexOf(string substring, int startIndex, StringComparison comparison)
        {
            return IndexOf(substring, startIndex, Length - startIndex, comparison);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified <see cref="string" /> in this instance. The
        /// search starts at a specified character position.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex"> The search starting position.. </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is 0.
        /// </returns>
        public int IndexOf(string substring, int startIndex)
        {
            return IndexOf(substring, 0, Length, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified <see cref="string" /> in this instance. The
        /// search starts at a specified character position and examines a specified number of character positions.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex"> The search starting position. </param>
        /// <param name="maximumCount"> The number of character positions to examine. </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is 0.
        /// </returns>
        public int IndexOf(string substring, int startIndex, int maximumCount)
        {
            return IndexOf(substring, 0, maximumCount, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified <see cref="string" /> in this instance.
        /// Parameters specify the starting search position in the current string, the number of characters in the current string
        /// to search, and the type of search to use for the specified string.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex"> The search starting position. </param>
        /// <param name="maximumCount"> The number of character positions to examine. </param>
        /// <param name="comparison">
        /// One of the enumeration values that specifies the rules for the search. This parameter is
        /// always "insensitivified" using <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is 0.
        /// </returns>
        public int IndexOf(string substring, int startIndex, int maximumCount, StringComparison comparison)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.IndexOf(substring, startIndex, maximumCount, insensitiveComparison);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode
        /// characters.
        /// </summary>
        /// <param name="characters"> A Unicode character array containing one or more characters to seek. </param>
        /// <returns>
        /// The zero-based index position of the first occurrence in this instance where any character in
        /// <paramref name="characters" /> was found; -1 if no character in <paramref name="characters" /> was found.
        /// </returns>
        public int IndexOfAny(params char[] characters)
        {
            return IndexOfAny(characters, 0, Length);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode
        /// characters. The search starts at a specified character position.
        /// </summary>
        /// <param name="characters"> A Unicode character array containing one or more characters to seek. </param>
        /// <param name="startIndex"> The search starting position. </param>
        /// <returns>
        /// The zero-based index position of the first occurrence in this instance where any character in
        /// <paramref name="characters" /> was found; -1 if no character in <paramref name="characters" /> was found.
        /// </returns>
        public int IndexOfAny(char[] characters, int startIndex)
        {
            return _baseString.IndexOfAny(characters, startIndex, Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence in this instance of any character in a specified array of Unicode
        /// characters. The search starts at a specified character position and examines a specified number of character positions.
        /// </summary>
        /// <param name="characters"> A Unicode character array containing one or more characters to seek. </param>
        /// <param name="startIndex"> The search starting position. </param>
        /// <param name="maximumCount"> The number of character positions to examine. </param>
        /// <returns>
        /// The zero-based index position of the first occurrence in this instance where any character in
        /// <paramref name="characters" /> was found; -1 if no character in <paramref name="characters" /> was found.
        /// </returns>
        public int IndexOfAny(char[] characters, int startIndex, int maximumCount)
        {
            Guard.ArgumentNotNull(characters, nameof(characters));
            var insensitiveCharacters = Insensitivify(characters);
            return _baseString.IndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> in which a specified <see cref="string" /> is inserted at a specified
        /// index position in this instance.
        /// </summary>
        /// <param name="startIndex"> The zero-based index position of the insertion. </param>
        /// <param name="substring"> The <see cref="string" /> to insert. </param>
        /// <returns>
        /// A new <see cref="InsensitiveString" /> that is equivalent to this instance, but with
        /// <paramref name="substring" /> inserted at position <paramref name="startIndex" />.
        /// </returns>
        public InsensitiveString Insert(int startIndex, string substring)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            var newBaseString = _baseString.Insert(startIndex, substring);
            return new InsensitiveString(newBaseString);
        }

        /// <summary>
        /// Indicates whether this <see cref="InsensitiveString" /> is in the specified Unicode normalization form.
        /// </summary>
        /// <param name="normalizationForm"> A Unicode normalization form. </param>
        /// <returns>
        /// <see langword="true" /> if this <see cref="InsensitiveString" /> is in the  normalization form specified by
        /// the <paramref name="normalizationForm" /> parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsNormalized(NormalizationForm normalizationForm)
        {
            return _baseString.IsNormalized(normalizationForm);
        }

        /// <summary>
        /// Indicates whether this <see cref="InsensitiveString" /> is in Unicode normalization form C.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if this <see cref="InsensitiveString" /> is in normalization form C; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool IsNormalized()
        {
            return IsNormalized(NormalizationForm.FormC);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance.
        /// </summary>
        /// <param name="character"> The Unicode character to seek. </param>
        /// <returns> The zero-based index position of <paramref name="character" /> if it is found, or -1 if it is not. </returns>
        public int LastIndexOf(char character)
        {
            var length = Length;
            return LastIndexOf(character, length - 1, length);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified Unicode character within this instance. The
        /// search starts at a specified character position and proceeds backward toward the beginning of the string.
        /// </summary>
        /// <param name="character"> The Unicode character to seek. </param>
        /// <param name="startIndex">
        /// The starting position of the search. The search proceeds from <paramref name="startIndex" />
        /// toward the beginning of this instance.
        /// </param>
        /// <returns> The zero-based index position of <paramref name="character" /> if it is found, or -1 if it is not. </returns>
        public int LastIndexOf(char character, int startIndex)
        {
            return LastIndexOf(character, startIndex, startIndex + 1);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of the specified Unicode character in a substring within
        /// this instance. The search starts at a specified character position and proceeds backward toward the beginning of the
        /// string for a specified number of character positions.
        /// </summary>
        /// <param name="character"> The Unicode character to seek. </param>
        /// <param name="startIndex">
        /// The starting position of the search. The search proceeds from <paramref name="startIndex" />
        /// toward the beginning of this instance.
        /// </param>
        /// <param name="maximumCount"> </param>
        /// <returns> The zero-based index position of <paramref name="character" /> if it is found, or -1 if it is not. </returns>
        public int LastIndexOf(char character, int startIndex, int maximumCount)
        {
            var insensitiveCharacters = Insensitivify(character);
            if (insensitiveCharacters.Length == 1)
                return _baseString.LastIndexOf(character, startIndex, maximumCount);
            return _baseString.LastIndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified <see cref="string" /> within the current
        /// <see cref="InsensitiveString" /> instance.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is the last index position in this
        /// instance.
        /// </returns>
        public int LastIndexOf(string substring)
        {
            var length = Length;
            return LastIndexOf(substring, length - 1, length, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified <see cref="string" /> within the current
        /// <see cref="InsensitiveString" /> instance. A parameter specifies the type of search to use for the specified string.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="comparison">
        /// One of the enumeration values that specifies the rules for the search. This parameter is
        /// always "insensitivified" using <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is the last index position in this
        /// instance.
        /// </returns>
        public int LastIndexOf(string substring, StringComparison comparison)
        {
            var length = Length;
            return LastIndexOf(substring, length - 1, length, comparison);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified <see cref="string" /> within the current
        /// <see cref="InsensitiveString" /> instance. The search starts at a specified character position and proceeds backward
        /// toward the beginning of the string.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex">
        /// The search starting position. The search proceeds from <paramref name="startIndex" /> toward
        /// the beginning of this instance.
        /// </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is the last index position in this
        /// instance.
        /// </returns>
        public int LastIndexOf(string substring, int startIndex)
        {
            return LastIndexOf(substring, startIndex, startIndex + 1, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified <see cref="string" /> within the current
        /// <see cref="InsensitiveString" /> instance. The search starts at a specified character position and proceeds backward
        /// toward the beginning of the string. A parameter specifies the type of comparison to perform when searching for the
        /// specified string.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex">
        /// The search starting position. The search proceeds from <paramref name="startIndex" /> toward
        /// the beginning of this instance.
        /// </param>
        /// <param name="comparison">
        /// One of the enumeration values that specifies the rules for the search. This parameter is
        /// always "insensitivified" using <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is the last index position in this
        /// instance.
        /// </returns>
        public int LastIndexOf(string substring, int startIndex, StringComparison comparison)
        {
            return LastIndexOf(substring, startIndex, startIndex + 1, comparison);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified <see cref="string" /> within the current
        /// <see cref="InsensitiveString" /> instance. The search starts at a specified character position and proceeds backward
        /// toward the beginning of the string for a specified number of character positions.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex">
        /// The search starting position. The search proceeds from <paramref name="startIndex" /> toward
        /// the beginning of this instance.
        /// </param>
        /// <param name="maximumCount"> The number of character positions to examine. </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is the last index position in this
        /// instance.
        /// </returns>
        public int LastIndexOf(string substring, int startIndex, int maximumCount)
        {
            return LastIndexOf(substring, startIndex, maximumCount, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified <see cref="string" /> within the current
        /// <see cref="InsensitiveString" /> instance. The search starts at a specified character position and proceeds backward
        /// toward the beginning of the string for the specified number of character positions. A parameter specifies the type of
        /// comparison to perform when searching for the specified string.
        /// </summary>
        /// <param name="substring"> The <see cref="string" /> to seek. </param>
        /// <param name="startIndex">
        /// The search starting position. The search proceeds from <paramref name="startIndex" /> toward
        /// the beginning of this instance.
        /// </param>
        /// <param name="maximumCount"> The number of character positions to examine. </param>
        /// <param name="comparison">
        /// One of the enumeration values that specifies the rules for the search. This parameter is
        /// always "insensitivified" using <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// The zero-based index position of <paramref name="substring" /> if it is found, or -1 if it is not. If
        /// <paramref name="substring" /> is <see cref="string.Empty" />, the return value is the last index position in this
        /// instance.
        /// </returns>
        public int LastIndexOf(string substring, int startIndex, int maximumCount, StringComparison comparison)
        {
            Guard.ArgumentNotNull(substring, nameof(substring));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.LastIndexOf(substring, startIndex, maximumCount, insensitiveComparison);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a
        /// Unicode array.
        /// </summary>
        /// <param name="characters"> A Unicode character array containing one or more characters to seek. </param>
        /// <returns>
        /// The index position of the last occurrence in this instance where any character in
        /// <paramref name="characters" /> was found; -1 if no character in <paramref name="characters" /> was found.
        /// </returns>
        public int LastIndexOfAny(params char[] characters)
        {
            var length = Length;
            return LastIndexOfAny(characters, length - 1, length);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a
        /// Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the
        /// string.
        /// </summary>
        /// <param name="characters"> A Unicode character array containing one or more characters to seek. </param>
        /// <param name="startIndex">
        /// The search starting position. The search proceeds from <paramref name="startIndex" /> toward
        /// the beginning of this instance.
        /// </param>
        /// <returns>
        /// The index position of the last occurrence in this instance where any character in
        /// <paramref name="characters" /> was found; -1 if no character in <paramref name="characters" /> was found.
        /// </returns>
        public int LastIndexOfAny(char[] characters, int startIndex)
        {
            return LastIndexOfAny(characters, startIndex, startIndex + 1);
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in a
        /// Unicode array. The search starts at a specified character position and proceeds backward toward the beginning of the
        /// string for a specified number of character positions.
        /// </summary>
        /// <param name="characters"> A Unicode character array containing one or more characters to seek. </param>
        /// <param name="startIndex">
        /// The search starting position. The search proceeds from <paramref name="startIndex" /> toward
        /// the beginning of this instance.
        /// </param>
        /// <param name="maximumCount"> The number of character positions to examine. </param>
        /// <returns>
        /// The index position of the last occurrence in this instance where any character in
        /// <paramref name="characters" /> was found; -1 if no character in <paramref name="characters" /> was found.
        /// </returns>
        public int LastIndexOfAny(char[] characters, int startIndex, int maximumCount)
        {
            Guard.ArgumentNotNull(characters, nameof(characters));
            var insensitiveCharacters = Insensitivify(characters);
            return _baseString.LastIndexOfAny(insensitiveCharacters, startIndex, maximumCount);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> whose textual value is the same as this instance, but whose binary
        /// representation is in the specified Unicode normalization form.
        /// </summary>
        /// <param name="normalizationForm"> </param>
        /// <returns>
        /// A new <see cref="InsensitiveString" /> whose textual value is the same as this instance, but whose binary
        /// representation is in the normalization form specified by the <paramref name="normalizationForm" /> parameter.
        /// </returns>
        public InsensitiveString Normalize(NormalizationForm normalizationForm)
        {
            var normalizedBaseString = _baseString.Normalize(normalizationForm);
            return new InsensitiveString(normalizedBaseString);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> whose textual value is the same as this instance, but whose binary
        /// representation is in Unicode normalization form C.
        /// </summary>
        /// <returns>
        /// A new, normalized <see cref="InsensitiveString" /> whose textual value is the same as this instance, but
        /// whose binary representation is in normalization form C.
        /// </returns>
        public InsensitiveString Normalize()
        {
            return Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> that right-aligns the characters in this instance by padding them with
        /// spaces on the left, for a specified total length.
        /// </summary>
        /// <param name="totalLength">
        /// The number of characters in the resulting <see cref="InsensitiveString" />, equal to the
        /// number of original characters plus any additional padding characters.
        /// </param>
        /// <returns>
        /// A new <see cref="InsensitiveString" /> that is equivalent to this instance, but right-aligned and padded on
        /// the left with as many spaces as needed to create a length of <paramref name="totalLength" />. However, if
        /// <paramref name="totalLength" /> is less than the length of this instance, the method returns a reference to the
        /// existing instance. If <paramref name="totalLength" /> is equal to the length of this instance, the method returns a new
        /// string that is identical to this instance.
        /// </returns>
        public InsensitiveString PadLeft(int totalLength)
        {
            return PadLeft(totalLength, ' ');
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> that right-aligns the characters in this instance by padding them on the
        /// left with a specified Unicode character, for a specified total length.
        /// </summary>
        /// <param name="totalLength">
        /// The number of characters in the resulting <see cref="InsensitiveString" />, equal to the
        /// number of original characters plus any additional padding characters.
        /// </param>
        /// <param name="padder"> A Unicode padding character. </param>
        /// <returns>
        /// A new <see cref="InsensitiveString" /> that is equivalent to this instance, but right-aligned and padded on
        /// the left with as many <paramref name="padder" /> characters as needed to create a length of
        /// <paramref name="totalLength" />. However, if <paramref name="totalLength" /> is less than the length of this instance,
        /// the method returns a reference to the existing instance. If <paramref name="totalLength" /> is equal to the length of
        /// this instance, the method returns a new string that is identical to this instance.
        /// </returns>
        public InsensitiveString PadLeft(int totalLength, char padder)
        {
            var length = Length;
            if (totalLength < length)
                return this;
            if (totalLength == length)
                return new InsensitiveString(_baseString);
            var paddedBaseString = _baseString.PadLeft(totalLength, padder);
            return new InsensitiveString(paddedBaseString);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> that left-aligns the characters in this string by padding them with
        /// spaces on the right, for a specified total length.
        /// </summary>
        /// <param name="totalLength">
        /// The number of characters in the resulting <see cref="InsensitiveString" />, equal to the
        /// number of original characters plus any additional padding characters.
        /// </param>
        /// <returns>
        /// A new <see cref="InsensitiveString" /> that is equivalent to this instance, but left-aligned and padded on
        /// the right with as many space characters as needed to create a length of <paramref name="totalLength" />. However, if
        /// <paramref name="totalLength" /> is less than the length of this instance, the method returns a reference to the
        /// existing instance. If <paramref name="totalLength" /> is equal to the length of this instance, the method returns a new
        /// string that is identical to this instance.
        /// </returns>
        public InsensitiveString PadRight(int totalLength)
        {
            return PadRight(totalLength, ' ');
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> that left-aligns the characters in this string by padding them on the
        /// right with a specified Unicode character, for a specified total length.
        /// </summary>
        /// <param name="totalLength">
        /// The number of characters in the resulting <see cref="InsensitiveString" />, equal to the
        /// number of original characters plus any additional padding characters.
        /// </param>
        /// <param name="padder"> A Unicode padding character. </param>
        /// <returns>
        /// A new <see cref="InsensitiveString" /> that is equivalent to this instance, but left-aligned and padded on
        /// the right with as many <paramref name="padder" /> characters as needed to create a length of
        /// <paramref name="totalLength" />. However, if <paramref name="totalLength" /> is less than the length of this instance,
        /// the method returns a reference to the existing instance. If <paramref name="totalLength" /> is equal to the length of
        /// this instance, the method returns a new string that is identical to this instance.
        /// </returns>
        public InsensitiveString PadRight(int totalLength, char padder)
        {
            var length = Length;
            if (totalLength < length)
                return this;
            if (totalLength == length)
                return new InsensitiveString(_baseString);
            var paddedBaseString = _baseString.PadRight(totalLength, padder);
            return new InsensitiveString(paddedBaseString);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> in which all the characters in the current instance, beginning at a
        /// specified position and continuing through the last position, have been deleted.
        /// </summary>
        /// <param name="startIndex"> The zero-based position to begin deleting characters. </param>
        /// <returns> A new <see cref="InsensitiveString" /> that is equivalent to this instance except for the removed characters. </returns>
        public InsensitiveString Remove(int startIndex)
        {
            return Substring(0, startIndex);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> in which a specified number of characters in the current instance
        /// beginning at a specified position have been deleted.
        /// </summary>
        /// <param name="startIndex"> The zero-based position to begin deleting characters. </param>
        /// <param name="count"> The number of characters to delete. </param>
        /// <returns> A new <see cref="InsensitiveString" /> that is equivalent to this instance except for the removed characters. </returns>
        public InsensitiveString Remove(int startIndex, int count)
        {
            var newBaseString = _baseString.Remove(startIndex, count);
            return new InsensitiveString(newBaseString);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> in which all occurrences of a specified Unicode character in this
        /// instance are replaced with another specified Unicode character.
        /// </summary>
        /// <param name="oldCharacter"> The Unicode character to be replaced. </param>
        /// <param name="newCharacter"> The Unicode character to replace all occurrences of <paramref name="oldCharacter" />. </param>
        /// <returns>
        /// An <see cref="InsensitiveString" /> that is equivalent to this instance except that all instances of
        /// <paramref name="oldCharacter" /> are replaced with <paramref name="newCharacter" />. If
        /// <paramref name="oldCharacter" /> is not found in the current instance, the method returns the current instance
        /// unchanged.
        /// </returns>
        public InsensitiveString Replace(char oldCharacter, char newCharacter)
        {
            var newBaseStringBuilder = new StringBuilder(_baseString, Length);
            var insensitiveOldCharacters = Insensitivify(oldCharacter);
            foreach (var character in insensitiveOldCharacters)
                newBaseStringBuilder = newBaseStringBuilder.Replace(character, newCharacter);
            var newBaseString = newBaseStringBuilder.ToString();
            if (newBaseString == _baseString)
                return this;
            return new InsensitiveString(newBaseString);
        }

        /// <summary>
        /// Returns a new <see cref="InsensitiveString" /> in which all occurrences of a specified string in the current instance
        /// are replaced with another specified string.
        /// </summary>
        /// <param name="oldSubstring"> The <see cref="string" /> to be replaced. </param>
        /// <param name="newSubstring"> The <see cref="string" /> to replace all occurrences of <paramref name="oldSubstring" />. </param>
        /// <returns>
        /// An <see cref="InsensitiveString" /> that is equivalent to the current instance except that all instances of
        /// <paramref name="oldSubstring" /> are replaced with <paramref name="newSubstring" />. If
        /// <paramref name="oldSubstring" /> is not found in the current instance, the method returns the current instance
        /// unchanged.
        /// </returns>
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

        /// <summary>
        /// Splits an <see cref="InsensitiveString" /> into substrings that are based on the characters in an array.
        /// </summary>
        /// <param name="separators"> A character array that delimits the substrings in this <see cref="InsensitiveString" />. </param>
        /// <returns>
        /// An array whose elements contain the substrings from this instance that are delimited by one or more
        /// characters in <paramref name="separators" />.
        /// </returns>
        public InsensitiveString[] Split(params char[] separators)
        {
            return Split(separators, int.MaxValue);
        }

        /// <summary>
        /// Splits a string into a maximum number of substrings based on the characters in an array.
        /// </summary>
        /// <param name="separators"> A character array that delimits the substrings in this <see cref="InsensitiveString" />. </param>
        /// <param name="options">
        /// <see cref="StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array
        /// returned; or <see cref="StringSplitOptions.None" /> to include empty array elements in the array returned.
        /// </param>
        /// <returns>
        /// An array whose elements contain the substrings from this instance that are delimited by one or more
        /// characters in <paramref name="separators" />.
        /// </returns>
        public InsensitiveString[] Split(char[] separators, StringSplitOptions options)
        {
            return Split(separators, int.MaxValue, options);
        }

        /// <summary>
        /// Splits an <see cref="InsensitiveString" /> into substrings that are based on the characters in an array. Parameter
        /// specifies the maximum number of substrings to return
        /// </summary>
        /// <param name="separators"> A character array that delimits the substrings in this <see cref="InsensitiveString" />. </param>
        /// <param name="maximumCount"> The maximum number of substrings to return. </param>
        /// <returns>
        /// An array whose elements contain the substrings from this instance that are delimited by one or more
        /// characters in <paramref name="separators" />.
        /// </returns>
        public InsensitiveString[] Split(char[] separators, int maximumCount)
        {
            return Split(separators, maximumCount, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits a string into a maximum number of substrings based on the characters in an array.
        /// </summary>
        /// <param name="separators"> A character array that delimits the substrings in this <see cref="InsensitiveString" />. </param>
        /// <param name="maximumCount"> The maximum number of substrings to return. </param>
        /// <param name="options">
        /// <see cref="StringSplitOptions.RemoveEmptyEntries" /> to omit empty array elements from the array
        /// returned; or <see cref="StringSplitOptions.None" /> to include empty array elements in the array returned.
        /// </param>
        /// <returns>
        /// An array whose elements contain the substrings from this instance that are delimited by one or more
        /// characters in <paramref name="separators" />.
        /// </returns>
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

        /// <summary>
        /// Determines whether the beginning of this <see cref="InsensitiveString" /> matches the specified string.
        /// </summary>
        /// <param name="prefix"> The <see cref="string" /> to compare. </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="prefix" /> matches the beginning of this instance; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool StartsWith(string prefix)
        {
            return StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Determines whether the beginning of this <see cref="InsensitiveString" /> matches the specified string using the
        /// specified comparison option.
        /// </summary>
        /// <param name="prefix"> The <see cref="string" /> to compare. </param>
        /// <param name="comparison">
        /// One of the enumeration values that determines how this <see cref="InsensitiveString" /> and
        /// <paramref name="prefix" /> are compared. This parameter is always "insensitivified" using
        /// <see cref="Insensitivify(System.StringComparison)" /> method.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="prefix" /> matches the beginning of this instance; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool StartsWith(string prefix, StringComparison comparison)
        {
            Guard.ArgumentNotNull(prefix, nameof(prefix));
            var insensitiveComparison = Insensitivify(comparison);
            return _baseString.StartsWith(prefix, insensitiveComparison);
        }

        /// <summary>
        /// Determines whether the beginning of this <see cref="InsensitiveString" /> matches the specified string using the
        /// specified culture.
        /// </summary>
        /// <param name="prefix"> The <see cref="string" /> to compare. </param>
        /// <param name="culture">
        /// Cultural information that determines how this instance and <paramref name="prefix" /> are
        /// compared. If <paramref name="culture" /> is <see langword="null" />, <see cref="CultureInfo.CurrentCulture" /> is used.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="prefix" /> matches the beginning of this instance; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool StartsWith(string prefix, CultureInfo culture)
        {
            Guard.ArgumentNotNull(prefix, nameof(prefix));
            culture = culture ?? CultureInfo.CurrentCulture;
            return _baseString.StartsWith(prefix, true, culture);
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified character position and continues to the
        /// end of the string.
        /// </summary>
        /// <param name="startIndex"> The zero-based starting character position of a substring in this instance. </param>
        /// <returns>
        /// An <see cref="InsensitiveString" /> that is equivalent to the substring that begins at
        /// <paramref name="startIndex" /> in this instance, or <see cref="string.Empty" /> if <paramref name="startIndex" /> is
        /// equal to the length of this instance.
        /// </returns>
        public InsensitiveString Substring(int startIndex)
        {
            return new InsensitiveString(_baseString.Substring(startIndex));
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified character position and has a specified
        /// length.
        /// </summary>
        /// <param name="startIndex"> The zero-based starting character position of a substring in this instance. </param>
        /// <param name="length"> The number of characters in the substring. </param>
        /// <returns>
        /// An <see cref="InsensitiveString" /> that is equivalent to the substring of length <paramref name="length" />
        /// that begins at <paramref name="startIndex" /> in this instance, or <see cref="string.Empty" /> if
        /// <paramref name="startIndex" /> is equal to the length of this instance and <paramref name="length" /> is zero.
        /// </returns>
        public InsensitiveString Substring(int startIndex, int length)
        {
            return new InsensitiveString(_baseString.Substring(startIndex, length));
        }

        /// <summary>
        /// Copies the characters in this instance to a Unicode character array.
        /// </summary>
        /// <returns>
        /// A Unicode character array whose elements are the individual characters of this instance. If this instance is
        /// an empty string, the returned array is empty and has a zero length.
        /// </returns>
        public char[] ToCharArray()
        {
            return _baseString.ToCharArray();
        }

        /// <summary>
        /// Copies the characters in a specified substring in this instance to a Unicode character array.
        /// </summary>
        /// <param name="startIndex"> The starting position of a substring in this instance. </param>
        /// <param name="length"> The length of the substring in this instance. </param>
        /// <returns>
        /// A Unicode character array whose elements are the <paramref name="length" /> number of characters in this
        /// instance starting from character position <paramref name="startIndex" />.
        /// </returns>
        public char[] ToCharArray(int startIndex, int length)
        {
            return _baseString.ToCharArray(startIndex, length);
        }

        /// <summary>
        /// Returns a copy of this <see cref="InsensitiveString" /> converted to lowercase, using the casing rules of the specified
        /// culture.
        /// </summary>
        /// <param name="culture"> An object that supplies culture-specific casing rules. </param>
        /// <returns> The lowercase equivalent of the current <see cref="InsensitiveString" />. </returns>
        public InsensitiveString ToLower(CultureInfo culture)
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            var lowerCasedBaseString = _baseString.ToLower(culture);
            return new InsensitiveString(lowerCasedBaseString);
        }

        /// <summary>
        /// Returns a copy of this <see cref="InsensitiveString" /> converted to lowercase.
        /// </summary>
        /// <returns> An <see cref="InsensitiveString" /> in lowercase. </returns>
        /// <remarks> This method takes into account the casing rules of the current culture. </remarks>
        public InsensitiveString ToLower()
        {
            return ToLower(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a copy of this <see cref="InsensitiveString" /> converted to lowercase, using the casing rules of invariant
        /// culture.
        /// </summary>
        /// <returns> The lowercase equivalent of the current <see cref="InsensitiveString" />. </returns>
        public InsensitiveString ToLowerInvariant()
        {
            return ToLower(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the string from which this instance was created. No actual conversion is performed.
        /// </summary>
        /// <param name="formatProvider"> An object that supplies culture-specific formatting information (not used). </param>
        /// <returns> The string from which this instance was created. </returns>
        public string ToString(IFormatProvider formatProvider)
        {
            return _baseString.ToString(formatProvider);
        }

        /// <summary>
        /// Returns a copy of this <see cref="InsensitiveString" /> converted to uppercase, using the casing rules of the specified
        /// culture.
        /// </summary>
        /// <param name="culture"> An object that supplies culture-specific casing rules. </param>
        /// <returns> The uppercase equivalent of the current <see cref="InsensitiveString" />. </returns>
        public InsensitiveString ToUpper(CultureInfo culture)
        {
            culture = culture ?? CultureInfo.CurrentCulture;
            var upperCasedBaseString = _baseString.ToUpper(culture);
            return new InsensitiveString(upperCasedBaseString);
        }

        /// <summary>
        /// Returns a copy of this <see cref="InsensitiveString" /> converted to uppercase.
        /// </summary>
        /// <returns> An <see cref="InsensitiveString" /> in uppercase. </returns>
        /// <remarks> This method takes into account the casing rules of the current culture. </remarks>
        public InsensitiveString ToUpper()
        {
            return ToUpper(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a copy of this <see cref="InsensitiveString" /> converted to uppercase, using the casing rules of invariant
        /// culture.
        /// </summary>
        /// <returns> The uppercase equivalent of the current <see cref="InsensitiveString" />. </returns>
        public InsensitiveString ToUpperInvariant()
        {
            return ToUpper(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of characters specified in an array from the current instance.
        /// </summary>
        /// <param name="trimmers"> An array of Unicode characters to remove. </param>
        /// <returns>
        /// The <see cref="InsensitiveString" /> that remains after all occurrences of the characters in the
        /// <paramref name="trimmers" /> parameter are removed from the start and end of the current instance. If
        /// <paramref name="trimmers" /> is null or an empty array, white-space characters are removed instead. If no characters
        /// can be trimmed from the current instance, the method returns the current instance unchanged.
        /// </returns>
        public InsensitiveString Trim(params char[] trimmers)
        {
            var insensitiveTrimmers = Insensitivify(trimmers);
            var trimmedBaseString = _baseString.Trim(insensitiveTrimmers);
            if (ReferenceEquals(trimmedBaseString, _baseString))
                return this;
            return new InsensitiveString(trimmedBaseString);
        }

        /// <summary>
        /// Removes all trailing occurrences of a set of characters specified in an array from the current instance.
        /// </summary>
        /// <param name="trimmers"> An array of Unicode characters to remove. </param>
        /// <returns>
        /// The <see cref="InsensitiveString" /> that remains after all occurrences of the characters in the
        /// <paramref name="trimmers" /> parameter are removed from the end of the current instance. If
        /// <paramref name="trimmers" /> is null or an empty array, white-space characters are removed instead. If no characters
        /// can be trimmed from the current instance, the method returns the current instance unchanged.
        /// </returns>
        public InsensitiveString TrimEnd(params char[] trimmers)
        {
            var insensitiveTrimmers = Insensitivify(trimmers);
            var trimmedBaseString = _baseString.TrimEnd(insensitiveTrimmers);
            if (ReferenceEquals(trimmedBaseString, _baseString))
                return this;
            return new InsensitiveString(trimmedBaseString);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of characters specified in an array from the current instance.
        /// </summary>
        /// <param name="trimmers"> An array of Unicode characters to remove. </param>
        /// <returns>
        /// The <see cref="InsensitiveString" /> that remains after all occurrences of the characters in the
        /// <paramref name="trimmers" /> parameter are removed from the start of the current instance. If
        /// <paramref name="trimmers" /> is null or an empty array, white-space characters are removed instead. If no characters
        /// can be trimmed from the current instance, the method returns the current instance unchanged.
        /// </returns>
        public InsensitiveString TrimStart(params char[] trimmers)
        {
            var insensitiveTrimmers = Insensitivify(trimmers);
            var trimmedBaseString = _baseString.TrimStart(insensitiveTrimmers);
            if (ReferenceEquals(trimmedBaseString, _baseString))
                return this;
            return new InsensitiveString(trimmedBaseString);
        }
    }
}