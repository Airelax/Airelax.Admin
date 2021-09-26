using Airelax.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airelax.Admin
{
    public class OrderService
    {
        public IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //1.轉成時間內訂單總金額
        //2.一周總金額
        //3.月銷售額

    }
}
