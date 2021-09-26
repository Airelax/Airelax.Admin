using Microsoft.AspNetCore.Mvc;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        //叫service做事囉
        public OrderService _orderService;
        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult RevenueBeforeNow()
        {
            return View();
        }
        public IActionResult RevenueDuringWeek()
        {
            return View();
        }
        public IActionResult SalesDuringMonth()
        {
            return View();
        }
    }
}