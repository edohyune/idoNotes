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
FRMMST
```
FRMMST {
FrmId varchar(20)
FrmNm varchar(100)
OwnId int
FrwId varchar(20)
FilePath nvarchar(500)
FileNm varchar(50)
NmSpace varchar(50)
FldYn bit
PId varchar(20)
Memo nvarchar(500)
}
```

```C#
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

private int _OwnId;
public int OwnId
{
    get => _OwnId;
    set => Set(ref _OwnId, value);
}

private string _FrwId;
public string FrwId
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

private bool _FldYn;
public bool FldYn
{
    get => _FldYn;
    set => Set(ref _FldYn, value);
}

private string _PId;
public string PId
{
    get => _PId;
    set => Set(ref _PId, value);
}

private string _Memo;
public string Memo
{
    get => _Memo;
    set => Set(ref _Memo, value);
}

```

```SQL
select a.FrmId, a.FrmNm, a.OwnId, a.FrwId, a.FilePath,
       a.FileNm, a.NmSpace, a.FldYn, a.PId, a.Memo,
       a.CId, a.CDt, a.MId, a.MDt
  from FRMMST a
 where 1=1
   and a.FrmId = @FrmId

insert into FRMMST
      (FrmId, FrmNm, OwnId, FrwId, FilePath,
       FileNm, NmSpace, FldYn, PId, Memo,
       CId, CDt, MId, MDt)
select @FrmId, @FrmNm, @OwnId, @FrwId, @FilePath,
       @FileNm, @NmSpace, @FldYn, @PId, @Memo,
       @CId, getdate(), @MId, getdate()

update a
   set FrmId= @FrmId,
       FrmNm= @FrmNm,
       OwnId= @OwnId,
       FrwId= @FrwId,
       FilePath= @FilePath,
       FileNm= @FileNm,
       NmSpace= @NmSpace,
       FldYn= @FldYn,
       PId= @PId,
       Memo= @Memo,
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