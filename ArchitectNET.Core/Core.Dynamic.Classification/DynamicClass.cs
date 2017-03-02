using System.Runtime.CompilerServices;

namespace ArchitectNET.Core.Dynamic.Classification
{
    public abstract class DynamicClass : IDynamicClass
    {
        private static readonly IDynamicClass _assemblyClass;
        private static readonly IDynamicClass _constructorClass;
        private static readonly IDynamicClass _dynamicSystemClass;
        private static readonly IDynamicClass _eventClass;
        private static readonly IDynamicClass _fieldClass;
        private static readonly IDynamicClass _internalClass;
        private static readonly IDynamicClass _memberClass;
        private static readonly IDynamicClass _methodClass;
        private static readonly IDynamicClass _moduleClass;
        private static readonly IDynamicClass _parameterClass;
        private static readonly IDynamicClass _privateClass;
        private static readonly IDynamicClass _propertyClass;
        private static readonly IDynamicClass _protectedClass;
        private static readonly IDynamicClass _protectedInternalClass;
        private static readonly IDynamicClass _publicClass;
        private static readonly IDynamicClass _runtimeObjectHolderClass;
        private static readonly IDynamicClass _staticClass;
        private static readonly IDynamicClass _typeClass;
        private static readonly IDynamicClass _typeParameterClass;

        static DynamicClass()
        {
            _dynamicSystemClass = new DynamicRuntimeClass();
            _runtimeObjectHolderClass = new RuntimeObjectHolderClass();
            _assemblyClass = new AssemblyClass();
            _moduleClass = new ModuleClass();
            _constructorClass = new ConstructorClass();
            _eventClass = new EventClass();
            _fieldClass = new FieldClass();
            _memberClass = new MemberClass();
            _methodClass = new MethodClass();
            _parameterClass = new ParameterClass();
            _propertyClass = new PropertyClass();
            _staticClass = new StaticMemberClass();
            _typeClass = new TypeClass();
            _typeParameterClass = new TypeParameterClass();
            _publicClass = new PublicMemberClass();
            _protectedClass = new ProtectedMemberClass();
            _privateClass = new PrivateMemberClass();
            _internalClass = new InternalMemberClass();
        }

        public static IDynamicClass Assembly => _assemblyClass;

        public static IDynamicClass Constructor => _constructorClass;

        public static IDynamicClass DynamicSystem => _dynamicSystemClass;

        public static IDynamicClass Event => _eventClass;

        public static IDynamicClass Field => _fieldClass;

        public static IDynamicClass Internal => _internalClass;

        public static IDynamicClass Member => _memberClass;

        public static IDynamicClass Method => _methodClass;

        public static IDynamicClass Module => _moduleClass;

        public static IDynamicClass Parameter => _parameterClass;

        public static IDynamicClass Private => _privateClass;

        public static IDynamicClass Property => _propertyClass;

        public static IDynamicClass Protected => _protectedClass;

        public static IDynamicClass ProtectedInternal => _protectedInternalClass;

        public static IDynamicClass Public => _publicClass;

        public static IDynamicClass RuntimeObjectHolder => _runtimeObjectHolderClass;

        public static IDynamicClass Static => _staticClass;

        public static IDynamicClass Type => _typeClass;

        public static IDynamicClass TypeParameter => _typeParameterClass;

        public virtual bool IsSubclassOf(IDynamicClass otherClass)
        {
            if (otherClass == null)
                return false;
            if (Equals(otherClass) || GetType().IsSubclassOf(otherClass.GetType()))
                return true;
            var otherClassUnion = otherClass as DynamicClassUnion;
            return otherClassUnion != null
                   && otherClassUnion.Contains(this);
        }

        public virtual bool Equals(IDynamicClass otherClass)
        {
            return ReferenceEquals(this, otherClass);
        }

        public abstract override string ToString();

        public override bool Equals(object otherObject)
        {
            var otherClass = otherObject as IDynamicClass;
            return otherClass != null
                   && Equals(otherClass);
        }

        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);
        }
    }
}