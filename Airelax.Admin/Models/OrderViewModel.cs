using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airelax.Admin.Models
{
    public class OrderViewModel
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string HouseName { get; set; }
        public string OrderDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
