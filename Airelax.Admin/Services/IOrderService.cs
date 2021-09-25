using Airelax.Admin.Models;

namespace Airelax.Domain
{
    public interface IOrderService
    {
        void DeleteOrder(OrderIdInput input);
    }
}