using ArchitectNET.Core.Collections;

namespace ArchitectNET.Core.Dynamic
{
    public interface IObjectAttributeCollection : IFixedCollection<object>, IHasOwner<IAttributedObject>
    {
    }
}