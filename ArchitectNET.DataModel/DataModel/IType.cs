namespace ArchitectNET.DataModel
{
    public interface IType : IDomainMember
    {
        ITypeSupertypeCollection Supertypes { get; }
    }
}