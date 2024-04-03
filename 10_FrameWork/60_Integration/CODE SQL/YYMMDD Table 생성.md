---
CDT: 2024-03-28 07:11
Discriptions: 테이블을 생성하고 테이블을 사용하는 방법
---
---
``` SQL
--
-- Create Table 
-- drop table yymmdd

USE ERP
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE YYMMDD(
    LS    varchar(10) NOT NULL,
    YYYY  int NOT NULL,
    QQ    int null,
    SEQ   int NOT NULL,
    MS    varchar(3) NULL,
    MM    varchar(2) NOT NULL,
    M     int NOT NULL,
    WW    int NOT NULL,
    WS    varchar(3) NOT NULL,
    WD    int NULL,
    WI    int NULL,
    SW    varchar(10) NULL,
    EW    varchar(10) NULL,
    DD    varchar(2) NOT NULL,
    D     int NOT NULL,
    SS    varchar(8) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	LS ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100) ON [PRIMARY]
) ON [PRIMARY]
GO
--drop table YYMMDD
--select yyyy=2022, seq=1, MS='SAT', MM='01', M=1, WW=1, DD='01', D=1, LS='2022-01-01', SS='20220101'
--into YYMMDD
select num = 1 into #tmp
declare @cnt int
SET @cnt=1
WHILE ( @cnt <= 1000)
BEGIN
     insert into #tmp (num)    
     select @cnt+1
     set @cnt=@cnt+1
END  

truncate table YYMMDD

declare @dt date, @topCnt int 
declare curF cursor LOCAL for

    select dt='2018-01-01' union all
    select dt='2019-01-01' union all
    select dt='2020-01-01' union all
    select dt='2021-01-01' union all
    select dt='2022-01-01' union all
    select dt='2023-01-01' union all
    select dt='2024-01-01' union all
    select dt='2025-01-01' union all
    select dt='2026-01-01'

open curF
while (1=1)
begin
    fetch next from curF into @dt
    IF (@@FETCH_STATUS <> 0) BREAK;

    set @topCnt = DATEDIFF(DD, concat(year(@dt),'-01-01'), concat(year(@dt),'-12-31'))

    insert into YYMMDD (LS, YYYY, SEQ, MS, MM, M, WW, WS, DD, D, SS)
    select top(@topCnt+1)
           LS = dateadd(day, ROW_NUMBER() over(order by num asc)-1 , @dt),
           YYYY = DATEPART(YYYY, @dt),
           SEQ = ROW_NUMBER() over(order by num asc), 
           MS = null,
           MM = right(convert(varchar(3), datepart(MM, dateadd(day, ROW_NUMBER() over(order by num asc)-1 , @dt))+100),2),
           M  = datepart(MM, dateadd(day, ROW_NUMBER() over(order by num asc)-1 , @dt)),
           WW = datepart(WEEK, dateadd(day, ROW_NUMBER() over(order by num asc)-2 , @dt)),
           WS = (case datepart(WEEKDAY, dateadd(day, ROW_NUMBER() over(order by num asc)-1 , @dt)) when 1 then 'SUN' when 2 then 'MON' when 3 then 'TUE' when 4 then 'WED' when 5 then 'THU' when 6 then 'FRI' when 7 then 'SAT' END),
           DD = right(convert(varchar(3), datepart(DD, dateadd(day, ROW_NUMBER() over(order by num asc)-1 , @dt))+100),2),
           D  = datepart(DD, dateadd(day, ROW_NUMBER() over(order by num asc)-1 , @dt)),
           SS = dbo.fnDATE2str(dateadd(day, ROW_NUMBER() over(order by num asc)-1 , @dt),'yyyymmdd')
      from #tmp 


end
close curF
deallocate curF       

update a 
   set MS = (case m when 1  then 'JAN'
                    when 2  then 'FEB'
                    when 3  then 'MAR'
                    when 4  then 'APR'
                    when 5  then 'MAY'
                    when 6  then 'JUN'
                    when 7  then 'JUL'
                    when 8  then 'AUG'
                    when 9  then 'SEP'
                    when 10 then 'OCT'
                    when 11 then 'NOV'
                    when 12 then 'DEC' end)
  from yymmdd a

update a 
   set WD = datepart(WEEKDAY,a.LS)
  from yymmdd a

update a 
   set WI = datepart(WEEK, dateadd(day, -1 , LS))
  from yymmdd a

update a 
   set WI = 1
  from yymmdd a
 where m=1 and d=1

update a
   set SW = (select min(LS) from yymmdd where YYYY=a.YYYY and WI=a.WI and left(LS,7)=left(a.LS,7)),
       EW = (select max(LS) from yymmdd where YYYY=a.YYYY and WI=a.WI and left(LS,7)=left(a.LS,7))
  from yymmdd a

update a
   set QQ=(case when m between 1 and 3 then 1
                when m between 4 and 6 then 2
                when m between 7 and 9 then 3
                when m between 10 and 12 then 4
           end)
  from YYMMDD a
```