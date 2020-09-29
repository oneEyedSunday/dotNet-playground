using System;
using System.Threading.Tasks;
using StreamDemo;
using Grpc.Net.Client;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverAddress = args.Length > 1 ? args[1] : "https://localhost:5001";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                // The following statement allows you to call insecure services. To be used only in development environments.
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }

            var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new Greeter.GreeterClient(channel);

            var greetRequest = new HelloRequest { Name = "Uno" };

            var reply = await client.SayHelloAsync(greetRequest, deadline: DateTime.UtcNow.AddSeconds(30));

            Console.WriteLine($"Reply: {reply.Message}...");
            Console.ReadKey();

            Console.WriteLine("Client Streaming now....");

            var clientStreamClient = new ClientEvent.ClientEventClient(channel);
            var sent = 0;

            using(var streamCall = clientStreamClient.UploadEvents())
            {
                while (sent < 20)
                {
                    streamCall.RequestStream.WriteAsync(new StringValue{ Value = "Some message here" });
                    sent++;
                    await Task.Delay(TimeSpan.FromSeconds(2));
                }
                await streamCall.RequestStream.CompleteAsync();

                var response = await streamCall.ResponseAsync;

                Console.WriteLine($"Call took {response.ElapsedTime} seconds to complete");
                Console.WriteLine($"Call sttaus was {response.CollationStatus}");
            }


            Console.ReadKey();

            Console.WriteLine("Streaming responses now...");

            var pulseClient = new Heartbeat.HeartbeatClient(channel);

            CancellationTokenSource streamCTS = new CancellationTokenSource(TimeSpan.FromSeconds(20));

            while (!streamCTS.IsCancellationRequested)
            {
                AsyncServerStreamingCall<Pulse> response = pulseClient.GetHeartbeatStream(new Empty(), cancellationToken: streamCTS.Token);

                await foreach (Pulse pulse in response.ResponseStream.ReadAllAsync(cancellationToken: streamCTS.Token))
                {
                    Console.WriteLine($"[+] {pulse.EventTime} {pulse.Path} {pulse.EventStatus}");
                }
            }
        }
    }
}
