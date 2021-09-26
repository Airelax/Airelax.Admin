using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Airelax.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public OrdersController _ordersController;
        public HomeController(OrdersController ordersController)
        {
            _ordersController = ordersController;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            //叫隔壁ordercontroller做事
            //1.取一周總金額
            //2.取訂單銷售額
            //3.取時間內訂單總金額
            //4.吐給view渲染
            return View();
        }

        public async Task<IActionResult> Houses()
        {
            return View();
        }

        public IActionResult Income()
        {
            return View();
        }
    }
}