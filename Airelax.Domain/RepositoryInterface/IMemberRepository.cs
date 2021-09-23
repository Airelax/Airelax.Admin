using System.Collections.Generic;
using System.Threading.Tasks;
using Airelax.Controllers;
using Airelax.Domain.Members;

namespace Airelax.Domain.RepositoryInterface
{
    public interface IMemberRepository : IGenericRepository<string, Member>
    {
        Task<Member> GetMemberByAccountAsync(string account);
        List<MemberInfoSearchObject> GetMemberInfoSearchObject(string memberId);
    }
}