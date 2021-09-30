
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Domain.Orders;
using System.Collections.Generic;
using System.Linq;

namespace Airelax.Domain.RepositoryInterface
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void SaveChanges();
        Task<Order> GetOrderAsync(Expression<Func<Order, bool>> expression);
        void Update(Order order);
        Task<IEnumerable<Order>> GetTrips(string memberId);
        IQueryable<Order> GetAll();
        void DeleteOrder(Order order);
        IQueryable<Order> GetTotalInCertainRange(DateTime startDate, DateTime endDate);
        
    }
}