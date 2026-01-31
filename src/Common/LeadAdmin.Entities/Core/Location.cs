namespace LeadAdmin.Entities.Core
{
    public class Location : Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public string Title { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string PINCode { get; set; }
     
    }
}
