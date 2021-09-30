using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airelax.Admin.Defines;

namespace Airelax.Admin.Services
{
    public interface ITripService
    {
        Task<IEnumerable<PopularLocation>> GetPopularLocations(DateTime startDate, DateTime endDate, DateType dateType = DateType.Day);
    }
}