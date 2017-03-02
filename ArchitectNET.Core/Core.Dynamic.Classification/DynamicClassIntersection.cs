using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArchitectNET.Core.Collections;

namespace ArchitectNET.Core.Dynamic.Classification
{
    public sealed class DynamicClassIntersection : DynamicClass, IFixedCollection<IDynamicClass>
    {
        private readonly HashSet<IDynamicClass> _intersectedClasses;
        private int _hashCode;

        public DynamicClassIntersection(IEnumerable<IDynamicClass> intersectedClasses)
        {
            Guard.ArgumentNotNull(intersectedClasses, nameof(intersectedClasses));
            var flattenedClasses = new List<IDynamicClass>();
            foreach (var @class in intersectedClasses)
            {
                var classIntersection = @class as DynamicClassIntersection;
                if (classIntersection == null)
                    flattenedClasses.Add(@class);
                else if (classIntersection._intersectedClasses.Count > 0)
                    flattenedClasses.AddRange(classIntersection._intersectedClasses);
            }
            _intersectedClasses = new HashSet<IDynamicClass>(flattenedClasses);
        }

        public DynamicClassIntersection(params IDynamicClass[] intersectedClasses)
            : this((IEnumerable<IDynamicClass>) intersectedClasses)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IDynamicClass> GetEnumerator()
        {
            return _intersectedClasses.GetEnumerator();
        }

        public bool Contains(IDynamicClass @class)
        {
            return @class != null
                   && _intersectedClasses.Contains(@class);
        }

        public int Count => _intersectedClasses.Count;

        public override bool Equals(IDynamicClass otherClass)
        {
            if (otherClass == null)
                return false;
            if (ReferenceEquals(this, otherClass))
                return true;
            var otherClassIntersection = otherClass as DynamicClassIntersection;
            return otherClassIntersection != null
                   && _intersectedClasses.SetEquals(otherClassIntersection._intersectedClasses);
        }

        public override int GetHashCode()
        {
            var hashCode = _hashCode;
            if (hashCode != 0 || _intersectedClasses.Count == 0)
                return hashCode;
            foreach (var @class in _intersectedClasses)
                hashCode *= @class.GetHashCode();
            return _hashCode = hashCode;
        }

        public override bool IsSubclassOf(IDynamicClass otherClass)
        {
            if (otherClass == null)
                return false;
            var intersectedClasses = _intersectedClasses;
            if (ReferenceEquals(otherClass, this)
                || intersectedClasses.Count == 0
                || intersectedClasses.Contains(otherClass)
                || intersectedClasses.Any(otherClass.IsSuperclassOf))
            {
                return true;
            }
            var otherClassUnion = otherClass as DynamicClassUnion;
            return otherClassUnion != null
                   && otherClassUnion.Contains(this);
        }

        public override string ToString()
        {
            return $"({string.Join(" & ", _intersectedClasses)})";
        }
    }
}