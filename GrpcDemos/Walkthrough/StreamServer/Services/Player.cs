using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using FootieBio;
using static FootieBio.PlayerDirectory;


namespace StreamServer
{
    public class PlayerService: PlayerDirectoryBase
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly DatabaseService _Db;


        public PlayerService(ILogger<PlayerService> logger, DatabaseService db)
        {
            _logger = logger;
            _Db = db;
        }

        public override Task<PlayerReply> GetPlayer(PlayerRequest request, Grpc.Core.ServerCallContext context)
        {
            _logger.LogInformation("Querying for player...");

            var Player = _Db.GetPlayers()
            .Where(candidate => String.Equals(candidate.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            .FirstOrDefault();

            var wrapReply = new PlayerReply { Message = $"Could not find Player {request.Name}" };

            if (Player != null)
            {
                wrapReply = new PlayerReply {
                    Message = $"Found {request.Name}",
                    Player = Player
                };
            }
            return Task.FromResult(wrapReply);
        }

        public override async Task GetPlayers(Empty _, IServerStreamWriter<PlayersReply> responseStream, ServerCallContext context)
        {
            var _randomGen = new Random();
            var playersEnumberator = _Db.GetPlayers().GetEnumerator();

            while (!context.CancellationToken.IsCancellationRequested && playersEnumberator.MoveNext())
            {
                _logger.LogInformation("Generating player info...");
                await Task.Delay(400);

                var Player = playersEnumberator.Current;

                var reply = new PlayersReply {
                    Message = $"Player Found {Player.Name} from {Player.Country}"
                };

                Player.DateOfBirth = Timestamp
                    .FromDateTime(DateTime.UtcNow
                        .AddYears(_randomGen.Next(18, 45) * - 1)
                        .AddMonths(_randomGen.Next(0, 12)));

                reply.Players.Add(Player);

                _logger.LogInformation("Writing player response");

                await responseStream.WriteAsync(reply);
            }
        }
    }
}
