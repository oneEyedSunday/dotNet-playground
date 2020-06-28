using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using FootieBio;
using static FootieBio.CountryDirectory;
using static FootieBio.PlayerDirectory;
// using System.Collections.Generic;
// using Grpc.Net.Compression;

namespace StreamClient
{
    class Program
    {
       static async Task Main()
       {
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            // using (var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions { CompressionProviders = new List<ICompressionProvider>() }))
            using (var channel = GrpcChannel.ForAddress("http://localhost:5000"))
            {
                var client = new CountryDirectoryClient(channel);

                #region  Consume Countries
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                using var streamingCall = client.GetAnyCountry(new Empty(), cancellationToken: cts.Token);
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

                var aCountry =  await client.GetCountryAsync(new CountryRequest { Country = "Italy" });
                Console.WriteLine($"Seeking Italy; Found {aCountry.ToString()}");

                aCountry =  await client.GetCountryAsync(new CountryRequest { Country = "No Country" });
                Console.WriteLine($"Seeking non existent country; Found {aCountry.ToString()}");

                #endregion


                #region Consume Players
                var playersClient = new PlayerDirectoryClient(channel);
                var playerCts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                using (var playersStreamingCall = playersClient.GetPlayers(new Empty(), cancellationToken: playerCts.Token))
                {
                    try
                    {
                        await foreach (PlayersReply playerData in playersStreamingCall.ResponseStream.ReadAllAsync())
                        {
                            Console.WriteLine($@"{playerData.Message}
                            {playerData.Players.ToString()}");
                        }
                    }
                    catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
                    {               
                        Console.WriteLine("Stream cancelled.");
                    }
                }

                PlayerReply aPlayer = await playersClient.GetPlayerAsync(new PlayerRequest { Name = "Hasan Sukur", Country = "Turkey" });
                Console.WriteLine($"Should have found: {aPlayer.ToString()}");


                PlayerReply notFound = await playersClient.GetPlayerAsync(new PlayerRequest { Name = "Idiakose O. Sunday" });
                Console.WriteLine($"Should have found me. Hehehehhe: {notFound.ToString()}");

                #endregion
            }
       }
    }
}
