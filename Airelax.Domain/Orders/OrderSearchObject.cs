using System.Collections.Generic;
using Airelax.Domain.Comments;
using Airelax.Domain.Houses.Price;

namespace Airelax
{
    public class OrderSearchObject
    {
        public HousePrice HousePrice { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }
}