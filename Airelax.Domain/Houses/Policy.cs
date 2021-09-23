using System;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses.Defines;

namespace Airelax.Domain.Houses
{
    public class Policy : Entity<string>
    {
        public Policy(string id)
        {
            Id = id;
            CanRealTime = false;
            CancelPolicy = CancelPolicy.Elasticity;
            CheckinTime = new DateTime(2000, 1, 1, 15, 0, 0);
        }

        public bool CanRealTime { get; set; }
        public CancelPolicy CancelPolicy { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public decimal? CashPledge { get; set; }
    }
}