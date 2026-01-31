namespace LeadAdmin.Entities.Core
{
    public class FeeSetup : Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public int AcademicYearId { get; set; }
        public int LocationId { get; set; }
        public int ClassId { get; set; }
        public int FeeComponentId { get; set; }
        public int FeeOrganizationId { get; set; }

        public int GrossPupilFee { get; set; }

        public decimal SGSTPer { get; set; }
        public decimal CGSTPer { get; set; }

        public decimal NetPupilFee { get; set; }

        public DateTime DueDate { get; set; }

    }
}
