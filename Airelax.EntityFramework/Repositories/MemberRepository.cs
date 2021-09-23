using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Airelax.Controllers;
using Airelax.Domain.Members;
using Airelax.Domain.Members.Defines;
using Airelax.Domain.RepositoryInterface;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(IMemberRepository))]
    public class MemberRepository : IMemberRepository
    {
        private readonly IRepository _repository;
        private readonly AirelaxContext _context;

        public MemberRepository(IRepository repository, AirelaxContext context)
        {
            _repository = repository;
            _context = context;
        }

        public IQueryable<Member> GetAll()
        {
            return _repository.GetAll<string, Member>().Where(x => !x.IsDeleted);
        }

        public async Task<Member> GetAsync(Expression<Func<Member, bool>> exp)
        {
            var memberIncludeAll = await (from m in _repository.GetAll<string, Member>().Where(exp)
                from memberInfo in _repository.GetAll<string, MemberInfo>().Where(x => x.Id == m.Id).DefaultIfEmpty()
                from memberLogInfo in _repository.GetAll<string, MemberLoginInfo>().Where(x => x.Id == m.Id).DefaultIfEmpty()
                where !m.IsDeleted
                select new {Member = m, MemberInfo = memberInfo, MemberLoginInfo = memberLogInfo}).FirstOrDefaultAsync();

            if (memberIncludeAll?.Member == null) return null;

            var member = memberIncludeAll.Member;
            var wishLists = await _repository.GetAll<int, WishList>().Where(x => x.MemberId == member.Id).ToListAsync();
            wishLists ??= new List<WishList>();
            member.WishLists = wishLists;
            member.MemberInfo = memberIncludeAll.MemberInfo;
            member.MemberLoginInfo = memberIncludeAll.MemberLoginInfo;

            return member;
        }

        public async Task CreateAsync(Member item)
        {
            await _repository.CreateAsync<string, Member>(item);
        }

        public async Task UpdateAsync(Member item)
        {
            await _repository.UpdateAsync<string, Member>(item);
        }

        public async Task DeleteAsync(Member item)
        {
            await _repository.DeleteAsync<string, Member>(item);
        }

        public async Task<Member> GetMemberByAccountAsync(string account)
        {
            var member = await _context.Members
                .Include(x => x.MemberLoginInfo)
                .Include(x => x.MemberInfo)
                .Include(x => x.WishLists)
                .FirstOrDefaultAsync(x => x.MemberLoginInfo.Account == account
                                          && x.MemberLoginInfo.LoginType == LoginType.Email
                                          && !x.IsDeleted);
            // var mem = await (from m in _repository.GetAll<string, Member>()
            //     from ml in _repository.GetAll<string, MemberLoginInfo>().Where(x => x.Id == m.Id)
            //     where ml.Account == account && ml.LoginType == LoginType.Email && !m.IsDeleted
            //     select new {Member = m, log = ml}).FirstOrDefaultAsync();
            //
            // mem.Member.MemberLoginInfo = mem.log;
            // return mem.Member;
            return member;
        }
        
        public List<MemberInfoSearchObject> GetMemberInfoSearchObject(string memberId)
        {
            return (from member in _context.Members
                from contextMemberInfo in _context.MemberInfos.Where(x => x.Id == member.Id).DefaultIfEmpty()
                from contextHouse in _context.Houses.Where(x => x.OwnerId == member.Id).DefaultIfEmpty()
                from contextPhoto in _context.Photos.Where(x => x.HouseId == contextHouse.Id).DefaultIfEmpty()
                from contextHouseCategory in _context.HouseCategories.Where(x => x.Id == contextHouse.Id).DefaultIfEmpty()
                from contextComment in _context.Comments.Where(x => x.HouseId == contextHouse.Id).DefaultIfEmpty()
                from contextStar in _context.Stars.Where(x => x.Id == contextComment.Id).DefaultIfEmpty()
                where member.Id == memberId && !member.IsDeleted
                select new MemberInfoSearchObject
                {
                    About = contextMemberInfo.About,
                    Location = contextMemberInfo.Location,
                    WorkTime = contextMemberInfo.WorkTime,
                    MemberName = member.Name,
                    RegisterTime = member.RegisterTime,
                    Email = member.Email,
                    HouseTitle = contextHouse.Title,
                    HouseType = contextHouseCategory.HouseType,
                    RoomType = contextHouseCategory.RoomCategory,
                    StarTotal = contextStar,
                    CommentHouseId = contextComment.HouseId,
                    HouseId = contextHouse.Id,
                    Cover = member.Cover,
                    HousePhoto = contextPhoto.Image
                })?.ToList();
        }


        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }
    }
}