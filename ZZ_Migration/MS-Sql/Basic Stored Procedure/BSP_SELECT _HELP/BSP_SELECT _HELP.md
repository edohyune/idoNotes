![[Untitled 25.png|Untitled 25.png]]

```SQL
USE [DIANA]
GO
/****** Object:  StoredProcedure [dbo].[select_help]    Script Date: 2023-01-27 오후 9:57:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[BSP_SELECT_HELP](@txt nvarchar(50))
as
begin

    declare @que nvarchar(100) = 'select top 1000 * from ' + @txt

    /*----------------------------------------------------------------------------------------------------
    Sample : 검색어가 텍스트인 경우
    ----------------------------------------------------------------------------------------------------*/
    --if exists(select * from bcw100 where wh_nm = @txt)
    --select uwh, uwh_cd, * from bcw100 where wh_nm = @txt
    /*----------------------------------------------------------------------------------------------------
    Sample : 검색어가 숫자의 경우
    ----------------------------------------------------------------------------------------------------*/
    --if exists( select * from dma100 where itm_id = @txt) and isnumeric(@txt) = 1
    --select * from dma100 where itm_id = @txt
    
    if exists( select * from diana.dbo.ZTA200 where (frm_id = @txt))
       select * from diana.dbo.ZTA200 where (frm_id = @txt)
 
    if exists(select * from sysobjects where xtype = 'U' and name = @txt)
    exec sp_executesql @que

 end
```