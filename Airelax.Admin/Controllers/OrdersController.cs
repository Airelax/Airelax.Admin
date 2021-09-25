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
        [HttpDelete]
        public async Task<bool> DeleteOrderId(OrderIdInput input)
        {
            await _orderService.DeleteOrder(input);
            return true;
        }

        [HttpGet]
        public IActionResult AAA()
        {
            return Ok();
        }
    }
}