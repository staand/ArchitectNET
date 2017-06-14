namespace ArchitectNET.Core.Support
{
    public abstract class ClassBase<TClass> : IClasslikeMetadata<TClass>
        where TClass : IClasslikeMetadata<TClass>
    {
        public virtual bool IsSubclassOf(TClass otherClass)
        {
            if (otherClass == null)
                return false;
            if (Equals(otherClass) || GetType().IsSubclassOf(otherClass.GetType()))
                return true;
            return false;
        }

        public virtual bool Equals(TClass otherClass)
        {
            return ReferenceEquals(this, otherClass);
        }
    }
}