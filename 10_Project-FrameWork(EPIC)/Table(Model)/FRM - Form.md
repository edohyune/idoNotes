---
Start Date: 
Status: 
Concept: false
Manifestation: false
Integration: false
Done: 
tags: 
CDT: <% tp.file.creation_date() %>
MDT: <% tp.file.last_modified_date() %>
---
---
```
FRMMST {
FrmId varchar(20)
FrmNm varchar(100)
Memo nvarchar(500)
FrwId int
FilePath nvarchar(500)
FileNm varchar(50)
NmSpace varchar(50)
CId int
CDt datetime
MId int
MDt datetime
}
```

```
private string _FrmId;
public string FrmId
{
    get => _FrmId;
    set => Set(ref _FrmId, value);
}

private string _FrmNm;
public string FrmNm
{
    get => _FrmNm;
    set => Set(ref _FrmNm, value);
}

private string _Memo;
public string Memo
{
    get => _Memo;
    set => Set(ref _Memo, value);
}

private int _FrwId;
public int FrwId
{
    get => _FrwId;
    set => Set(ref _FrwId, value);
}

private string _FilePath;
public string FilePath
{
    get => _FilePath;
    set => Set(ref _FilePath, value);
}

private string _FileNm;
public string FileNm
{
    get => _FileNm;
    set => Set(ref _FileNm, value);
}

private string _NmSpace;
public string NmSpace
{
    get => _NmSpace;
    set => Set(ref _NmSpace, value);
}

```

```
select a.FrmId, a.FrmNm, a.Memo, a.FrwId, a.FilePath,
       a.FileNm, a.NmSpace, a.CId, a.CDt, a.MId,
       a.MDt
  from FRMMST a
 where 1=1
   and a.FrmId = @FrmId
insert into FRMMST
      (FrmId, FrmNm, Memo, FrwId, FilePath,
       FileNm, NmSpace, CId, CDt, MId,
       MDt)
select @FrmId, @FrmNm, @Memo, @FrwId, @FilePath,
       @FileNm, @NmSpace, @CId, @CDt, @MId,
       @MDt
update a
   set FrmId= @FrmId,
       FrmNm= @FrmNm,
       Memo= @Memo,
       FrwId= @FrwId,
       FilePath= @FilePath,
       FileNm= @FileNm,
       NmSpace= @NmSpace,
       CId= @CId,
       CDt= @CDt,
       MId= @MId,
       MDt= @MDt
  from FRMMST a
 where 1=1
   and FrmId = @FrmId_old
delete
  from FRMMST
 where 1=1
   and FrmId = @FrmId_old
```
