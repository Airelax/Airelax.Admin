using Airelax.Domain.Houses;
using Airelax.Domain.Members;
using Airelax.Domain.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HousesController : Controller
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IHouseRepository _houseRepository;
        public HousesController(IMemberRepository memberRepository,IHouseRepository houseRepository)
        {
            _memberRepository = memberRepository;
            _houseRepository = houseRepository;
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

    }
}