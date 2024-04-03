```SQL
-- 이벤트 세션 생성
CREATE EVENT SESSION [ScalarFunctionTrace] ON SERVER 
ADD EVENT sqlserver.sp_statement_completed(
    ACTION(sqlserver.sql_text) -- 캡처할 추가 정보
    WHERE (
        sqlserver.database_name = N'ERP' -- 대상 데이터베이스 이름으로 교체
        AND (
            object_name = N'fnLimitedLicense' 
            OR object_name = N'fnCheckLicense'
            OR object_name = N'fnLicenseKey'
            OR object_name = N'fnDate2Str'
        )
    )
)
ADD TARGET package0.event_file(
    SET filename=N'ScalarFunctionTrace.xel' -- 이벤트 데이터를 저장할 파일 이름
);
GO

-- 이벤트 세션 시작
ALTER EVENT SESSION [ScalarFunctionTrace] ON SERVER
STATE = START;
GO

-- 이벤트 세션 종료
ALTER EVENT SESSION [ScalarFunctionTrace] ON SERVER
STATE = STOP;
GO

SELECT * 
FROM sys.fn_xe_file_target_read_file('ScalarFunctionTrace*.xel', NULL, NULL, NULL);
GO

--이벤트 세션 종료 후 삭제
ALTER EVENT SESSION [ScalarFunctionTrace] ON SERVER
STATE = STOP;
GO
drop EVENT SESSION [ScalarFunctionTrace] on server
```