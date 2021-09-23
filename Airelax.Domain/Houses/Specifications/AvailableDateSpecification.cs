using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Specifications
{
    public class AvailableDateSpecification : Specification<House>
    {
        private readonly IEnumerable<DateTime> _requireDates;

        public AvailableDateSpecification(IEnumerable<DateTime> requireDates)
        {
            _requireDates = requireDates;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => !_requireDates.Intersect(house.ReservationDates).Any();
        }
    }
}