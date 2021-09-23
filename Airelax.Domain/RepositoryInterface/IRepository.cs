using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.RepositoryInterface
{
    public interface IRepository
    {
        IQueryable<TEntity> GetAll<TId, TEntity>() where TEntity : Entity<TId>;
        Task<TEntity> GetAsync<TId, TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : Entity<TId>;
        Task CreateAsync<TId, TEntity>(TEntity item) where TEntity : Entity<TId>;
        Task UpdateAsync<TId, TEntity>(TEntity item) where TEntity : Entity<TId>;
        Task DeleteAsync<TId, TEntity>(TEntity item) where TEntity : Entity<TId>;
        Task SaveChangesAsync();
    }
}