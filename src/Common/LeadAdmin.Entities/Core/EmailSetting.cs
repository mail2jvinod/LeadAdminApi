using System.ComponentModel.DataAnnotations;

namespace LeadAdmin.Entities.Core
{
    public class EmailSetting : Entity
    {
        [Key]
        public int Id { get; set; }
        public string SMTPAddress { get; set; }
        public string FromAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PortAddreess { get; set; }

        public bool IsActive { get; set; }
    }
}
