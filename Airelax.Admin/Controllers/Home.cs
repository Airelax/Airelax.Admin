﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Airelax.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET

        public HomeController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult Houses()
        {
            return View();
        }

        public IActionResult Income()
        {
            //1.取一周總金額
            //2.取訂單銷售額
            //3.取時間內訂單總金額
            //4.吐給view渲染
            return View();
        }
    }
}