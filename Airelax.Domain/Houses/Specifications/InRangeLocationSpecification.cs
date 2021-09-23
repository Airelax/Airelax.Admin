using System;
using System.Linq.Expressions;
using Airelax.Domain.DomainObject;
using Airelax.Infrastructure.Map.Responses;

namespace Airelax.Domain.Houses.Specifications
{
    public class InRangeLocationSpecification : Specification<House>
    {
        private readonly Coordinate _northeast;
        private readonly Coordinate _southwest;


        public InRangeLocationSpecification(Coordinate southwest, Coordinate northeast)
        {
            _southwest = southwest;
            _northeast = northeast;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => house.HouseLocation.Longitude >= _southwest.Longitude &&
                            house.HouseLocation.Longitude <= _northeast.Longitude &&
                            house.HouseLocation.Latitude >= _southwest.Latitude &&
                            house.HouseLocation.Latitude <= _northeast.Latitude;
        }
    }
}