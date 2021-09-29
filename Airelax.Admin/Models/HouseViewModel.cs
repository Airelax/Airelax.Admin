using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airelax.Admin.Models
{
    public class HouseViewModel
    {
        public string Photo { get; set; }
        public string Title { get; set; }
        public string HouseId { get; set; }
        public string OwnerId { get; set; }
        public string CreateTime { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public int Status { get; set; }
    }
}
