namespace ArchitectNET.DataModel
{
    public interface IDomainModelAware
    {
        IDomainModel Model { get; }
    }
}