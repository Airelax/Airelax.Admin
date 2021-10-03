using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Airelax.Admin.Models;
using Airelax.Domain;
using System.Threading.Tasks;
using System;
using Airelax.Admin.Defines;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<OrderViewModel> GetOrder(string id)
        {
            return await _orderService.GetOrderAsync(id);
        }

        [HttpGet]
        [Route("{startDate}/{endDate}/{page}")]
        public SearchOrdersResponse GetRangeOrder(DateTime startDate, DateTime endDate, int page)
        {
            var orderInput = new IncomeInput { StartDate = startDate, EndDate = endDate, Page = page };
            var orderLists = _orderService.GetRangeOrder(orderInput);
            return orderLists;
        }

        [HttpPost]
        public async Task<bool> DeleteOrder(OrderIdInput input)
        {
            await _orderService.DeleteOrder(input);
            return true;
        }

        [HttpGet]
        [Route("count")]
        public async Task<IEnumerable<OrderCount>> Count()
        {
            return await _orderService.GetCount();
        }
    }
}