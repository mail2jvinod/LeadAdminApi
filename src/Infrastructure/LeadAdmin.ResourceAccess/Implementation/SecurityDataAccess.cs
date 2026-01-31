using LeadAdmin.Entities.Core;
using LeadAdmin.Entities.Security;
using LeadAdmin.ResourceAccess.Contracts;
using System.Data;

namespace LeadAdmin.ResourceAccess.Implementation
{
    public class SecurityDataAccess : DataAccess, ISecurityDataAccess
    {
        public SecurityDataAccess(UserContext userContext)
             : base(userContext)
        {
        }

        public bool ChangePassword(UserLogin userLogin)
        {
            var parmsCollection = new ParmsCollection();
            var result = this.collectDbContext.ExecuteStoredProcedure_ToList<vLoginEntity>("[Core].[changePassword]",
                            parmsCollection
                            .AddParm("@mobileNumber", SqlDbType.VarChar, userLogin.MobileNumber)
                            .AddParm("@loginScope", SqlDbType.VarChar, userLogin.LoginScope)
                            .AddParm("@currentPassword", SqlDbType.NVarChar, userLogin.CurrentPassword)
                            .AddParm("@newPassword", SqlDbType.NVarChar, userLogin.Password)
                            );

            return result.Count > 0;
        }

        public bool ForgotPassword(UserLogin userLogin)
        {
            var parmsCollection = new ParmsCollection();
            var result = this.collectDbContext.ExecuteStoredProcedure_ToList<vLoginEntity>("[Core].[forgotPassword]",
                            parmsCollection
                            .AddParm("@mobileNumber", SqlDbType.VarChar, userLogin.MobileNumber)
                            .AddParm("@loginScope", SqlDbType.VarChar, userLogin.LoginScope)
                            );

            return result.Count > 0;
        }

        public ValidateUserResult ValidateUser(UserLogin userLogin)
        {
            userLogin.MobileNumber = (userLogin.UserName.IndexOf("@") > -1) ? "" : userLogin.UserName;
            userLogin.Email = (userLogin.UserName.IndexOf("@") > -1) == false ? "" : userLogin.UserName;

            var result = new ValidateUserResult();
            var parmsCollection = new ParmsCollection();
            var loginEntities = this.collectDbContext.ExecuteStoredProcedure_ToList<vLoginEntity>("[Core].[validateUser]",
                            parmsCollection
                            .AddParm("@mobileNumber", SqlDbType.VarChar, userLogin.MobileNumber)
                            .AddParm("@email", SqlDbType.VarChar, userLogin.Email)
                            .AddParm("@loginScope", SqlDbType.VarChar, userLogin.LoginScope)
                            );

            if (loginEntities.Count == 0)
            {
                result.ErrorMessage = "Invalid user";
            }
            else if (!loginEntities.Any(a => a.IsLocked == false))
            {
                result.ErrorMessage = "User has locked";
            }
            else if (!loginEntities.Any(a => string.Equals(a.ParsedPassword, userLogin.Password)))
            {
                result.ErrorMessage = "Invalid password";
            }

            foreach (var item in loginEntities)
            {
                item.ParsedPassword = "";
            }

            result.LoginEntities = loginEntities;

            return result;
        }

        public List<Claim> GetClaimsById(int userId, string scope)
        {
            var result = new List<Claim>();
            var parmsCollection = new ParmsCollection();
            result = this.collectDbContext.ExecuteStoredProcedure_ToList<Claim>("[Core].[getClaimsById]",
                            parmsCollection
                            .AddParm("@userId", SqlDbType.Int, userId)
                            .AddParm("@loginScope", SqlDbType.VarChar, scope)
                            );

            return result;
        }

        public List<vLoginEntity> GetProfileByMobileNumber(string mobileNumber, string scope)
        {
            var result = new List<vLoginEntity>();
            var parmsCollection = new ParmsCollection();
            result = this.collectDbContext.ExecuteStoredProcedure_ToList<vLoginEntity>("[Core].[getProfileByMobileNumber]",
                            parmsCollection
                            .AddParm("@mobileNumber", SqlDbType.VarChar, mobileNumber)
                            .AddParm("@loginScope", SqlDbType.VarChar, scope)
                            );

            return result;
        }
    }
}
