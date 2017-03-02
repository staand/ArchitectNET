using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArchitectNET.Core.Collections;

namespace ArchitectNET.Core.Dynamic.Classification
{
    public sealed class DynamicClassUnion : DynamicClass, IFixedCollection<IDynamicClass>
    {
        private readonly HashSet<IDynamicClass> _unitedClasses;
        private int _hashCode;

        public DynamicClassUnion(IEnumerable<IDynamicClass> unitedClasses)
        {
            Guard.ArgumentNotNull(unitedClasses, nameof(unitedClasses));
            var flattenedClasses = new List<IDynamicClass>();
            foreach (var @class in unitedClasses)
            {
                var classUnion = @class as DynamicClassUnion;
                if (classUnion == null)
                    flattenedClasses.Add(@class);
                else if (classUnion._unitedClasses.Count > 0)
                    flattenedClasses.AddRange(classUnion._unitedClasses);
            }
            _unitedClasses = new HashSet<IDynamicClass>(flattenedClasses);
        }

        public DynamicClassUnion(params IDynamicClass[] unitedClasses)
            : this((IEnumerable<IDynamicClass>) unitedClasses)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IDynamicClass> GetEnumerator()
        {
            return _unitedClasses.GetEnumerator();
        }

        public bool Contains(IDynamicClass @class)
        {
            return @class != null
                   && _unitedClasses.Contains(@class);
        }

        public int Count => _unitedClasses.Count;

        public override bool Equals(IDynamicClass otherClass)
        {
            if (otherClass == null)
                return false;
            if (ReferenceEquals(this, otherClass))
                return true;
            var otherClassUnion = otherClass as DynamicClassUnion;
            return otherClassUnion != null
                   && _unitedClasses.SetEquals(otherClassUnion._unitedClasses);
        }

        public override int GetHashCode()
        {
            var hashCode = _hashCode;
            if (hashCode != 0 || _unitedClasses.Count == 0)
                return hashCode;
            foreach (var @class in _unitedClasses)
                hashCode ^= @class.GetHashCode();
            return _hashCode = hashCode;
        }

        public override bool IsSubclassOf(IDynamicClass otherClass)
        {
            var unitedClasses = _unitedClasses;
            if (otherClass == null)
                return false;
            return ReferenceEquals(otherClass, this)
                   || unitedClasses.Count == 0
                   || unitedClasses.All(otherClass.IsSubclassOf);
        }

        public override string ToString()
        {
            return $"({string.Join(" | ", _unitedClasses)})";
        }
    }
}