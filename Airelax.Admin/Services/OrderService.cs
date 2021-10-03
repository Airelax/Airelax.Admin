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
using Airelax.Admin;

namespace Airelax.Domain
{
    [DependencyInjection(typeof(IOrderService))]
    [Authorize]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHouseRepository _houseRepository;
        private const int pageCount = 20;

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
                CustomerName = order.Member.Name,
                HouseName = order.House.Title,
                OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
                StartDate = order.OrderDetail.StartDate.ToString("yyyy-MM-dd"),
                EndDate = order.OrderDetail.EndDate.ToString("yyyy-MM-dd")
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

        public SearchOrdersResponse GetRangeOrder(IncomeInput orderInput)
        {
            var ordersQueryable = _orderRepository.GetTotalInCertainRange(orderInput.StartDate, orderInput.EndDate);
            var totalCount = ordersQueryable.Count();
            var orders = ordersQueryable.Skip((orderInput.Page - 1) * pageCount).Take(pageCount).ToList();
            var orderViewModels = orders.Select(order =>
                new OrderViewModel()
                {
                    OrderId = order.Id,
                    CustomerName = order.Member.Name,
                    HouseName = order.House.Title,
                    OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
                    StartDate = order.OrderDetail.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = order.OrderDetail.EndDate.ToString("yyyy-MM-dd")
                }
            ).ToList();

            var SearchOrdersResponse = new SearchOrdersResponse()
            {
                Total = totalCount,
                OrderViewModels = orderViewModels
            };
            return SearchOrdersResponse;
        }

        public async Task<IEnumerable<OrderCount>> GetCount()
        {
            var now = DateTime.Now;
            var halfYear = now.AddMonths(-6);
            var orderDates = await _orderRepository.GetAll()
                .Where(x => x.OrderDate > new DateTime(halfYear.Year, halfYear.Month, 1)
                            && x.OrderDate < new DateTime(now.Year, now.Month, 1))
                .Select(x => new { Date = x.OrderDate })
                .ToListAsync();
            return orderDates.GroupBy(x => x.Date.ToString("yyyy-MM")).OrderBy(x => x.Key).Select(x => new OrderCount()
            {
                Date = x.Key,
                Total = x.Count()
            });
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