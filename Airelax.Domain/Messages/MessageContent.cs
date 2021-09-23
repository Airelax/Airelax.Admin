using System;

namespace Airelax.Domain.Messages
{
    public class MessageContent
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}