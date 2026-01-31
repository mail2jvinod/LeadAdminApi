using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadAdmin.Entities.Student
{
    public class Distibutors:Entity
    {
        public int Id { get; set; }
        public string DistributorName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public int Status { get; set; }

    }
}
