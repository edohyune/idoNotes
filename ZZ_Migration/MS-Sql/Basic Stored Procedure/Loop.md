WHILE 기본

```SQL
DECLARE @Counter INT 
SET @Counter=1
WHILE ( @Counter <= 10)
BEGIN
    PRINT 'The counter value is = ' + CONVERT(VARCHAR,@Counter)
    SET @Counter  = @Counter  + 1
END
```

WHILE Table 이용

```SQL
declare @dt date, @topCnt int 
declare curF cursor LOCAL for
    select dt='2018-01-01' union all
    select dt='2019-01-01' 
open curF
while (1=1)
begin
    fetch next from curF into @dt
    IF (@@FETCH_STATUS <> 0) BREAK;

    set @topCnt = DATEDIFF(DD, concat(year(@dt),'-01-01'), concat(year(@dt),'-12-31'))
    print concat(@dt, ' / ', @topCnt+1)
end
close curF
deallocate curF
```