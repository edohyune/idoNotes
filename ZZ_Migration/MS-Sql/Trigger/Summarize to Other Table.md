```SQL
USE [ERP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER trigger [dbo].[TG_ACA100] ON [dbo].[ACA100]
    for insert, update, delete
as
begin

  /*-------------------------------
  --Account Summry
  -------------------------------*/
  merge ACA200 a
  using (
          select company, plant, ymd, a.acc, bankacc, dr=sum(dr), cr=sum(cr)
              from (
                    select company, plant, ymd=cfm_date, acc, bankacc=isnull(bankacc,'*'),
                           dr=isnull(dr,0), cr=isnull(cr,0)
                      from inserted
                     union all
                    select company, plant, ymd=cfm_date, acc, bankacc=isnull(bankacc,'*'),
                           dr=0-isnull(dr,0), cr=0-isnull(cr,0)
                      from deleted
                   ) a
		  left join cda200 b on a.acc =b.acc 
		    where b.yn_balance =1
             group by a.company, a.plant, a.ymd, a.acc, a.bankacc
        ) b on a.company=b.company and a.plant=b.plant 
           and a.ymd=b.ymd and a.acc=b.acc and a.bankacc=b.bankacc
  when matched then
    update set
      a.dr=a.dr+b.dr,
      a.cr=a.cr+b.cr
  when not matched then
    insert (company,plant,ymd,acc,bankacc,dr,cr)
    values (b.company,b.plant,b.ymd,b.acc,b.bankacc,b.dr,b.cr);

  /*-------------------------------
    --Account Balance
  -------------------------------*/
  merge ACA300 a
  using (
          select a.company,a.plant,a.slip_no,offset=isnull(a.offset,'*'),a.vendor,a.acc,idate=a.rmks_DATE1,
                 ddate=a.rmks_DATE2,a.curr,
                 iamt=(case when b.yndc='D' and dr<>0 then dr 
                            when b.yndc='C' and cr<>0 then cr else 0 end),
                 oamt=(case when b.yndc='D' and cr<>0 then cr 
                            when b.yndc='C' and dr<>0 then dr else 0 end),
                 invoice=a.rmks_NO3,cntlno=a.rmks_NO2,a.rmks
            from (
                  select company, plant, slip_no, offset, vendor, acc,
                         rmks_DATE1, rmks_DATE2, curr, dr, cr,
                         rmks_NO3, rmks_NO2, rmks
                    from inserted
                   union all
                  select company, plant, slip_no, offset, vendor, acc,
                         rmks_DATE1, rmks_DATE2, curr, 0-dr, 0-cr,
                         rmks_NO3, rmks_NO2, rmks
                    from deleted
                 ) a
            join coa b on a.acc=b.acc
           where 1=1 
             --and b.yn_balance=1
             and a.acc like '104'+'%'
         ) b on a.company=b.company and a.plant=b.plant and a.slip_no=b.slip_no and a.offset=b.offset
  when matched then
    update set
      a.iamt=a.iamt+b.iamt,
      a.oamt=a.oamt+b.oamt
  when not matched then
    insert (company,plant,slip_no,offset,vendor,acc,idate,ddate, 
            curr,iamt,oamt,invoice,cntlno,rmks)
    values (b.company,b.plant,b.slip_no,offset,b.vendor,b.acc,b.idate,b.ddate,
            b.curr,b.iamt,b.oamt,b.invoice,b.cntlno,b.rmks);
end;
```