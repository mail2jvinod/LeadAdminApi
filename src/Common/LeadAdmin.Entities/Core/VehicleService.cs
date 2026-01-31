using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadAdmin.Entities.Core
{
    public class VehicleService :Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public int AcademicYearId { get; set; }
        public int LocationId { get; set; }
        public int FeeOrganizationId { get; set; }
        public string ServiceName { get; set; }
        public string RegNo { get; set; }
        public List<VehicleServiceRoute> Routes { get; set; }
    }

    public class VehicleServiceRoute : Entity
    {
        public int Id { get; set; }
        public int InstitutionId { get; set;}
        public int VehicleServiceId { get; set; }
        public int RouteName { get; set; }
        public int NoOfKms { get; set; }
        public int Amount { get; set; }

    }
}
