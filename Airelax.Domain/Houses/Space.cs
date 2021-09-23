using System;
using System.Collections.Generic;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses.Defines.Spaces;
using Lazcat.Infrastructure.Common;

namespace Airelax.Domain.Houses
{
    public class Space : Entity<string>
    {
        public Space(string houseId)
        {
            Id = GuidHelper.CreateId(prefix: "S");
            HouseId = houseId;
            SpaceType = SpaceType.Bedroom;
            IsShared = false;
            CreateTime = DateTime.Now;
        }

        public string HouseId { get; set; }
        public SpaceType SpaceType { get; set; }
        public bool IsShared { get; set; }
        public DateTime CreateTime { get; set; }

        public ICollection<BedroomDetail> BedroomDetails { get; set; }
    }
}