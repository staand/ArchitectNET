using System;

namespace ArchitectNET.Core
{
    /// <summary>
    /// Marks the object which represents metadata in the form of mathematician classes (or sets). An object that has metadata
    /// of type <typeparamref name="TClass" /> is considered a member of the set represented by that instance of
    /// <typeparamref name="TClass" />. Typically, any implementer of <see cref="IClasslikeMetadata{TClass}" /> should also be
    /// a <typeparamref name="TClass" /> type.
    /// </summary>
    /// <typeparam name="TClass"> A type representing </typeparam>
    public interface IClasslikeMetadata<TClass> : IMetadata, IEquatable<TClass>
        where TClass : IClasslikeMetadata<TClass>
    {
        /// <summary>
        /// Determines whether current instance is a subclass (subset) of the given class. In case of <see langword="true" />
        /// result <paramref name="otherClass" /> is considered a superclass (superset) of this instance, thus any object
        /// belonging to this instance is also a member of <paramref name="otherClass" />.
        /// </summary>
        /// <param name="otherClass"> A class (set) which is a candidate to be a superclass of the current instance. </param>
        /// <returns>
        /// <see langword="true" />, if this instance is a subclass of the <paramref name="otherClass" />; otherwise
        /// <see langword="false" />.
        /// </returns>
        bool IsSubclassOf(TClass otherClass);
    }
}