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
    public class FreeUnsubscribeSpecification : Specification<House>
    {
        private readonly bool _freeCancel;

        public FreeUnsubscribeSpecification(bool freeCancel)
        {
            _freeCancel = freeCancel;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            if(_freeCancel) 
                return house => (int)house.Policy.CancelPolicy % 2 != 0;
            else
                return house => (int)house.Policy.CancelPolicy % 2 == 0;
        }
    }
}
