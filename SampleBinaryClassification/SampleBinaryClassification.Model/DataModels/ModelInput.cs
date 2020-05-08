//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace SampleBinaryClassification.Model.DataModels
{
    public class ModelInput
    {
        [ColumnName("sentiment"), LoadColumn(0)]
        public bool Sentiment { get; set; }


        [ColumnName("status_id"), LoadColumn(1)]
        public float Status_id { get; set; }


        [ColumnName("user_id"), LoadColumn(2)]
        public float User_id { get; set; }


        [ColumnName("text"), LoadColumn(3)]
        public string Text { get; set; }


        [ColumnName("source"), LoadColumn(4)]
        public string Source { get; set; }


        [ColumnName("is_quote"), LoadColumn(5)]
        public bool Is_quote { get; set; }


        [ColumnName("lang"), LoadColumn(6)]
        public string Lang { get; set; }


        [ColumnName("favourites_count"), LoadColumn(7)]
        public float Favourites_count { get; set; }


        [ColumnName("retweet_count"), LoadColumn(8)]
        public float Retweet_count { get; set; }


    }
}
