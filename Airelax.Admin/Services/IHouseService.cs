using Airelax.Domain;
using System.Threading.Tasks;

namespace Airelax.Admin.Services
{
    public interface IHouseService
    {
        Task OffShelf(HousesIdInput input);
    }
}