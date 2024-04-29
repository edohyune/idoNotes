테이블 타입을 정의

```SQL
CREATE Type [dbo].[TVP_SP_WBSLOG] as TABLE (
	[CID] [varchar](20) NULL,
	[LOGSTR] [varchar](500) NULL
)

--데이터 보내는 쪽
declare @_tbl TVP_CID

insert into @_tbl
SELECT CID 
  FROM WBSMST
 where CID in (@CID)

--데이터 받는 쪽
CREATE FUNCTION [dbo].[tbl_WBSASS_TVP] (@CID TVP_CID readonly)
...
    select CID, PID, ordSq, lvl=0, HID=convert(varchar(max),'/')
      from WBSMST
     where CID in (select * from @CID)

```

![[Untitled 19.png|Untitled 19.png]]