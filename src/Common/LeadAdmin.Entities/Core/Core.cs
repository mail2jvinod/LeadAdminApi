using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadAdmin.Entities.Core
{
    public class Executive : Entity
    {
        public int ExecutiveId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string DeviceId { get; set; }
    }
    public class Inward: Entity
    {
        public int InwardId { get; set; }
        public int BranchId { get; set; }
        public DateTime InwardDate { get; set; }
        public string RegistrationNo { get; set; }
        public int Reading { get; set; }
        public int VehicleStatus { get; set; }
        public List<VehicleImages> vehicleImages { get; set; }
    }

    public class Outward : Entity
    {
        public int OutwardId { get; set; }
        public int InwardId { get; set; }
        public DateTime OutwardDate { get; set; }
        public string RegistrationNo { get; set; }
        public int Reading { get; set; }
        public List<VehicleImages> vehicleImages { get; set; }
    }

    public class VehicleImages : Entity
    {
        public int Id { get; set; }
        public int InOut { get; set; }
        public string ImageUrl { get; set; }

    }

    public class VehiclesInPending : Entity
    {
        public int InwardId { get; set; }
        public String RegistrationNo { get; set; }
        public int Reading { get; set; }

        public List<VehicleImages> vehicleImages = new List<VehicleImages>();
    }

}
