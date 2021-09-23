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
    public class FacilitySpecification : Specification<House>
    {
        private readonly IEnumerable<Facility> _facilities;

        public FacilitySpecification(IEnumerable<Facility> facilities)
        {
            _facilities = facilities;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => _facilities.Any(x => house.ProvideFacilities.Contains(x));
        }
    }
}
