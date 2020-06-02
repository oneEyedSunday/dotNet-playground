using System;
using StackExchange.Redis;
using System.Text.Json;

namespace redis_play
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IDatabase cache = Connection.GetDatabase();
            Console.WriteLine("Does myhash.one exists: {0}", cache.HashExists("myhash", "one") ? "True": "False");
            // serialize as JSON.strngify in js
            string serializedTeams = cache.StringGet("teamsList");
            Console.WriteLine("Retrieving Team List: {0}", serializedTeams);
            foreach (string item in JsonSerializer.Deserialize<string[]>(serializedTeams))
            {
                Console.WriteLine("{0} in cache", item);
            }
        }

        // Redis Connection string info
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = "localhost:6379";
            return ConnectionMultiplexer.Connect(cacheConnection);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
