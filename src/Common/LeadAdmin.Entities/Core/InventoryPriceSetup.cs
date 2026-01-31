using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadAdmin.Entities.Core
{
    public class InventoryPriceSetup:Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }

        public int AcademicYearId { get; set; }
        public int LocationId { get; set; }
        public int ClassId { get; set; }

        public int ProductId { get; set; }
        public int FeeOrganizationId { get; set; }
        public int GrossPrice { get; set; }

    }
}
