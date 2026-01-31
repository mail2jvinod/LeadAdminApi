using LeadAdmin.Entities.Security;

namespace LeadAdmin.API.Security
{
    public interface ITokenProvider
    {
        JsonWebToken CreateToken(string userName, string password, string scope = "Student", int userId = 0,
            int expiryInMinutes = 180, string deviceId = "", string deviceInfromation = "");
    }
}
