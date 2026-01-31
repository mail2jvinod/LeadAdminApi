namespace LeadAdmin.Entities
{
    public abstract class Entity
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public abstract class EntityString
    {
        public string CreatedByString { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ModifiedByString { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
