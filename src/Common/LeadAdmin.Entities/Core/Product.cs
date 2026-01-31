using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadAdmin.Entities.Core
{
    public class Product : Entity
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

    }


}