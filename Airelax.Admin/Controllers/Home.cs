using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Airelax.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
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