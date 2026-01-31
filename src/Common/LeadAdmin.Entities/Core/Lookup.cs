namespace LeadAdmin.Entities.Core
{
    public class Lookup
    {
        public Lookup()
        {
            this.Items = new List<LookupDetail>();
        }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool IsReadOnly { get; set; }
        
        public List<LookupDetail> Items { get; set; }
    }

    public class LookupDetail
    {
        public string LookupName { get; set; }

        public string Title { get; set; }
        public short SequenceId { get; set; }
    }
}
