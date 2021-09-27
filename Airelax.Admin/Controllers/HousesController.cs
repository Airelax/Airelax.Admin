using Airelax.Admin.Services;
using Airelax.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HousesController : Controller
    {
        private readonly IHouseService _houseService;

        public HousesController(IHouseService houseService)
        {
            _houseService = houseService;
        }
        [HttpPost]
        public async Task<bool> OffShelf(HousesIdInput input)
        {
            await _houseService.OffShelf(input);
            return true;
        }
    }
}