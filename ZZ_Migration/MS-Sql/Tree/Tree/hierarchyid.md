```SQL
CREATE TABLE [dbo].[HIDTBL](
	[CID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NULL,                   --Parents ID
	[AMT] [decimal](22, 3) NULL,        
	[HID] [hierarchyid] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[HIDTBL] (PID, AMT, HID) 
VALUES (NULL, 0,  hierarchyid::Parse('/')),           --1
       (1,    0,  hierarchyid::Parse('/1/')),         --2
       (1,    0,  hierarchyid::Parse('/2/')),         --3
       (1,    0,  hierarchyid::Parse('/3/')),         --4
       (1,    0,  hierarchyid::Parse('/4/')),         --5
       (2,    10, hierarchyid::Parse('/1/1/')),       --6
       (2,    10, hierarchyid::Parse('/1/2/')),       --7
       (2,    10, hierarchyid::Parse('/1/3/')),       --8
       (3,    10, hierarchyid::Parse('/2/1/')),       --9
       (3,    10, hierarchyid::Parse('/2/3/')),       --10
       (3,    10, hierarchyid::Parse('/2/5/')),       --11
       (4,    10, hierarchyid::Parse('/3/10/')),      --12
       (4,    10, hierarchyid::Parse('/3/20/')),      --13
       (4,    10, hierarchyid::Parse('/3/30/')),      --14
       (5,    10, hierarchyid::Parse('/4/100/')),     --15
       (5,    10, hierarchyid::Parse('/4/104/')),     --16
       (5,    10, hierarchyid::Parse('/4/10/')),      --17
       (5,    10, hierarchyid::Parse('/4/30/')),      --18
       (3,    10, hierarchyid::Parse('/2/2/'))        --19

INSERT INTO [dbo].[HIDTBL] (PID, AMT, HID) 
VALUES (2,    10, hierarchyid::Parse('/1/1.5/'))

--4-1은 불가
--INSERT INTO [dbo].[HIDTBL] (PID, AMT, HID) 
--VALUES (2,    10, hierarchyid::Parse('/1/4-1/'))






DECLARE @ParentHID hierarchyid, 
        @MaxChildHID hierarchyid
--------------------------------------------------------------
--CID의 자식 노드로 추가
--------------------------------------------------------------
DECLARE @CID int = 4
DECLARE @AMT int = 100

SELECT @ParentHID = HID 
  FROM HIDTBL 
 WHERE CID = @CID

SELECT @MaxChildHID = MAX(HID)
  FROM HIDTBL
 WHERE HID.GetAncestor(1) = @ParentHID

insert into HIDTBL (PID, AMT, HID)
VALUES (@CID, @AMT, @ParentHID.GetDescendant(@MaxChildHID, NULL))





--------------------------------------------------------------
--CID의 자식 노드로 추가 
--아직 확인안함
--------------------------------------------------------------
CREATE PROCEDURE SP_nodePush
    @CID INT,
    @Position INT,
    @AMT DECIMAL(22, 3)
AS
BEGIN
    DECLARE @parentHID hierarchyid, @newHID hierarchyid

    -- Get the HID for the specified CID
    SELECT @parentHID = HID
    FROM [dbo].[HIDTBL] 
    WHERE CID = @CID

    -- Get the new HID for the new record
    SET @newHID = @parentHID.GetDescendant(
        (SELECT HID FROM (SELECT HID, ROW_NUMBER() OVER (ORDER BY HID) AS RowNum FROM [dbo].[HIDTBL] WHERE HID.GetAncestor(1) = @parentHID) AS x WHERE RowNum = @Position - 1),
        (SELECT HID FROM (SELECT HID, ROW_NUMBER() OVER (ORDER BY HID) AS RowNum FROM [dbo].[HIDTBL] WHERE HID.GetAncestor(1) = @parentHID) AS x WHERE RowNum = @Position)
    )

    -- Update the HIDs of the subsequent nodes
    UPDATE [dbo].[HIDTBL]
    SET HID = HID.GetReparentedValue(@parentHID, @parentHID.GetDescendant(
        (SELECT HID FROM (SELECT HID, ROW_NUMBER() OVER (ORDER BY HID) AS RowNum FROM [dbo].[HIDTBL] WHERE HID.GetAncestor(1) = @parentHID) AS x WHERE RowNum = @Position),
        NULL
    ))
    WHERE HID.IsDescendantOf(@parentHID) = 1
    AND HID.GetLevel() = @parentHID.GetLevel() + 1
    AND HID.ToString() > @newHID.ToString()

    -- Insert the new record
    INSERT INTO [dbo].[HIDTBL] (PID, AMT, HID)
    VALUES (@CID, @AMT, @newHID)
END
GO
/*
위 프로시저는 CID, 위치(index), 그리고 AMT 값을 인자로 받아, 
해당 CID의 자식노드로 새로운 레코드를 추가합니다. 
그리고 추가된 레코드 이후의 모든 자식 노드들의 HID 값을 갱신합니다. 
이를 통해 새로운 레코드가 삽입된 위치 이후의 모든 레코드들이 한 단계씩 뒤로 밀리게 됩니다.

주의할 점은 이 쿼리가 정확하게 동작하기 위해서는 입력받는 @Position 값이 
현재 CID의 자식 노드 수보다 작거나 같아야 합니다. 
또한, 이 쿼리는 자식 노드들이 하나의 부모 노드 아래에만 존재하고, 
그렇지 않은 경우에는 정확하게 동작하지 않을 수 있습니다.
*/
```

```SQL
--HID.ToString()을 부모로 가지는 쿼리
select a.buId, a.accId, a.accPid, a.lvl,
       a.accNm, a.dcTy, a.keyinYn, a.useYn, 
       a.accTy01, a.accTy02, a.accTy03, a.accTy04, a.accTy05, 
       a.accTy06, a.ref01, a.ref02, a.ref03, a.ref04, a.ref05, a.ref06, a.ordSq
  from COAMST a
 where a.HID.GetAncestor(1) = (hierarchyid::Parse(@HID))
 order by a.HID
```

```SQL
--HID.ToString()을 부모로 가지는 쿼리의 수
select a.buId, a.accId, a.accPid, a.lvl,
       a.accNm, a.dcTy, a.keyinYn, a.useYn, 
       a.accTy01, a.accTy02, a.accTy03, a.accTy04, a.accTy05, 
       a.accTy06, a.ref01, a.ref02, a.ref03, a.ref04, a.ref05, a.ref06, a.ordSq
  from COAMST a
 where a.HID.GetAncestor(1) = (hierarchyid::Parse(@HID))
 order by a.HID
```