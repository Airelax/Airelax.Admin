using System;
using System.Collections.Generic;
using Airelax.Admin.Models;
using Airelax.Domain.Orders;
using Airelax.Domain.RepositoryInterface;
using Airelax.Infrastructure.Helpers;
using Lazcat.Infrastructure.DependencyInjection;
using Lazcat.Infrastructure.ExceptionHandlers;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Airelax.Domain
{
    [DependencyInjection(typeof(IOrderService))]
    [Authorize]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHouseRepository _houseRepository;

        public OrderService(IOrderRepository orderRepository, IHouseRepository houseRepository)
        {
            _orderRepository = orderRepository;
            _houseRepository = houseRepository;
        }

        public async Task<OrderViewModel> GetOrderAsync(string id)
        {
            var order = await _orderRepository.GetOrderAsync(x => x.Id == id);
            CheckOrderId(order);
            var orderViewModel = new OrderViewModel()
            {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                HouseId = order.HouseId,
                OrderDate = order.OrderDate.ToString("yyyy-MM-dd")
            };
            return orderViewModel;
        }

        public async Task DeleteOrder(OrderIdInput input)
        {
            var order = await _orderRepository.GetOrderAsync(x => x.Id == input.OrderId);
            CheckOrderId(order);
            var endDate = order.OrderDetail.EndDate;
            var house = await _houseRepository.GetAsync(x => x.Id == order.HouseId);
            var dateRange = DateTimeHelper.GetDateRange(order.OrderDetail.StartDate, order.OrderDetail.EndDate);
            house.ReservationDates = house.ReservationDates.Except(dateRange).ToList();

            await _houseRepository.UpdateAsync(house);
            await _houseRepository.SaveChangesAsync();
            _orderRepository.DeleteOrder(order);
            _orderRepository.SaveChanges();
        }

        public async Task<Dictionary<string, int>> GetCount()
        {
            var now = DateTime.Now;
            var halfYear = now.AddMonths(-6);
            var orderDates = await _orderRepository.GetAll()
                .Where(x => x.OrderDate > new DateTime(halfYear.Year, halfYear.Month, 1)
                            && x.OrderDate < new DateTime(now.Year, now.Month, 1))
                .Select(x => new {Date = x.OrderDate})
                .ToListAsync();
            return orderDates.GroupBy(x => x.Date.ToString("yyyy-MM"))
                .ToDictionary(x => x.Key, x => x.Count());
        }

        /// <summary>
        /// 判斷訂單編號存在
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private static void CheckOrderId(Order order)
        {
            if (order == null)
                throw ExceptionBuilder.Build(HttpStatusCode.BadRequest, "doesnt match OrderId");
        }
    }
}