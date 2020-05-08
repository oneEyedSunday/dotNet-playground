#!/usr/local/Cellar/python/3.7.6_1/bin/python3


from dotenv import load_dotenv
# from twitter import *
from os import getenv, walk, path
from tweepy import API, OAuthHandler
from csv import reader as CSVReader, writer as CSVWriter
import time

load_dotenv(verbose=True)


settings = {
        "token": getenv('token'), "token_secret": getenv('token_secret'), "consumer_key": getenv('consumer_key'),"consumer_secret": getenv('consumer_secret')
        }

"""

t = Twitter(auth=OAuth(settings.get("token"), settings.get("token_secret"), settings.get("consumer_key"), settings.get("consumer_secret")))

t.statuses.oembed(1240727808080410000)

"""

"""

id_of_tweet = 124072780808041

auth = OAuthHandler(settings.get("consumer_key"), settings.get("consumer_secret"))
auth.set_access_token(settings.get("token"), settings.get("token_secret"))
api = API(auth)

tweet = api.get_status(1257955497224912896)
print(tweet.__dict__.keys())
print("-----------------------------------------------------------------------")
print(vars(tweet))
print("------------------------------------------------------------------------")
print(tweet)
print("------------------------------------------------------------------------")
print(tweet.text)

"""


def get_files_in_dir(path="."):
    f = []
    for (_, _, file_names) in walk(path):
        f.extend(file_names)
        break
    return file_names


def read_csv_into_list(csv_path):
    a_list = list()
    with open(path.join("input", csv_path), newline='') as stream:
        for row in CSVReader(stream):
            a_list.append(row)
    return a_list


def read_csv_into_list_with_limit(csv_path, limit=10):
    a_list = list()
    with open(csv_path, newline='') as stream:
        for row in list(CSVReader(stream))[:limit]:
            a_list.append(row)
    return a_list


def write_csv_into_file(file_path, data_list, headers):
    with open(path.join("output", file_path), 'w', newline='') as stream:
        writer = CSVWriter(stream, delimiter=',')
        writer.writerow(headers)
        writer.writerows(data_list)


def new_list(begin, old_list):
    a_list = [begin]
    a_list.extend(old_list)
    return a_list


def merge_data(to_filter, with_sentiment_score):
    as_dict = dict(with_sentiment_score)
    return [new_list(float(as_dict.get(item[0])), item) for item in to_filter if item[0] in as_dict]


def merge_data_for_day(score_file_name_list, tweet_data_file_name, store_as_name = f'merged.{str(time.time())}.csv'):
        withSentiments = []
        for file_name in score_file_name_list:
                withSentiments.extend(read_csv_into_list(file_name)[1:])
        tweet_data = read_csv_into_list(tweet_data_file_name)

        print(f"All Sentiments are {len(withSentiments)} long while the tweet data has {len(tweet_data)} records")

        ### Merge data from both sources

        merged = merge_data(tweet_data[1:], withSentiments)
        print('Size of found data: {}'.format(len(merged)))
        header = list(['sentiment'])
        header.extend(tweet_data[0])
        write_csv_into_file(store_as_name, merged, header)


if __name__ == '__main__':
    start_sec = time.time()
    merge_data_for_day(["corona_tweets_01.csv", "corona_tweets_02.csv", "corona_tweets_03.csv"], \
     "2020-03-20 Coronavirus Tweets.CSV",\
      "raw_merged_2020-03-20.csv")
    print('Duration in seconds: {}'.format(time.time() - start_sec))

