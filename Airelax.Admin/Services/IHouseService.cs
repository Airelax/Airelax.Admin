using Airelax.Admin.Models;
using Airelax.Domain;
using System.Threading.Tasks;

namespace Airelax.Admin.Services
{
    public interface IHouseService
    {
        Task<HouseViewModel> GetHouseAsync(string id);
        Task OffShelf(HouseIdInput input);
        Task DeleteHouse(HouseIdInput input);
    }
}