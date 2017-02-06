namespace ArchitectNET.Core.Dynamic
{
    public interface IGenericMember : IMember
    {
        IGenericMember GenericDefinition { get; }
        IMemberTypeArgumentCollection TypeArguments { get; }
    }
}