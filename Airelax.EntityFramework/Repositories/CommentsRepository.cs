using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Domain.Comments;
using Airelax.Domain.Orders;
using Airelax.Domain.RepositoryInterface;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(ICommentsRepository))]
    public class CommentsRepository : ICommentsRepository
    {
        private readonly AirelaxContext _context;

        public CommentsRepository(AirelaxContext context)
        {
            _context = context;
        }

        public IEnumerable<IGrouping<string, HouseCommentObject>> Get(string memberId)
        {
            var comments =
                (from h in _context.Houses 
                    from c in _context.Comments.Where(x => h.Id == x.HouseId).DefaultIfEmpty()
                    from m in _context.Members.Where(x => x.Id == c.ReceiverId).DefaultIfEmpty()
                    from mem in _context.Members.Where(x => c.AuthorId == x.Id).DefaultIfEmpty()
                    from s in _context.Stars.Where(x => x.Id == c.Id).DefaultIfEmpty()
                    where h.OwnerId == memberId
                    select new HouseCommentObject {Comment = c, HouseId = h.Id, HouseName = h.Title, HouseStatus = h.Status, AuthorName = mem.Name, Stars = s}).ToList();
            var commentsGroup = comments.GroupBy(x => x.HouseId);
            return commentsGroup;
        }

        public IQueryable<HouseCommentObject> GetAll()
        {
            var comments =
                (from c in _context.Comments
                    join m in _context.Members on c.ReceiverId equals m.Id
                    join mem in _context.Members on c.AuthorId equals mem.Id
                    join h in _context.Houses on c.HouseId equals h.Id
                    join s in _context.Stars on c.Id equals s.Id
                    select new HouseCommentObject
                    {
                        Comment = c,
                        HouseId = h.Id,
                        HouseName = h.Title,
                        HouseStatus = h.Status,
                        AuthorName = mem.Name,
                        Cover = mem.Cover,
                        Stars = s,
                        AuthorId = c.AuthorId
                    });

            return comments;
        }

        public Order GetOrder(string orderId)
        {
            var order = _context.Orders.Include(x => x.House)
                .FirstOrDefault(o => o.Id == orderId);
            return order;
        }

        public void Add(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}