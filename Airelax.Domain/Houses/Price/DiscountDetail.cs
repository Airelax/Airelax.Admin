using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Price
{
    public record DiscountDetail : ValueObject
    {
        public int Days { get; set; }
        public int Discount { get; set; }
    }
}