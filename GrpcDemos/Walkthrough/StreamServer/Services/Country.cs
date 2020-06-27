using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using FootieBio;
using static FootieBio.CountryDirectory;
using Google.Protobuf.WellKnownTypes;

namespace StreamServer
{
    public class CountryService : CountryDirectoryBase
    {
        private readonly ILogger<CountryService> _logger;
        private readonly DatabaseService _Db;

        public CountryService(ILogger<CountryService> logger, DatabaseService database)
        {
            _logger = logger;
            _Db = database;
        }

        public override Task<CountryReply> GetCountry(CountryRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CountryReply
            {
                Message = "Fetched country Successfully",
                CountryFull = "A Country FDR"
            });
        }

        private IEnumerable<Player> GetPlayersFromCountry(string country)
        {
            return _Db.GetPlayers()
                    .Where((Player candidate) => (String.Equals(country, candidate.Country, StringComparison.OrdinalIgnoreCase)));
        }

        public override async Task GetAnyCountry(Empty _, IServerStreamWriter<CountryReply> responseStream, ServerCallContext context)
        {
            var _randomGen = new Random();

            var _countryRator = _Db.GetCountries().GetEnumerator();

            while (!context.CancellationToken.IsCancellationRequested && _countryRator.MoveNext())
            {
                _logger.LogInformation("Generating country info...");
                await Task.Delay(500 * _randomGen.Next(5));

                var country = _countryRator.Current;

                country.Players.AddRange(GetPlayersFromCountry(country.CountryFull));

                _logger.LogInformation("Writing country response");

                await responseStream.WriteAsync(country);
            }
        }
    }
}