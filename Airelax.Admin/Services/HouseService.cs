using Airelax.Domain;
using Airelax.Domain.Houses;
using Airelax.Domain.RepositoryInterface;
using Lazcat.Infrastructure.DependencyInjection;
using Lazcat.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Threading.Tasks;

namespace Airelax.Admin.Services
{
    [DependencyInjection(typeof(IHouseService))]
    [Authorize]
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }
        public async Task OffShelf(HousesIdInput input)
        {
            var house = await _houseRepository.GetAsync(x => x.Id == input.HousesId);
            CheckHouseId(house);
            house.Status = (Domain.Houses.Defines.HouseStatus)3;

            await _houseRepository.UpdateAsync(house);
            await _houseRepository.SaveChangesAsync();
        }
        /// <summary>
        /// 判斷房源編號存在
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        private static void CheckHouseId(House house)
        {
            if (house == null)
                throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, "doesnt match HouseId");
        }
    }
}
