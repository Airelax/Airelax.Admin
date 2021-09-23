using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Houses.Price
{
    public record Fee : ValueObject
    {
        public decimal CleanFee { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal TaxFee { get; set; }
    }
}