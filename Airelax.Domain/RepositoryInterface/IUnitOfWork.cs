using System.Threading.Tasks;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.RepositoryInterface
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        IGenericRepository<TId, TEntity> GetRepository<TId, TEntity>() where TEntity : Entity<TId>;
    }
}