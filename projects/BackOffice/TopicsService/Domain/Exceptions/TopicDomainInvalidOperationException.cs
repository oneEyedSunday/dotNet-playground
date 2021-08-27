using System;

namespace TopicsService.Domain.Exceptions
{
    public class TopicDomainInvalidOperationException : InvalidOperationException
    {
        public TopicDomainInvalidOperationException()
        { }

        public TopicDomainInvalidOperationException(string message)
            : base(message)
        { }

        public TopicDomainInvalidOperationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}