using System;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses
{
    public class ReservationRule : Entity<string>
    {
        public ReservationRule(string id)
        {
            Id = id;
            MinNight = 1;
            LastReservationTime = new DateTime(2000, 1, 1, 12, 0, 0, 0);
        }

        public int MinNight { get; set; }
        public int? MaxNight { get; set; }
        public DateTime LastReservationTime { get; set; }
        public int? PrepareTime { get; set; }
        public int? AvailableTime { get; set; }
        public DayOfWeek? RejectDate { get; set; }
    }
}