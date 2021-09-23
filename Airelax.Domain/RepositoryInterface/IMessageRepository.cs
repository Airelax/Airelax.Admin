using System.Collections.Generic;
using Airelax.Domain.Messages;

namespace Airelax.EntityFramework.Repositories
{
    public interface IMessageRepository
    {
        List<Message> GetMessage(string merberId);
        Message UpdateMessage(string sender, string receiver);
        void SaveChange();
        void Update(Message message);
        List<Message> CreateMessage();
        void Create(Message message);
    }
}