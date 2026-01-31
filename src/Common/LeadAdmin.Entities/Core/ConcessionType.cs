namespace LeadAdmin.Entities.Core
{
    public class ConcessionType : Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public string Title { get; set; }
    }
}
