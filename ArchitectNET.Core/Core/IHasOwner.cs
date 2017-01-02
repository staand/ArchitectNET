namespace ArchitectNET.Core
{
    public interface IHasOwner<out TOwner>
    {
        TOwner Owner { get; }
    }
}