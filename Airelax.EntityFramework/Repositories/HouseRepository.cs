using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses;
using Airelax.Domain.RepositoryInterface;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(IHouseRepository))]
    public class HouseRepository : IHouseRepository
    {
        private readonly IRepository _repository;

        public HouseRepository(IRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<House> GetAll()
        {
            return GetHouseIncludeAll().Where(x => x.IsDeleted == false);
        }

        public async Task<House> GetAsync(Expression<Func<House, bool>> exp)
        {
            return await GetHouseIncludeAll()
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(exp);
        }

        public async Task CreateAsync(House item)
        {
            await _repository.CreateAsync<string, House>(item);
        }

        public async Task UpdateAsync(House item)
        {
            await _repository.UpdateAsync<string, House>(item);
        }

        public async Task DeleteAsync(House item)
        {
            item.IsDeleted = true;
            await UpdateAsync(item);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public IQueryable<House> GetSatisfyFromAsync(Specification<House> specification)
        {
            return GetHouseIncludeAll().Where(specification.ToExpression()).Where(x => !x.IsDeleted);
        }

        private IIncludableQueryable<House, ReservationRule> GetHouseIncludeAll()
        {
            return _repository.GetAll<string, House>()
                .Include(x => x.Comments)
                .ThenInclude(x => x.Star)
                .Include(x => x.Photos)
                .Include(x => x.Policy)
                .Include(x => x.Spaces)
                .ThenInclude(x => x.BedroomDetails)
                .Include(x => x.HouseCategory)
                .Include(x => x.HouseDescription)
                .Include(x => x.HousePrice)
                .Include(x => x.HouseLocation)
                .Include(x => x.HouseRule)
                .Include(x => x.ReservationRule);
        }
    }
}