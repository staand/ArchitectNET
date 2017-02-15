namespace ArchitectNET.Core
{
    /// <summary>
    /// Marks the object which has an identifier (associated <see cref="ID" />)
    /// </summary>
    public interface IHasID
    {
        /// <summary>
        /// Returns the identifier of the current instance
        /// </summary>
        ID ID { get; }
    }
}