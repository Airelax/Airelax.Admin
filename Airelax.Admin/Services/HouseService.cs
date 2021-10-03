using Airelax.Admin.Models;
using Airelax.Domain;
using Airelax.Domain.Houses;
using Airelax.Domain.RepositoryInterface;
using Lazcat.Infrastructure.DependencyInjection;
using Lazcat.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Airelax.Admin.Services
{
    [DependencyInjection(typeof(IHouseService))]
    [Authorize]
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;
        private const int pageCount = 10;

        public HouseService(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }
        public async Task<HouseViewModel> GetHouseAsync(string id)
        {
            var house = await FindHouseId(id);
            var houseViewModel = new HouseViewModel()
            {
                Photo = house.Photos.FirstOrDefault()?.Image,
                Title = house.Title,
                HouseId = house.Id,
                OwnerId = house.OwnerId,
                CreateTime = house.CreateTime.ToString("yyyy-MM-dd"),
                City = house.HouseLocation.City,
                Town = house.HouseLocation.Town,
                Status = (int)house.Status
            };
            return houseViewModel;
        }

        public SearchHousesResponse GetRangeHouse(IncomeInput houseInput)
        {
            var housesQueryable = _houseRepository.GetTotalInCertainRange(houseInput.StartDate, houseInput.EndDate);
            var totalCount = housesQueryable.Count();
            var houses = housesQueryable.Skip((houseInput.Page - 1) * pageCount).Take(pageCount).ToList();
            var houseViewModels = houses.Select(house =>
                new HouseViewModel()
                {
                    Photo = house.Photos.FirstOrDefault()?.Image,
                    Title = house.Title,
                    HouseId = house.Id,
                    OwnerId = house.OwnerId,
                    CreateTime = house.CreateTime.ToString("yyyy-MM-dd"),
                    City = house.HouseLocation.City,
                    Town = house.HouseLocation.Town,
                    Status = (int)house.Status
                }
            ).ToList();

            var SearchHousesResponse = new SearchHousesResponse()
            {
                Total = totalCount,
                HouseViewModels = houseViewModels
            };
            return SearchHousesResponse;
        }

        public async Task OffShelf(HouseIdInput input)
        {
            var house = await FindHouseId(input.HouseId);
            if ((int)house.Status == 1)
                house.Status = (Domain.Houses.Defines.HouseStatus)3;
            else
                house.Status = (Domain.Houses.Defines.HouseStatus)1;

            await _houseRepository.UpdateAsync(house);
            await _houseRepository.SaveChangesAsync();
        }

        public async Task DeleteHouse(HouseIdInput input)
        {
            var house = await FindHouseId(input.HouseId);
            house.IsDeleted = true;

            await _houseRepository.UpdateAsync(house);
            await _houseRepository.SaveChangesAsync();
        }

        private async Task<House> FindHouseId(string id)
        {
            var house = await _houseRepository.GetAsync(x => x.Id == id);
            CheckHouseId(house);
            return house;
        }

        /// <summary>
        /// 判斷房源編號存在
        /// </summary>
        /// <param name="house"></param>
        /// <returns></returns>
        private static void CheckHouseId(House house)
        {
            if (house == null || house.IsDeleted == true)
                throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, "doesnt match HouseId");
        }
    }
}
