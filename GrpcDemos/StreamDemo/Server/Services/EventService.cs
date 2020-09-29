using System;
using System.Threading.Tasks;
using Grpc.Core;
using StreamDemo;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Server
{
    public class EventService: ClientEvent.ClientEventBase
    {
        private readonly ILogger<EventService> _logger;

        public EventService(ILogger<EventService> logger)
        {
            _logger = logger;
        }

        public override async Task<EventResponseStatus> UploadEvents(IAsyncStreamReader<StringValue> requestStream, ServerCallContext context)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            while (await requestStream.MoveNext())
            {
                var data = requestStream.Current;
                _logger.LogInformation($"[+] {data.Value}");
            }

            stopwatch.Stop();
            UInt64 elapsedMS = (UInt32)(stopwatch.ElapsedMilliseconds / 1000);
            return new EventResponseStatus {
                CollationStatus = EventResponseStatus.Types.ProcessStatus.Success,
                ElapsedTime = elapsedMS
            };
        }
    }
}
