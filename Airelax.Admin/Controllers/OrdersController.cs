using Airelax.Admin.Models;
using Airelax.Domain;
using Microsoft.AspNetCore.Mvc;

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
        public bool DeleteOrderId(OrderIdInput input)
        {
            _orderService.DeleteOrder(input);
            return true;
        }

        [HttpGet]
        public IActionResult AAA()
        {
            return Ok();
        }
    }
}