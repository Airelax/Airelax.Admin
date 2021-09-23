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
    public class HouseTypeSpecification : Specification<House>
    {
        private readonly IEnumerable<HouseType> _houseTypes;

        public HouseTypeSpecification(IEnumerable<HouseType> houseTypes)
        {
            _houseTypes = houseTypes;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => _houseTypes.Contains(house.HouseCategory.HouseType.Value);
        }
    }
}
