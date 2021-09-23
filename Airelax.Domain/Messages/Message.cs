using System;
using System.Collections.Generic;
using Airelax.Domain.DomainObject;
using Airelax.Domain.Houses;
using Lazcat.Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Airelax.Domain.Messages
{
    public class Message : AggregateRoot<string>
    {
        public Message()
        {
            Id = GuidHelper.CreateId(prefix:"MSG");
        }
        public string MemberOneId { get; set; }
        public string MemberTwoId { get; set; }
        public List<MessageContent> Contents { get; set; }
        public string HouseId { get; set; }
        public House House { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MemberOneStatus { get; set; }
        public int MemberTwoStatus { get; set; }
    }
}