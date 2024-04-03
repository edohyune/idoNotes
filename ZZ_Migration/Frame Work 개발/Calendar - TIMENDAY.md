```SQL
USE [ERP]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SETUP_MAKE_TIMENDAY]
		(
		@startYYYY varchar(4), 
		@endYYYY varchar(4),
    @startWorkDay int --주의 시작이 월요일이면 2, 일요일이면 1
		)		
AS
BEGIN
    
    /*
    --drop PROCEDURE [dbo].[SETUP_MAKE_TIMENDAY]
    --drop table TIMENDAY
    */

    --exec [dbo].[SETUP_MAKE_TIMENDAY] '2023','2025',2
    --select * from TIMENDAY

    declare @dtSint int = convert(int, convert(datetime,concat(@startYYYY,'-01-01')))
    declare @dtEint int = convert(int, convert(datetime,concat(@endYYYY,'-12-31')))

    declare @excStr nvarchar(MAX)

    IF EXISTS (SELECT * FROM sys.tables WHERE name = 'TIMENDAY')
    BEGIN
        DROP TABLE TIMENDAY
    END

    --set @excStr = concat('select ',@startYYYY, ', ', @endYYYY)
    set @excStr = concat('CREATE TABLE [dbo].[TIMENDAY](
	                          [dateint] [int] IDENTITY(', @dtSint, ',1) NOT NULL,
	                          [YYYYMMDD]  AS (CONVERT([date],CONVERT([datetime],[dateint]))),
	                          [quarterInt]  AS (datepart(quarter,CONVERT([datetime],[dateint]))),
	                          [dayofyearInt]  AS (datepart(dayofyear,CONVERT([datetime],[dateint]))),
	                          [weekInt]  AS (datepart(week,CONVERT([datetime],[dateint]))),
	                          [weekdayInt]  AS (datepart(weekday,CONVERT([datetime],[dateint]))),
	                          [weekdayStr]  AS (case datepart(weekday,CONVERT([datetime],[dateint])) when 1 then ''SUN'' when 2 then ''MON'' when 3 then ''TUE'' when 4 then ''WED'' when 5 then ''THU'' when 6 then ''FRI'' when 7 then ''SAT'' end),
	                          [weekworkInt] [int] NULL,
	                          [rmks] [varchar](20) NULL
                          ) ON [PRIMARY]')

    exec sp_executesql @excStr

    DECLARE @Counter int, @Workint int
    SET @Counter=@dtSint
    WHILE ( @Counter <= @dtEint)
    BEGIN
        if (datepart(mm,convert(date,convert(datetime,@Counter)))=1 and datepart(dd,convert(date,convert(datetime,@Counter)))=1)
        begin
            set @Workint = 1
        end

        insert into TIMENDAY(weekworkInt, rmks)
        select @workint, convert(varchar(20),((@Counter+@startWorkDay)%7))

        set @Counter = @Counter + 1

        if (datepart(weekday,CONVERT(datetime,@counter))=2)
        begin
            set @workint = @workint + 1
        end
    END;
END;
```