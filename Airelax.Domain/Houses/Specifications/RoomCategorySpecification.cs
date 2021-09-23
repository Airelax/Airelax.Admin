using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses.Defines;

namespace Airelax.Domain.Houses.Specifications
{
    public class RoomCategorySpecification : Specification<House>
    {
        private readonly IEnumerable<RoomCategory> _roomCategory;

        public RoomCategorySpecification(IEnumerable<RoomCategory> roomCategory)
        {
            _roomCategory = roomCategory;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => _roomCategory.Contains(house.HouseCategory.RoomCategory.Value);
        }
    }
}
