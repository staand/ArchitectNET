namespace ArchitectNET.Core
{
    /// <summary>
    /// Marks the object which supports unified synchronized access using the <see cref="System.Threading.Monitor" /> class
    /// methods or
    /// <see langword="lock" /> keyword.
    /// </summary>
    /// <example>
    /// This sample shows how an <see cref="ISynchronizable" /> object can be used:
    /// <code>
    /// ISynchronizable synchronizable = ...;
    /// lock(synchronizable.Synchronizer)
    /// {
    ///     ...
    /// }
    /// </code>
    /// </example>
    public interface ISynchronizable
    {
        /// <summary>
        /// Returns an object which should be passed to <see cref="System.Threading.Monitor" /> class methods or used with
        /// <see langword="lock" />
        /// keyword.
        /// </summary>
        object Synchronizer { get; }
    }
}