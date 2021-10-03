using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airelax.Admin.Models
{
    public class SearchHousesResponse
    {
        public int Total { get; set; }
        public List<HouseViewModel> HouseViewModels { get; set; }
    }
}
