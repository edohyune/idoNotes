```SQL
/*
https://learn.microsoft.com/en-us/sql/relational-databases/hierarchical-data-sql-server?view=sql-server-ver16
*/

--drop table ORGHIR
CREATE TABLE ORGHIR
   (  
    HID hierarchyid,  
    orgLvl as HID.GetLevel(),   
    orgSnm nvarchar(200) NOT NULL, 
    orgNm nvarchar(MAX) NOT NULL
   ) ;  
GO  
  
CREATE CLUSTERED INDEX Org_Breadth_First   
    ON ORGHIR(orgLvl,HID) ;  
GO  
  
CREATE UNIQUE INDEX Org_Depth_First   
    ON ORGHIR(HID) ;  
GO  


;With x as
(
    select orgId, orgPid, lvl=0, ord,
           orgPidorgIdSTR=convert(varchar(max), '/')
      from erp.dbo.org100
     where orgPid is null
       and orgDt = '2023-02-07'
     union all
    select a.orgId, a.orgPid, lvl=x.lvl+1, a.ord, 
           orgPidorgIdSTR=convert(varchar(max),concat(x.orgPidorgIdSTR, a.orgId, '/'))
      from x
      join erp.dbo.org100 a on x.orgId=a.orgPid
) select orgPidorgIdHID=convert(hierarchyid,x.orgPidorgIdSTR), 
         x.orgPidorgIdSTR, b.orgSnm, lvl
    into \#tmp --drop table #tmp
    from x
    join erp.dbo.org000 b on x.orgId=b.orgId
   order by lvl, ord


insert into ORGHIR(HID, orgNm, orgSnm)
select orgPidorgIdHID, orgPidorgIdSTR, orgSnm
  from \#tmp


declare @node hierarchyid
set @node = '/149958/'
select * 
  from ORGHIR
 where HID.IsDescendantOf(@node)=1

select * 
  from ORGHIR
 where HID.GetAncestor(0)=(select HID from ORGHIR where orgNm='/149958/')
select * 
  from ORGHIR
 where HID.GetAncestor(1)=(select HID from ORGHIR where orgNm='/149958/')
select * 
  from ORGHIR
 where HID.GetAncestor(2)=(select HID from ORGHIR where orgNm='/149958/')
select * 
  from ORGHIR
 where HID.GetAncestor(3)=(select HID from ORGHIR where orgNm='/149958/')

select * 
  from ORGHIR
 where HID.GetAncestor(3) in (select HID from ORGHIR where HID.IsDescendantOf(@node)=1)

select * 
  from ORGHIR
 WHERE HID = hierarchyid::GetRoot()


select * from ORGHIR

-------------------------------------------------------------------------------
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------

CREATE TABLE Org_T1  
   (  
    EmployeeId hierarchyid PRIMARY KEY,  
    OrgLevel AS EmployeeId.GetLevel(),  
    EmployeeName nvarchar(50)   
   ) ;  
GO  
  
CREATE INDEX Org_BreadthFirst ON Org_T1(OrgLevel, EmployeeId);
GO  
  
CREATE PROCEDURE AddEmp(@mgrid hierarchyid, @EmpName nvarchar(50) )   
AS  
BEGIN  
    DECLARE @last_child hierarchyid;
INS_EMP:   
    SELECT @last_child = MAX(EmployeeId) FROM Org_T1   
        WHERE EmployeeId.GetAncestor(1) = @mgrid;
    INSERT INTO Org_T1 (EmployeeId, EmployeeName)  
        SELECT @mgrid.GetDescendant(@last_child, NULL), @EmpName;
-- On error, return to INS_EMP to recompute @last_child  
IF @@error <> 0 GOTO INS_EMP   
END ;  
GO
```