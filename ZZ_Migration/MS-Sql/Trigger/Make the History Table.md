```SQL
USE [ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[RCA430_LOG] ON [dbo].[RCA430]
  for insert, update, delete
as
begin
    declare @co_cd varchar(10),
            @bs_cd varchar(10),
            @wplace varchar(10),
            @bill_no varchar(200),
            @seq int,
            @cntr_no varchar(50),
            @planSeq int,
            @preNo int,
            @cdate date,
            @kbn varchar(10),
            @payRoute varchar(10),
            @currency varchar(10),
            @ex_principle decimal(20,5),
            @ex_interest decimal(20,5),
            @ex_interest_adjust decimal(20,5),
            @ex_penalty decimal(20,5),
            @ex_penalty_adjust decimal(20,5),
            @rmks nvarchar(400),
            @cid int,
            @cdt datetime,
            @mid int,
            @mdt datetime,
            @ty int
            
	declare acur cursor fast_forward local for 
    select a.co_cd, a.bs_cd, a.wplace, a.bill_no, a.seq, 
           a.cntr_no, a.planSeq, a.preNo, a.cdate, a.kbn, 
           a.payRoute, a.currency, a.ex_principle, a.ex_interest, a.ex_interest_adjust, 
           a.ex_penalty, a.ex_penalty_adjust, a.rmks, a.cid, a.cdt, 
           a.mid, a.mdt, ty=1
      from deleted  a
     where 1=1
       and not exists(select 1 from inserted)
    union
    select a.co_cd, a.bs_cd, a.wplace, a.bill_no, a.seq, 
           a.cntr_no, a.planSeq, a.preNo, a.cdate, a.kbn, 
           a.payRoute, a.currency, a.ex_principle, a.ex_interest, a.ex_interest_adjust, 
           a.ex_penalty, a.ex_penalty_adjust, a.rmks, a.cid, a.cdt, 
           a.mid, a.mdt, ty=0
      from inserted a
	open acur
		fetch next from acur into 
        @co_cd, @bs_cd, @wplace, @bill_no, @seq, 
        @cntr_no, @planSeq, @preNo, @cdate, @kbn, 
        @payRoute, @currency, @ex_principle, @ex_interest, @ex_interest_adjust, 
        @ex_penalty, @ex_penalty_adjust, @rmks, @cid, @cdt, 
        @mid, @mdt, @ty
    
	while @@fetch_status = 0
	begin	
        insert into rca440
               (log_dt, co_cd, bs_cd, wplace, bill_no, seq, 
               cntr_no, planSeq, preNo, cdate, kbn, 
               payRoute, currency, ex_principle, ex_interest, ex_interest_adjust, 
               ex_penalty, ex_penalty_adjust, rmks, cid, cdt, 
               mid, mdt)
        select getdate(), @co_cd, @bs_cd, @wplace, @bill_no, @seq, 
               @cntr_no, @planSeq, @ty, @cdate, @kbn, 
               @payRoute, @currency, @ex_principle, @ex_interest, @ex_interest_adjust, 
               @ex_penalty, @ex_penalty_adjust, @rmks, @cid, @cdt, 
               @mid, @mdt
	fetch next from acur into 
       @co_cd, @bs_cd, @wplace, @bill_no, @seq, 
       @cntr_no, @planSeq, @preNo, @cdate, @kbn, 
       @payRoute, @currency, @ex_principle, @ex_interest, @ex_interest_adjust, 
       @ex_penalty, @ex_penalty_adjust, @rmks, @cid, @cdt, 
       @mid, @mdt, @ty
    end
end
```