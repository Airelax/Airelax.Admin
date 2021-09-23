using System;
using Airelax.Domain.Comments;
using Airelax.Domain.Houses.Defines;

namespace Airelax.Controllers
{
    public class MemberInfoSearchObject
    {
        public string About { get; set; }
        public string Location { get; set; }
        public string WorkTime { get; set; }
        public string MemberName { get; set; }
        public DateTime RegisterTime { get; set; }
        public string Email { get; set; }
        public string HouseTitle { get; set; }
        public HouseType? HouseType { get; set; }
        public RoomCategory? RoomType { get; set; }
        public Star StarTotal { get; set; }
        public string CommentHouseId { get; set; }
        public string HouseId { get; set; }
        public string Cover { get; set; }
        public string HousePhoto { get; set; }
    }
}