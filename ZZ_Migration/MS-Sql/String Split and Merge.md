STRING_SPLIT

```SQL
//STRING_SPLIT 함수를 지원하는지 확인후 없으면 아래의 테이블 함수 생성
SELECT strVal FROM STRING_SPLIT('as.sd.er.drf.te.fge', '.');

USE [ERP]
GO
/****** Object:  UserDefinedFunction [dbo].[fcCarStock]    Script Date: 2023-03-08 오전 11:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[STRING_SPLIT] (
    @str nvarchar(max),
    @sep nvarchar(10)
    )
    returns @tbl table (
    strVal nvarchar(Max) 
    )
as
begin

    insert into @tbl (strVal)
    SELECT Split.a.value('.', 'NVARCHAR(MAX)') DATA
    FROM
    (
    SELECT CAST('<X>'+REPLACE(@str, @sep, '</X><X>')+'</X>' AS XML) AS String
    ) AS A
    CROSS APPLY String.nodes('/X') AS Split(a);

    return;
end;
```

  

STRING_AGG

```SQL
USE [ERP]
GO
/****** Object:  StoredProcedure [dbo].[STRING_AGG]    Script Date: 2023-03-08 오후 1:44:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[STRING_AGG] (@selectStr nvarchar(500))
as            
begin

    declare @tbl table(colNm nvarchar(20))
    insert into @tbl(colNm)
    Exec sp_executesql @selectStr

    SELECT SUBSTRING((SELECT ',' + CAST(colNm AS nvarchar)
                        FROM @tbl FOR xml PATH ('')), 2, 100000) AS Group1

END

declare @str nvarchar(max)
set @str = N'select top (5) colNm=cntr_no from rca100'
exec STRING_AGG @str
```

### 사용안함

```SQL
declare @que nvarchar
declare @tbl table(colNm varchar)
insert @tbl Exec sp_executesql @selectStr

SELECT SUBSTRING((SELECT ',' + CAST(colNm AS nvarchar)
                    FROM @tbl FOR xml PATH ('')), 2, 10000) AS Group1

-- 또는 

select top 5 colNm=cntr_no
  into \#tbl
  from rca100

SELECT SUBSTRING((SELECT ',' + CAST(colNm AS nvarchar)
                    FROM \#tbl FOR xml PATH ('')), 2, 10000) AS Group1
```