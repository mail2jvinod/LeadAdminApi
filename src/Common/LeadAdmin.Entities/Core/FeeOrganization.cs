using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadAdmin.Entities.Core
{ 
       public class FeeOrganization: Entity
        {
            public int Id { get; set; }
            public int InstitutionId { get; set; }

            public string Title { get; set; }
            public string Code { get; set; }
        public int IsDefault { get; set; }
    }
    }
