namespace ArchitectNET.Core.Dynamic
{
    public interface IReturningInvocableMember : IInvocableMember
    {
        IType ReturnType { get; }
    }
}