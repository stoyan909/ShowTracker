using Microsoft.IdentityModel.Tokens;
using ShowTracker.Data;
using ShowTracker.Services.Core.Interfaces;

namespace ShowTracker.Services.Core
{
    public class GeneralServices : IGeneralServices
    {
        public bool IsStringNullOrEmpty(string str)
        {
            bool isStringNull = str.IsNullOrEmpty();

            return isStringNull;
        }

        public Guid GetGuidFromString(string str)
        {
            Guid showId = Guid.Parse(str);

            return showId;
        }

        public bool isGuidValid(string str)
        {
            bool guidIsValid = Guid.TryParse(str, out Guid showId);

            return guidIsValid;
        }

        public int GetIntFromString(string str)
        {
            return int.Parse(str);
        }

        public bool isIntValid(string str)
        {
            return int.TryParse(str, out int result);
        }
    }
}
