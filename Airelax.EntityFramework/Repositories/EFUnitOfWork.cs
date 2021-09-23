using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;
using Airelax.Domain.RepositoryInterface;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.Common.Abstractions;
using Lazcat.Infrastructure.DependencyInjection;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(IUnitOfWork))]
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IActivator _activator;
        private readonly AirelaxContext _context;
        private bool _disposed;
        private ConcurrentDictionary<string, object> _repositories;

        public EFUnitOfWork(AirelaxContext context, IActivator activator)
        {
            _context = context;
            _activator = activator;
            _repositories = new ConcurrentDictionary<string, object>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IGenericRepository<TId, TEntity> GetRepository<TId, TEntity>() where TEntity : Entity<TId>
        {
            _repositories ??= new ConcurrentDictionary<string, object>();
            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type)) return (EFGenericRepository<TId, TEntity>) _repositories[type];

            var repository = _activator.CreateInstanceByContainer(typeof(EFGenericRepository<,>).MakeGenericType(typeof(TId), typeof(TEntity)));
            _repositories.TryAdd(type, repository);

            return (EFGenericRepository<TId, TEntity>) _repositories[type];
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }
    }
}