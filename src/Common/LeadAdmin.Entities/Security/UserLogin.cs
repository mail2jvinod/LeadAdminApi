namespace LeadAdmin.Entities.Security
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string DeviceId { get; set; }
        public string LocalIPAddress { get; set; }
        public string RemoteIPAddress { get; set; }
        public string LoginScope { get; set; }
        public string DeviceInformation { get; set; }

        public int InstitutionId { get; set; }
        public int UserId { get; set; }

        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }

    }
}
