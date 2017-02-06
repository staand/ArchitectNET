namespace ArchitectNET.Core.Dynamic
{
    public interface IMethod : IReturningInvocableMember, IGenericMember
    {
        new IMethod GenericDefinition { get; }
    }
}