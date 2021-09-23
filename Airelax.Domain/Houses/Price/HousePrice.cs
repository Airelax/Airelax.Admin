using System;
using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Price
{
    public class HousePrice : Entity<string>
    {
        public HousePrice(string id)
        {
            Id = id;
        }

        public decimal PerNight { get; set; }
        public decimal? PerWeekNight { get; set; }

        public Fee Fee { get; set; }
        public Discount Discount { get; set; }

        public decimal CalculateTotalPrice(DateTime checkin, DateTime checkout)
        {
            var diffTime = checkout - checkin;
            var diffDay = diffTime.Days;

            var sweetPrice = PerWeekNight ?? PerNight;
            var nightsPrice = sweetPrice * diffDay;

            if (Fee != null) nightsPrice += Fee.CleanFee + Fee.ServiceFee + Fee.TaxFee;

            return nightsPrice;
            //return nightsPrice + Fee?.CleanFee ?? 0 + Fee?.ServiceFee ?? 0 + Fee?.TaxFee ?? 0;
        }
    }
}