namespace LeadAdmin.Entities.Security
{
    public class vLoginEntity
    {
        public int InstitutionId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public Guid UniqueId { get; set; }
        public string ParsedPassword { get; set; }
        public bool IsLocked { get; set; }
        public bool IsSuspended { get; set; }

        public string Institution { get; set; }
        public string LogoUrl { get; set; }

        public string AcademicYear { get; set; }
        public string StudentClass { get; set; }
        public string Section { get; set; }
    }
}
