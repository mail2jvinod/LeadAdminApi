namespace LeadAdmin.Entities.Core
{
    public class UserContext
    {
        public int InstitutionId { get; set; }
        public short TimezoneOffset { get; set; }
        public short BrowserTimezoneOffset { get; set; }
        public int LoggedInId { get; set; }
        public List<int> RoleIds { get; set; }
        public string IPAddress { get; set; }

        public string SiteUrl { get; set; }
        public string LoginScope { get; set; }
        public string LanguageCode { get; set; }

        public void Copy(UserContext remoteUserContext)
        {
            this.InstitutionId = remoteUserContext.InstitutionId;
            this.TimezoneOffset = remoteUserContext.TimezoneOffset;
            this.BrowserTimezoneOffset = remoteUserContext.BrowserTimezoneOffset;
            this.LoggedInId = remoteUserContext.LoggedInId;
            this.RoleIds = remoteUserContext.RoleIds;
            this.IPAddress = remoteUserContext.IPAddress;

            this.SiteUrl = remoteUserContext.SiteUrl;
            this.LoginScope = remoteUserContext.LoginScope;
            this.LanguageCode = remoteUserContext.LanguageCode;
        }
    }

    public class UserScope
    {
        public const string Student = "Student";
        public const string User = "User";
    }
}
