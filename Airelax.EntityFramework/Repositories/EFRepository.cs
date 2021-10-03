using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses;
using Airelax.Domain.RepositoryInterface;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(IRepository))]
    public class EFRepository : IRepository
    {
        private readonly AirelaxContext _context;

        public EFRepository(AirelaxContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll<TId, TEntity>() where TEntity : Entity<TId>
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync<TId, TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : Entity<TId>
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(exp);
        }

        public async Task CreateAsync<TId, TEntity>(TEntity item) where TEntity : Entity<TId>
        {
            await _context.Set<TEntity>().AddAsync(item);
        }

        public async Task UpdateAsync<TId, TEntity>(TEntity item) where TEntity : Entity<TId>
        {
            _context.Set<TEntity>().Update(item);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync<TId, TEntity>(TEntity item) where TEntity : Entity<TId>
        {
            _context.Set<TEntity>().Remove(item);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}