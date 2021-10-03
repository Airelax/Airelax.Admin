using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airelax.Admin.Models
{
    public class SearchOrdersResponse
    {
        public int Total { get; set; }
        public List<OrderViewModel> OrderViewModels { get; set; }
    }
}