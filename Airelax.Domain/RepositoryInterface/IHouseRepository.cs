using System.Linq;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses;

namespace Airelax.Domain.RepositoryInterface
{
    public interface IHouseRepository : IGenericRepository<string, House>
    {
        IQueryable<House> GetSatisfyFromAsync(Specification<House> specification);
    }
}