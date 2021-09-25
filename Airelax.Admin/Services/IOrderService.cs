using Airelax.Admin.Models;
using System.Threading.Tasks;

namespace Airelax.Domain
{
    public interface IOrderService
    {
        Task DeleteOrder(OrderIdInput input);
    }
}