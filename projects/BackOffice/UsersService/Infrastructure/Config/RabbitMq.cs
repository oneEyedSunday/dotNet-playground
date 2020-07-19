using System.Collections.Generic;

namespace UsersService.Infrastructure.Config
{
    public interface IRabbitMQConfig
    {
        string Host { get; set; }
        ushort Port { get; set; }
        string VirtualHost { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        bool? PublisherConfirmation { get; set; }
        IEnumerable<string> ClusterMembers { get; set; }
        bool PurgeOnStartup { get; set; }
        ushort PrefetchCount { get; set; }
        string Endpoint { get; set; }
        bool? DurableQueue { get; set; }
    }

    /// <summary>
    /// Rabbitmq config for masstransit read more about it here: https://masstransit-project.com/usage/transports/rabbitmq.html#cloudamqp
    /// </summary>
    public class RabbitMQConfig : IRabbitMQConfig
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? PublisherConfirmation { get; set; } = false;
        public IEnumerable<string> ClusterMembers { get; set; }
        public bool PurgeOnStartup { get; set; }
        public ushort PrefetchCount { get; set; }
        /// <summary>
        /// The queue endpoint for the current service
        /// </summary>
        public string Endpoint { get; set; }
        public bool? DurableQueue { get; set; } = false;
    }
}