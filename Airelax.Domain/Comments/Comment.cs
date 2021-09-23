using System;
using Airelax.Domain.DomainObject;
using Lazcat.Infrastructure.Common;

namespace Airelax.Domain.Comments
{
    public class Comment : AggregateRoot<string>
    {
        public Comment(string authorId, string houseId, string receiverId, string orderId, string content)
        {
            Id = GuidHelper.CreateId(prefix: "C");
            AuthorId = authorId;
            HouseId = houseId;
            ReceiverId = receiverId;
            OrderId = orderId;
            Content = content;
            CommentTime = DateTime.Now;
            LastModifyTime = DateTime.Now;
        }

        public string AuthorId { get; set; }
        public string HouseId { get; set; }
        public string ReceiverId { get; set; }
        public string OrderId { get; set; }
        public string Content { get; set; }
        public DateTime CommentTime { get; set; }
        public DateTime? LastModifyTime { get; set; }

        public Star Star { get; set; }
    }
}