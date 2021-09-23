using Airelax.Domain.DomainObject;
using Airelax.Domain.Orders.Define;

namespace Airelax.Domain.Orders
{
    public class Payment : Entity<string>
    {
        public Payment(string id)
        {
            Id = id;
            PayState = PayState.Unpaid;
            PayType = PayType.NotPay;
        }

        public PayState PayState { get; set; }
        public PayType PayType { get; set; }
        public decimal? Refund { get; set; }
    }
}