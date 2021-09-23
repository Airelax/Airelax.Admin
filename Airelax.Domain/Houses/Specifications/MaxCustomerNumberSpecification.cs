using System;
using System.Linq.Expressions;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Specifications
{
    public class MaxCustomerNumberSpecification : Specification<House>
    {
        private readonly int _customerNumber;

        public MaxCustomerNumberSpecification(int customerNumber)
        {
            _customerNumber = customerNumber;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            return house => house.CustomerNumber >= _customerNumber;
        }
    }
}