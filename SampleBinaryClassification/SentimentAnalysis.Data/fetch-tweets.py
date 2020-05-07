#!/usr/local/Cellar/python/3.7.6_1/bin/python3


from dotenv import load_dotenv
# from twitter import *
from os import getenv, walk
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
    with open(csv_path, newline='') as stream:
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
    with open(file_path, 'w', newline='') as stream:
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


if __name__ == '__main__':
    withSentiments = read_csv_into_list("./corona_tweets_01.csv")
    withTweetData = read_csv_into_list("./2020-03-20 Coronavirus Tweets.CSV")

    id_filter = [id_str[0] for id_str in withSentiments[1:]]

    print('Size of original data set: ', len(withTweetData[1:]))
    start_sec = time.time()

    merged = merge_data(withTweetData, withSentiments[1:])
    print('Size of found data: {0}', len(merged))
    print('Duration in seconds: {0}', time.time() - start_sec)
    header = list(['sentiment'])
    header.extend(withTweetData[0])
    write_csv_into_file("./merged.csv", merged, header)

