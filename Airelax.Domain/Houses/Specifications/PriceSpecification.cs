using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Specifications
{
    public class PriceSpecification : Specification<House>
    {
        private readonly decimal _lowPrice;
        private readonly decimal _highPrice;

        public PriceSpecification(decimal lowPrice, decimal highPrice)
        {
            _lowPrice = lowPrice;
            _highPrice = highPrice;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => house.HousePrice.PerNight >= _lowPrice && house.HousePrice.PerNight <= _highPrice;
        }
    }
}
