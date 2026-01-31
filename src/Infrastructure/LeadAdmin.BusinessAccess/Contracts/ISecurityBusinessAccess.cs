using LeadAdmin.Entities.Security;

namespace LeadAdmin.BusinessAccess.Contracts
{
    public interface ISecurityBusinessAccess
    {
        bool ChangePassword(UserLogin userLogin);
        bool ForgotPassword(UserLogin userLogin);
        ValidateUserResult ValidateUser(UserLogin userLogin);
        List<Claim> GetClaimsById(int userId, string scope);
        List<vLoginEntity> GetProfileByMobileNumber(string mobileNumber, string scope);
    }
}
