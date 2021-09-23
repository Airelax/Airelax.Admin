using Airelax.Domain.DomainObject;
using Airelax.Domain.Members.Defines;

namespace Airelax.Domain.Members
{
    public class MemberLoginInfo : Entity<string>
    {
        public MemberLoginInfo(string id)
        {
            Id = id;
        }

        public string Account { get; set; }
        public string Password { get; set; }
        public LoginType LoginType { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string ThirdPartyToken { get; set; }
        public string ThirdPartyRefreshToken { get; set; }
    }
}