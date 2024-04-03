Table Create : tblSys

```SQL
USE ERP
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE dbo.TBLSYS(
	log_dt datetime NOT NULL,
	SPID smallint NOT NULL,
	ECID smallint NULL,
	ECID_CNT int NULL,
	TRAN_CNT int NULL,
	STATUS nvarchar(50) NULL,
	LOGINAME nvarchar(300) NULL,
	HOSTNAME nvarchar(300) NULL,
	BLK varchar(5) NULL,
	DBNAME nvarchar(200) NULL,
	CMD nchar(100) NOT NULL,
	EXCUTE_TIME datetime NOT NULL,
	DBCC_CMD nvarchar(max) NULL,
	PROGRAM_NAME nchar(200) NOT NULL,
	ipAddress varchar(50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
```

tblSys Insert

```SQL
USE [ERP]
GO
/****** Object:  StoredProcedure [dbo].[sp_tblsys]    Script Date: 9/20/2022 12:01:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[sp_tblsys]
as
begin
----------------------------------------------------------------
--현재 돌고 있는 놈들의 리스트를 뽑아낸다.
--RUNNABLE	현재 돌고있는 놈
--SUSPENDED	현재 뒤에서 몰래 돌고있는 놈
----------------------------------------------------------------

SELECT	SPID, 
       ECID=MAX(ECID),
       ECID_CNT=COUNT(*),
       TRAN_CNT=SUM(OPEN_TRAN)	,
       STATUS=RTRIM(MAX(STATUS))	,
       LOGINAME=RTRIM(LOGINAME)	,
       HOSTNAME=RTRIM(HOSTNAME)	,
       BLK=	(CASE WHEN BLOCKED = 0 OR (BLOCKED > 0 AND SPID = BLOCKED) THEN '0'
                		ELSE CONVERT(VARCHAR(5),BLOCKED)
            	END),
       DBNAME=(CASE	WHEN DBID = 0 THEN NULL
                    WHEN DBID <> 0 THEN DB_NAME(DBID)
               END),
       CMD,
       EXCUTE_TIME=LAST_BATCH,
       DBCC_CMD = CONVERT(NVARCHAR(4000),''),
       PROGRAM_NAME
  INTO	\#RUNNING
  FROM	MASTER.DBO.SYSPROCESSES WITH (NOLOCK)
 WHERE	SPID > 50
  	AND SPID <> @@SPID --자신의 SPID는 조회할 필요가 없다.
	  AND UPPER(CMD) <> 'AWAITING COMMAND'
 GROUP BY	SPID, RTRIM(LOGINAME), RTRIM(HOSTNAME),
       (CASE WHEN BLOCKED = 0 OR (BLOCKED > 0 AND SPID = BLOCKED) THEN '0'
		           ELSE CONVERT(VARCHAR(5),BLOCKED)
        END),
       (CASE	WHEN DBID = 0 THEN NULL
             WHEN DBID <> 0 THEN DB_NAME(DBID)
        END),
       CMD, LAST_BATCH, PROGRAM_NAME


--커서에 넣고 각 SPID에 해당하는 명령어를 DBCC INPUTBUFFER로 뽑아낸다.
DECLARE CURSOR1 CURSOR FAST_FORWARD
FOR
	 SELECT	DISTINCT SPID
	   FROM	\#RUNNING
	  ORDER BY SPID

--DBCC INPUTBUFFER로 뽑은 데이터를 테이블에 넣으려면 이 방법밖에 없는거 같다.

CREATE TABLE \#DBCC(
       EVENTTYPE nvarchar(30),
       PARAMETERS INT,
       EVENTINFO NVARCHAR(4000)
)

DECLARE @SPID INT
DECLARE @STR_SPID VARCHAR(20)
DECLARE @EVENTINFO NVARCHAR(4000)

OPEN CURSOR1
   FETCH NEXT FROM CURSOR1 INTO @SPID
      WHILE (@@FETCH_STATUS=0)
      BEGIN
	        SET @STR_SPID=CONVERT(VARCHAR(20),@SPID)
	        INSERT INTO \#DBCC (EVENTTYPE,PARAMETERS,EVENTINFO)
	          EXEC ('dbcc inputbuffer('+@STR_SPID+')')
	
	        SELECT TOP 1 @EVENTINFO=EVENTINFO FROM \#DBCC
	
	        UPDATE \#RUNNING
	           SET DBCC_CMD=@EVENTINFO
	         WHERE SPID=@SPID
	
	        TRUNCATE TABLE \#DBCC
         FETCH NEXT FROM CURSOR1 INTO @SPID
      END
CLOSE CURSOR1
DEALLOCATE CURSOR1

INSERT INTO TBLSYS
SELECT	log_dt=GETDATE(), *, convert(varchar(50),(select CONNECTIONPROPERTY('client_net_address') AS client_net_address))
  FROM	\#RUNNING
 ORDER BY SPID

end;
```