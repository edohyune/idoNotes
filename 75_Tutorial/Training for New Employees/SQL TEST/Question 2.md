Find the contract number at the same time in both tables of the termination on the contract master.

---

  

TST201 : Contract master table

|   |   |
|---|---|
|cntr_no|stat|
|2012-LCF-120001|Termination|
|2012-LCF-120002|Termination|
|2012-LCF-120003|Termination|
|2013-LCF-020009|Nomal|

TST202 : Termination Detail Table

|   |
|---|
|cntr_no|
|2012-LCF-120001|
|2012-LCF-120022|
|2012-LCF-120003|
|2013-LCF-020009|

Make a query that can make the below

|   |
|---|
|cntr_no|
|2012-LCF-120001|
|2012-LCF-120003|

Tip 1 Intersect

Tip 2 Join