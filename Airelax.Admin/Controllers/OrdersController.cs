using Airelax.Admin.Models;
using Airelax.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<bool> DeleteOrder(OrderIdInput input)
        {
            await _orderService.DeleteOrder(input);
            return true;
        }
    }
}