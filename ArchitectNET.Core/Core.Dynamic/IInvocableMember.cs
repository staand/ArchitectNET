namespace ArchitectNET.Core.Dynamic
{
    public interface IInvocableMember : IMember
    {
        IMemberParameterCollection Parameters { get; }
    }
}