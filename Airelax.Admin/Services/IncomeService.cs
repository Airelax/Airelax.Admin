using Airelax.Domain.RepositoryInterface;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airelax.Admin
{
    [DependencyInjection(typeof(IIncomeService))]
    public class IncomeService : IIncomeService
    {
        private readonly IOrderRepository _orderRepository;
        public IncomeService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //拿到order的total
        //1.轉換資料為指定區間內銷售額(每日一筆)
        ////2.轉換資料為指定區間內銷售額(每月一筆) (前)
        ////3.轉換資料為指定區間內銷售額(每周一筆) (前)
        ////4.轉換資料今天往回推一周之總銷售額 (前)
        ////5.回傳至controller
        public Dictionary<string, decimal> GetIncome(IncomeInput incomeInput)
        {
            var orderTotalInCertainRange = _orderRepository
                .GetTotalInCertainRange(incomeInput.StartDate, incomeInput.EndDate);
            var group = (from x in orderTotalInCertainRange
                         group new { OrderDate = x.OrderDate, Price = x.OrderPriceDetail } by x.OrderDate into g
                         select new { date = g.Key, sum = g.Sum(y => y.Price.Total) }).ToList();

            var dict = group.OrderBy(x => x.date).GroupBy(x => x.date.ToString("yyyy-MM-dd")).ToDictionary(x => x.Key, x => x.Sum(y => y.sum));
            return dict;
        }
    }
}
