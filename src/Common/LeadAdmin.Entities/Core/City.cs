using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadAdmin.Entities.Core
{
    public class City:Entity
    {
        public int Id { get; set; }
        public int StateId { get; set; }

        public string Title { get; set; }
       
    }
}
