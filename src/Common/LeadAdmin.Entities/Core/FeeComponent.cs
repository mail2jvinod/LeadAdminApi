namespace LeadAdmin.Entities.Core
{
    public class FeeComponent : Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public string Title { get; set; }
        public string Code { get; set; }
        public string HSNCode { get; set; }
        public int SequenceId { get; set; }

        public int FeeComponentTypeId { get; set; }

    }
}
