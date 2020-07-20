using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UsersService.Application.Events;

namespace UsersService.Application.Consumers
{
    public class RegistrationSuccessfulConsumer : IConsumer<UserSubscribedToTopicEvent>
    {
        private readonly ILogger<RegistrationSuccessfulConsumer> _logger;

        public RegistrationSuccessfulConsumer(ILogger<RegistrationSuccessfulConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<UserSubscribedToTopicEvent> context)
        {
            _logger.LogInformation($"Payload from {context.SourceAddress}");
            _logger.LogDebug($"Subscribed successfully at {context.DestinationAddress}");
            _logger.LogTrace($"Payload: {context.Message.UserId} {context.Message.TopicIds.ToString()}");
            return Task.CompletedTask;
        }
    }
}