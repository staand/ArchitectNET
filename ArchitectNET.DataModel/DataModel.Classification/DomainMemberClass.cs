using ArchitectNET.Core;

namespace ArchitectNET.DataModel.Classification
{
    public class DomainMemberClass : IDomainMemberClass
    {
        private static readonly IDomainMemberClass _domainModelClass;
        private static readonly IDomainMemberClass _literalClass;
        private static readonly IDomainMemberClass _queryClass;
        private static readonly IDomainMemberClass _typeClass;
        private readonly string _alias;

        static DomainMemberClass()
        {
            _domainModelClass = new DomainModelMemberClass();
            _typeClass = new TypeMemberClass();
            _queryClass = new QueryMemberClass();
            _literalClass = new LiteralMemberClass();
        }

        public DomainMemberClass(string alias)
        {
            Guard.ArgumentNotNull(alias, nameof(alias));
            _alias = alias;
        }

        public static IDomainMemberClass DomainModel => _domainModelClass;

        public static IDomainMemberClass Literal => _literalClass;

        public static IDomainMemberClass Query => _queryClass;

        public static IDomainMemberClass Type => _typeClass;

        public virtual bool IsSubclassOf(IDomainMemberClass otherMemberClass)
        {
            if (otherMemberClass == null)
                return false;
            return Equals(this, otherMemberClass)
                   || GetType().IsSubclassOf(otherMemberClass.GetType());
        }

        public virtual bool Equals(IDomainMemberClass otherMemberClass)
        {
            return ReferenceEquals(this, otherMemberClass);
        }

        public string Alias => _alias;

        public override bool Equals(object otherObject)
        {
            var otherMemberClass = otherObject as IDomainMemberClass;
            return otherMemberClass != null
                   && Equals(otherMemberClass);
        }

        public override int GetHashCode()
        {
            return _alias.GetHashCode();
        }

        public override string ToString()
        {
            return _alias;
        }
    }
}