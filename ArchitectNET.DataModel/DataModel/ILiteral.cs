namespace ArchitectNET.DataModel
{
    public interface ILiteral : IDomainMember
    {
        IType Type { get; }
    }
}