#!/usr/local/Cellar/python/3.7.6_1/bin/python3

from os import getenv, walk, path
from csv import reader as CSVReader, writer as CSVWriter
import time


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


def write_csv_into_file(file_path, data_list, headers = None):
    with open(path.join("output", file_path), 'w', newline='') as stream:
        writer = CSVWriter(stream, delimiter=',')
        if headers:
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

