using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UsersService.Application.Events;

namespace TopicsService.Application.Consumers
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
            _logger.LogInformation($"Subscribed successfully at {context.DestinationAddress}");
            _logger.LogInformation($"Payload: {context.Message.UserId} {context.Message.TopicIds.ToString()}");
            return Task.CompletedTask;
        }
    }
}