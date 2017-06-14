using System.Collections;
using System.Collections.Generic;
using ArchitectNET.Core.Collections;

namespace ArchitectNET.Core.Support
{
    public abstract class ClassUnionBase<TClass> : ClassBase<TClass>, IFixedCollection<TClass>
        where TClass : IClasslikeMetadata<TClass>
    {
        private readonly HashSet<TClass> _unitedClasses;
        private int _hashCode;

        protected ClassUnionBase(IEnumerable<TClass> unitedClasses)
        {
            Guard.ArgumentNotNull(unitedClasses, nameof(unitedClasses));
            var flattenedClasses = new List<TClass>();
            foreach (var @class in unitedClasses)
            {
                var classUnion = @class as ClassUnionBase<TClass>;
                if (classUnion == null)
                    flattenedClasses.Add(@class);
                else if (classUnion._unitedClasses.Count > 0)
                    flattenedClasses.AddRange(classUnion._unitedClasses);
            }
            _unitedClasses = new HashSet<TClass>(flattenedClasses);
        }

        protected ClassUnionBase(params TClass[] unitedClasses)
            : this((IEnumerable<TClass>) unitedClasses)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TClass> GetEnumerator()
        {
            return _unitedClasses.GetEnumerator();
        }

        public bool Contains(TClass @class)
        {
            return @class != null
                   && _unitedClasses.Contains(@class);
        }

        public int Count => _unitedClasses.Count;
    }
}