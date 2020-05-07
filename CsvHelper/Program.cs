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
            Console.WriteLine("Reading in csv file to remove line breaks!");

            WriteToFile(ReadFromFile("/Users/ispoa/Workspace/project/SentimentAnalysis.Data/merged.csv"), "/Users/ispoa/projects/dotnetplay/data.csv");
           
        }

        static List<ITwitter> ReadFromFile(string path)
        {
            List<ITwitter> records = new List<ITwitter>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read() && csv.GetField("lang") == "en")
                {
                    var record = new TwitterCsvData
                    {
                        sentiment = double.Parse(csv.GetField<string>("sentiment"), CultureInfo.InvariantCulture) > 0.5,
                        status_id = csv.GetField("status_id"),
                        user_id = csv.GetField("user_id"),
                        text = csv.GetField("text").Replace(Environment.NewLine, "").Replace("\r\n", ""),
                        source = csv.GetField("source"),
                        is_quote = csv.GetField<bool>("is_quote"),
                        lang = csv.GetField("lang"),
                        favourites_count = csv.GetField("favourites_count"),
                        retweet_count = csv.GetField("retweet_count")
                    };
                    records.Add(record);
                }

                System.Console.WriteLine("Number of records read {0}", records.Count);
            }
            return records;
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
