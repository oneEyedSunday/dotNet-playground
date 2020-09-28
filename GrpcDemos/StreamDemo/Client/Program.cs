using System;
using System.Threading.Tasks;
using StreamDemo;
using Grpc.Net.Client;
using System.Runtime.InteropServices;

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
        }
    }
}
