using System;
using System.Threading.Tasks;
using Grpc.Core;
using StreamDemo;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Server
{
    public class ChatService: StreamDemo.ChatService.ChatServiceBase
    {
        private readonly ILogger<ChatService> _logger;

        public ChatService(ILogger<ChatService> logger)
        {
            _logger = logger;
        }

        private List<string> store { get; } = new List<string>(); 

        public override async Task Chat(IAsyncStreamReader<StringValue> requestStream,
        IServerStreamWriter<StringValue> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var item = requestStream.Current;
                List<string> prevNotes = AddValues(item.Value);
                foreach (var prevNote in prevNotes)
                {
                    await responseStream.WriteAsync(new StringValue {
                        Value = prevNote
                    });
                }
            }
        }

        private List<string> AddValues(string value)
        {
            store.Add(value);
            return store;
        }
    }
}

