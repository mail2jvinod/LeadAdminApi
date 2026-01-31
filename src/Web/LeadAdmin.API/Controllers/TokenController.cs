using LeadAdmin.API.Security;
using LeadAdmin.BusinessAccess.Contracts;
using LeadAdmin.Entities.Core;
using LeadAdmin.Entities.Security;
using LeadAdmin.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadAdmin.API.Controllers
{
    [Route("[controller]")]
    public class TokenController : DigiCollectController
    {
        private ITokenProvider _tokenProvider;
        private ISecurityBusinessAccess securityBusinessAccess;

        public TokenController(UserContext userContext,
            ITokenProvider tokenProvider,
            ISecurityBusinessAccess securityBusinessAccess) // We'll create this later, don't worry.
        {
            this.userContext = userContext;
            _tokenProvider = tokenProvider;
            this.securityBusinessAccess = securityBusinessAccess;
        }

        [Route("[action]")]
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JsonWebToken))]
        public IActionResult Generate([FromBody] LoginViewModel model)
        {
            var token = _tokenProvider.CreateToken(
              model.username, model.password, model.Scope, model.UserId
              , expiryInMinutes: ConfigSettings.Instance.ConfigSettings.TokenExpiryInMinutes
              , deviceId: model.DeviceId, deviceInfromation: model.DeviceInformation
            );

            return Ok(token);
        }

        [Route("[action]")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] LoginViewModel model)
        {
            var response = new ForgotPasswordResponse();
            this.securityBusinessAccess.ForgotPassword(new Entities.Security.UserLogin { LoginScope = model.Scope, UserName = model.username });
            response.ErrorMessage = "Your password has been shared successfully";
            return Ok(response);
        }

        [Route("[action]")]
        [HttpGet]
        [Authorize]
        public IActionResult GetUserProfileByMobileNumber()
        {
            var mobileNumber = this.Claims.First(c => c.Type == "Mobile").Value;
            var scope = this.Claims.First(c => c.Type == "Scope").Value;
            
            var result = this.securityBusinessAccess.GetProfileByMobileNumber(mobileNumber, scope);
            return Ok(result);
        }

        [Route("[action]")]
        [HttpPost]
        [Authorize]
        public JsonWebToken Refresh()
        {
            var _diUserContext = DIContainer.ServiceLocator.Instance.Get<UserContext>();
            _diUserContext.Copy(this.userContext);

            return _tokenProvider.CreateToken(this.LoggedInUserName, "", this.userContext.LoginScope, this.userContext.LoggedInId, 
                ConfigSettings.Instance.ConfigSettings.TokenExpiryInMinutes, "", "");
        }

    }

    public class LoginViewModel
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        //For Mobile
        public string MobileNumber { get; set; }
        public string DeviceInformation { get; set; }
        public string DeviceId { get; set; }
        public string Scope { get; set; }
        public int UserId { get; set; }
    }

    public class ForgotPasswordResponse
    {
        public string ErrorMessage { get; set; }
    }
}