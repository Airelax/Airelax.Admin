using Airelax.Domain.Houses.Defines;

namespace Airelax.Domain.Comments
{
    public class HouseCommentObject
    {
        public Comment Comment { get; set; }
        public string HouseId { get; set; }
        public string HouseName { get; set; }
        public HouseStatus HouseStatus { get; set; }
        public string AuthorName { get; set; }
        public Star Stars { get; set; }
        public string AuthorId { get; set; }
        public string Cover { get; set; }
    }
}