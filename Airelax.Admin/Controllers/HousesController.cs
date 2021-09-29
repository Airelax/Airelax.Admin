using Airelax.Admin.Services;
using Airelax.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Airelax.Domain.RepositoryInterface;
using System.Linq;
using Airelax.Admin.Models;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HousesController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly IHouseService _houseService;
        public HousesController(IMemberRepository memberRepository, IHouseRepository houseRepository, IHouseService houseService)
        {
            _memberRepository = memberRepository;
            _houseRepository = houseRepository;
            _houseService = houseService;
        }

        [HttpGet]
        [Route("GetAllMembers")]//https://localhost:44305/api/Houses/GetAllMembers
        public int GetMembers()
        {
            return _memberRepository.GetAll().Count();
        }

        [HttpGet]
        [Route("GetAllHouses")]//https://localhost:44305/api/Houses/GetAllHouses
        public int GetHouses()
        {
            return _houseRepository.GetAll().Count();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HouseViewModel> GetHouse(string id)
        {
            return await _houseService.GetHouseAsync(id);
        }

        [HttpPost]
        public async Task<bool> OffShelf(HouseIdInput input)
        {
            await _houseService.OffShelf(input);
            return true;
        }

        [HttpDelete]
        public async Task<bool> DeleteHouse(HouseIdInput input)
        {
            await _houseService.DeleteHouse(input);
            return true;
        }
    }
}