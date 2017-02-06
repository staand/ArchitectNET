using ArchitectNET.Core;

namespace ArchitectNET.DataModel.Support
{
    public class DomainRole : IDomainRole
    {
        private readonly string _alias;

        public DomainRole(string alias)
        {
            Guard.ArgumentNotNull(alias, nameof(alias));
            _alias = alias;
        }

        public bool Equals(IDomainRole otherRole)
        {
            return otherRole != null
                   && _alias == otherRole.Alias;
        }

        public string Alias => _alias;
    }
}