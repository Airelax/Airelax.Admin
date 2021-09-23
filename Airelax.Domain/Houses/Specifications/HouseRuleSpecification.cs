using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Specifications
{
    public class HouseRuleSpecification : Specification<House>
    {
        private readonly bool _allowPet;
        private readonly bool _allowSmoke;

        public HouseRuleSpecification(bool allowPet, bool allowSmoke)
        {
            _allowPet = allowPet;
            _allowSmoke = allowSmoke;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => house.HouseRule.AllowPet == _allowPet && house.HouseRule.AllowSmoke == _allowSmoke;
        }
    }
}
