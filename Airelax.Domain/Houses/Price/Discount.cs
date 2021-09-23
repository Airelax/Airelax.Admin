using System.Collections.Generic;

namespace Airelax.Domain.Houses.Price
{
    public class Discount
    {
        public int Week { get; set; }
        public int Month { get; set; }
        public List<DiscountDetail> OtherDiscount { get; set; }
    }
}