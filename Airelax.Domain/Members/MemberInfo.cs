using Airelax.Domain.DomainObject;

namespace Airelax.Domain.Members
{
    public class MemberInfo : Entity<string>
    {
        public MemberInfo(string id)
        {
            Id = id;
        }

        public string About { get; set; }
        public string Location { get; set; }
        public string WorkTime { get; set; }
    }
}