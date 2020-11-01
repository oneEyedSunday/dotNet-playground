using System;
using System.Threading.Tasks;
using MassTransit;
using UsersService.Infrastructure.Config;

namespace UsersService.Infrastructure.Notifiers
{

    public class SendMessageToEndpoint: ISendMessage
    {

        private readonly ISendEndpointProvider _sendEndpoint;
        private readonly IRabbitMQConfig _rabbitConfig;

        public SendMessageToEndpoint(ISendEndpointProvider sendEndpoint, IRabbitMQConfig config)
        {
            _sendEndpoint = sendEndpoint;
            _rabbitConfig = config;
        }

        public async Task SendMessage<T>(T message, string targetEndpoint)
        {
            var endpointString = $"rabbitmq://{_rabbitConfig.Host}:{_rabbitConfig.Port}/{targetEndpoint}?durable={_rabbitConfig.DurableQueue}";
            var endpoint = _sendEndpoint.GetSendEndpoint(new Uri(endpointString));
            await endpoint.GetAwaiter().GetResult().Send(message);
        }
    }
}