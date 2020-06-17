using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace GreeterClients
{
    class Program
    {
        static void Main(string[] args)
        {
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);
            var request = new HelloRequest { Name = "I'm back and i'm better" };

            // Yes, Im making a sync request first...
            var reply = client.SayHello(request);

            Console.WriteLine($"Greet reply for hail `{request.Name}` is `{(reply.Message)}`!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
