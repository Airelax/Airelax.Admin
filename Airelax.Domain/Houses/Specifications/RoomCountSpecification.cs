using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses.Defines.Spaces;

namespace Airelax.Domain.Houses.Specifications
{
    public class RoomCountSpecification : Specification<House>
    {
        private readonly int _bedCount;
        private readonly int _bedroomCount;
        private readonly int _bathroomCount;

        public RoomCountSpecification(int bedCount, int bedroomCount, int bathroomCount)
        {
            _bedCount = bedCount;
            _bedroomCount = bedroomCount;
            _bathroomCount = bathroomCount;
        }

        public override Expression<Func<House, bool>> ToExpression()
        {
            //if(house.Spaces != null)
            return house => house.Spaces.Count(x => x.SpaceType == SpaceType.Bath) >= _bathroomCount &&
                            house.Spaces.Count(x => x.SpaceType == SpaceType.Bedroom) >= _bedroomCount &&
                            house.Spaces.Where(s => s.SpaceType == SpaceType.Bedroom).SelectMany(s => s.BedroomDetails).Sum(b => b.BedCount) >= _bedCount;
        }
    }
}
