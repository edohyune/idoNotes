Server SPID 확인하는 법

```SQL
SELECT p.spid, p.blocked, p.status, p.hostname, p.loginame, p.waitresource, p.program_name, p.kpid, 
       p.cpu, p.physical_io, p.waittype, p.waittime, p.lastwaittype, 
       p.dbid, p.uid, p.memusage, p.login_time, p.last_batch, p.ecid, p.open_tran, 
       p.hostprocess, p.cmd, p.nt_domain, p.nt_username, 
       p.net_address, p.net_library, 
       p.context_info, p.sql_handle, p.stmt_start, p.stmt_end, p.sid 
  FROM master..sysprocesses p
 WHERE (STATUS LIKE 'run%'
       OR waittime > 0
       OR blocked <> 0
       OR open_tran <> 0
       OR EXISTS (SELECT *
                    FROM master..sysprocesses p1
                   WHERE p.spid = p1.blocked
                     AND p1.spid <> p1.blocked)
       )
  AND spid > 50
  AND spid <> @@spid
ORDER BY (CASE WHEN STATUS LIKE 'run%' THEN 0
              ELSE 1
          END),
         waittime DESC, open_tran DESC
```

```SQL
--SPID의 Query 문장을 확인
DBCC INPUTBUFFER (125)
--lock걸린 쿼리 조회
sp_lock
--SPID Kill
kill 72

--현재상태를 저장
exec dbo.sp_tblsys

--조회
select *
  from ( 
        select tm=DATEDIFF( s , log_dt , excute_time),log_dt, dbcc_cmd, SPID, ECID, ECID_CNT, TRAN_CNT, STATUS, LOGINAME, HOSTNAME, BLK, DBNAME, CMD, PROGRAM_NAME
          from tblsys 
         where log_dt between dateadd(HH,-1,getdate()) and getdate()
       ) a
 order by tm
```