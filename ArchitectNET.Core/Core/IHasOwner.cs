namespace ArchitectNET.Core
{
    /// <summary>
    /// Marks the object which has an owner. "Ownership" reflects a relation of aggregation between objects, thus
    /// <see cref="Owner" /> is mandatory and can't be <see langword="null" />
    /// </summary>
    /// <typeparam name="TOwner"> </typeparam>
    public interface IHasOwner<out TOwner>
    {
        /// <summary>
        /// Returns the owner of the current instance
        /// </summary>
        TOwner Owner { get; }
    }
}