using System;

namespace TopicsService.Domain.Exceptions
{
    public class TopicDomainNotFoundException: Exception
    {
        public TopicDomainNotFoundException(short Id)
            : base($"Topic {Id} Not Found") {}
        public TopicDomainNotFoundException(int Id)
            : base($"Topic {Id} Not Found") {}
        public TopicDomainNotFoundException()
            : base("Topic Not Found") {}
        public TopicDomainNotFoundException(string message)
            : base(message) {}
        public TopicDomainNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {}
    }
}