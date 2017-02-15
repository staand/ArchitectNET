namespace ArchitectNET.Core
{
    /// <summary>
    /// Marks the object which has a name. Unlike the alias which is introduced by <see cref="IHasAlias" /> interface, name is
    /// mandatory and can NOT be <see langword="null" />
    /// </summary>
    public interface IHasName
    {
        /// <summary>
        /// Returns the name of the current instance
        /// </summary>
        string Name { get; }
    }
}