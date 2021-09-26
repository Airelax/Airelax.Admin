using Microsoft.AspNetCore.Mvc;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : Controller
    {
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