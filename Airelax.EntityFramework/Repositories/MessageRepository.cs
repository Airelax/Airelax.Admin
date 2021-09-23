using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airelax.Domain.Messages;
using Airelax.EntityFramework.DbContexts;
using Lazcat.Infrastructure.DependencyInjection;

namespace Airelax.EntityFramework.Repositories
{
    [DependencyInjection(typeof(IMessageRepository))]
    public class MessageRepository : IMessageRepository
    {
        private readonly AirelaxContext _context;

        public MessageRepository(AirelaxContext context)
        {
            _context = context;
        }

        public List<Message> GetMessage(string merberId)
        {
            var messages = from m in _context.Messages
                           where m.MemberOneId == merberId || m.MemberTwoId == merberId
                           select new Message
                           {
                               MemberOneId = m.MemberOneId,
                               MemberTwoId = m.MemberTwoId,
                               Contents = m.Contents,
                               Id = m.Id,
                               EndDate = m.EndDate,
                               StartDate = m.StartDate,
                               HouseId = m.HouseId,
                               MemberOneStatus = m.MemberOneStatus,
                               MemberTwoStatus = m.MemberTwoStatus
                           };

            var message = messages?.ToList();
            return message;
        }

        public Message UpdateMessage(string sender, string receiver)
        {
            var message = from m in _context.Messages
                           where (m.MemberOneId == sender || m.MemberOneId == receiver) && (m.MemberTwoId == sender || m.MemberTwoId == receiver)
                           select new Message
                           {
                               MemberOneId = m.MemberOneId,
                               MemberTwoId = m.MemberTwoId,
                               Contents = m.Contents,
                               Id = m.Id,
                               EndDate = m.EndDate,
                               StartDate = m.StartDate,
                               HouseId = m.HouseId,
                               MemberOneStatus = m.MemberOneStatus,
                               MemberTwoStatus = m.MemberTwoStatus
                           };

            return message?.ToList().FirstOrDefault();
        }

        public List<Message> CreateMessage()
        {
            var message = from m in _context.Messages
                          select new Message
                          {
                              MemberOneId = m.MemberOneId,
                              MemberTwoId = m.MemberTwoId,
                              Contents = m.Contents,
                              Id = m.Id,
                              EndDate = m.EndDate,
                              StartDate = m.StartDate,
                              HouseId = m.HouseId,
                              MemberOneStatus = m.MemberOneStatus,
                              MemberTwoStatus = m.MemberTwoStatus
                          };

            return message?.ToList();
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public void Create(Message message)
        {
            _context.Messages.Add(message);
        }

        public void Update(Message message)
        {
            _context.Messages.Update(message);
        }
    }
}
