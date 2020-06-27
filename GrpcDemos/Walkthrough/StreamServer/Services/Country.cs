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

        private static readonly string[] Countries = {
            "Netherlands", "Singapore", "Mali", "Morocco",
            "Belgium", "Canada", "Myammar", "Maldives", "Guam",
            "Goa", "Germany", "Turkey", "Sweden", "Mexico",
            "Turkmenistan", "Mongolia", "Nepal", "San Marino"
        };

        public CountryService(ILogger<CountryService> logger)
        {
            _logger = logger;
        }

        public override Task<CountryReply> GetCountry(CountryRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CountryReply
            {
                Message = "Fetched country Successfully",
                CountryFull = "A Country FDR"
            });
        }

        public override async Task GetAnyCountry(Empty _, IServerStreamWriter<CountryReply> responseStream, ServerCallContext context)
        {
            var _randomGen = new Random();
            DateTime now = DateTime.UtcNow;

            short index = 0;

            while (index < 5)
            {
                _logger.LogInformation("Generating country info...");
                var country = new CountryReply
                {
                    Message = "Fetched country successfully",
                    CountryFull = Countries[index >= Countries.Length ? Countries.Length - 1 : index]
                };

                country.Players.AddRange(new Player[]
                {
                    new Player {  Name = "Antonio Di Natalie", Country = "Italy" },
                    new Player { Name = "Thorgan Hazard", Country = "Belgium" },
                    new Player { Name = "Pele", Country = "Brazil" }
                });

                _logger.LogInformation("Writing country response");

                await responseStream.WriteAsync(country);
                index++;
            }
        }
    }
}