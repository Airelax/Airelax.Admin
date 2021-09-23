using System.Collections.Generic;
using System.Linq;
using Airelax.Domain.Comments;
using Airelax.Domain.Orders;

namespace Airelax.Domain.RepositoryInterface
{
    public interface ICommentsRepository
    {
        void Add(Comment comment);
        IEnumerable<IGrouping<string, HouseCommentObject>> Get(string memberId);
        Order GetOrder(string orderId);
        void SaveChanges();
        IQueryable<HouseCommentObject> GetAll();
    }
}