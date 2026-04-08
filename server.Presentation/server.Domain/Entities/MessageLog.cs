using server.Domain.Enum;
namespace server.Domain.Entities
{
    public class MessageLog
    {
        public Guid Id { get; private set; }
        public string MessageType { get; private set; }
        public string Payload { get; private set; }
        public MessageStatus Status { get; private set; }
        public int RetryCount { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private MessageLog() { }

        public MessageLog(string messageType, string payload)
        {
            Id = Guid.NewGuid();
            MessageType = messageType;
            Payload = payload;
            Status = MessageStatus.Pending;
            RetryCount = 0;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkProcessed()
        {
            Status = MessageStatus.Processed;
        }

        public void MarkFailed()
        {
            Status = MessageStatus.Failed;
            RetryCount++;
        }
    }
}
