namespace LeadAdmin.Entities.Core
{
    public class AcademicYear : Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Title { get; set; }
    }
}
