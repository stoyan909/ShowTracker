namespace ShowTracker.Services.Core.Interfaces
{
    public interface IGeneralServices
    {
        bool IsStringNullOrEmpty(string str);

        bool isGuidValid(string str);

        bool isIntValid(string str);

        Guid GetGuidFromString(string str);

        int GetIntFromString(string str);
    }
}
