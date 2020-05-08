#!/usr/local/Cellar/python/3.7.6_1/bin/python3

from sys import argv, exit
from fetch_tweets import read_csv_into_list, write_csv_into_file
from os import path


if __name__ == '__main__':
    if not argv:
        raise FileNotFoundError
    if not path.isfile(argv[1]):
        print(f"File not found {argv[1]}")
        exit()

    data = read_csv_into_list(argv[1])
    sentiments_file_name = f"{''.join(path.basename(argv[1]).split('.csv'))}_sentiments.csv"
    only_first_row = [ [row[0]] for row in data ]
    write_csv_into_file(sentiments_file_name, only_first_row)
    print([ row[0] for row in data[1:] if row[0] not in ['True', 'False'] ])