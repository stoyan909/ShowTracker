namespace ShowTracker.Services.Core.Interfaces
{
    public interface IGeneralServices
    {
        bool IsStringNullOrEmpty(string str);

        bool isGuidValid(string str);

        Guid GetGuidFromString(string str);
    }
}
