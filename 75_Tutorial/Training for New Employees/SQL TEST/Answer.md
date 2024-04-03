```SQL
--1. union all
select req_no, ioty='in', req_qty, qty=in_qty
  from tst101
 union all
select req_no, ioty='out', req_qty, out_qty
  from tst101
--2. Intersect or join
select cntr_no from tst201 where stat='termination'
Intersect
select cntr_no from tst202

select *
  from tst201 a
  join tst202 b on a.cntr_no=b.cntr_no
 where stat='termination'
--3 except , not in , not exists
select cntr_no from tst201 where stat='termination'
except
select cntr_no from tst202

select cntr_no 
  from tst201 
 where stat='termination'
   and cntr_no not in (select cntr_no 
                         from tst202)

select cntr_no 
  from tst201 a
 where stat='termination'
   and not exists(select b.cntr_no 
                    from tst202 b
                   where b.cntr_no=a.cntr_no)
--4 group by & having
select vin_no, count(*)
  from TST401
 group by vin_no
having count(*)>1
--5. group by & max
select a.*
  from TST401 a
  join (
        select vin_no
          from TST401
         group by vin_no
        having count(*)>1
       ) b on a.vin_no=b.vin_no
 where a.book_dt=(select max(book_dt) 
                    from TST401 
                   where 1=1
                     and vin_no=a.vin_no
                   group by vin_no)
--6. compare two table (do not use join, left join)
select bill_no, amt_601=sum(amt_601), amt_602=sum(amt_602)
  from (
        select bill_no, amt_601=ex_amt, amt_602=0
          from tst601 a
         union all
        select bill_no, amt_601=0, amt_602=sum(amt)
          from tst602 a
         group by bill_no
       ) a
 where 1=1
 group by bill_no
having sum(amt_601) <> sum(amt_602)

--7. case when 
select vin_no, code, code_nm,      
       newGroup=(case when code_nm ='HYUNDAI' then 'HYUNDAI'
                      when code_nm ='KIA' then 'KIA'
                      when code_nm ='DAEHAN' then 'DAEHAN'
                      else 'Others'
                 end)
 From tst701
```