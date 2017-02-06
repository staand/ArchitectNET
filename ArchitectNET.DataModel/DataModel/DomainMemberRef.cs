using System;
using ArchitectNET.Core;
using ArchitectNET.DataModel._Internal_;

namespace ArchitectNET.DataModel
{
    public struct DomainMemberRef : IEquatable<DomainMemberRef>
    {
        private readonly DomainMemberRefKind _kind;
        private readonly object _refObject;

        public DomainMemberRef(string alias)
        {
            Guard.ArgumentNotNull(alias, nameof(alias));
            _kind = DomainMemberRefKind.Alias;
            _refObject = alias;
        }

        public DomainMemberRef(ID id)
            : this()
        {
            _kind = DomainMemberRefKind.ID;
            _refObject = id;
        }

        public DomainMemberRef(IDomainRole role)
            : this()
        {
            Guard.ArgumentNotNull(role, nameof(role));
            _kind = DomainMemberRefKind.Role;
            _refObject = role;
        }

        public static DomainMemberRef Empty => new DomainMemberRef();

        public static bool operator ==(DomainMemberRef memberRef1, DomainMemberRef memberRef2)
        {
            return memberRef1.Equals(memberRef2);
        }

        public static implicit operator DomainMemberRef(string alias)
        {
            if (alias == null)
                return Empty;
            return new DomainMemberRef(alias);
        }

        public static implicit operator DomainMemberRef(ID id)
        {
            return new DomainMemberRef(id);
        }

        public static bool operator !=(DomainMemberRef memberRef1, DomainMemberRef memberRef2)
        {
            return !memberRef1.Equals(memberRef2);
        }

        public string Alias
        {
            get
            {
                if (_kind == DomainMemberRefKind.Alias)
                    return (string) _refObject;
                throw new Exception(Resources.FormatString("64062D4D-1C7F-4B09-B678-A7A373C04F57", _kind));
            }
        }

        public ID ID
        {
            get
            {
                if (_kind == DomainMemberRefKind.ID)
                    return (ID) _refObject;
                throw new Exception(Resources.FormatString("127D6A32-2BE5-47B3-8761-3A79C5BB103A", _kind));
            }
        }

        public bool IsEmpty => _refObject == null;

        public DomainMemberRefKind Kind => _kind;

        public bool RefersByAlias => _kind == DomainMemberRefKind.Alias;

        public bool RefersByID => _kind == DomainMemberRefKind.ID;

        public bool RefersByRole => _kind == DomainMemberRefKind.Role;

        public IDomainRole Role
        {
            get
            {
                if (_kind == DomainMemberRefKind.Role)
                    return (IDomainRole) _refObject;
                throw new Exception(Resources.FormatString("0BCCDEC4-1B19-4B8F-9307-B9086BC8CB01", _kind));
            }
        }

        public bool Equals(DomainMemberRef otherMemberRef)
        {
            var refObject = _refObject;
            var otherRefObject = otherMemberRef._refObject;
            if (ReferenceEquals(refObject, otherRefObject))
                return true;
            if (refObject == null || otherRefObject == null)
                return false;
            var kind = _kind;
            var otherKind = otherMemberRef._kind;
            if (kind != otherKind)
                return false;
            switch (kind)
            {
                case DomainMemberRefKind.None:
                    return true;
                case DomainMemberRefKind.Alias:
                    return (string) refObject == (string) otherRefObject;
                case DomainMemberRefKind.ID:
                    return (ID) refObject == (ID) otherRefObject;
                case DomainMemberRefKind.Role:
                    return ((IDomainRole) refObject).Equals((IDomainRole) otherRefObject);
            }
            throw new Exception(Resources.FormatString("CB24088C-0C5D-48D7-8040-6AEEDAB31F61", kind));
        }

        public override bool Equals(object otherObject)
        {
            var otherMemberRef = otherObject as DomainMemberRef?;
            return otherMemberRef.HasValue
                   && Equals(otherMemberRef.Value);
        }

        public override int GetHashCode()
        {
            if (_refObject == null)
                return 0;
            return _refObject.GetHashCode();
        }

        public override string ToString()
        {
            if (IsEmpty)
                return "<empty>";
            return $"{_refObject} [{_kind}]";
        }
    }
}