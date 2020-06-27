using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using FootieBio;


namespace StreamServer
{
    public class DatabaseService
    {
        private static string DataPath => "/Users/ispoa/Projects/DotNetPlay/GrpcDemos/Walkthrough/StreamServer/Data";

        public static IEnumerable<CountryReply> GetCountries()
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

        public static IEnumerable<Player> GetPlayers()
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
