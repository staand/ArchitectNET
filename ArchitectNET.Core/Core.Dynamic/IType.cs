namespace ArchitectNET.Core.Dynamic
{
    public interface IType : IGenericMember
    {
        new IType GenericDefinition { get; }
    }
}