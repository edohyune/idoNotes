```SQL
USE [ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [dbo].[RCA430_Closing] ON [dbo].[RCA430]
for 	insert, update, delete
as
begin 
    set nocount on
    set transaction isolation level read uncommitted
    declare @ty				    char(1),
	          @co_cd			  varchar(10),
	          @close_dt     date, 
            @module       varchar(20)='CA11060'
    declare curC cursor fast_forward local for 
    select ty='d', co_cd, close_dt=cdate from deleted where kbn<>'SD795998' --RCA389 - cdate AC.Date 
     union all
    select ty='i', co_cd, close_dt=cdate from inserted where kbn<>'SD795998'
    open curC
	   while (1=1)
	   begin
	       fetch next from curC into @ty, @co_cd, @close_dt;
        if (@@fetch_status<>0) Break;
        if (Select dbo.fcClosing(@co_cd,@module,@close_dt)) = 'YM'
	       begin
		          rollback transaction
		          exec PutMessage '','Already month closed (RCA430-Credit)'
		          return;
	       end
    
	       if (Select dbo.fcClosing(@co_cd,@module,@close_dt)) = 'YD'
	       begin
		          rollback transaction
            exec PutMessage '','Already daily closed (RCA430-Credit)'
		          return;
	       end
    end
    close curC
    deallocate curC
	end;
```