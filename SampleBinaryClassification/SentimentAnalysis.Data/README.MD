## Running scripts
python script.py or to allow execution as script

```sh
chmod x+a scriptname && ./scriptname
```


## Verifying data
First, extract only the sentiment rows with the `python3 verify-data.py filename` 
Then, look for items that dont match True or False with ```sh
 cat ${filename}_sentiments.csv | grep -vE 'True|False'
```