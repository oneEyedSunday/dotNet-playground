using System;
namespace CsvHelperTools.Models
{
    public class TwitterCsvData: ITwitter
    {
        public bool sentiment { get; set; }
        public string status_id { get; set; }
        public string user_id { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public bool is_quote { get; set; }
        public string lang { get; set; }
        public string favourites_count { get; set; }
        public string retweet_count { get; set; }

        public TwitterCsvData()
        {
        }

        public override string ToString()
        {
            return $"Sentiment is {sentiment} TExt is {text}";
        }
    }
}
