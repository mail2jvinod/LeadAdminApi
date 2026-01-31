namespace LeadAdmin.Entities.Core
{
    public class Institution : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }

        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public string CommAddress { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string PINCode { get; set; }
        public string LogoUrl { get; set; }

        public string InStatus { get; set; }
    }
}
