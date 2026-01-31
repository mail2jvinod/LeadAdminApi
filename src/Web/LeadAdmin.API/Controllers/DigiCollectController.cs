using LeadAdmin.Entities.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace LeadAdmin.API.Controllers
{
    public abstract class DigiCollectController : Controller
    {
        protected UserContext userContext = default(UserContext);

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //this.FillUserContext();
        }

        protected void FillUserContext()
        {
            var institutionId = default(short);
            var timezoneOffset = default(short);
            var hostName = default(string);
            Microsoft.Extensions.Primitives.StringValues institutionIdString;

            if (this.Request.Headers["HostName"].Any())
            {
                hostName = this.Request.Headers["HostName"].First();
            }
            else
            {
                hostName = this.HttpContext.Request.Host.Host;
            }

            if (this.Request.Headers["InstitutionId"].Any())
            {
                short.TryParse(this.Request.Headers["InstitutionId"].First(), out institutionId);
            }

            if (institutionId == 0)
            {
                if (this.Request.Query.TryGetValue("institutionId", out institutionIdString) && !(string.IsNullOrWhiteSpace(institutionIdString.FirstOrDefault())))
                {
                    short.TryParse(institutionIdString.First(), out institutionId);
                }
            }

            if (institutionId == 0)
            {
                if (this.Claims != null && this.Claims.Any(c => c.Type == "InstitutionId"))
                {
                    short.TryParse(this.Claims.First(c => c.Type == "InstitutionId").Value, out institutionId);
                }
            }

            if (this.Request.Headers["TimezoneOffset"].Any())
            {
                short.TryParse(this.Request.Headers["TimezoneOffset"].First(), out timezoneOffset);
            }
            else
            {
                timezoneOffset = 330;
            }

            this.userContext.SiteUrl = hostName;
            this.userContext.InstitutionId = institutionId;
            this.userContext.TimezoneOffset = timezoneOffset;
            this.userContext.LoggedInId = default(int);
            this.userContext.RoleIds = new List<int>();

            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                this.userContext.LoggedInId = this.LoggedInId;
                this.userContext.RoleIds = this.RoleIds;
                this.userContext.LoginScope = this.LoginScope;
            }
        }

        public IEnumerable<Claim> Claims
        {
            get
            {
                if (this.User == null) return null;
                else return this.User.Claims;
            }
        }

        public string LoginScope
        {
            get
            {
                return this.Claims.Any(c => c.Type == "LoginScope") ?
                    this.Claims.First(c => c.Type == "LoginScope").Value : "Employee";
            }
        }

        public int LoggedInId
        {
            get
            {
                return int.Parse(
                    this.Claims.Any(c => c.Type == "LoggedInId") ?
                    this.Claims.First(c => c.Type == "LoggedInId").Value : "0"
                    );
            }
        }

        public bool IsStudent { get { return this.Claims.Any(c => c.Type == "StudentId"); } }
        public int StudentId { get { return int.Parse(this.Claims.First(c => c.Type == "StudentId").Value); } }
        public string LoggedInUserName
        {
            get { return this.Claims.Any(c => c.Type == "UserName") ? this.Claims.First(c => c.Type == "UserName").Value : ""; }
        }

        public List<int> RoleIds { get { return this.Claims.Where(c => c.Type == "RoleId").Select(c => int.Parse(c.Value)).ToList(); } }
        public short ClientId
        {
            get
            {
                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    return short.Parse(this.Claims.First(c => c.Type == "ClientId").Value);
                }
                else
                {
                    return 0;
                }
            }
        }

        public string ClientName
        {
            get
            {
                return this.Claims.First(c => c.Type == "ClientName").Value;
            }
        }

        public short InstitutionId
        {
            get
            {
                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    var institutionId = default(short);

                    if (this.Request.Headers["InstitutionId"].Any())
                    {
                        short.TryParse(this.Request.Headers["InstitutionId"].First(), out institutionId);
                    }

                    return institutionId;
                }
                return 0;
            }
        }

        public short LocationId
        {
            get
            {
                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    var locationId = default(short);

                    if (this.Request.Headers["LocationId"].Any())
                    {
                        short.TryParse(this.Request.Headers["LocationId"].First(), out locationId);
                    }

                    return locationId;
                }
                return 0;
            }
        }
    }
}
