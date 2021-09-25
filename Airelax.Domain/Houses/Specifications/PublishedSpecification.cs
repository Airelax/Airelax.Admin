using System;
using System.Linq.Expressions;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses.Defines;

namespace Airelax.Domain.Houses.Specifications
{
    public class PublishedSpecification: Specification<House>
    {
        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => house.Status == HouseStatus.Publish;
        }
    }
}