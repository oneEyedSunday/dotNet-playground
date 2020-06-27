using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using FootieBio;
using static FootieBio.CountryDirectory;

namespace StreamClient
{
    class Program
    {
       static async Task Main()
       {
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var client = new CountryDirectoryClient(channel);
            using var streamingCall = client.GetAnyCountry(new Empty());

            try
            {
                await foreach (CountryReply countryData in streamingCall.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($@"{countryData.CountryFull}
                                                                        {countryData.Message}
                                                                        with {countryData.Players.Count} registered players");
                }
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
            {               
                Console.WriteLine("Stream cancelled.");
            }
       }
    }
}
