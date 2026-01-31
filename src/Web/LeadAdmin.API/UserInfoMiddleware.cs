using LeadAdmin.Entities.Core;
using System.Security.Claims;

namespace LeadAdmin.API
{
    public class UserInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public UserInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserContext userContext)
        {
            this.FillUserContext(context.Request, context, userContext);

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        protected void FillUserContext(HttpRequest request, HttpContext httpContext, UserContext userContext)
        {
            var institutionId = default(int);
            var timezoneOffset = default(short);
            var hostName = default(string);
            Microsoft.Extensions.Primitives.StringValues institutionIdString;

            try
            {
                if (request.Headers["HostName"].Any())
                {
                    hostName = request.Headers["HostName"].First();
                }
                else
                {
                    hostName = request.Host.Host;
                }

                if (request.Headers["InstitutionId"].Any())
                {
                    int.TryParse(request.Headers["InstitutionId"].First(), out institutionId);
                }
                else if (request.Query.TryGetValue("InstitutionId", out institutionIdString) && !(string.IsNullOrWhiteSpace(institutionIdString.FirstOrDefault())))
                {
                    int.TryParse(institutionIdString.First(), out institutionId);
                }
                else
                {
                    institutionId = this.InstitutionId(httpContext);
                }

                if (request.Headers["LanguageCode"].Any())
                {
                    userContext.LanguageCode = request.Headers["LanguageCode"].First();
                }
                else
                {
                    userContext.LanguageCode = "en-US";
                }

                if (request.Headers["TimezoneOffset"].Any())
                {
                    short.TryParse(request.Headers["TimezoneOffset"].First(), out timezoneOffset);
                }
                else
                {
                    timezoneOffset = 330;
                }

            }
            catch (System.Exception ex)
            {
                System.Diagnostics.EventLog.WriteEntry("Application", ex.ToString(), System.Diagnostics.EventLogEntryType.Error);
                throw;
            }

            userContext.SiteUrl = hostName;
            userContext.InstitutionId = institutionId;
            userContext.TimezoneOffset = timezoneOffset;
            userContext.LoggedInId = default(int);
            userContext.RoleIds = new List<int>();

            var ipAddress = httpContext.Connection.RemoteIpAddress;

            userContext.IPAddress = (ipAddress == null) ? "n/a" : ipAddress.ToString();

            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                userContext.LoggedInId = this.LoggedInId(httpContext);
                userContext.LoginScope = this.LoginScope(httpContext);

                if (httpContext.User.Claims != null)
                {
                    userContext.RoleIds = httpContext.User.Claims.Where(c => c.Type == "RoleId").Select(c => int.Parse(c.Value)).ToList();
                }

            }
        }

        public IEnumerable<Claim> Claims(HttpContext httpContext)
        {
            if (httpContext.User == null) return null;
            else return httpContext.User.Claims;
        }

        public string LoginScope(HttpContext httpContext)
        {
            return this.Claims(httpContext).Any(c => c.Type == "LoginScope") ?
                    this.Claims(httpContext).First(c => c.Type == "LoginScope").Value : "Employee";
        }

        public int LoggedInId(HttpContext httpContext)
        {
            return int.Parse(
                    this.Claims(httpContext).Any(c => c.Type == "LoggedInId") ?
                    this.Claims(httpContext).First(c => c.Type == "LoggedInId").Value : "0"
                    );
        }

        public int InstitutionId(HttpContext httpContext)
        {
            return int.Parse(
                    this.Claims(httpContext).Any(c => c.Type == "InstitutionId") ?
                    this.Claims(httpContext).First(c => c.Type == "InstitutionId").Value : "0"
                    );
        }
    }
}
