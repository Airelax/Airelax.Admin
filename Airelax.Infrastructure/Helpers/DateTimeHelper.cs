using System;
using System.Collections.Generic;

namespace Airelax.Infrastructure.Helpers
{
    public static class DateTimeHelper
    {
        public static IEnumerable<DateTime> GetDateRange(DateTime start, DateTime end)
        {
            var dateRange = new List<DateTime>();
            for (var dt = start; dt < end; dt = dt.AddDays(1)) dateRange.Add(dt);

            return dateRange;
        }
    }
}