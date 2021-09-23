using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;
using Airelax.Domain.RepositoryInterface;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(IGenericRepository<,>))]
    public class EFGenericRepository<TId, TEntity> : IGenericRepository<TId, TEntity> where TEntity : Entity<TId>
    {
        private readonly AirelaxContext _context;

        public EFGenericRepository(AirelaxContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(exp);
        }

        public async Task CreateAsync(TEntity item)
        {
            await _context.Set<TEntity>().AddAsync(item);
        }

        public async Task UpdateAsync(TEntity item)
        {
            _context.Set<TEntity>().Update(item);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TEntity item)
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