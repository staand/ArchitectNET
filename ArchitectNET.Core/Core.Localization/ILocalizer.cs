namespace ArchitectNET.Core.Localization
{
    public interface ILocalizer
    {
        IResource TryGetResource(ID resourceID, ILocale targetLocale = null);
    }
}