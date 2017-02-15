namespace ArchitectNET.Core
{
    /// <summary>
    /// Marks the object which can have an alias (pseudonym). Unlike the name which is introduced by <see cref="IHasName" />
    /// interface, alias is optional and can be <see langword="null" />
    /// </summary>
    public interface IHasAlias
    {
        /// <summary>
        /// Returns the alias of the current instance or <see langword="null" /> if the alias is not defined
        /// </summary>
        string Alias { get; }
    }
}