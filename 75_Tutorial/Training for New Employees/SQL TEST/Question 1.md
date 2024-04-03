One ROW contains in and out data together.  
The user wants to see the data in and out separately.  

---

  

select * from TST100

|   |   |   |   |
|---|---|---|---|
|req_no|req_qty|in_qty|out_qty|
|20190104LE2086.1|1|1|1|
|20190104LE2086.2|6|5|5|
|20190104LE2086.3|8|8|8|
|20190104LE2086.4|2|3|3|

Make a query that can make the below

|   |   |   |   |
|---|---|---|---|
|req_no|ioty|req_qty|qty|
|20190104LE2086.1|in|1|1|
|20190104LE2086.1|out|1|1|
|20190104LE2086.2|in|6|5|
|20190104LE2086.2|out|6|5|
|20190104LE2086.3|in|8|8|
|20190104LE2086.3|out|8|8|
|20190104LE2086.4|in|2|3|
|20190104LE2086.4|out|2|3|

Tip 1. Using ‘Union all’