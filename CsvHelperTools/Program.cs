using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelperTools.Models;

namespace CsvHelperTools
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Reading in csv file(s) to format for mlnet!");
            string fromPath = null, toPath = null;

            switch (args.Length)
            {
                case 1:
                    fromPath = args[0];
                    Console.WriteLine("Saving to {0}, this will delete any files present there !!!", Path.Combine("../", "data.csv"));
                    Console.ReadKey();
                    toPath = "../data.csv";
                    break;
                case 0:
                    Console.WriteLine("Please pass in, paths for the input and output files");
                    Console.WriteLine("Usage: *.exe fromPath toPath");
                    System.Environment.Exit(255);
                    break;
                default:
                    fromPath = args[0];
                    toPath = args[1];
                    break;
            }

            Console.WriteLine("Reading from {0} and writing to {1}...", fromPath, toPath);

            WriteToFile(ReadFromFile(fromPath), toPath);
           
        }

        static List<ITwitter> ReadFromFile(string path)
        {
            List<ITwitter> records = new List<ITwitter>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                
                while (csv.Read() && passesSanityChecks(csv))
                {
                    var record = new TwitterCsvData
                    {
                        sentiment = double.Parse(csv.GetField<string>("sentiment"), CultureInfo.InvariantCulture) > 0.5,
                        status_id = csv.GetField("status_id"),
                        user_id = csv.GetField("user_id"),
                        // TODO (oneeyedsunday) clean text of emojis, etc, links hashtags
                        text = csv.GetField("text").Replace(Environment.NewLine, " ").Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "),
                        source = csv.GetField("source"),
                        is_quote = csv.GetField<bool>("is_quote"),
                        lang = csv.GetField("lang"),
                        favourites_count = csv.GetField("favourites_count"),
                        retweet_count = csv.GetField("retweet_count")
                    };
                    if (!String.IsNullOrEmpty(record.text)) records.Add(record);
                }

                System.Console.WriteLine("Number of records read {0}", records.Count);
            }
            return records;
        }

        static bool passesSanityChecks(CsvReader reader)
        {
            if (String.IsNullOrEmpty(reader.GetField("sentiment"))) return false;
            string text = reader.GetField("text");
            string lang = reader.GetField("lang");
            return (!(String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text)) && lang == "en");
        } 

        static void WriteToFile(List<ITwitter> records, string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        static string OpenFileWithoutCRLF(string path)
        {
            var sr = new StreamReader(path);
            return sr.ReadToEnd().Replace("\r", "").Replace("\n", "");
        }
    }

}
