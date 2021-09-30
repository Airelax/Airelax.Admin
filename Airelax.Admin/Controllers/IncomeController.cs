using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using Airelax.Admin.Defines;

namespace Airelax.Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;
        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }
        [HttpGet]
        [Route("{startDate}/{endDate}")]
        public Dictionary<string, long> GetIncome(DateTime startDate, DateTime endDate, DateType type = DateType.Day)
        {
            var incomeInput = new IncomeInput { StartDate = startDate, EndDate = endDate, DateType = type };
            var incomePerDateDict = _incomeService.GetIncome(incomeInput);
            return incomePerDateDict;
        }
    }
}