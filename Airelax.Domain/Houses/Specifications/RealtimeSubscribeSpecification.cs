using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Specifications
{
    public class RealtimeSubscribeSpecification : Specification<House>
    {
        private readonly bool _realtimeSubscribe;

        public RealtimeSubscribeSpecification(bool realtimeSubscribe) 
        {
            _realtimeSubscribe = realtimeSubscribe;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            if (_realtimeSubscribe)
                return house => house.Policy.CanRealTime == true;
            else
                return house => house.Policy.CanRealTime == false;
        }
    }
}
