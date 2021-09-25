using Airelax.Admin.Models;
using Airelax.Domain.Orders;
using Airelax.Domain.RepositoryInterface;
using Lazcat.Infrastructure.DependencyInjection;
using Lazcat.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Airelax.Domain
{
    [DependencyInjection(typeof(IOrderService))]
    [Authorize]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async void DeleteOrder(OrderIdInput input)
        {
            var order = await _orderRepository.GetOrderAsync(x => x.Id == input.OrderId);
            CheckOrderId(order);
            _orderRepository.Delete(order);
            _orderRepository.SaveChanges();
        }

        /// <summary>
        /// 判斷訂單編號存在
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool CheckOrderId(Order order)
        {
            if (order == null)
                throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, "doesnt match OrderId");
            return true;
        }
    }
}
