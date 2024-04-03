Scalar Function

```SQL
USE [ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[fn_FunctionName] (@param1 nvarchar(20))  
    Returns NVARCHAR(1000)
As 
Begin 
    declare @sts nvarchar(1000)

    set @sts = @param1
	return @sts
End
```

Table Function

```SQL
USE [ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[tbl_FunctionName] (
    @param1  varchar(40),
    @param2  varchar(40)
    )
returns @tbl table (
    colStr       date
    colInt       decimal(22,3)
    )
as
begin

    insert into @tbl(colStr, colInt)
    select @param1, @param2

    return @tbl
end
```

Stored Procedures

```SQL
USE [ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[STRING_AGG] (@selectStr nvarchar(500))
as            
begin

    declare @tbl table(colNm nvarchar(200))
    insert into @tbl(colNm)
    Exec sp_executesql @selectStr

    SELECT strAgg = SUBSTRING((SELECT ',' + CAST(colNm AS nvarchar)
                                 FROM @tbl FOR xml PATH ('')), 2, 100000)
END

```