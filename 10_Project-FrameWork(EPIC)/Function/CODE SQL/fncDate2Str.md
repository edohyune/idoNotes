---
CDT: 2024-03-28 06:25
---
---
``` SQL
ALTER function [dbo].[fncDate2Str] (@dat varchar(20), @ty varchar(20))
   returns varchar(20)
as
begin

    if len(@dat)<=7
    begin 
       if len(@dat)<7 and len(@dat)>4
       begin 
           set @dat=concat(@dat,'01')
       end else begin
          if len(@dat)=4
          begin 
              set @dat=concat(@dat,'-01-01')
          end else begin
              set @dat=concat(@dat,'-01')
          end
       end 
    end 
    
    declare @dt date = convert(date,@dat,120) --2019-03-13
    declare @dd varchar(20)

    select @dd=(case --2019-03-12
                     when @ty='ld'       then convert(varchar(10),dateadd(day,-1,@dt),120)                            
                     --2019-02
                     when @ty='lm'       then convert(varchar(7) ,dateadd(month,-1,@dt),120)                          
                     --2019
                     when @ty='yyyy'     then convert(varchar(4) ,@dt,112)                                            
                     --2019-01-01
                     when @ty='yy0101'   then concat(year(@dt),'-01-01')                                              
                     --2019-12-31    
                     when @ty='yy1231'   then concat(year(@dt),'-12-31')                                              
                     --2019-01
                     when @ty='yy01'     then concat(year(@dt),'-01')                                                 
                                                                                                                      
                     --2019-03
                     when @ty='yymm'     then convert(varchar(7) ,@dt,120)                                            
                     --2019-04
                     when @ty='yy+1'     then convert(varchar(7) ,dateadd(month,+1,@dt),120)                          
                     --2019-05
                     when @ty='yy+2'     then convert(varchar(7) ,dateadd(month,+2,@dt),120)                          
                     --2019-06
                     when @ty='yy+3'     then convert(varchar(7) ,dateadd(month,+3,@dt),120)                          
                     --2019-02
                     when @ty='yy-1'     then convert(varchar(7) ,dateadd(month,-1,@dt),120)                          
                     --2019-01
                     when @ty='yy-2'     then convert(varchar(7) ,dateadd(month,-2,@dt),120)                          
                     --2018-12
                     when @ty='yy-3'     then convert(varchar(7) ,dateadd(month,-3,@dt),120)                          
                                         
                     --2019-03-01
                     when @ty='yymm01'   then convert(varchar(10),dateadd(day,1,eomonth(dateadd(month,-1,@dt))),120)  
                     --2019-03-31
                     when @ty='yymm30'   then convert(varchar(10),eomonth(@dt),120)                                   
                     --2019-04-01
                     when @ty='yy+101'   then convert(varchar(10),dateadd(day,+1,eomonth(@dt)),120)                   
                     --2019-04-30
                     when @ty='yy+130'   then convert(varchar(10),dateadd(month,+1,eomonth(@dt)),120)                 

                     --2019-02-01
                     when @ty='yy-101'   then convert(varchar(10),dateadd(day,+1,dateadd(month,-2,eomonth(@dt))),120) 
                     --2019-02-28
                     when @ty='yy-130'   then convert(varchar(10),
                                              eomonth(dateadd(month,-1,@dt))
                                              ,120)                 

                     --2019-02-28
                     when @ty='yymm-1'   then convert(varchar(10),dateadd(day,-1,@dt),120)                                      
                     --2018-12-01
                     when @ty='yy-301'   then convert(varchar(10),dateadd(day,+1,dateadd(month,-4,eomonth(@dt))),120) 
                                         
                     --201903
                     when @ty='yyyymm'   then convert(varchar(6) ,@dt,112)                                            
                     --201903
                     when @ty='yyyymmdd' then convert(varchar(8) ,@dt,112)                                            
                     --201904
                     when @ty='yyyy+1'   then convert(varchar(6) ,dateadd(month,+1,@dt),112)                          
                     --201902
                     when @ty='yyyy-1'   then convert(varchar(6) ,dateadd(month,-1,@dt),112)                          
                                                                                                                      
                     --31
                     when @ty='30'       then right(convert(varchar(10),eomonth(@dt),120),2)                          

                     else '***'
                end)

    return @dd
end

/*  How to use
    declare @_dt101 varchar(10) = dbo.fnDate2Str(@dt, 'yymm01')
*/
```