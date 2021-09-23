using System.Collections.Generic;
using System.Linq;
using Airelax.Domain.Houses;
using Airelax.Domain.RepositoryInterface;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(IManageHouseRepository))]
    public class ManageHouseRepository : IManageHouseRepository
    {
        private readonly AirelaxContext _context;

        public ManageHouseRepository(AirelaxContext context)
        {
            _context = context;
        }

        public List<SpaceBed> GetSpace(string id)
        {
            var spaceBeds = from h in _context.Houses
                from s in _context.Spaces.Where(x => x.HouseId == h.Id)
                from b in _context.BedroomDetails.Where(x => x.SpaceId == s.Id).DefaultIfEmpty()
                where h.Id == id
                orderby s.CreateTime
                select new SpaceBed
                {
                    Space = s,
                    BedroomDetail = b
                };

            var space = spaceBeds?.ToList();
            return space;
        }


        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public void Delete(House house)
        {
            house.IsDeleted = true;
            Update(house);
        }

        public void Update(House house)
        {
            _context.Houses.Update(house);
        }

        public void CreateBedroom(BedroomDetail bedroom)
        {
            _context.BedroomDetails.Add(bedroom);
        }

        public DbSet<BedroomDetail> GetBedroom()
        {
            return _context.BedroomDetails;
        }
    }
}