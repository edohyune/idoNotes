---
CDT: 2024-03-28 06:25
---
---
#### Linked Server 연결

```SQL
USE [master]
GO

-- Linked Server 추가
EXEC sp_addlinkedserver 
	@server = N'LSVR',  -- 원하는 Linked Server 이름을 지정하세요.
	@srvproduct = N'',      -- 제품 이름
	@provider = N'SQLNCLI',           -- SQL Server Native Client의 OLE DB 공급자
	@datasrc = N'161.82.251.50';    -- 원격 서버의 IP 주소

-- Linked Server 로그인 정보 추가
EXEC sp_addlinkedsrvlogin 
	@rmtsrvname = N'LSVR', -- 앞서 지정한 Linked Server 이름을 사용하세요.
	@useself = N'False', 
	@locallogin = NULL, 
	@rmtuser = N'sa',                   -- 원격 서버의 사용자 ID
	@rmtpassword = N'Team123Erp';         -- 원격 서버의 비밀번호
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'collation compatible', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'data access', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'dist', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'pub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'rpc', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'rpc out', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'sub', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'connect timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'collation name', @optvalue=null
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'lazy schema validation', @optvalue=N'false'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'query timeout', @optvalue=N'0'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'use remote collation', @optvalue=N'true'
GO

EXEC master.dbo.sp_serveroption @server=N'LSVR', @optname=N'remote proc transaction promotion', @optvalue=N'true'
GO
```
