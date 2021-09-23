using System;
using System.Collections.Generic;
using Airelax.Domain.Comments;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses.Defines;
using Airelax.Domain.Houses.Price;
using Airelax.Domain.Members;
using Lazcat.Infrastructure.Common;

namespace Airelax.Domain.Houses
{
    public class House : AggregateRoot<string>
    {
        public House(string ownerId)
        {
            OwnerId = ownerId;
            Id = GuidHelper.CreateId(prefix: "H");
            Title = "未命名房源";
            Status = HouseStatus.CancelPublish;
            CreateState = CreateState.Building;
            CustomerNumber = 1;
            CreateTime = DateTime.Now;
            IsDeleted = false;
            ProvideFacilities = new List<Facility>();
            NotProvideFacilities = new List<Facility>();
        }

        public string Title { get; set; }
        public HouseStatus Status { get; set; }
        public CreateState CreateState { get; set; }
        public int CustomerNumber { get; set; }
        public string OwnerId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModifyTime { get; set; }
        public List<Facility> ProvideFacilities { get; set; }
        public List<Facility> NotProvideFacilities { get; set; }
        public bool IsDeleted { get; set; }
        public List<DateTime> ReservationDates { get; set; }

        #region Navigation

        public Member Member { get; set; }
        public HouseCategory HouseCategory { get; set; }
        public ICollection<Space> Spaces { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ReservationRule ReservationRule { get; set; }
        public Policy Policy { get; set; }
        public HouseRule HouseRule { get; set; }
        public HouseDescription HouseDescription { get; set; }
        public HouseLocation HouseLocation { get; set; }
        public HousePrice HousePrice { get; set; }
        public ICollection<Comment> Comments { get; set; }

        #endregion
    }
}