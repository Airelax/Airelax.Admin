using System;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses;
using Airelax.Domain.Members;
using Airelax.Domain.Orders.Define;
using Lazcat.Infrastructure.Common;

namespace Airelax.Domain.Orders
{
    public class Order : AggregateRoot<string>
    {
        public Order(string customerId, string houseId)
        {
            Id = GuidHelper.CreateId(prefix: "O");
            CustomerId = customerId;
            HouseId = houseId;
            OrderDate = DateTime.Now;
            State = OrderState.Established;
            OrderDetail = new OrderDetail(Id);
            Payment = new Payment(Id);
            OrderPriceDetail = new OrderPriceDetail(Id);
            LastModifyTime = DateTime.Now;
        }

        public string CustomerId { get; set; }
        public string HouseId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime LastModifyTime { get; set; }
        public OrderState State { get; set; }

        public House House { get; set; }
        public Member Member { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public Payment Payment { get; set; }
        public OrderPriceDetail OrderPriceDetail { get; set; }
    }
}