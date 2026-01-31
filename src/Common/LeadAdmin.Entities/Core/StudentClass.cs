namespace LeadAdmin.Entities.Core
{
    public class StudentClass : Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public string Title { get; set; }
        public int SortId { get; set; }
    }
}
