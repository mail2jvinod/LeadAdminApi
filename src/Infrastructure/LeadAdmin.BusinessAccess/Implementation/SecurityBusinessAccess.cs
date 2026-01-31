using LeadAdmin.BusinessAccess.Contracts;
using LeadAdmin.Entities.Core;
using LeadAdmin.Entities.Security;
using LeadAdmin.ResourceAccess.Contracts;

namespace LeadAdmin.BusinessAccess.Implementation
{
    public class SecurityBusinessAccess : ISecurityBusinessAccess
    {
        private ISecurityDataAccess securityDataAccess;
        public SecurityBusinessAccess(UserContext userContext, ISecurityDataAccess securityDataAccess)
        {
            this.securityDataAccess = securityDataAccess;
        }

        public bool ChangePassword(UserLogin userLogin)
        {
            return this.securityDataAccess.ChangePassword(userLogin);
        }

        public bool ForgotPassword(UserLogin userLogin)
        {
            return this.securityDataAccess.ForgotPassword(userLogin);
        }

        public List<Claim> GetClaimsById(int userId, string scope)
        {
            return this.securityDataAccess.GetClaimsById(userId, scope);
        }

        public List<vLoginEntity> GetProfileByMobileNumber(string mobileNumber, string scope)
        {
            return this.securityDataAccess.GetProfileByMobileNumber(mobileNumber, scope);
        }

        public ValidateUserResult ValidateUser(UserLogin userLogin)
        {
            return this.securityDataAccess.ValidateUser(userLogin);
        }
    }
}
