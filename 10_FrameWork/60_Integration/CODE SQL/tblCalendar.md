---
CDT: 2024-03-28 07:07
---
---
``` SQL
USE [Database]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[tblCalendar] (
    @frdt  date, 
    @todt  date
) 
returns @calendar	table(
    Dt	date,
    Mt	int, 
    FMt	varchar(2),
    Yt	int,
    Wd int,
    NWd	varchar(3),
    YYMM varchar(7),
    WeekNo int,
    RestYn	char(1),
    WeekStartDate date,
    WeekEndDate date
)
as
begin
    --SET DATEFIRST 1;  -- Set Monday as the first day of the week

    ;with x as 
    (
        select dt = @frdt
         union all
        select dateadd(day, 1, x.dt)
          from x
         where dt < @todt
    ) insert into @calendar
      select Dt=dt, 
             Mt=datepart(month, dt),
             FMt=FORMAT(datepart(month, dt), '00'),
             Yt=datepart(year, dt),
             Wd = datepart(weekday, dt),
             NWd=(case datepart(weekday, dt) when 1 then 'SUN'
                                             when 2 then 'MON'
                                             when 3 then 'TUE'
                                             when 4 then 'WED'
                                             when 5 then 'THU'
                                             when 6 then 'FRI'
                                             when 7 then 'SAT' end),
             YYMM=concat(datepart(year, dt),'-',FORMAT(datepart(month, dt), '00')),
             WeekNo = datepart(week,dt), 
             RestYn = (case when b.pid is null 
                            then (case when datepart(weekday, dt)=1 
                                       then '1' 
                                       else '0' end)
                            else '1' end),
             WeekStartDate = dateadd(day, 1-datepart(weekday, dt), dt),
             WeekEndDate = dateadd(day, 7-datepart(weekday, dt), dt)
        from x
        left join ha630 b on x.dt = b.holiday_date
    OPTION (MAXRECURSION 0);
    return
end
```