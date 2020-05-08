using System;
namespace CsvHelperTools.Models
{
    public interface ITwitter
    {
        bool sentiment { get; set; }
        string status_id { get; set; }
        string user_id { get; set; }
        string text { get; set; }
        string source { get; set; }
        bool is_quote { get; set; }
        string lang { get; set; }
        string favourites_count { get; set; }
        string retweet_count { get; set; }
    }
}
