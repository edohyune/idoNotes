---
CDT: 2024-03-28 06:25
---
---
``` SQL

USE [ERP]
GO

/****** Object:  StoredProcedure [dbo].[BSP_SELECT_HELP]    Script Date: 2024-03-28 11:36:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[BSP_SELECT_HELP](@txt nvarchar(50))
as
begin
    set @txt = replace(replace(@txt, ']', ''), '[', '')

    declare @que nvarchar(100) = 'select top 1000 * from ' + @txt

    if exists(select * from bca200v where base_cd = @txt)
    select * from bca200v where base_cd = @txt order by base_cd

    if exists(select * from bca200v where main_cd = @txt)
    select * from bca200v where main_cd = @txt order by base_cd

    if exists(select * from bcc200 where bs_cd = @txt)
    select * from bcc200 where bs_cd = @txt
 
    if exists(select * from bcw100 where wh_cd = @txt)
    select * from bcw100 where wh_cd = @txt
 
    if exists(select * from scu100 where reg_id = @txt) and isnumeric(@txt) = 1
    select * from scu100 where reg_id = @txt

    if exists(select * from scu100 where emp_no = @txt)
    select * from scu100 where emp_no = @txt

    if exists(select * from fa100 where acc_cd = @txt)
    select * from fa100 where acc_cd = @txt
 
    if exists(select * from sysobjects where xtype = 'U' and name = @txt)
    exec sp_executesql @que

 end
GO
```