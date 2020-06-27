using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using FootieBio;
using Microsoft.AspNetCore.Hosting;


namespace StreamServer
{
    public class DatabaseService
    {
        private string DataPath => Path.Combine(WebHostEnvironment.ContentRootPath, "Data");

        private IWebHostEnvironment WebHostEnvironment { get; }

        public DatabaseService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<CountryReply> GetCountries()
        {
            using(var jsonFileReader = File.OpenText(Path.Combine(DataPath, "Countries.json")))
            {
                return JsonSerializer.Deserialize<CountryReply[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public IEnumerable<Player> GetPlayers()
        {
             using(var jsonFileReader = File.OpenText(Path.Combine(DataPath, "Players.json")))
            {
                return JsonSerializer.Deserialize<Player[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }
    }
}
