using LeadAdmin.BusinessAccess.Contracts;
using LeadAdmin.Entities.Security;
using LeadAdmin.Utilities.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace LeadAdmin.API.Security
{
    public class RsaJwtTokenProvider : ITokenProvider
    {
        public RsaJwtTokenProvider()
        {

        }
        public JsonWebToken CreateToken(string userName, string password, string scope = "Student", int userId = 0,
            int expiryInMinutes = 180, string deviceId = "", string deviceInfromation = "")
        {
            var httpContextAccessor = DIContainer.ServiceLocator.Instance.Get<IHttpContextAccessor>();

            var userLoginLog = new UserLogin
            {
                UserName = userName,
                Password = password,
                UserId = userId,

                LocalIPAddress = (httpContextAccessor.HttpContext.Connection.LocalIpAddress != null) ? httpContextAccessor.HttpContext.Connection.LocalIpAddress.ToString() : "Not exists",
                RemoteIPAddress = (httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null) ? httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString() : "Not exists",
                LoginScope = scope
            };

            return GetToken(userLoginLog, expiryInMinutes, userId);
        }

        private JsonWebToken GetToken(UserLogin userLoginLog, int expiryInMinutes, int userId = 0)
        {
            var result = new JsonWebToken();

            var userBusinessAccess = DIContainer.ServiceLocator.Instance.Get<ISecurityBusinessAccess>();

            var validateUserResult = userBusinessAccess.ValidateUser(userLoginLog);

            if (string.IsNullOrEmpty(validateUserResult.ErrorMessage))
            {
                result.LoginEntities = validateUserResult.LoginEntities;

                if (userId > 0 && validateUserResult.LoginEntities.Any(a => a.Id == userId))
                {
                    var selectedUser = validateUserResult.LoginEntities.First(a => a.Id == userId);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var expires_on = DateTime.Now.AddMinutes(expiryInMinutes);

                    var claims = userBusinessAccess.GetClaimsById(userId, userLoginLog.LoginScope);
                    validateUserResult.Claims = claims;

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigSettings.Instance.ConfigSettings.Auth_IssuerSigningKey)); //Secret
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var identity = new ClaimsIdentity(new GenericIdentity(userLoginLog.LoginScope + userId, "jwt"));
                    var filteredClaims = validateUserResult.Claims.Where(c => c.ClaimType != "Status");
                    foreach (var c in filteredClaims)
                    {
                        identity.AddClaim(new System.Security.Claims.Claim(c.ClaimType, c.ClaimValue ?? ""));
                    }
                    identity.AddClaim(new System.Security.Claims.Claim("Scope", userLoginLog.LoginScope));

                    var token = new JwtSecurityToken(ConfigSettings.Instance.ConfigSettings.Auth_ValidIssuer,
                        ConfigSettings.Instance.ConfigSettings.Auth_ValidAudience,
                        identity.Claims,
                        expires: expires_on,
                        signingCredentials: creds);

                    result.access_token = tokenHandler.WriteToken(token);
                    result.expires_in = expiryInMinutes;
                    result.expires_on = expires_on;

                    result.LoginScope = userLoginLog.LoginScope;
                    result.UserId = string.Equals(userLoginLog.LoginScope, "Student", StringComparison.OrdinalIgnoreCase) ? 0 : userId;
                    result.StudentId = string.Equals(userLoginLog.LoginScope, "Student", StringComparison.OrdinalIgnoreCase) ? userId : 0;

                    result.Mobile = selectedUser.MobileNumber;
                    result.Email = selectedUser.Email;
                    result.InstitutionId = selectedUser.InstitutionId;
                    result.RoleName = selectedUser.RoleName;
                }
                else
                {
                    result.IsShowUsers = true;
                }
            }
            else
            {
                result = new JsonWebToken
                {
                    IsSuccess = false,
                    ErrorMessage = validateUserResult.ErrorMessage
                };
            }

            return result;
        }
    }
}
