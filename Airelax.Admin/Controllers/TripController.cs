using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airelax.Admin.Defines;
using Airelax.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : Controller
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        [Route("popular/{startDate:datetime}/{endDate:datetime}")]
        public async Task<IEnumerable<PopularLocation>> GetPopularLocation(DateTime startDate, DateTime endDate, DateType dateType = DateType.Day)
        {
            return await _tripService.GetPopularLocations(startDate, endDate, dateType);
        }
    }
}