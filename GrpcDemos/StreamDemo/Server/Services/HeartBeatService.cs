using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using StreamDemo;
using Microsoft.Extensions.Logging;


namespace Server
{
    public class HeartBeatService : Heartbeat.HeartbeatBase
    {
        private readonly ILogger<HeartBeatService> _logger;

        public HeartBeatService(ILogger<HeartBeatService> logger)
        {
            _logger = logger;
        }

        public override async Task GetHeartbeatStream(Empty _, IServerStreamWriter<Pulse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                
                await responseStream.WriteAsync(GetPulse());
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        private Pulse GetPulse()
        {
            return new Pulse {
                EventTime = Timestamp.FromDateTime(DateTime.UtcNow),
                Path = "/home",
                Weight = 45.0F,
                WindowOffset = 234,
                EventStatus = Pulse.Types.EventStatus.Handled
            };
        }
    }
}
