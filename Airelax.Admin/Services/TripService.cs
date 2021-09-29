using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airelax.Admin.Defines;
using Airelax.Domain.RepositoryInterface;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Airelax.Admin.Services
{
    [DependencyInjection(typeof(ITripService))]
    public class TripService : ITripService
    {
        private readonly IOrderRepository _orderRepository;

        public TripService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<PopularLocation>> GetPopularLocations(DateTime startDate, DateTime endDate, DateType dateType = DateType.Day)
        {
            var houseLocations = await _orderRepository.GetAll()
                .Where(x => x.OrderDetail.StartDate >= startDate && x.OrderDetail.StartDate <= endDate)
                .Select(x => new {Date = x.OrderDetail.StartDate, Location = x.House.HouseLocation}).ToListAsync();

            var popularLocations = houseLocations?.GroupBy(x => (GetDateCondition(x.Date, dateType), x.Location.Town))
                .Select(x => new PopularLocation
                {
                    Date = x.Key.Item1,
                    Town = x.Key.Town,
                    Total = x.Count()
                });

            return popularLocations;
        }


        private static string GetDateCondition(DateTime date, DateType dateType = DateType.Day)
        {
            switch (dateType)
            {
                case DateType.Day:
                    return date.ToString(date.ToString("yyyy-MM-dd"));
                case DateType.Week:
                    return default;
                    break;
                case DateType.Month:
                    return date.ToString(date.ToString("yyyy-MM"));
                default:
                    throw new ArgumentOutOfRangeException(nameof(dateType), dateType, null);
            }
        }
    }
}